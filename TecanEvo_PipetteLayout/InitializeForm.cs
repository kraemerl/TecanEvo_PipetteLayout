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

namespace TecanEvo_PipetteLayout
{
    public partial class InitializeForm : Form
    {
        /// <summary>
        /// retVal: 0 - all OK
        /// retVal: 1 - all OK, load variables
        /// retVal: 2 - Errors, abort Evoware script
        /// retVal: 3
        /// retVal: 4
        /// retVal: 5
        /// 
        /// </summary>
        private int m_retVal = 2;

        private Int32 m_maxTargetConc = 10000; // in ng/µl
        private Int32 m_maxTargetVol = 0; // in µl
        private Int32 m_targetConc = 0; // in ng/µl
        private Int32 m_targetVol = 0; // in µl
        private Int32 m_normalize = 0; // boolean: 0 or 1
        private Int32 m_drop_sense_measurement = 1; // boolean: 0 or 1
        private Int32 m_drop_sense_mix = 1; // boolean: 0 or 1
        private Int32 m_source_eppi = 0; // boolean: 0 or 1
        private Int32 m_source_micronic14 = 0; // boolean: 0 or 1
        private Int32 m_source_micronic07 = 0; // boolean: 0 or 1
        private Int32 m_source_pcr_plate1 = 0; // boolean: 0 or 1
        private Int32 m_source_pcr_plate2 = 0; // boolean: 0 or 1
        private Int32 m_source_pcr_plate3 = 0; // boolean: 0 or 1
        private string m_destination = "";

        private string[] m_args;

        public InitializeForm(string[] args)
        {
            m_args = args;
            InitializeComponent();
        }

