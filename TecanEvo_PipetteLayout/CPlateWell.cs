using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecanEvo_PipetteLayout
{
    class CPlateWell
    {
        private string m_sample_barcode = "";
        private string m_well = "";
        private Int32 m_patientId = -1;
        private Int32 m_sampleId = -1;
        private Char m_row = '-';
        private Int32 m_col = -1;
        private Boolean m_used = false;
        private double m_sample_vol = 0.0;
        private double m_buffer_vol = 0.0;

        public void setSampleBarcode(string sample_barcode)
        {
            m_sample_barcode = sample_barcode;
        }

        public void setUsed(Boolean used)
        {
            m_used = used;
        }

        public string getSampleBarcode()
        {
            return m_sample_barcode;
        }

        public Boolean getUsed()
        {
            return m_used;
        }

        public Char getRow()
        {
            return m_row;
        }

        public Int32 getCol()
        {
            return m_col;
        }

        public string getWell()
        {
            return m_well;
        }

        public CPlateWell(Int32 patient_id, string well, Char row, Int32 col)
        {
            m_patientId = patient_id;
            m_well = well;
            m_row = row;
            m_col = col;
        }

        public CPlateWell(Int32 patient_id, Int32 sampleId, string well, Char row, Int32 col)
        {
            m_patientId = patient_id;
            m_sampleId = sampleId;
            m_well = well;
            m_row = row;
            m_col = col;
        }

        public int getPatient()
        {
            return m_patientId;
        }

        public int getSample()
        {
            return m_sampleId;
        }

        public void setSampleVol(double sample_vol)
        {
            m_sample_vol = sample_vol;
        }

        public void setBufferVol(double buffer_vol)
        {
            m_buffer_vol = buffer_vol;
        }

        public double getSampleVol()
        {
            return m_sample_vol;
        }

        public double getBufferVol()
        {
            return m_buffer_vol;
        }
    }
}
