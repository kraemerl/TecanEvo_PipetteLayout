using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace TecanEvo_PipetteLayout
{
    public partial class ScanReport : Form
    {
        /// <summary>
        /// retVal: 0 - all OK
        /// retVal: 1 - all OK, load variables
        /// retVal: 2 - Errors, abort Evoware script
        /// retVal: 3
        /// retVal: 4
        /// retVal: 5
        /// 
        /// scan_report C:\Lars\Git\C#_projects\TecanEvo_PipetteLayout\Data\variables.csv C:\Lars\Git\C#_projects\TecanEvo_PipetteLayout\Data\source_eppi_barcode.csv C:\Lars\Git\C#_projects\TecanEvo_PipetteLayout\Data\4001124008.csv C:\Lars\Git\C#_projects\TecanEvo_PipetteLayout\Data\4001088480.csv C:\Lars\Git\C#_projects\TecanEvo_PipetteLayout\Data\lastJobInfo.txt C:\Lars\Git\C#_projects\TecanEvo_PipetteLayout\Data\worklist.gwl
        /// 
        /// </summary>
        private int m_retVal = 2;

        private double m_max_diluter_volume = 400.0;

        private Int32 m_targetConc = 0; // in ng/µl
        private Int32 m_targetVol = 0; // in µl
        private Int32 m_normalize = 0; // boolean: 0 or 1
        private Int32 m_source_eppi = 0; // boolean: 0 or 1
        private Int32 m_source_micronic14 = 0; // boolean: 0 or 1
        private Int32 m_source_micronic07 = 0; // boolean: 0 or 1
        private Int32 m_source_pcr_plate1 = 0; // boolean: 0 or 1
        private Int32 m_source_pcr_plate2 = 0; // boolean: 0 or 1
        private Int32 m_source_pcr_plate3 = 0; // boolean: 0 or 1
        private string m_sample_destination = "";
        private string m_destination_plate_barcode = "";
        private string m_destination_plate_platform = "";
        private string m_destination_plate_layout = "";
        private Int32 m_destination_plate_number = 0;
        private string m_sample_source_box_barcode1 = "";
        private string m_sample_source_box_barcode2 = "";
        private string m_sample_source_box_barcode3 = "";
        private Dictionary<string, CSample> sourceBarcodesDict = new Dictionary<string, CSample>();
        private Dictionary<string, CPlateWell> destinationWellDict = new Dictionary<string, CPlateWell>();

        private string[] m_args;

        public ScanReport(string[] args)
        {
            m_args = args;
            InitializeComponent();
        }

        private void ScanReport_Load(object sender, EventArgs e)
        {
            try
            {
                if (m_args.Length != 7)
                {
                    throw new Exception("Invalid argument list. Need exactly 6 parameters: \r\n" +
                                     "1. Variable file path,\r\n" +
                                     "2. Eppi 2ml Barcode file path,\r\n" +
                                     "3. Micronic 1.4ml Barcode file path,\r\n" +
                                     "4. Micronic 0.7ml Barcode file path,\r\n" +
                                     "5. JobInfo output file path,\r\n" +
                                     "6. Worklist output file path,\r\n");
                }
                loadVariables(m_args[1]);
                loadSourceBarcodes();
                loadDestinationLayout();
                getTransferVolumes();

                bool all_okay = showTransferOverview();

                if (all_okay)
                {
                    butContinue.Enabled = true;
                }
            }
            catch (Exception exc)
            {
                m_retVal = 2;
                MessageBox.Show("Error initializing dialog:\r\n" + exc.Message + "\r\n\r\nClosing application ...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        public int returnValue()
        {
            return m_retVal;
        }

        private void loadVariables(string file)
        {
            try
            {
                lblSampleSource.Text = "";
                var fileStream = new FileStream(@file, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        String[] tokenized = line.Split(',');
                        if (tokenized.Length != 3)
                        {
                            throw new Exception("Invalid entries in import variables file: \"" + file + "\" with entries:\r\n" + line);
                        }
                        else
                        {
                            if (tokenized[1] == "sample_source_eppi")
                            {
                                m_source_eppi = int.Parse(tokenized[2].Trim());
                                if (m_source_eppi == 1)
                                {
                                    lblSampleSource.Text += "Eppi 2ml; ";
                                }
                                
                            }
                            else if (tokenized[1] == "sample_source_micronic14")
                            {
                                m_source_micronic14 = int.Parse(tokenized[2].Trim());
                                if (m_source_micronic14 == 1)
                                {
                                    lblSampleSource.Text += "Micronic Tube 1.4ml; ";
                                }
                            }
                            else if (tokenized[1] == "sample_source_micronic07")
                            {
                                m_source_micronic07 = int.Parse(tokenized[2].Trim());
                                if (m_source_micronic07 == 1)
                                {
                                    lblSampleSource.Text += "Micronic Tube 0.7ml; ";
                                }
                            }
                            else if (tokenized[1] == "sample_source_pcr_plate1")
                            {
                                m_source_pcr_plate1 = int.Parse(tokenized[2].Trim());
                                if (m_source_pcr_plate1 == 1)
                                {
                                    lblSampleSource.Text += "PCR Plate1; ";
                                }
                            }
                            else if (tokenized[1] == "sample_source_pcr_plate2")
                            {
                                m_source_pcr_plate2 = int.Parse(tokenized[2].Trim());
                                if (m_source_pcr_plate2 == 1)
                                {
                                    lblSampleSource.Text += "PCR Plate2; ";
                                }
                            }
                            else if (tokenized[1] == "sample_source_pcr_plate3")
                            {
                                m_source_pcr_plate3 = int.Parse(tokenized[2].Trim());
                                if (m_source_pcr_plate3 == 1)
                                {
                                    lblSampleSource.Text += "PCR Plate3; ";
                                }
                            }
                            else if (tokenized[1] == "sample_source_box_barcode1")
                            {
                                m_sample_source_box_barcode1 = tokenized[2].Trim();
                            }
                            else if (tokenized[1] == "sample_source_box_barcode2")
                            {
                                m_sample_source_box_barcode2 = tokenized[2].Trim();
                            }
                            else if (tokenized[1] == "sample_source_box_barcode3")
                            {
                                m_sample_source_box_barcode3 = tokenized[2].Trim();
                            }
                            else if (tokenized[1] == "sample_destination")
                            {
                                m_sample_destination = tokenized[2].Trim();
                                lblSampleDestination.Text = m_sample_destination;
                            }
                            else if (tokenized[1] == "plate_barcode")
                            {
                                m_destination_plate_barcode = tokenized[2].Trim();
                                String[] plate_tokenized = m_destination_plate_barcode.Split('-');
                                m_destination_plate_platform = plate_tokenized[0];
                                m_destination_plate_layout = plate_tokenized[1];
                                m_destination_plate_number = int.Parse(plate_tokenized[2]);
                                lblSampleDestinationPlate.Text = m_destination_plate_barcode;
                            }
                            else if (tokenized[1] == "target_volume")
                            {
                                m_targetVol = int.Parse(tokenized[2].Trim());
                                lblTargetVolume.Text = m_targetVol.ToString();
                            }
                            else if (tokenized[1] == "target_concentration")
                            {
                                m_targetConc = int.Parse(tokenized[2].Trim());
                                lblTargetConcentration.Text = m_targetConc.ToString();
                            }
                            else if (tokenized[1] == "normalize")
                            {
                                m_normalize = int.Parse(tokenized[2].Trim());
                                if (m_normalize == 1)
                                {
                                    lblMethod.Text = "Normalize";
                                }
                                else
                                {
                                    lblMethod.Text = "Transfer";
                                    lblTargetConcentration.Visible = false;
                                    lblTargetConcentrationText.Visible = false;
                                }
                            }
                        }
                    }
                    fileStream.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error importing variables file: \"" + file + "\"\r\n" + e.Message);
                throw new Exception("Error importing variables file: \"" + file + "\"\r\n" + e.Message);
            }
        }

        private void loadSourceBarcodes()
        {
            if (m_source_eppi == 1)
            {
                readEppendorfScanFile(m_args[2], sourceBarcodesDict);
            }
            if (m_source_micronic07 == 1)
            {
                readMicronicScanFile("S Micronic07", m_args[4], sourceBarcodesDict);
            }
            if (m_source_micronic14 == 1)
            {
                readMicronicScanFile("S Micronic14", m_args[3], sourceBarcodesDict);
            }
            if (m_source_pcr_plate1 == 1)
            {
                CSQL.getBoxSamples("S PCR Plate1", m_sample_source_box_barcode1, sourceBarcodesDict);
            }
            if (m_source_pcr_plate2 == 1)
            {
                CSQL.getBoxSamples("S PCR Plate2", m_sample_source_box_barcode2, sourceBarcodesDict);
            }
            if (m_source_pcr_plate3 == 1)
            {
                CSQL.getBoxSamples("S PCR Plate3", m_sample_source_box_barcode3, sourceBarcodesDict);
            }

            CSQL.setPatients(sourceBarcodesDict);
            CSQL.addVolAndConc(sourceBarcodesDict);
        }

        private void loadDestinationLayout()
        {
            CSQL.loadLayout(m_destination_plate_layout, destinationWellDict);
        }

        private bool showTransferOverview()
        {
            bool all_okay = true;
            string sample_barcode = "";
            foreach (string key in destinationWellDict.Keys) 
            {
                sample_barcode = destinationWellDict[key].getSampleBarcode();
                DataGridViewRow row = (DataGridViewRow)grdTransferOverview.Rows[0].Clone();
                if (sample_barcode != "")
                {
                    row.Cells[0].Value = sourceBarcodesDict[sample_barcode].getSource();
                    row.Cells[1].Value = sourceBarcodesDict[sample_barcode].getPatient();
                    if (sourceBarcodesDict[sample_barcode].getPatient() == -1)
                    {
                        row.Cells[1].Style.BackColor = Color.Red;
                        all_okay = false;
                    }
                    row.Cells[2].Value = sourceBarcodesDict[sample_barcode].getSample();
                    if (sourceBarcodesDict[sample_barcode].getSample() == -1)
                    {
                        row.Cells[2].Style.BackColor = Color.Red;
                        all_okay = false;
                    }
                    row.Cells[3].Value = sourceBarcodesDict[sample_barcode].getBarcode();
                    if (sourceBarcodesDict[sample_barcode].getBarcode() == "***" || sourceBarcodesDict[sample_barcode].getBarcode() == "$$$")
                    {
                        row.Cells[3].Style.BackColor = Color.Red;
                        all_okay = false;
                    }
                    row.Cells[4].Value = sourceBarcodesDict[sample_barcode].getCategory();
                    row.Cells[5].Value = sourceBarcodesDict[sample_barcode].getVolume();
                    if (sourceBarcodesDict[sample_barcode].getVolume() <= 0)
                    {
                        row.Cells[5].Style.BackColor = Color.Red;
                    }
                    row.Cells[6].Value = sourceBarcodesDict[sample_barcode].getConcentration();
                    if (sourceBarcodesDict[sample_barcode].getConcentration() <= 0)
                    {
                        row.Cells[6].Style.BackColor = Color.Red;
                        if (m_normalize == 1)
                        {
                            all_okay = false;
                        }
                    }
                    else if (sourceBarcodesDict[sample_barcode].getConcentration() < m_targetConc)
                    {
                        row.Cells[6].Style.BackColor = Color.Orange;
                    }
                }
                else
                {
                    row.Cells[0].Style.BackColor = Color.Red;
                    row.Cells[1].Style.BackColor = Color.Red;
                    row.Cells[2].Style.BackColor = Color.Red;
                    row.Cells[3].Style.BackColor = Color.Red;
                    row.Cells[4].Style.BackColor = Color.Red;
                    row.Cells[5].Style.BackColor = Color.Red;
                    row.Cells[6].Style.BackColor = Color.Red;
                    all_okay = false;
                }

                row.Cells[7].Value = key;
                row.Cells[8].Value = destinationWellDict[key].getPatient();
                row.Cells[9].Value = destinationWellDict[key].getSampleVol();
                if (destinationWellDict[key].getSampleVol() < 3)
                {
                    row.Cells[9].Style.BackColor = Color.Red;
                    all_okay = false;
                }
                row.Cells[10].Value = destinationWellDict[key].getBufferVol();

                grdTransferOverview.Rows.Add(row);
            }

            grdTransferOverview.AllowUserToAddRows = false;

            return all_okay;
        }

        private void getTransferVolumes()
        {
            double buferVol = 0;
            double sampleVol = 0;

            foreach (string dkey in destinationWellDict.Keys)
            {
                foreach (string skey in sourceBarcodesDict.Keys)
                {
                    if (destinationWellDict[dkey].getPatient() == sourceBarcodesDict[skey].getPatient())
                    {
                        if (destinationWellDict[dkey].getSampleBarcode() == "")
                        {
                            destinationWellDict[dkey].setSampleBarcode(skey);

                            if (m_normalize == 1)
                            {
                                if (m_targetConc >= sourceBarcodesDict[skey].getConcentration())
                                {
                                    destinationWellDict[dkey].setSampleVol(m_targetVol);
                                    destinationWellDict[dkey].setBufferVol(0);
                                }
                                else
                                {
                                    sampleVol = Math.Round(m_targetVol / sourceBarcodesDict[skey].getConcentration() * m_targetConc);
                                    buferVol = m_targetVol - sampleVol;
                                    destinationWellDict[dkey].setSampleVol(sampleVol);
                                    destinationWellDict[dkey].setBufferVol(buferVol);
                                }
                            }
                            else
                            {
                                destinationWellDict[dkey].setSampleVol(m_targetVol);
                                destinationWellDict[dkey].setBufferVol(0);
                            }
                        }
                    }
                }
            }
        }

        private void readEppendorfScanFile(string file, Dictionary<string, CSample> tubeBarcodes)
        {
            try
            {
                Dictionary<String, String> duplicateEppendorfBarcodes = new Dictionary<string, string>();
                Dictionary<string, string> origEntry = new Dictionary<string, string>();

                int idx_mod = 1;
                var fileStream = new FileStream(@file, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    if ((line = streamReader.ReadLine()) != null)
                    {
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            String[] tokenized = line.Split(';');
                            for (int i = 0; i < tokenized.Length; i++)
                                tokenized[i] = tokenized[i].Trim(' ');
                            if (tokenized.Length == 7)
                            {
                                string barcode = tokenized[6];
                                if (barcode.CompareTo("***") == 0)
                                    continue;
                                int rack = Convert.ToInt32(tokenized[0]);
                                int well = Convert.ToInt32(tokenized[2]);
                                int idx = (rack - idx_mod) * 16 + well;

                                if (duplicateEppendorfBarcodes.Keys.Contains(barcode))
                                    throw new Exception("Duplicate barcode in scan file.\r\nOld entry: " + duplicateEppendorfBarcodes[barcode] + "\r\nNew entry: " + line);
                                else
                                    duplicateEppendorfBarcodes.Add(barcode, line);
                                tubeBarcodes[barcode] = new CSample("Eppi 2ml", barcode, rack, well, "");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                m_retVal = 2;
                throw new Exception("Error importing eppi barcode file: \"" + file + "\"\r\n" + e.Message);
            }
        }

        private void readMicronicScanFile(string source, string file, Dictionary<string, CSample> tubeBarcodes)
        {
            try
            {
                var fileStream = new FileStream(@file, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    int idx = -1;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        String[] tokenized = line.Split(',');
                        if (tokenized.Length > 1)
                        {
                            string barcode = tokenized[1];
                            if (barcode.CompareTo("***") == 0)
                                continue;
                            idx = RackPosTo96wellNumber(tokenized[0]);
                            tubeBarcodes[tokenized[1]] = new CSample(source, tokenized[1], -1, idx, tokenized[0]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                m_retVal = 2;
                throw new Exception("Error importing micronic barcode file: \"" + file + "\"\r\n" + e.Message);
            }
        }

        private void writeJobFile(string file)
        {
            try
            {
                var fileStream = new FileStream(@file, FileMode.Create, FileAccess.ReadWrite);
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write("source\tpatient_id\tsample_id\tsource BC\tcategory\tvolume (µl)\tconcentration (ng/µl)\tdestination well\tpatient_id\tsample (µl)\tTE (µl)\t\r\n");
                    foreach (DataGridViewRow row in grdTransferOverview.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            streamWriter.Write(cell.Value + "\t");
                        }
                        streamWriter.Write("\r\n");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error writing variables file: \"" + file + "\"\r\n" + e.Message);
                throw new Exception("Error writing variables file: \"" + file + "\"\r\n" + e.Message);
            }
        }

        private void writeWorklistFile(string file)
        {

            string sourceLabwareName = "";
            string sourceLabwareType = "";
            string destinationLabwareName = "";
            string destinationLabwareType = "";

            if (m_sample_destination == "D PCR Plate")
            {
                destinationLabwareName = "D PCR Plate";
                destinationLabwareType = "96 well PCR in Metalladapter";
            }
            else if (m_sample_destination == "D DeepWell Plate")
            {
                destinationLabwareName = "D DeepWell Plate";
                destinationLabwareType = "Deepwell 0.8 mL";
            }
            else if (m_sample_destination == "D iScan Plate")
            {
                destinationLabwareName = "D iScan Plate";
                destinationLabwareType = "4titudePCR96wellSkirted";
            }
            else
            {
                m_retVal = 2;
                throw new Exception("Error getting sample destination: " + m_sample_destination);
            }

            try
            {
                var fileStream = new FileStream(@file, FileMode.Create, FileAccess.ReadWrite);
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    DateTime localDate = DateTime.Now;
                    int tip = 0;
                    double volume = 0.0;
                    double maxAspirate = m_max_diluter_volume;

                    streamWriter.WriteLine("C; This GWL was generated by TecanEvo_MultilabwareNormalization at " + localDate);
                    streamWriter.WriteLine("B;");
                    streamWriter.WriteLine("C; Pipetting scheme for job type: " + lblMethod.Text);
                    streamWriter.WriteLine("B;");
                    streamWriter.WriteLine("C; DNA Source: " + lblSampleSource.Text.Trim() + ", DNA Destination: " + m_sample_destination);
                    streamWriter.WriteLine("B;");

                    if (m_normalize == 1)
                    {
                        streamWriter.WriteLine("C; pipetting Buffer");
                        streamWriter.WriteLine("B;");
                        foreach (string key in destinationWellDict.Keys)
                        {
                            volume = destinationWellDict[key].getBufferVol();

                            if (volume > 0)
                            {
                                if (volume <= maxAspirate)
                                {
                                    streamWriter.WriteLine("A;Buffer;;Trough 100ml;" + (tip + 1) + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job Buffer;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                    streamWriter.WriteLine("D;" + destinationLabwareName + ";;" + destinationLabwareType + ";" + RackPosTo96wellNumber(key) + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                    streamWriter.WriteLine("W3;");
                                }
                                else
                                {
                                    double readwriteVolume = volume;
                                    double actPipetVolume = maxAspirate;

                                    while (readwriteVolume > 0)
                                    {
                                        streamWriter.WriteLine("A;Buffer;;Trough 100ml;" + (tip + 1) + ";;" + actPipetVolume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job Buffer;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                        streamWriter.WriteLine("D;" + destinationLabwareName + ";;" + destinationLabwareType + ";" + RackPosTo96wellNumber(key) + ";;" + actPipetVolume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                        readwriteVolume -= actPipetVolume;
                                        if (readwriteVolume > maxAspirate)
                                            actPipetVolume = maxAspirate;
                                        else
                                            actPipetVolume = readwriteVolume;
                                    }
                                    streamWriter.WriteLine("W3;");
                                }
                            }
                            if (++tip == 8)
                            {
                                tip = 0;
                            }
                        }
                        streamWriter.WriteLine("B;");
                    }

                    streamWriter.WriteLine("C; pipetting DNA");
                    streamWriter.WriteLine("B;");
                    foreach (string key in destinationWellDict.Keys)
                    {
                        volume = destinationWellDict[key].getSampleVol();
                        sourceLabwareName = sourceBarcodesDict[destinationWellDict[key].getSampleBarcode()].getSource();
                        sourceLabwareType = getLabwareType(sourceLabwareName);

                        if (volume <= maxAspirate)
                        {
                            if (sourceLabwareName == "Eppi 2ml")
                            {
                                streamWriter.WriteLine("A;" + sourceBarcodesDict[destinationWellDict[key].getSampleBarcode()].getEppieRack() + ";;" + sourceLabwareType + ";" + sourceBarcodesDict[destinationWellDict[key].getSampleBarcode()].getRackPos() + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                            }
                            else
                            {
                                streamWriter.WriteLine("A;" + sourceLabwareName + ";;" + sourceLabwareType + ";" + sourceBarcodesDict[destinationWellDict[key].getSampleBarcode()].getRackPos() + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                            }
                            streamWriter.WriteLine("D;" + destinationLabwareName + ";;" + destinationLabwareType + ";" + RackPosTo96wellNumber(key) + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                        }
                        else
                        {
                            double readwriteVolume = volume;
                            double actPipetVolume = maxAspirate;

                            while (readwriteVolume > 0)
                            {
                                if (sourceLabwareName == "Eppi 2ml")
                                {
                                    streamWriter.WriteLine("A;" + sourceBarcodesDict[destinationWellDict[key].getSampleBarcode()].getEppieRack() + ";;" + sourceLabwareType + ";" + sourceBarcodesDict[destinationWellDict[key].getSampleBarcode()].getRackPos() + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                }
                                else
                                {
                                    streamWriter.WriteLine("A;" + sourceLabwareName + ";;" + sourceLabwareType + ";" + sourceBarcodesDict[destinationWellDict[key].getSampleBarcode()].getRackPos() + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                }
                                streamWriter.WriteLine("D;" + destinationLabwareName + ";;" + destinationLabwareType + ";" + RackPosTo96wellNumber(key) + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                readwriteVolume -= actPipetVolume;
                                if (readwriteVolume > maxAspirate)
                                    actPipetVolume = maxAspirate;
                                else
                                    actPipetVolume = readwriteVolume;
                            }
                        }
                        if (m_normalize == 1)
                        {
                            streamWriter.WriteLine("A;" + destinationLabwareName + ";;" + destinationLabwareType + ";" + RackPosTo96wellNumber(key) + ";;" + (3.0).ToString("R", CultureInfo.InvariantCulture) + ";DropSense;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                            streamWriter.WriteLine("D;NSense1;;;" + RackPosTo96wellNumber(key) + ";;" + (2.5).ToString("R", CultureInfo.InvariantCulture) + ";DropSense;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                        }
                        streamWriter.WriteLine("W;");

                        if (++tip == 8)
                        {
                            tip = 0;
                            streamWriter.WriteLine("B;");
                            for (int tipWash = 0; tipWash < 8; tipWash++)
                            {
                                streamWriter.WriteLine("A;NaClO;;Trough 100ml;" + (tipWash + 1) + ";;" + maxAspirate.ToString("R", CultureInfo.InvariantCulture) + ";Decon Mix In Trough Detect;;" + Convert.ToInt16(Math.Pow(2, tipWash)) + ";;;");
                                streamWriter.WriteLine("D;NaClO;;Trough 100ml;" + (tipWash + 1) + ";;" + maxAspirate.ToString("R", CultureInfo.InvariantCulture) + ";Decon Mix In Trough Detect;;" + Convert.ToInt16(Math.Pow(2, tipWash)) + ";;;");
                                streamWriter.WriteLine("W2;");
                            }
                            streamWriter.WriteLine("B;");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error writing variables file: \"" + file + "\"\r\n" + e.Message);
                throw new Exception("Error writing variables file: \"" + file + "\"\r\n" + e.Message);
            }
        }

        public int RackPosTo96wellNumber(string well)
        {
            switch (well)
            {
                case "A01": return 1;
                case "B01": return 2;
                case "C01": return 3;
                case "D01": return 4;
                case "E01": return 5;
                case "F01": return 6;
                case "G01": return 7;
                case "H01": return 8;
                case "A02": return 9;
                case "B02": return 10;
                case "C02": return 11;
                case "D02": return 12;
                case "E02": return 13;
                case "F02": return 14;
                case "G02": return 15;
                case "H02": return 16;
                case "A03": return 17;
                case "B03": return 18;
                case "C03": return 19;
                case "D03": return 20;
                case "E03": return 21;
                case "F03": return 22;
                case "G03": return 23;
                case "H03": return 24;
                case "A04": return 25;
                case "B04": return 26;
                case "C04": return 27;
                case "D04": return 28;
                case "E04": return 29;
                case "F04": return 30;
                case "G04": return 31;
                case "H04": return 32;
                case "A05": return 33;
                case "B05": return 34;
                case "C05": return 35;
                case "D05": return 36;
                case "E05": return 37;
                case "F05": return 38;
                case "G05": return 39;
                case "H05": return 40;
                case "A06": return 41;
                case "B06": return 42;
                case "C06": return 43;
                case "D06": return 44;
                case "E06": return 45;
                case "F06": return 46;
                case "G06": return 47;
                case "H06": return 48;
                case "A07": return 49;
                case "B07": return 50;
                case "C07": return 51;
                case "D07": return 52;
                case "E07": return 53;
                case "F07": return 54;
                case "G07": return 55;
                case "H07": return 56;
                case "A08": return 57;
                case "B08": return 58;
                case "C08": return 59;
                case "D08": return 60;
                case "E08": return 61;
                case "F08": return 62;
                case "G08": return 63;
                case "H08": return 64;
                case "A09": return 65;
                case "B09": return 66;
                case "C09": return 67;
                case "D09": return 68;
                case "E09": return 69;
                case "F09": return 70;
                case "G09": return 71;
                case "H09": return 72;
                case "A10": return 73;
                case "B10": return 74;
                case "C10": return 75;
                case "D10": return 76;
                case "E10": return 77;
                case "F10": return 78;
                case "G10": return 79;
                case "H10": return 80;
                case "A11": return 81;
                case "B11": return 82;
                case "C11": return 83;
                case "D11": return 84;
                case "E11": return 85;
                case "F11": return 86;
                case "G11": return 87;
                case "H11": return 88;
                case "A12": return 89;
                case "B12": return 90;
                case "C12": return 91;
                case "D12": return 92;
                case "E12": return 93;
                case "F12": return 94;
                case "G12": return 95;
                case "H12": return 96;
                default: return -1;
            }
        }

        public int RackPosTo384wellNumber(string well)
        {
            switch (well)
            {
                case "A01": return 1;
                case "B01": return 2;
                case "C01": return 3;
                case "D01": return 4;
                case "E01": return 5;
                case "F01": return 6;
                case "G01": return 7;
                case "H01": return 8;
                case "I01": return 9;
                case "J01": return 10;
                case "K01": return 11;
                case "L01": return 12;
                case "M01": return 13;
                case "N01": return 14;
                case "O01": return 15;
                case "P01": return 16;
                case "A02": return 17;
                case "B02": return 18;
                case "C02": return 19;
                case "D02": return 20;
                case "E02": return 21;
                case "F02": return 22;
                case "G02": return 23;
                case "H02": return 24;
                case "I02": return 25;
                case "J02": return 26;
                case "K02": return 27;
                case "L02": return 28;
                case "M02": return 29;
                case "N02": return 30;
                case "O02": return 31;
                case "P02": return 32;
                case "A03": return 33;
                case "B03": return 34;
                case "C03": return 35;
                case "D03": return 36;
                case "E03": return 37;
                case "F03": return 38;
                case "G03": return 39;
                case "H03": return 40;
                case "I03": return 41;
                case "J03": return 42;
                case "K03": return 43;
                case "L03": return 44;
                case "M03": return 45;
                case "N03": return 46;
                case "O03": return 47;
                case "P03": return 48;
                case "A04": return 49;
                case "B04": return 50;
                case "C04": return 51;
                case "D04": return 52;
                case "E04": return 53;
                case "F04": return 54;
                case "G04": return 55;
                case "H04": return 56;
                case "I04": return 57;
                case "J04": return 58;
                case "K04": return 59;
                case "L04": return 60;
                case "M04": return 61;
                case "N04": return 62;
                case "O04": return 63;
                case "P04": return 64;
                case "A05": return 65;
                case "B05": return 66;
                case "C05": return 67;
                case "D05": return 68;
                case "E05": return 69;
                case "F05": return 70;
                case "G05": return 71;
                case "H05": return 72;
                case "I05": return 73;
                case "J05": return 74;
                case "K05": return 75;
                case "L05": return 76;
                case "M05": return 77;
                case "N05": return 78;
                case "O05": return 79;
                case "P05": return 80;
                case "A06": return 81;
                case "B06": return 82;
                case "C06": return 83;
                case "D06": return 84;
                case "E06": return 85;
                case "F06": return 86;
                case "G06": return 87;
                case "H06": return 88;
                case "I06": return 89;
                case "J06": return 90;
                case "K06": return 91;
                case "L06": return 92;
                case "M06": return 93;
                case "N06": return 94;
                case "O06": return 95;
                case "P06": return 96;
                case "A07": return 97;
                case "B07": return 98;
                case "C07": return 99;
                case "D07": return 100;
                case "E07": return 101;
                case "F07": return 102;
                case "G07": return 103;
                case "H07": return 104;
                case "I07": return 105;
                case "J07": return 106;
                case "K07": return 107;
                case "L07": return 108;
                case "M07": return 109;
                case "N07": return 110;
                case "O07": return 111;
                case "P07": return 112;
                case "A08": return 113;
                case "B08": return 114;
                case "C08": return 115;
                case "D08": return 116;
                case "E08": return 117;
                case "F08": return 118;
                case "G08": return 119;
                case "H08": return 120;
                case "I08": return 121;
                case "J08": return 122;
                case "K08": return 123;
                case "L08": return 124;
                case "M08": return 125;
                case "N08": return 126;
                case "O08": return 127;
                case "P08": return 128;
                case "A09": return 129;
                case "B09": return 130;
                case "C09": return 131;
                case "D09": return 132;
                case "E09": return 133;
                case "F09": return 134;
                case "G09": return 135;
                case "H09": return 136;
                case "I09": return 137;
                case "J09": return 138;
                case "K09": return 139;
                case "L09": return 140;
                case "M09": return 141;
                case "N09": return 142;
                case "O09": return 143;
                case "P09": return 144;
                case "A10": return 145;
                case "B10": return 146;
                case "C10": return 147;
                case "D10": return 148;
                case "E10": return 149;
                case "F10": return 150;
                case "G10": return 151;
                case "H10": return 152;
                case "I10": return 153;
                case "J10": return 154;
                case "K10": return 155;
                case "L10": return 156;
                case "M10": return 157;
                case "N10": return 158;
                case "O10": return 159;
                case "P10": return 160;
                case "A11": return 161;
                case "B11": return 162;
                case "C11": return 163;
                case "D11": return 164;
                case "E11": return 165;
                case "F11": return 166;
                case "G11": return 167;
                case "H11": return 168;
                case "I11": return 169;
                case "J11": return 170;
                case "K11": return 171;
                case "L11": return 172;
                case "M11": return 173;
                case "N11": return 174;
                case "O11": return 175;
                case "P11": return 176;
                case "A12": return 177;
                case "B12": return 178;
                case "C12": return 179;
                case "D12": return 180;
                case "E12": return 181;
                case "F12": return 182;
                case "G12": return 183;
                case "H12": return 184;
                case "I12": return 185;
                case "J12": return 186;
                case "K12": return 187;
                case "L12": return 188;
                case "M12": return 189;
                case "N12": return 190;
                case "O12": return 191;
                case "P12": return 192;
                case "A13": return 193;
                case "B13": return 194;
                case "C13": return 195;
                case "D13": return 196;
                case "E13": return 197;
                case "F13": return 198;
                case "G13": return 199;
                case "H13": return 200;
                case "I13": return 201;
                case "J13": return 202;
                case "K13": return 203;
                case "L13": return 204;
                case "M13": return 205;
                case "N13": return 206;
                case "O13": return 207;
                case "P13": return 208;
                case "A14": return 209;
                case "B14": return 210;
                case "C14": return 211;
                case "D14": return 212;
                case "E14": return 213;
                case "F14": return 214;
                case "G14": return 215;
                case "H14": return 216;
                case "I14": return 217;
                case "J14": return 218;
                case "K14": return 219;
                case "L14": return 220;
                case "M14": return 221;
                case "N14": return 222;
                case "O14": return 223;
                case "P14": return 224;
                case "A15": return 225;
                case "B15": return 226;
                case "C15": return 227;
                case "D15": return 228;
                case "E15": return 229;
                case "F15": return 230;
                case "G15": return 231;
                case "H15": return 232;
                case "I15": return 233;
                case "J15": return 234;
                case "K15": return 235;
                case "L15": return 236;
                case "M15": return 237;
                case "N15": return 238;
                case "O15": return 239;
                case "P15": return 240;
                case "A16": return 241;
                case "B16": return 242;
                case "C16": return 243;
                case "D16": return 244;
                case "E16": return 245;
                case "F16": return 246;
                case "G16": return 247;
                case "H16": return 248;
                case "I16": return 249;
                case "J16": return 250;
                case "K16": return 251;
                case "L16": return 252;
                case "M16": return 253;
                case "N16": return 254;
                case "O16": return 255;
                case "P16": return 256;
                case "A17": return 257;
                case "B17": return 258;
                case "C17": return 259;
                case "D17": return 260;
                case "E17": return 261;
                case "F17": return 262;
                case "G17": return 263;
                case "H17": return 264;
                case "I17": return 265;
                case "J17": return 266;
                case "K17": return 267;
                case "L17": return 268;
                case "M17": return 269;
                case "N17": return 270;
                case "O17": return 271;
                case "P17": return 272;
                case "A18": return 273;
                case "B18": return 274;
                case "C18": return 275;
                case "D18": return 276;
                case "E18": return 277;
                case "F18": return 278;
                case "G18": return 279;
                case "H18": return 280;
                case "I18": return 281;
                case "J18": return 282;
                case "K18": return 283;
                case "L18": return 284;
                case "M18": return 285;
                case "N18": return 286;
                case "O18": return 287;
                case "P18": return 288;
                case "A19": return 289;
                case "B19": return 290;
                case "C19": return 291;
                case "D19": return 292;
                case "E19": return 293;
                case "F19": return 294;
                case "G19": return 295;
                case "H19": return 296;
                case "I19": return 297;
                case "J19": return 298;
                case "K19": return 299;
                case "L19": return 300;
                case "M19": return 301;
                case "N19": return 302;
                case "O19": return 303;
                case "P19": return 304;
                case "A20": return 305;
                case "B20": return 306;
                case "C20": return 307;
                case "D20": return 308;
                case "E20": return 309;
                case "F20": return 310;
                case "G20": return 311;
                case "H20": return 312;
                case "I20": return 313;
                case "J20": return 314;
                case "K20": return 315;
                case "L20": return 316;
                case "M20": return 317;
                case "N20": return 318;
                case "O20": return 319;
                case "P20": return 320;
                case "A21": return 321;
                case "B21": return 322;
                case "C21": return 323;
                case "D21": return 324;
                case "E21": return 325;
                case "F21": return 326;
                case "G21": return 327;
                case "H21": return 328;
                case "I21": return 329;
                case "J21": return 330;
                case "K21": return 331;
                case "L21": return 332;
                case "M21": return 333;
                case "N21": return 334;
                case "O21": return 335;
                case "P21": return 336;
                case "A22": return 337;
                case "B22": return 338;
                case "C22": return 339;
                case "D22": return 340;
                case "E22": return 341;
                case "F22": return 342;
                case "G22": return 343;
                case "H22": return 344;
                case "I22": return 345;
                case "J22": return 346;
                case "K22": return 347;
                case "L22": return 348;
                case "M22": return 349;
                case "N22": return 350;
                case "O22": return 351;
                case "P22": return 352;
                case "A23": return 353;
                case "B23": return 354;
                case "C23": return 355;
                case "D23": return 356;
                case "E23": return 357;
                case "F23": return 358;
                case "G23": return 359;
                case "H23": return 360;
                case "I23": return 361;
                case "J23": return 362;
                case "K23": return 363;
                case "L23": return 364;
                case "M23": return 365;
                case "N23": return 366;
                case "O23": return 367;
                case "P23": return 368;
                case "A24": return 369;
                case "B24": return 370;
                case "C24": return 371;
                case "D24": return 372;
                case "E24": return 373;
                case "F24": return 374;
                case "G24": return 375;
                case "H24": return 376;
                case "I24": return 377;
                case "J24": return 378;
                case "K24": return 379;
                case "L24": return 380;
                case "M24": return 381;
                case "N24": return 382;
                case "O24": return 383;
                case "P24": return 384;

                default: return -1;
            }
        }

        public static Int32 wellToNumber(char row, int col, string mode = "96well")
        {
            int i_row = 0;
            switch (row)
            {
                case 'A':
                    i_row = 1;
                    break;
                case 'B':
                    i_row = 2;
                    break;
                case 'C':
                    i_row = 3;
                    break;
                case 'D':
                    i_row = 4;
                    break;
                case 'E':
                    i_row = 5;
                    break;
                case 'F':
                    i_row = 6;
                    break;
                case 'G':
                    i_row = 7;
                    break;
                case 'H':
                    i_row = 8;
                    break;
                case 'I':
                    i_row = 9;
                    break;
                case 'J':
                    i_row = 10;
                    break;
                case 'K':
                    i_row = 11;
                    break;
                case 'L':
                    i_row = 12;
                    break;
                case 'M':
                    i_row = 13;
                    break;
                case 'N':
                    i_row = 14;
                    break;
                case 'O':
                    i_row = 15;
                    break;
                case 'P':
                    i_row = 16;
                    break;
                default:
                    i_row = 0;
                    break;
            }
            if (i_row > 0)
            {
                if( mode == "96well")
                {
                    return i_row + (col - 1) * 8;
                }
                else
                {
                    return i_row + (col - 1) * 16;
                }
            }
            else
            {
                return -1;
            }
        }

        public static Int32 rowToNumber(char row)
        {
            int i_row = 0;
            switch (row)
            {
                case 'A':
                    i_row = 1;
                    break;
                case 'B':
                    i_row = 2;
                    break;
                case 'C':
                    i_row = 3;
                    break;
                case 'D':
                    i_row = 4;
                    break;
                case 'E':
                    i_row = 5;
                    break;
                case 'F':
                    i_row = 6;
                    break;
                case 'G':
                    i_row = 7;
                    break;
                case 'H':
                    i_row = 8;
                    break;
                case 'I':
                    i_row = 9;
                    break;
                case 'J':
                    i_row = 10;
                    break;
                case 'K':
                    i_row = 11;
                    break;
                case 'L':
                    i_row = 12;
                    break;
                case 'M':
                    i_row = 13;
                    break;
                case 'N':
                    i_row = 14;
                    break;
                case 'O':
                    i_row = 15;
                    break;
                case 'P':
                    i_row = 16;
                    break;
                default:
                    i_row = 0;
                    break;
            }
             return i_row;
        }

        public static string getWell(char row, int col)
        {
            if (col < 10)
            {
                return row + "0" + col.ToString();
            }
            else
            {
                return row + col.ToString();
            }
        }

        public static string getLabwareType(string labwareName)
        {
            if (labwareName == "Eppi 2ml")
            {
                return "Tube Eppendorf 16 Pos";
            }
            else if (labwareName == "Micronic 1.4ml")
            {
                return "Micronic 1.4 mL";
            }
            else if (labwareName == "Micronic 0.7ml")
            {
                return "Micronic 0.7 mL";
            }
            else if (labwareName == "Plate Well")
            {
                return "96 well PCR in Metalladapter";
            }
            else
            {
                return "";
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            m_retVal = 2;
            Application.Exit();
        }

        private void butContinue_Click(object sender, EventArgs e)
        {
            writeJobFile(m_args[5]);
            writeWorklistFile(m_args[6]);
            m_retVal = 0;
            Application.Exit();
        }
    }
}