        private void chkNormalize_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNormalize.Checked)
            {
                lblConcentration.Visible = true;
                txtConcentration.Visible = true;
                chkDropSense.Enabled = true;
                chkMix.Enabled = true;
                m_normalize = 1;
            }
            else
            {
                lblConcentration.Visible = false;
                txtConcentration.Visible = false;
                chkDropSense.Enabled = false;
                chkMix.Enabled = false;
                m_normalize = 0;
            }
        }

        public int returnValue()
        {
            return m_retVal;
        }

        private void writeVariables(string file)
        {
            try
            {
                var fileStream = new FileStream(@file, FileMode.Create, FileAccess.ReadWrite);
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write("S,sample_destination," + m_destination + "\r\n");
                    streamWriter.Write("S,plate_barcode," + txtPlateBarcode.Text.Trim() + "\r\n");
                    streamWriter.Write("I,sample_source_eppi," + m_source_eppi + "\r\n");
                    streamWriter.Write("I,sample_source_micronic14," + m_source_micronic14 + "\r\n");
                    streamWriter.Write("I,sample_source_micronic07," + m_source_micronic07 + "\r\n");
                    streamWriter.Write("I,sample_source_pcr_plate1," + m_source_pcr_plate1 + "\r\n");
                    streamWriter.Write("I,sample_source_pcr_plate2," + m_source_pcr_plate2 + "\r\n");
                    streamWriter.Write("I,sample_source_pcr_plate3," + m_source_pcr_plate3 + "\r\n");
                    streamWriter.Write("I,target_volume," + m_targetVol + "\r\n");
                    streamWriter.Write("I,target_concentration," + m_targetConc + "\r\n");
                    streamWriter.Write("I,normalize," + m_normalize + "\r\n");
                    if (txtSourcePlate1BoxBarcode.Text.Trim() != "")
                    {
                        streamWriter.Write("S,sample_source_box_barcode1," + txtSourcePlate1BoxBarcode.Text.Trim() + "\r\n");
                    }
                    if (txtSourcePlate2BoxBarcode.Text.Trim() != "")
                    {
                        streamWriter.Write("S,sample_source_box_barcode2," + txtSourcePlate2BoxBarcode.Text.Trim() + "\r\n");
                    }
                    if (txtSourcePlate3BoxBarcode.Text.Trim() != "")
                    {
                        streamWriter.Write("S,sample_source_box_barcode3," + txtSourcePlate3BoxBarcode.Text.Trim() + "\r\n");
                    }
                    if (m_normalize == 1)
                    {
                        streamWriter.Write("I,drop_sense," + m_drop_sense_measurement + "\r\n");
                        streamWriter.Write("I,drop_sense_mix," + m_drop_sense_mix + "\r\n");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error writing variables file: \"" + file + "\"\r\n" + e.Message);
                throw new Exception("Error writing variables file: \"" + file + "\"\r\n" + e.Message);
            }
        }

        private void butOkay_Click(object sender, EventArgs e)
        {
            m_targetConc = int.Parse(txtConcentration.Text);
            m_targetVol = int.Parse(txtVolume.Text);

            if (chkSourcePlate1.Checked)
            {
                if (txtSourcePlate1BoxBarcode.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter a box barcode for PCR plate 1.");
                    txtSourcePlate1BoxBarcode.Focus();
                    return;
                }
                else
                {
                    if (CSQL.checkBoxExist(txtSourcePlate1BoxBarcode.Text.Trim()) == false)
                    {
                        MessageBox.Show("Box " + txtSourcePlate1BoxBarcode.Text.Trim() + " does not exist.");
                        txtSourcePlate1BoxBarcode.Focus();
                        return;
                    }
                    if (CSQL.checkBoxIsEmpty(txtSourcePlate1BoxBarcode.Text.Trim()) == true)
                    {
                        MessageBox.Show("Box " + txtSourcePlate1BoxBarcode.Text.Trim() + " is empty.");
                        txtSourcePlate1BoxBarcode.Focus();
                        return;
                    }
                }
            }

            if (chkSourcePlate2.Checked)
            {
                if (txtSourcePlate2BoxBarcode.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter a box barcode for PCR plate 2.");
                    txtSourcePlate2BoxBarcode.Focus();
                    return;
                }
                else
                {
                    if (CSQL.checkBoxExist(txtSourcePlate2BoxBarcode.Text.Trim()) == false)
                    {
                        MessageBox.Show("Box " + txtSourcePlate2BoxBarcode.Text.Trim() + " does not exist.");
                        txtSourcePlate2BoxBarcode.Focus();
                        return;
                    }
                    if (CSQL.checkBoxIsEmpty(txtSourcePlate2BoxBarcode.Text.Trim()) == true)
                    {
                        MessageBox.Show("Box " + txtSourcePlate2BoxBarcode.Text.Trim() + " is empty.");
                        txtSourcePlate2BoxBarcode.Focus();
                        return;
                    }
                }
            }

            if (chkSourcePlate3.Checked)
            {
                if (txtSourcePlate3BoxBarcode.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter a box barcode for PCR plate 3.");
                    txtSourcePlate3BoxBarcode.Focus();
                    return;
                }
                else
                {
                    if (CSQL.checkBoxExist(txtSourcePlate3BoxBarcode.Text.Trim()) == false)
                    {
                        MessageBox.Show("Box " + txtSourcePlate3BoxBarcode.Text.Trim() + " does not exist.");
                        txtSourcePlate3BoxBarcode.Focus();
                        return;
                    }
                    if (CSQL.checkBoxIsEmpty(txtSourcePlate3BoxBarcode.Text.Trim()) == true)
                    {
                        MessageBox.Show("Box " + txtSourcePlate3BoxBarcode.Text.Trim() + " is empty.");
                        txtSourcePlate3BoxBarcode.Focus();
                        return;
                    }
                }
            }

            if (txtPlateBarcode.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a plate barcode.");
                txtPlateBarcode.Focus();
                return;
            }

            if (CSQL.checkPlateExist(txtPlateBarcode.Text.Trim()) == false)
            {
                MessageBox.Show("Unknown or unusable plate.");
                txtPlateBarcode.Focus();
                return;
            }

            if (m_targetVol < 3)
            {
                MessageBox.Show("Please enter a volume >= 3");
                txtVolume.Focus();
                return;
            }

            if (m_targetConc <= 0 && m_normalize == 1)
            {
                MessageBox.Show("Please enter a concentration > 0");
                txtConcentration.Focus();
                return;
            }

            writeVariables(m_args[1]);
            m_retVal = 1;
            Application.Exit();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            m_retVal = 2;
            Application.Exit();
        }

        private void rbDestinationPCRPlate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDestinationPCRPlate.Checked)
            {
                m_destination = "D PCR Plate";
                m_maxTargetVol = 200;
            }
            if (int.Parse(txtVolume.Text.Trim()) > m_maxTargetVol)
            {
                txtVolume.Text = m_maxTargetVol.ToString();
            }
        }

        private void rbDestinationDeepWellPlate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDestinationDeepWellPlate.Checked)
            {
                m_destination = "D DeepWell Plate";
                m_maxTargetVol = 800;
            }
            if (int.Parse(txtVolume.Text.Trim()) > m_maxTargetVol)
            {
                txtVolume.Text = m_maxTargetVol.ToString();
            }
        }

        private void rbDestinationIScanPlate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDestinationIScanPlate.Checked)
            {
                m_destination = "D iScan Plate";
                m_maxTargetVol = 200;
            }
            if (int.Parse(txtVolume.Text.Trim()) > m_maxTargetVol)
            {
                txtVolume.Text = m_maxTargetVol.ToString();
            }
        }

        private void chkSourceEppi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSourceEppi.Checked)
            {
                m_source_eppi = 1;
            }
            else
            {
                m_source_eppi = 0;
            }
        }

        private void chkSourceMicronic14_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSourceMicronic14.Checked)
            {
                m_source_micronic14 = 1;
            }
            else
            {
                m_source_micronic14 = 0;
            }
        }

        private void chkSourceMicronic07_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSourceMicronic07.Checked)
            {
                m_source_micronic07 = 1;
            }
            else
            {
                m_source_micronic07 = 0;
            }
        }

        private void txtVolume_TextChanged(object sender, EventArgs e)
        {
            if (txtVolume.Text == "")
            {
                txtVolume.Text = "0";
            }
            else if (int.Parse(txtVolume.Text) > m_maxTargetVol)
            {
                txtVolume.Text = m_maxTargetVol.ToString();
            }
            m_targetVol = int.Parse(txtVolume.Text);
        }

        private void txtConcentration_TextChanged(object sender, EventArgs e)
        {
            if (txtConcentration.Text == "")
            {
                txtConcentration.Text = "0";
            }
            else if (int.Parse(txtConcentration.Text) > m_maxTargetConc)
            {
                txtConcentration.Text = m_maxTargetConc.ToString();
            }
            m_targetConc = int.Parse(txtConcentration.Text);
        }

        private void chkSourcePlate1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSourcePlate1.Checked)
            {
                m_source_pcr_plate1 = 1;
                txtSourcePlate1BoxBarcode.Visible = true;
                txtSourcePlate1BoxBarcode.Focus();
            }
            else
            {
                m_source_pcr_plate1 = 0;
                txtSourcePlate1BoxBarcode.Visible = false;
                txtSourcePlate1BoxBarcode.Text = "";
            }
        }

        private void chkSourcePlate2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSourcePlate2.Checked)
            {
                m_source_pcr_plate2 = 1;
                txtSourcePlate2BoxBarcode.Visible = true;
                txtSourcePlate2BoxBarcode.Focus();
            }
            else
            {
                m_source_pcr_plate2 = 0;
                txtSourcePlate2BoxBarcode.Visible = false;
                txtSourcePlate2BoxBarcode.Text = "";
            }
        }

        private void chkSourcePlate3_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSourcePlate3.Checked)
            {
                m_source_pcr_plate3 = 1;
                txtSourcePlate3BoxBarcode.Visible = true;
                txtSourcePlate3BoxBarcode.Focus();
            }
            else
            {
                m_source_pcr_plate3 = 0;
                txtSourcePlate3BoxBarcode.Visible = false;
                txtSourcePlate3BoxBarcode.Text = "";
            }
        }

        private void chkDropSense_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDropSense.Checked)
            {
                m_drop_sense_measurement = 1;
            }
            else
            {
                m_drop_sense_measurement = 0;
            }
            chkMix.Checked = chkDropSense.Checked;
        }

        private void chkMix_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMix.Checked)
            {
                m_drop_sense_mix = 1;
            }
            else
            {
                m_drop_sense_mix = 0;
            }
        }
    }
}
