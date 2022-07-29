using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.Entities;

namespace Usermngt.DAL
{
    
    public class DL_LicenseApplication
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<LicenseApplication> Getlicense()
        {
            List<LicenseApplication> license = new List<LicenseApplication>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    //if (party_code == null || party_code == "All")
                    cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.lic_application where record_active='true'", cn);
                    //else
                    //    cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name  from exciseautomation.dailydispatchclosure a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.from_dispatchvat=c.vat_code where a.party_code='" + party_code + "' order by a.party_code,a.closure_date", cn);

                    cmd.CommandType = System.Data.CommandType.Text;

                    //NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.dailydispatchclosure where  user_id='"+userid+"' ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        license = new List<LicenseApplication>();
                        while (dr.Read())
                        {
                            LicenseApplication lic = new LicenseApplication();
                            lic.lic_application_id = Convert.ToInt32(dr["lic_application_id"].ToString());
                            lic.dob = Convert.ToDateTime(dr["dob"].ToString()).ToString("dd-MM-yyyy");
                            lic.lic_fee_code = dr["lic_fee_code"].ToString();
                            lic.financial_year= dr["financial_year"].ToString();
                            lic.mobile = Convert.ToDouble(dr["mobile"].ToString());
                           // lic.lic_subtype_code = dr["lic_subtype_code"].ToString();
                            lic.lic_type_code = dr["lic_type_code"].ToString();
                            lic.aadhaar =Convert.ToInt32( dr["aadhaar"].ToString());
                            lic.address = dr["address"].ToString();
                            lic.applicant_name = dr["applicant_name"].ToString();
                            lic.district_code = dr["district_code"].ToString();
                            lic.lic_status = dr["lic_status"].ToString();
                            lic.state_code = dr["state_code"].ToString();
                            lic.party_code = dr["party_code"].ToString();
                            //   lic.thana_code = dr["thana_code"].ToString();
                            lic.division_code = dr["division_code"].ToString();
                            lic.taluk_town = dr["taluk_town"].ToString();
                            lic.father_unit_name = dr["father_unit_name"].ToString();
                            lic.pin = Convert.ToInt32(dr["pincode"].ToString());
                            lic.gst = dr["gst"].ToString();
                            lic.pan = dr["pan"].ToString();
                            lic.tan = dr["tan"].ToString();
                            lic.tin = dr["tin"].ToString();
                            lic.email = dr["email"].ToString();
                            lic.idproof_image = dr["idproof_image"].ToString();
                            lic.photo_image = dr["photo_image"].ToString();
                            lic.photoname = dr["photoname"].ToString();
                            lic.remarks = dr["remarks"].ToString();
                            lic.record_status = dr["record_status"].ToString();
                            if (dr["lic_start_date"].ToString() != "")
                                lic.start_date = Convert.ToDateTime(dr["lic_start_date"].ToString()).ToString("dd-MM-yyyy");
                            if (dr["lic_end_date"].ToString() != "")
                                lic.end_date = Convert.ToDateTime(dr["lic_end_date"].ToString()).ToString("dd-MM-yyyy");
                            lic.user_id= dr["user_id"].ToString();
                            license.Add(lic);

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return license;
            }
        }
        public static List<LicenseApplication> Getlic( int id)
        {
            List<LicenseApplication> license = new List<LicenseApplication>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                   
                    cmd = new NpgsqlCommand("SELECT a.*,b.lic_type_name,d.lic_fee_amt FROM exciseautomation.lic_application a inner join exciseautomation.lic_type_master  b on a.lic_type_code=b.lic_type_code inner join  exciseautomation.lic_subtype_master c on b.lic_type_code=c.lic_type_code inner join   exciseautomation.lic_fee_master d on a.lic_fee_code=d.lic_fee_code   where a.lic_application_id='"+id+"' ", cn);
                    //else
                    //    cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name  from exciseautomation.dailydispatchclosure a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.from_dispatchvat=c.vat_code where a.party_code='" + party_code + "' order by a.party_code,a.closure_date", cn);

                    cmd.CommandType = System.Data.CommandType.Text;

                    //NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.dailydispatchclosure where  user_id='"+userid+"' ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        license = new List<LicenseApplication>();
                        while (dr.Read())
                        {
                            LicenseApplication lic = new LicenseApplication();
                            lic.lic_application_id = Convert.ToInt32(dr["lic_application_id"].ToString());
                            lic.dob = Convert.ToDateTime(dr["dob"].ToString()).ToString("dd-MM-yyyy");
                            lic.lic_fee_code = dr["lic_fee_code"].ToString();
                            lic.lic_fee_amt= Convert.ToDouble(dr["lic_fee_amt"].ToString());
                            lic.mobile = Convert.ToDouble(dr["mobile"].ToString());
                            // lic.lic_subtype_code = dr["lic_subtype_code"].ToString();
                            lic.lic_type_code = dr["lic_type_code"].ToString();
                            lic.lic_type_name = dr["lic_type_name"].ToString();
                            lic.aadhaar = Convert.ToInt32(dr["aadhaar"].ToString());
                            lic.address = dr["address"].ToString();
                            lic.applicant_name = dr["applicant_name"].ToString();
                            lic.district_code = dr["district_code"].ToString();
                            lic.lic_status = dr["lic_status"].ToString();
                            lic.state_code = dr["state_code"].ToString();
                           // lic.thana_code = dr["thana_code"].ToString();
                            lic.division_code = dr["division_code"].ToString();
                            lic.taluk_town = dr["taluk_town"].ToString();
                            lic.father_unit_name = dr["father_unit_name"].ToString();
                            lic.pin = Convert.ToInt32(dr["pincode"].ToString());
                            lic.gst = dr["gst"].ToString();
                            lic.pan = dr["pan"].ToString();
                            lic.tan = dr["tan"].ToString();
                            lic.tin = dr["tin"].ToString();
                            lic.email = dr["email"].ToString();
                            lic.idproof_image = dr["idproof_image"].ToString();
                            lic.photo_image = dr["photo_image"].ToString();
                            lic.photoname = dr["photoname"].ToString();
                            lic.remarks = dr["remarks"].ToString();
                           if( dr["lic_start_date"].ToString() !="")
                            lic.start_date = Convert.ToDateTime(dr["lic_start_date"].ToString()).ToString("dd-MM-yyyy");
                            if (dr["lic_end_date"].ToString() != "")
                                lic.end_date = Convert.ToDateTime(dr["lic_end_date"].ToString()).ToString("dd-MM-yyyy");
                            lic.record_status = dr["record_status"].ToString();
                            lic.user_id = dr["user_id"].ToString();
                            license.Add(lic);

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return license;
            }
        }

