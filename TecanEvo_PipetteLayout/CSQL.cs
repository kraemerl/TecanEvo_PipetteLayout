using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Threading;

namespace TecanEvo_PipetteLayout
{
    class CSQL
    {
        private static string conn_string = "User Id=tecan;Password=tecan;Data Source=ukshikmb-sw049;Initial Catalog=ibdbase;";

        public static void setPatients(Dictionary<string, CSample> tubeBarcodes)
        {
            string lst = "";
            foreach (string key in tubeBarcodes.Keys) 
            {
                lst += ", '" + key + "'";
            }
            lst = "(" + lst.Substring(1) + ")";

            SqlConnection myConnection = new SqlConnection(conn_string);
            myConnection.Open();

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("SELECT bar_code, patient_id, sample_id, sc.categ_name_id " +
                                                " FROM sample s" +
                                                " JOIN sample_category sc ON s.category_id = sc.category_id" +
                                                " WHERE bar_code IN " + lst
                                                , myConnection);

            myReader = myCommand.ExecuteReader();
            if (myReader.HasRows)
                while (myReader.Read())
                {
                    if (tubeBarcodes.ContainsKey(myReader["bar_code"].ToString()))
                    {
                        tubeBarcodes[myReader["bar_code"].ToString()].setPatientAndSample(int.Parse(myReader["patient_id"].ToString()), int.Parse(myReader["sample_id"].ToString()));
                        tubeBarcodes[myReader["bar_code"].ToString()].setCategory(myReader["categ_name_id"].ToString());
                    }
                }
            myReader.Close();
        }

        /// <summary>
        ///  updates List<CDBSample> dbEntries with concentration and volume and returns a list of sample_ids that are in the DB layout but have no entry
        ///  for concentration and/or value
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="dbEntries"></param>
        /// <returns></returns>
        public static void addVolAndConc(Dictionary<string, CSample> tubeBarcodes)
        {
            string lst = "";
            foreach (string key in tubeBarcodes.Keys)
            {
                lst += ", " + tubeBarcodes[key].getSample();
            }
            lst = "(" + lst.Substring(1) + ")";

            SqlConnection myConnection = new SqlConnection(conn_string);
            myConnection.Open();

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("SELECT case when sc.sample_id is null then sv.sample_id else sc.sample_id end as sample_id, " +
                                                " isnull(volume,-1) as volume, " +
                                                " isnull(concentration,-1) as concentration " +
                                                " from ( " +
                                                "	select s.sample_id, s.property_value as volume " +
                                                "	from sample_prop_dec s " +
                                                "	JOIN ( " +
                                                "		select sample_id, max(date_entered) as date_entered " +
                                                "		from sample_prop_dec " +
                                                "        where property_id = 2 " +
                                                "		and property_value != -1 " +
                                                "       and sample_id in " + lst + " " +
                                                "		group by sample_id " +
                                                "	) dd on dd.sample_id = s.sample_id and property_id = 2 and dd.date_entered = s.date_entered " +
                                                ") sv " +
                                                "FULL OUTER JOIN( " +
                                                "	select s.sample_id, s.property_value as concentration " +
                                                "	from sample_prop_dec s " +
                                                "	join ( " +
                                                "		select sample_id, max(date_entered) as date_entered " +
                                                "		from sample_prop_dec " +
                                                "        where property_id = 1 " +
                                                "		and property_value != -1 " +
                                                "       and sample_id in " + lst + " " +
                                                "		group by sample_id " +
                                                "	) dd on dd.sample_id = s.sample_id and property_id = 1 and dd.date_entered = s.date_entered " +
                                                ") sc on sc.sample_id = sv.sample_id"
                                                , myConnection);

            myReader = myCommand.ExecuteReader();
            if (myReader.HasRows)
                while (myReader.Read())
                {
                    foreach (string key in tubeBarcodes.Keys)
                    {
                        if (tubeBarcodes[key].getSample() == int.Parse(myReader["sample_id"].ToString()))
                        {
                            tubeBarcodes[key].setVolumeAndConcentration(Convert.ToDouble(myReader["volume"].ToString()), Convert.ToDouble(myReader["concentration"].ToString()));
                        }
                    }
                }
            myReader.Close();
        }

