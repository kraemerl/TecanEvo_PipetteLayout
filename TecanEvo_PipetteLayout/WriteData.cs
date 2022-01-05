using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TecanEvo_PipetteLayout
{
    class WriteData
    {
        /// <summary>
        /// retVal: 0 - all OK
        /// retVal: 1 - all OK, load variables
        /// retVal: 2 - Errors, abort Evoware script
        /// 
        /// </summary>
        private int m_retVal = 2;

        private Int32 m_targetConc = 0; // in ng/µl
        private Int32 m_targetVol = 0; // in µl
        private Int32 m_normalize = 0; // boolean: 0 or 1
        private Int32 m_destination_plate_id = 0;
        private string m_destination_plate_barcode = "";
        private string m_destination_plate_platform = "";
        private string m_destination_plate_layout = "";
        private Int32 m_destination_plate_number = 0;
        private Int32 m_drop_sense_measurement = 0; // boolean: 0 or 1

        private Dictionary<string, CPlateWell> newWellDict = new Dictionary<string, CPlateWell>();

        private string[] m_args;

        public WriteData(string[] args)
        {
            m_args = args;
            try
            {
                loadVariables(m_args[1]);
                backupFiles();
                loadLastJobFile(m_args[2]);
                writeToIBDbase();
                m_retVal = 0;
            }
            catch (Exception e)
            {
                m_retVal = 2;
                MessageBox.Show(e.Message);
                Application.Exit();
            }
        }

        public int returnValue()
        {
            return m_retVal;
        }

        private void backupFiles()
        {
            if (Directory.Exists(m_args[5]) == false)
            {
                DirectoryInfo di = Directory.CreateDirectory(m_args[5]);
            }

            string prefix = m_destination_plate_barcode + "_";
            File.Copy(m_args[1], prefix + "variables.txt", true);
            File.Copy(m_args[2], prefix + "jobInfo.txt", true);
            File.Copy(m_args[3], prefix + "worklist.txt", true);
            if (m_drop_sense_measurement == 1)
            {
                File.Copy(m_args[4], prefix + "dropSense.txt",true);
            }
        }

        private void deleteFiles()
        {
            File.Delete(m_args[1]);
            File.Delete(m_args[2]);
            File.Delete(m_args[3]);
            if (m_drop_sense_measurement == 1)
            {
                File.Delete(m_args[4]);
            }
        }

        private void loadLastJobFile(string file)
        {
            try
            {
                var fileStream = new FileStream(@file, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    if ((line = streamReader.ReadLine()) != null)
                    {
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            String[] tokenized = line.Split('\t');
                            for (int i = 0; i < tokenized.Length; i++)
                                tokenized[i] = tokenized[i].Trim(' ');
                            if (tokenized[0] != "source" && tokenized[0] != "")
                            {
                                newWellDict[tokenized[7]] = new CPlateWell(int.Parse(tokenized[1]), int.Parse(tokenized[2]), tokenized[7], tokenized[7][0], int.Parse(tokenized[7].Substring(1)));
                                newWellDict[tokenized[7]].setSampleVol(Convert.ToDouble(tokenized[9]));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                m_retVal = 2;
                throw new Exception("Error importing last job file: \"" + file + "\"\r\n" + e.Message);
            }
        }

        private void loadVariables(string file)
        {
            try
            {
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
                            if (tokenized[1] == "plate_barcode")
                            {
                                m_destination_plate_barcode = tokenized[2].Trim();
                                String[] plate_tokenized = m_destination_plate_barcode.Split('-');
                                m_destination_plate_platform = plate_tokenized[0];
                                m_destination_plate_layout = plate_tokenized[1];
                                m_destination_plate_number = int.Parse(plate_tokenized[2]);
                                m_destination_plate_id = CSQL.getPlateID_by_barcode(m_destination_plate_barcode);
                            }
                            else if (tokenized[1] == "target_volume")
                            {
                                m_targetVol = int.Parse(tokenized[2].Trim());
                            }
                            else if (tokenized[1] == "target_concentration")
                            {
                                m_targetConc = int.Parse(tokenized[2].Trim());
                            }
                            else if (tokenized[1] == "normalize")
                            {
                                m_normalize = int.Parse(tokenized[2].Trim());
                                if (m_normalize == 0)
                                {
                                    m_targetConc = 0;
                                }
                            }
                            else if (tokenized[1] == "drop_sense")
                            {
                                m_drop_sense_measurement = int.Parse(tokenized[2].Trim());
                                m_targetVol = m_targetVol - 3;
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

        private void writeToIBDbase()
        {
            try
            {
                CSQL.addSamplesToPlate(
                    m_destination_plate_id,
                    m_targetVol,
                    m_targetConc,
                    newWellDict
                );
            }
            catch (Exception e)
            {
                m_retVal = 2;
                throw new Exception("Error while writing to IBDbase:" + e.Message);
            }
        }
    }
}