        public static string Insert(LicenseApplication license)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cm = new NpgsqlCommand("select max(lic_application_id) from exciseautomation.lic_application", cn);
                    string m = cm.ExecuteScalar().ToString();
                    int p = 0;
                    if (m == "")
                        p = 1;
                    else
                        p = Convert.ToInt32(m) + 1;
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.lic_application(lic_application_id,lic_type_code, lic_fee_code, applicant_name, father_unit_name, dob, advt_ref, address, state_code, district_code,taluk_town, pincode, mobile, pan, tan, tin, aadhaar, gst, email, photoname,photo_image, idproof_code, idproof_image, remarks, creation_date, user_id, record_status,party_code,division_code,renewed,financial_year)VALUES('"+p+"','" + license.lic_type_code+"','"+license.lic_fee_code+"', '"+license.applicant_name+"', '"+license.father_unit_name+"','"+license.dob+"', '"+license.advt_ref+"', '"+license.address+"','"+license.state_code+"','"+license.district_code+"', '"+license.taluk_town+"','"+license.pin+"', '"+license.mobile+"', '"+license.pan+"', '"+license.tan+"', '"+license.tin+"','"+license.aadhaar+"', '"+license.gst+"', '"+license.email+"', '"+license.photoname+"','"+license.photo_image+"', '"+license.idproof_code+"', '"+license.idproof_image+"', '"+license.remarks+"', '"+DateTime.Now.ToShortDateString()+"', '"+license.user_id+"','"+license.record_status+"','"+license.party_code+"','"+license.division_code+"','"+license.renewed+"','"+license.financial_year+"') ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                  int a= Convert.ToInt32( cmd.ExecuteNonQuery());
                    StringBuilder str = new StringBuilder();
                    if (a == 1)
                    {
                        if(license.renewed=="Y")
                        {
                           if(license.lic_application_id.ToString() !="")
                            {
                                str.Append("update exciseautomation.lic_application set  record_status='E' where lic_application_id ='" + license.lic_application_id + "' and financial_year='"+license.financial_year+"'");
                                NpgsqlCommand cmd1 = new NpgsqlCommand(str.ToString(), cn);
                                int n = cmd1.ExecuteNonQuery();
                            }
                      
                        }
                        NpgsqlCommand cmd3 = new NpgsqlCommand("SELECT case when max(lic_application_id) is null then 0 else max(lic_application_id) end as lic_application_id FROM exciseautomation.lic_application where  financial_year='" + license.financial_year + "'", cn);
                        int b = Convert.ToInt32(cmd3.ExecuteScalar());

                        for (int i = 0; i < license.applied.Count; i++)
                        {
                            
                            NpgsqlCommand cmd5 = new NpgsqlCommand("INSERT INTO exciseautomation.lic_applied_for(lic_application_id, lic_subtype_code, creation_date, user_id)VALUES('" + b + "','" + license.applied[0].lic_subtype_code+"', '"+DateTime.Now.ToShortDateString()+"', '"+license.user_id+"')", cn);
                            cmd5.ExecuteNonQuery();
                           
                        }
                        for (int i1 = 0; i1 < license.doc.Count; i1++)
                        {
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                            str.Append("Values('" + b + "','" + license.doc[i1].doc_name + "', '" + license.doc[i1].description + "','" + license.doc[i1].doc_path + "','LIA','" + license.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+license.financial_year+"','"+license.party_code+"')");
                            NpgsqlCommand cmd4 = new NpgsqlCommand(str.ToString(), cn);
                            int r = cmd4.ExecuteNonQuery();
                        }
                    }