        public static bool checkPlateExist(string plate_barcode)
        {
            SqlConnection myConnection = new SqlConnection(conn_string);
            myConnection.Open();

            SqlCommand myCommand = new SqlCommand("select count(*) as num" +
                                                    " from plate_tracking_location" +
                                                    " where plate_id = (" +
                                                    "   select plate_id" + 
                                                    "   from plate p" +
                                                    "   join plate_layout pl on p.plate_layout_id = pl.plate_layout_id" +
                                                    "   where barcode = '" + plate_barcode + "'" +
                                                    "   and pl.plate_type_id = 4" +
                                                    " )"
                                                , myConnection);

            if (int.Parse(myCommand.ExecuteScalar().ToString()) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool checkBoxExist(string box_barcode)
        {
            SqlConnection myConnection = new SqlConnection(conn_string);
            myConnection.Open();

            SqlCommand myCommand = new SqlCommand("SELECT count(*) as box_count " +
                                                " FROM sample_box" +
                                                " WHERE sample_box_barcode = '" + box_barcode + "'" +
                                                " AND storage_container_id = (" +
                                                "   SELECT storage_container_id" +
                                                "   FROM storage_container" +
                                                "   WHERE storage_container_name = '96well Plate'" +
                                                ")"
                                                , myConnection);

            if (int.Parse(myCommand.ExecuteScalar().ToString()) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool checkBoxIsEmpty(string box_barcode)
        {
            SqlConnection myConnection = new SqlConnection(conn_string);
            myConnection.Open();

            SqlCommand myCommand = new SqlCommand("SELECT count(*) as sample_count " +
                                                " FROM sample_box_sample" +
                                                " WHERE sample_box_id = (" +
                                                "   SELECT sample_box_id" +
                                                "   FROM sample_box" +
                                                "   WHERE sample_box_barcode = '" + box_barcode + "'" +
                                                ")" +
                                                " AND sample_id > 0 AND ukn_barcode IS NULL;"
                                                , myConnection);

            if (int.Parse(myCommand.ExecuteScalar().ToString()) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void getBoxSamples(string source, string box_barcode, Dictionary<string, CSample> tubeBarcodes)
        {
            SqlConnection myConnection = new SqlConnection(conn_string);
            myConnection.Open();

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("SELECT bar_code, patient_id, s.sample_id, sc.categ_name_id, row, col" +
                                                " FROM sample s" +
                                                " JOIN sample_category sc ON sc.category_id = s.category_id" +
                                                " JOIN sample_box_sample sbs ON s.sample_id = sbs.sample_id" +
                                                " WHERE sbs.sample_box_id = (" +
                                                "   SELECT sample_box_id" +
                                                "   FROM sample_box" +
                                                "   WHERE sample_box_barcode = '" + box_barcode + "'" +
                                                ")"
                                                , myConnection);

            myReader = myCommand.ExecuteReader();
            if (myReader.HasRows)
                while (myReader.Read())
                {
                    tubeBarcodes[myReader["bar_code"].ToString()] = new CSample(source, myReader["bar_code"].ToString(), -1, ScanReport.wellToNumber(char.Parse(myReader["row"].ToString()), int.Parse(myReader["col"].ToString())), ScanReport.getWell(char.Parse(myReader["row"].ToString()), int.Parse(myReader["col"].ToString())));
                    tubeBarcodes[myReader["bar_code"].ToString()].setPatientAndSample(int.Parse(myReader["patient_id"].ToString()), int.Parse(myReader["sample_id"].ToString()));
                    tubeBarcodes[myReader["bar_code"].ToString()].setCategory(myReader["categ_name_id"].ToString());
                }
            myReader.Close();
        }

        public static void loadLayout(string layout, Dictionary<string, CPlateWell> plateWells)
        {
            SqlConnection myConnection = new SqlConnection(conn_string);
            myConnection.Open();

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("SELECT r.row_c as row, plp.col, plp.patient_id" +
                                                " FROM plate_layout_patient plp" +
                                                " JOIN plate_layout pl ON pl.plate_layout_id = plp.plate_layout_id" +
                                                " JOIN plate_row_char2int r ON r.row_i = plp.row" +
                                                " WHERE pl.name = '" + layout + "'" +
                                                " AND patient_id NOT IN (" +
                                                "   SELECT patient_id" +
                                                "   FROM patient_iscontrol" + 
                                                "   WHERE positive = 0" +
                                                " )" +
                                                " ORDER BY plp.col, r.row_c"
                                                , myConnection);

            myReader = myCommand.ExecuteReader();
            string well = "";
            if (myReader.HasRows)
                while (myReader.Read())
                {
                    well = ScanReport.getWell(char.Parse(myReader["row"].ToString()), int.Parse(myReader["col"].ToString()));
                    plateWells[well] = new CPlateWell(int.Parse(myReader["patient_id"].ToString()), well, char.Parse(myReader["row"].ToString()), int.Parse(myReader["col"].ToString()));
                }
            myReader.Close();
        }

        public static void addSamplesToPlate(Int32 plate_id, Int32 volume, Int32 concentration, Dictionary<string, CPlateWell> plateWells)
        {
            SqlConnection myConnection = new SqlConnection(conn_string);
            myConnection.Open();

            SqlTransaction trans;
            // Start a local transaction.
            trans = myConnection.BeginTransaction("SampleTransaction");

            try 
            {
                SqlCommand myCommand = new SqlCommand(" INSERT INTO plate_prop_dec (property_id, plate_id, property_value, date_entered, created_by)" +
                                                        " VALUES (1," + plate_id + "," + volume + ",getdate(),'tecan')", myConnection);
                myCommand.Transaction = trans;
                myCommand.ExecuteNonQuery();
                if (concentration > 0)
                {
                    myCommand.CommandText = " INSERT INTO plate_prop_dec (property_id, plate_id, property_value, date_entered, created_by)" +
                                        " VALUES (2," + plate_id + "," + concentration + ",getdate(),'tecan')";
                    myCommand.ExecuteNonQuery();
                }
                
                foreach (string key in plateWells.Keys)
                {
                    myCommand.CommandText = " INSERT INTO plate_sample (plate_id, row, col, sample_id)" +
                                        " VALUES (" + plate_id + "," + ScanReport.rowToNumber(plateWells[key].getRow()).ToString() + "," + plateWells[key].getCol() + "," + plateWells[key].getSample() + ")";
                    myCommand.ExecuteNonQuery();
                    using (SqlCommand cmd = new SqlCommand("spu_sample_reduce_volume", myConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@sample_id", SqlDbType.Int).Value = plateWells[key].getSample();
                        cmd.Parameters.Add("@used_vol", SqlDbType.Decimal, 10).Value = plateWells[key].getSampleVol();
                        cmd.Transaction = trans;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        Thread.Sleep(50);
                    }
                }
                using (SqlCommand cmd = new SqlCommand("spu_plate_track_location_by_plate_id", myConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@plate_id", SqlDbType.Int).Value = plate_id;
                    cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = 24;
                    cmd.Parameters.Add("@done_by", SqlDbType.VarChar, 25).Value = "tecan";
                    cmd.Parameters.Add("@tracking_location_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                trans.Commit();
                myCommand.Dispose();
            }
            catch (Exception e)
            {
                try
                {
                    trans.Rollback();
                }
                catch (Exception ex2)
                {
                    MessageBox.Show("Rollback Exception Type: " + ex2.GetType() + "\r\n" + ex2.Message);
                }
                MessageBox.Show("Error writing to IBDbase:\r\n" + e.Message);
            }

            trans.Dispose();
            myConnection.Close();
        }

        public static int getPlateID_by_barcode(string plate_barcode)
        {
            int plate_id = -1;
            SqlConnection myConnection = new SqlConnection(conn_string);

            myConnection.Open();

            SqlTransaction trans;
            // Start a local transaction.
            trans = myConnection.BeginTransaction("SampleTransaction");

            try
            {
                SqlCommand myCommand = new SqlCommand("SELECT plate_id" +
                                                    " FROM plate" +
                                                    " WHERE barcode = '" + plate_barcode + "'"
                                                    , myConnection);
                myCommand.Transaction = trans;
                plate_id = int.Parse(myCommand.ExecuteScalar().ToString());

                trans.Commit();
                myCommand.Dispose();
            }
            catch (Exception e)
            {
                try
                {
                    trans.Rollback();
                }
                catch (Exception ex2)
                {
                    MessageBox.Show("Rollback Exception Type: " + ex2.GetType() + "\r\n" + ex2.Message);
                }
                MessageBox.Show("Error writing to IBDbase:\r\n" + e.Message);
            }

            trans.Dispose();
            myConnection.Close();

            return plate_id;
        }
    }
}
