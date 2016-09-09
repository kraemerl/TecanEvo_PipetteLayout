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
                    throw new Exception("Invalid argument list. Need exactly 4 parameters: \r\n" +
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
                if (destinationWellDict[key].getSampleVol() < 4)
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

        public static Int32 wellToNumber(char row, int col)
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
                default:
                    i_row = 0;
                    break;
            }
            if (i_row > 0)
            {
                return i_row + (col - 1) * 8;
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