                        val = "0";


                }
                catch (Exception ex)
                {

                    throw (ex);
                }
                return val;
            }
        }

        public static string Update(LicenseApplication license)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.lic_application SET   lic_type_code ='"+license.lic_type_code+"', lic_fee_code ='"+license.lic_fee_code+"', applicant_name ='"+license.applicant_name+"', father_unit_name ='"+license.father_unit_name+"', dob ='"+license.dob+"', advt_ref ='"+license.advt_ref+"', address ='"+license.address+"', state_code ='"+license.state_code+"', district_code ='"+license.district_code+"', taluk_town ='"+license.taluk_town+"', pincode ='"+license.pin+"', mobile ='"+license.mobile+"', pan ='"+license.pan+"', tan ='"+license.tan+"', tin ='"+license.tin+"', aadhaar ='"+license.aadhaar+"', gst ='"+license.gst+"', email ='"+license.email+"', photoname ='"+license.photoname+ "', photo_image ='" + license.photo_image + "',  idproof_code ='" + license.idproof_code+"', idproof_image ='"+license.idproof_image+"', remarks ='"+license.remarks+"', lic_status ='"+license.lic_status+"',  lastmodified_date ='"+DateTime.Now.ToShortDateString()+"', user_id ='"+license.user_id+"', record_status ='"+license.record_status+"',division_code='"+license.division_code+"'   WHERE lic_application_id ='"+license.lic_application_id+ "' and financial_year='" + license.financial_year + "'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    int a = cmd.ExecuteNonQuery();
                    StringBuilder str = new StringBuilder();
                    if (a == 1)
                    {
                       
                        NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.lic_applied_for where  lic_application_id='" + license.lic_application_id + "' ", cn);
                        cmd1.ExecuteNonQuery();
                        for (int i = 0; i < license.applied.Count; i++)
                        {
                            NpgsqlCommand cmd5 = new NpgsqlCommand("INSERT INTO exciseautomation.lic_applied_for(lic_application_id, lic_subtype_code, creation_date, user_id)VALUES('" + license.lic_application_id + "','" + license.applied[0].lic_subtype_code + "', '" + DateTime.Now.ToShortDateString() + "', '" + license.user_id + "')", cn);
                            cmd5.ExecuteNonQuery();
                        }

                        NpgsqlCommand cmd4 = new NpgsqlCommand("delete from exciseautomation.eascm_docs where doc_id='" + license.lic_application_id + "' and doc_type_code='LIA' and financial_year='" + license.financial_year + "' ", cn);
                        cmd4.ExecuteNonQuery();
                        for (int i = 0; i < license.doc.Count; i++)
                        {
                            int n = 0;
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                            str.Append("Values('" + license.lic_application_id + "','" + license.doc[i].doc_name + "', '" + license.doc[i].description + "','" + license.doc[i].doc_path + "','LIA','" + license.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+license.financial_year+"','"+license.party_code+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();
                        }
                    }
                    val = "0";

                }
                catch (Exception ex)
                {

                    throw (ex);
                }
                return val;
            }
        }


        public static List<VAT_Master> GetVat(string userid)
        {
            List<VAT_Master> vat = new List<VAT_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select vat_name,vat_code from exciseautomation.vat_master where user_id='" + userid + "'", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        vat = new List<VAT_Master>();
                        while (dr.Read())
                        {
                            VAT_Master vats = new VAT_Master();
                            vats.vat_name = dr["vat_name"].ToString();
                            vats.vat_code = dr["vat_code"].ToString();
                            vats.user_id = userid;
                            vat.Add(vats);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return vat;
            }
        }

        public static List<VAT_Master> GetVatAvilQty(string vatcode)
        {
            List<VAT_Master> vat = new List<VAT_Master>();
            VAT_Master vats = new VAT_Master();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where  vat_code ='" + vatcode + "' ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        vat = new List<VAT_Master>();
                        while (dr.Read())
                        {
                            vats.vat_availablecapacity = Convert.ToDouble(dr["vat_availablecapacity"].ToString());
                            vat.Add(vats);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return vat;
            }
        }
        public static List<District> GetDistrictList(string division_code)
        {
            List<District> districts = new List<District>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    if (division_code.Length == 2)
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select c.*,a.division_name,t.state_name from exciseautomation.division_master a inner join  exciseautomation.State_master t on a.state_code=t.state_code inner join exciseautomation.district_master c on  a.division_code = c.division_code and c.state_code='" + division_code + "' order by district_master_id", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                districts = new List<District>();
                                while (dr.Read())
                                {
                                    District district = new District();
                                    district.district_Code = dr["District_Code"].ToString();
                                    district.district_Name = dr["District_Name"].ToString();
                                    district.state_name = dr["State_Name"].ToString();
                                    district.state_Code = dr["state_Code"].ToString();
                                    district.division_Code = dr["division_Code"].ToString();
                                    district.division_name = dr["division_name"].ToString();
                                    district.id = dr["district_master_id"].ToString();
                                    districts.Add(district);
                                }
                            }
                        }
                    }
                    else
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select c.*,a.division_name,t.state_name from exciseautomation.division_master a inner join  exciseautomation.State_master t on a.state_code=t.state_code inner join exciseautomation.district_master c on  a.division_code = c.division_code order by district_master_id", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                districts = new List<District>();
                                while (dr.Read())
                                {
                                    District district = new District();
                                    district.district_Code = dr["District_Code"].ToString();
                                    district.district_Name = dr["District_Name"].ToString();
                                    district.state_name = dr["State_Name"].ToString();
                                    district.state_Code = dr["state_Code"].ToString();
                                    district.division_Code = dr["division_Code"].ToString();
                                    district.division_name = dr["division_name"].ToString();
                                    district.id = dr["district_master_id"].ToString();
                                    districts.Add(district);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }


            }
            return districts;
        }
        public static List<aplliedfor> getdetail(int lic)
        {
            List<aplliedfor> applied = new List<aplliedfor>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    NpgsqlCommand cmd1 = new NpgsqlCommand("select a.*,b.lic_subtype_name from exciseautomation.lic_applied_for a inner join exciseautomation.lic_subtype_master b on a.lic_subtype_code=b.lic_subtype_code where a.lic_application_id='" + lic + "'", cn);
            
                cmd1.CommandType = System.Data.CommandType.Text;
                    //cmd.Parameters.AddWithValue("@UserID", userid);
                    NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr2);
                    dr2.Close();
                    applied = new List<aplliedfor>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        aplliedfor Setup = new aplliedfor();
                        Setup.lic_applied_for_id = Convert.ToInt32(dr["lic_applied_for_id"].ToString());
                        Setup.lic_application_id = Convert.ToInt32(dr["lic_application_id"].ToString());
                        Setup.lic_subtype_code = dr["lic_subtype_code"].ToString();
                        Setup.lic_subtype_name = dr["lic_subtype_name"].ToString();
                        applied.Add(Setup);


                    }
                }

                catch (Exception ex)
                {
                    throw (ex);
                }
                return applied;
            }
        }
        public static LicenseApplication GetDetails( int lic_application_id,string financial_year)
        {

            LicenseApplication lic= new LicenseApplication();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT a.*,b.idproof_name FROM exciseautomation.lic_application a inner join exciseautomation.idproof_master b on a.idproof_code=b.idproof_code  where a.lic_application_id='" + lic_application_id+"' and a.financial_year='"+financial_year+"'", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                        lic.lic_application_id = Convert.ToInt32(dr["lic_application_id"].ToString());
                        lic.dob = Convert.ToDateTime(dr["dob"].ToString()).ToString("dd-MM-yyyy");
                       lic.lic_fee_code = dr["lic_fee_code"].ToString();
                       lic.mobile= Convert.ToDouble(dr["mobile"].ToString());
                       // lic.lic_subtype_code = dr["lic_subtype_code"].ToString();
                        lic.lic_type_code = dr["lic_type_code"].ToString();
                        lic.aadhaar = Convert.ToInt32(dr["aadhaar"].ToString());
                        lic.address = dr["address"].ToString();
                        lic.applicant_name= dr["applicant_name"].ToString();
                        lic.district_code = dr["district_code"].ToString();
                        lic.division_code = dr["division_code"].ToString();
                        lic.lic_status = dr["lic_status"].ToString();
                        lic.state_code = dr["state_code"].ToString();
                      //  lic.thana_code= dr["thana_code"].ToString();
                        lic.taluk_town= dr["taluk_town"].ToString();
                        lic.father_unit_name = dr["father_unit_name"].ToString();
                        lic.pin = Convert.ToInt32(dr["pincode"].ToString());
                        lic.gst = dr["gst"].ToString();
                        lic.pan = dr["pan"].ToString();
                        lic.tan = dr["tan"].ToString();
                        lic.tin = dr["tin"].ToString();
                        lic.email= dr["email"].ToString();
                        lic.idproof_code = dr["idproof_code"].ToString();
                        lic.idproof_name = dr["idproof_name"].ToString();
                        lic.idproof_image= dr["idproof_image"].ToString();
                        lic.photo_image = dr["photo_image"].ToString();
                        lic.photoname= dr["photoname"].ToString();
                        if(dr["lic_start_date"].ToString()!="")
                        lic.start_date= Convert.ToDateTime(dr["lic_start_date"].ToString()).ToString("dd-MM-yyyy");
                        if (dr["lic_end_date"].ToString() != "")
                            lic.end_date= Convert.ToDateTime(dr["lic_end_date"].ToString()).ToString("dd-MM-yyyy");
                        lic.remarks = dr["remarks"].ToString();
                        lic.record_status = dr["record_status"].ToString();
                        lic.renewed= dr["renewed"].ToString();
                        lic.user_id = dr["user_id"].ToString();
                        using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.lic_applied_for where  lic_application_id='" + lic.lic_application_id + "' ", cn))
                        {
                            cmd1.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                            lic.applied = new List<aplliedfor>();
                            if (dr2.HasRows)
                            {
                                while (dr2.Read())
                                {
                                    aplliedfor Setup = new aplliedfor();
                                    Setup.lic_applied_for_id = Convert.ToInt32(dr2["lic_applied_for_id"].ToString());
                                    Setup.lic_application_id = Convert.ToInt32(dr2["lic_application_id"].ToString());
                                    Setup.lic_subtype_code = dr2["lic_subtype_code"].ToString();
                                    lic.applied.Add(Setup);
                                }
                            }
                            dr2.Close();
                        }
                        using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.eascm_docs where doc_id='" + lic.lic_application_id + "' and doc_type_code='LIA'  and financial_year='" + financial_year + "' order by eascm_docs_id", cn))
                        {
                            cmd1.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr3 = cmd1.ExecuteReader();
                            lic.doc = new List<EASCM_DOCS>();
                            if (dr3.HasRows)
                            {

                                while (dr3.Read())
                                {
                                    EASCM_DOCS doc = new EASCM_DOCS();
                                    doc.id = Convert.ToInt32(dr3["eascm_docs_id"].ToString());
                                    doc.doc_id = dr3["doc_id"].ToString();
                                    doc.doc_name = dr3["doc_Name"].ToString();
                                    doc.description = dr3["doc_desc"].ToString();
                                    doc.doc_path = dr3["doc_path"].ToString();
                                    lic.doc.Add(doc);
                                }

                            }

                        }
                        // dispatch.party_name = dr["party_name"].ToString();

                    }


                }

                catch (Exception ex)
                {
                    throw (ex);
                }
                return lic;
            }
        }


        public static string Approve(LicenseApplication DDC)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    StringBuilder str = new StringBuilder();
                    StringBuilder str1 = new StringBuilder();
                    if (DDC.record_status == "R" || DDC.record_status == "A")
                    {
                        str1.Append("update exciseautomation.lic_application set  record_status='" + DDC.record_status + "' where lic_application_id ='" + DDC.lic_application_id + "' and financial_year='" + DDC.financial_year + "'");
                        NpgsqlCommand cmd5 = new NpgsqlCommand("delete from exciseautomation.eascm_docs where doc_id='" + DDC.lic_application_id + "' and doc_type_code='LIA' and financial_year='" + DDC.financial_year + "'", cn);
                        cmd5.ExecuteNonQuery();
                        for (int i1 = 0; i1 < DDC.doc.Count; i1++)
                        {
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                            str.Append("Values('" + DDC.lic_application_id + "','" + DDC.doc[i1].doc_name + "', '" + DDC.doc[i1].description + "','" + DDC.doc[i1].doc_path + "','LIA','" + DDC.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+DDC.financial_year+"','"+DDC.party_code+"')");
                            NpgsqlCommand cmd4 = new NpgsqlCommand(str.ToString(), cn);
                            int r = cmd4.ExecuteNonQuery();
                        }
                        
                    }
                    else
                    {
                        str1.Append("update exciseautomation.lic_application set  record_status='" + DDC.record_status + "',lic_start_date='"+DDC.start_date+"',lic_end_date='"+DDC.end_date+"',lic_status='"+DDC.lic_status+"' where lic_application_id ='" + DDC.lic_application_id + "' and financial_year='" + DDC.financial_year + "'");

                    }
                    NpgsqlCommand cmd = new NpgsqlCommand(str1.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();

                    if (DDC.record_status == "R")
                    {
                       
                        DDC.record_status = "Rejected By "+ DDC.user_id;
                    }
                    else if (DDC.record_status=="B")
                    {
                        DDC.record_status = "Refer Back By "+ DDC.user_id;
                    }
                    else if (DDC.record_status == "I")
                    {
                        DDC.record_status = "Issued By " + DDC.user_id;
                    }
                    else
                    {
                        DDC.record_status = "Approved By " + DDC.user_id;
                    }
                    str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + DDC.lic_application_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy") + "','LIC','" + DDC.record_status + "','" + DDC.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DDC.user + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DDC.user + "','"+DDC.financial_year+"','"+DDC.party_code+"')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                    n = cmd.ExecuteNonQuery();
                    VAL = "0";
                    trn.Commit();
                    cn.Close();

                }
                catch (Exception ex1)
                {
                    trn.Rollback();


                }

            }
            return VAL;
        }

        public static LicenseFee GetAval(string code)
        {
            LicenseFee vat = new LicenseFee();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.lic_fee_amt,a.lic_renewal_fee from exciseautomation.lic_fee_master a where lic_fee_code='" + code + "'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            vat.lic_fee_amt = Convert.ToDouble(dr["lic_fee_amt"].ToString());
                           if( dr["lic_renewal_fee"].ToString()!="")
                            vat.lic_renewal_fee = Convert.ToDouble(dr["lic_renewal_fee"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return vat;
            }

        }
        public static int GetExistsData(string year, string party, string party_code)
        {
            int value1 = 0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                        cmd = new NpgsqlCommand("select count(1) from exciseautomation.lic_application a inner join exciseautomation.party_master b  on a.party_code=b.party_code   where b.financial_year='"+year+ "' and a.lic_type_code='" + party+ "'and b.party_code='"+party_code+ "' and a.record_active='true' ", cn);
                    int re = Convert.ToInt32( cmd.ExecuteScalar());
                    if (re > 0 )
                    {
                        value1 = 1;
                        _log.Info("Get Existing data Success : lic_application");
                    }
                    else
                    {
                        
                            value1 = 0;
                        _log.Info("Get Existing data Fail :lic_application");
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get Existing data Fail :lic_application " + ex.Message);
                    value1 = 0;
                }
            }
            return value1;
        }
    }
}
