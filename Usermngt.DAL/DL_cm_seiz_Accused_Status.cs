using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.Entities;

namespace Usermngt.DAL
{
    public class DL_cm_seiz_Accused_Status
    {
        public static List<cm_seiz_Accused_Status> GetList()
        {
            List<cm_seiz_Accused_Status> lstObj = new List<cm_seiz_Accused_Status>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.accusedstatus_master order by accusedstatus_name ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_Accused_Status>();
                            while (dr.Read())
                            {
                                cm_seiz_Accused_Status record = new cm_seiz_Accused_Status();
                                record.accusedstatus_master_id = dr["accusedstatus_master_id"].ToString();
                                record.accusedstatus_code = dr["accusedstatus_code"].ToString();
                                record.accusedstatus_name = dr["accusedstatus_name"].ToString();
                                record.accusedtype = dr["accusedtype"].ToString();
                               
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
    }
}
