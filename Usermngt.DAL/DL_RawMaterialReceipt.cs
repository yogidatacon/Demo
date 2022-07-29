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
    public class DL_RawMaterialReceipt
    {
        public static List<RawMaterialReceipt> GetRawMaterial(string userid)
        {
            List<RawMaterialReceipt> rawmaterial = new List<RawMaterialReceipt>();

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.pass,d.pass_issueno,d.vehicle_no,d.financial_year as passfinancial_year from exciseautomation.rawmaterial_receipt a inner join exciseautomation.party_master b on a.supplier_party_code=b.party_code left join exciseautomation.document_format_master c on c.party_code=a.supplier_party_code inner join exciseautomation.pass d on a.passno=d.pass_id and a.financial_year=b.financial_year where a.attribute1 is null  order by rmr_entrydate desc", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        rawmaterial = new List<RawMaterialReceipt>();
                        while (dr.Read())
                        {
                            RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
                            rawmaterials.rawmaterial_receipt_id = dr["rawmaterial_receipt_id"].ToString();
                            rawmaterials.party_code = dr["party_code"].ToString();
                            rawmaterials.party_name = dr["party_name"].ToString();
                            rawmaterials.financial_year = dr["passfinancial_year"].ToString();
                            if (dr["rmr_entrydate"].ToString()!="")
                            rawmaterials.rmr_entrydate =dr["rmr_entrydate"].ToString().Substring(0,10).Replace("/","-");
                            rawmaterials.passissuedate =dr["passissuedate"].ToString();
                            rawmaterials.passno = dr["pass"] + dr["passfinancial_year"].ToString() + "/" + dr["pass_issueno"].ToString();
                            rawmaterials.vehicleno = dr["vehicle_no"].ToString();
                            if (dr["passqty"].ToString() == "0")
                                rawmaterials.passqty = 0;
                            else
                                rawmaterials.passqty = Convert.ToDouble(dr["passqty"].ToString());
                            rawmaterials.vehicleno = dr["vehicleno"].ToString();
                            if (dr["grossweight"].ToString() == "")
                                rawmaterials.grossweight = 0;
                            else
                            rawmaterials.grossweight = Convert.ToDouble(dr["grossweight"].ToString());
                            if (dr["tankerweight"].ToString() == "")
                                rawmaterials.tankerweight = 0;
                            else
                            rawmaterials.tankerweight = Convert.ToDouble(dr["tankerweight"].ToString());
                            if (dr["supplierweight"].ToString() == "")
                                rawmaterials.supplierweight = 0;
                            else
                            rawmaterials.supplierweight = Convert.ToDouble(dr["supplierweight"].ToString());
                            if (dr["transitweight"].ToString() == "")
                                rawmaterials.transitweight = 0;
                            else
                            rawmaterials.transitweight = Convert.ToDouble(dr["transitweight"].ToString());
                            if (dr["netweight"].ToString() == "")
                                rawmaterials.netweight = 0;
                            else
                            rawmaterials.netweight = Convert.ToDouble(dr["netweight"].ToString());

                            rawmaterials.record_status = dr["record_status"].ToString();
                            rawmaterials.user_id = userid;
                            rawmaterials.remarks = dr["remarks"].ToString();
                            rawmaterial.Add(rawmaterials);
                        }
                    } 
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return rawmaterial;
            }
        }

        public static List<RawMaterialReceipt> Search(string tablename, string column, string value)
        {
            List<RawMaterialReceipt> mir = new List<RawMaterialReceipt>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.pass,d.pass_issueno,d.vehicle_no, d.financial_year as passfinancial_year from exciseautomation.rawmaterial_receipt a inner join exciseautomation.party_master b on a.supplier_party_code=b.party_code left join exciseautomation.document_format_master c on c.party_code=a.supplier_party_code inner join exciseautomation.pass d on a.passno=d.pass_id where a.attribute1 is null and " + column + " Ilike '%" + value + "%' and a.record_active='true'  order by   rmr_entrydate desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<RawMaterialReceipt>();
                            while (dr.Read())
                            {
                                RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
                                rawmaterials.rawmaterial_receipt_id = dr["rawmaterial_receipt_id"].ToString();
                                rawmaterials.party_code = dr["party_code"].ToString();
                                rawmaterials.party_name = dr["party_name"].ToString();
                                rawmaterials.financial_year = dr["passfinancial_year"].ToString();
                                if (dr["rmr_entrydate"].ToString() != "")
                                    rawmaterials.rmr_entrydate = dr["rmr_entrydate"].ToString().Substring(0, 10).Replace("/", "-");
                                rawmaterials.passissuedate = dr["passissuedate"].ToString();
                                rawmaterials.passno = dr["pass"] + dr["passfinancial_year"].ToString() + "/" + dr["pass_issueno"].ToString();
                                rawmaterials.vehicleno = dr["vehicle_no"].ToString();
                                if (dr["passqty"].ToString() == "0")
                                    rawmaterials.passqty = 0;
                                else
                                    rawmaterials.passqty = Convert.ToDouble(dr["passqty"].ToString());
                                rawmaterials.vehicleno = dr["vehicleno"].ToString();
                                if (dr["grossweight"].ToString() == "")
                                    rawmaterials.grossweight = 0;
                                else
                                    rawmaterials.grossweight = Convert.ToDouble(dr["grossweight"].ToString());
                                if (dr["tankerweight"].ToString() == "")
                                    rawmaterials.tankerweight = 0;
                                else
                                    rawmaterials.tankerweight = Convert.ToDouble(dr["tankerweight"].ToString());
                                if (dr["supplierweight"].ToString() == "")
                                    rawmaterials.supplierweight = 0;
                                else
                                    rawmaterials.supplierweight = Convert.ToDouble(dr["supplierweight"].ToString());
                                if (dr["transitweight"].ToString() == "")
                                    rawmaterials.transitweight = 0;
                                else
                                    rawmaterials.transitweight = Convert.ToDouble(dr["transitweight"].ToString());
                                if (dr["netweight"].ToString() == "")
                                    rawmaterials.netweight = 0;
                                else
                                    rawmaterials.netweight = Convert.ToDouble(dr["netweight"].ToString());

                                rawmaterials.record_status = dr["record_status"].ToString();
                                rawmaterials.user_id = dr["user_id"].ToString(); ;
                                rawmaterials.remarks = dr["remarks"].ToString();
                                mir.Add(rawmaterials);

                            }
                        }
                    }
                    cn.Close();
                    
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            return mir;
        }




        public static List<RawMaterialReceipt> GetMTPRaw(string userid)
        {
            List<RawMaterialReceipt> rawmaterial = new List<RawMaterialReceipt>();

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.pass,d.pass_issueno,d.vehicle_no,d.financial_year as passfinancial_year from exciseautomation.rawmaterial_receipt a inner join exciseautomation.party_master b on a.party_code=b.party_code left join exciseautomation.document_format_master c on c.party_code=a.party_code inner join exciseautomation.pass d on a.passno=d.pass_id where a.attribute1 is null  and a.record_active='true' order by rmr_entrydate desc", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        rawmaterial = new List<RawMaterialReceipt>();
                        while (dr.Read())
                        {
                            RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
                            rawmaterials.rawmaterial_receipt_id = dr["rawmaterial_receipt_id"].ToString();
                            rawmaterials.party_code = dr["party_code"].ToString();
                            rawmaterials.party_name = dr["party_name"].ToString();
                            rawmaterials.financial_year = dr["passfinancial_year"].ToString();
                            if (dr["rmr_entrydate"].ToString() != "")
                                rawmaterials.rmr_entrydate = dr["rmr_entrydate"].ToString().Substring(0, 10).Replace("/", "-");
                            rawmaterials.passissuedate = dr["passissuedate"].ToString();
                            rawmaterials.passno = dr["pass"] + dr["passfinancial_year"].ToString() + "/" + dr["pass_issueno"].ToString();
                            rawmaterials.vehicleno = dr["vehicle_no"].ToString();
                            if (dr["passqty"].ToString() == "0")
                                rawmaterials.passqty = 0;
                            else
                                rawmaterials.passqty = Convert.ToDouble(dr["passqty"].ToString());
                            rawmaterials.vehicleno = dr["vehicleno"].ToString();
                            if (dr["grossweight"].ToString() == "")
                                rawmaterials.grossweight = 0;
                            else
                                rawmaterials.grossweight = Convert.ToDouble(dr["grossweight"].ToString());
                            if (dr["tankerweight"].ToString() == "")
                                rawmaterials.tankerweight = 0;
                            else
                                rawmaterials.tankerweight = Convert.ToDouble(dr["tankerweight"].ToString());
                            if (dr["supplierweight"].ToString() == "")
                                rawmaterials.supplierweight = 0;
                            else
                                rawmaterials.supplierweight = Convert.ToDouble(dr["supplierweight"].ToString());
                            if (dr["transitweight"].ToString() == "")
                                rawmaterials.transitweight = 0;
                            else
                                rawmaterials.transitweight = Convert.ToDouble(dr["transitweight"].ToString());
                            if (dr["netweight"].ToString() == "")
                                rawmaterials.netweight = 0;
                            else
                                rawmaterials.netweight = Convert.ToDouble(dr["netweight"].ToString());

                            rawmaterials.record_status = dr["record_status"].ToString();
                            rawmaterials.user_id = userid;
                            rawmaterials.remarks = dr["remarks"].ToString();
                            rawmaterial.Add(rawmaterials);
                        }
                    }
                    //NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.pass from exciseautomation.pass a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.document_format_master c on c.party_code=a.party_code inner join exciseautomation.permit f on f.purchase_from_party=a.user_id where a.record_status='I' and b.party_type_code='ENA'   order by pass_date desc", cn);
                    //NpgsqlDataReader dr = cmd.ExecuteReader();
                    //if (dr.HasRows)
                    //{
                    //    rawmaterial = new List<RawMaterialReceipt>();
                    //    while (dr.Read())
                    //    {
                    //        RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
                    //        rawmaterials.rawmaterial_receipt_id = dr["pass_id"].ToString();
                    //        rawmaterials.party_code = dr["party_code"].ToString();
                    //        rawmaterials.party_name = dr["party_name"].ToString();
                    //      //  if (dr["rmr_entrydate"].ToString() != "")
                    //         //   rawmaterials.rmr_entrydate = dr["rmr_entrydate"].ToString().Substring(0, 10).Replace("/", "-");
                    //        rawmaterials.passissuedate = dr["pass_issuedate"].ToString();
                    //        rawmaterials.passno = dr["pass"] + dr["pass_issueno"].ToString();
                    //        rawmaterials.vehicleno = dr["vehicle_no"].ToString();
                    //        //if (dr["passqty"].ToString() == "0")
                    //        //    rawmaterials.passqty = 0;
                    //        //else
                    //        //    rawmaterials.passqty = Convert.ToDouble(dr["passqty"].ToString());
                    //        //rawmaterials.vehicleno = dr["vehicleno"].ToString();
                    //        //if (dr["grossweight"].ToString() == "")
                    //        //    rawmaterials.grossweight = 0;
                    //        //else
                    //        //    rawmaterials.grossweight = Convert.ToDouble(dr["grossweight"].ToString());
                    //        //if (dr["tankerweight"].ToString() == "")
                    //        //    rawmaterials.tankerweight = 0;
                    //        //else
                    //        //    rawmaterials.tankerweight = Convert.ToDouble(dr["tankerweight"].ToString());
                    //        //if (dr["supplierweight"].ToString() == "")
                    //        //    rawmaterials.supplierweight = 0;
                    //        //else
                    //        //    rawmaterials.supplierweight = Convert.ToDouble(dr["supplierweight"].ToString());
                    //        //if (dr["transitweight"].ToString() == "")
                    //        //    rawmaterials.transitweight = 0;
                    //        //else
                    //        //    rawmaterials.transitweight = Convert.ToDouble(dr["transitweight"].ToString());
                    //        //if (dr["netweight"].ToString() == "")
                    //        //    rawmaterials.netweight = 0;
                    //        //else
                    //        //    rawmaterials.netweight = Convert.ToDouble(dr["netweight"].ToString());

                    //       rawmaterials.record_status = dr["record_status"].ToString();
                    //        rawmaterials.user_id = userid;
                    //        rawmaterials.remarks = dr["remarks"].ToString();
                    //        rawmaterial.Add(rawmaterials);
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return rawmaterial;
            }
        }

        public static List<RawMaterialReceipt> Search1(string tablename, string column, string value)
        {
            List<RawMaterialReceipt> mir = new List<RawMaterialReceipt>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.pass,d.pass_issueno,d.vehicle_no from exciseautomation.rawmaterial_receipt a inner join exciseautomation.party_master b on a.party_code=b.party_code left join exciseautomation.document_format_master c on c.party_code=a.party_code inner join exciseautomation.pass d on a.passno=d.pass_id where a.attribute1 is null and " + column + " Ilike '%" + value + "%' and and a.record_active='true'  order by   rmr_entrydate desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<RawMaterialReceipt>();
                            while (dr.Read())
                            {
                                RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
                                rawmaterials.rawmaterial_receipt_id = dr["rawmaterial_receipt_id"].ToString();
                                rawmaterials.party_code = dr["party_code"].ToString();
                                rawmaterials.party_name = dr["party_name"].ToString();
                                rawmaterials.financial_year = dr["financial_year"].ToString();
                                if (dr["rmr_entrydate"].ToString() != "")
                                    rawmaterials.rmr_entrydate = dr["rmr_entrydate"].ToString().Substring(0, 10).Replace("/", "-");
                                rawmaterials.passissuedate = dr["passissuedate"].ToString();
                                rawmaterials.passno = dr["pass"] + dr["financial_year"].ToString() + "/" + dr["pass_issueno"].ToString();
                                rawmaterials.vehicleno = dr["vehicle_no"].ToString();
                                if (dr["passqty"].ToString() == "0")
                                    rawmaterials.passqty = 0;
                                else
                                    rawmaterials.passqty = Convert.ToDouble(dr["passqty"].ToString());
                                rawmaterials.vehicleno = dr["vehicleno"].ToString();
                                if (dr["grossweight"].ToString() == "")
                                    rawmaterials.grossweight = 0;
                                else
                                    rawmaterials.grossweight = Convert.ToDouble(dr["grossweight"].ToString());
                                if (dr["tankerweight"].ToString() == "")
                                    rawmaterials.tankerweight = 0;
                                else
                                    rawmaterials.tankerweight = Convert.ToDouble(dr["tankerweight"].ToString());
                                if (dr["supplierweight"].ToString() == "")
                                    rawmaterials.supplierweight = 0;
                                else
                                    rawmaterials.supplierweight = Convert.ToDouble(dr["supplierweight"].ToString());
                                if (dr["transitweight"].ToString() == "")
                                    rawmaterials.transitweight = 0;
                                else
                                    rawmaterials.transitweight = Convert.ToDouble(dr["transitweight"].ToString());
                                if (dr["netweight"].ToString() == "")
                                    rawmaterials.netweight = 0;
                                else
                                    rawmaterials.netweight = Convert.ToDouble(dr["netweight"].ToString());

                                rawmaterials.record_status = dr["record_status"].ToString();
                                rawmaterials.user_id = dr["user_id"].ToString();
                                rawmaterials.remarks = dr["remarks"].ToString();
                                mir.Add(rawmaterials);


                            }
                        }
                    }
                    cn.Close();

                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            return mir;
        }




        public static object InsertRawGrain(List<RawMaterialReceipt> rawmatrial)
        {
            string val = "";

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cm = new NpgsqlCommand("select case when max(rawmaterial_receipt_id) is null then 0 else max(rawmaterial_receipt_id) end from exciseautomation.rawmaterial_receipt where   financial_year='" + rawmatrial[0].financial_year + "'", cn);
                    int m = Convert.ToInt32(cm.ExecuteScalar());
                    m +=1;
                    StringBuilder str1 = new StringBuilder();
                    if(rawmatrial[0].supplier !="" && rawmatrial[0].supplier !="Select")
                    {
                        str1.Append("INSERT INTO exciseautomation.rawmaterial_receipt(rawmaterial_receipt_id, rmr_entrydate, party_code, vehicleno, passno,passissuedate,  passqty, grossweight, netweight, remarks, user_id, creation_date, record_status, supplier_party_code,attribute1,attribute2,attribute3,attribute4,financial_year)values(");
                        str1.Append("'" + m + "', '" + rawmatrial[0].rmr_entrydate + "','" + rawmatrial[0].party_code + "','" + rawmatrial[0].vehicleno + "','" + rawmatrial[0].passno + "','" + rawmatrial[0].passissuedate + "','" + rawmatrial[0].passqty + "','" + rawmatrial[0].grossweight + "','" + rawmatrial[0].netweight + "','" + rawmatrial[0].remarks + "','" + rawmatrial[0].user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + rawmatrial[0].record_status + "','" + rawmatrial[0].supplier + "','" + rawmatrial[0].rawmaterial + "','" + rawmatrial[0].suppliertype + "','" + rawmatrial[0].suppliername + "','"+rawmatrial[0].uom+"','"+rawmatrial[0].financial_year+"')");
                    }
                    else
                    {
                        str1.Append("INSERT INTO exciseautomation.rawmaterial_receipt(rawmaterial_receipt_id, rmr_entrydate, party_code, vehicleno, passno,passissuedate,  passqty, grossweight, netweight, remarks, user_id, creation_date, record_status,attribute1,attribute2,attribute3,attribute4,financial_year)values(");
                        str1.Append("'" + m + "', '" + rawmatrial[0].rmr_entrydate + "','" + rawmatrial[0].party_code + "','" + rawmatrial[0].vehicleno + "','" + rawmatrial[0].passno + "','" + rawmatrial[0].passissuedate + "','" + rawmatrial[0].passqty + "','" + rawmatrial[0].grossweight + "','" + rawmatrial[0].netweight + "','" + rawmatrial[0].remarks + "','" + rawmatrial[0].user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + rawmatrial[0].record_status + "','" + rawmatrial[0].rawmaterial + "','" + rawmatrial[0].suppliertype + "','" + rawmatrial[0].suppliername + "','" + rawmatrial[0].uom + "','"+rawmatrial[0].financial_year+"')");
                    }

                    NpgsqlCommand cmd33 = new NpgsqlCommand(str1.ToString(), cn);
                    cmd33.ExecuteNonQuery();
                    RawMaterialReceipt rmr = new RawMaterialReceipt();
                    for (int i1 = 0; i1 < rawmatrial.Count; i1++)
                    {

                        StringBuilder str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.rmreceipt_storage(rawmaterial_receipt_id, vat_code, storedqty,opening_dips,user_id, creation_date,financial_year)");
                        str.Append("Values('" + m + "', '" + rawmatrial[i1].vat_code + "','" +rawmatrial[i1].storedqty + "','" + rawmatrial[i1].opening_dips + "','" + rawmatrial[0].user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+rawmatrial[0].financial_year+"')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        int r = cmd3.ExecuteNonQuery();
                        if (rawmatrial[0].record_status == "A")
                        {
                            NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + rawmatrial[i1].vat_code + "' and party_code='" + rawmatrial[0].party_code + "'", cn);
                            double k = Convert.ToDouble(cmd6.ExecuteScalar());
                            double v = Convert.ToDouble(k) + Convert.ToDouble(rawmatrial[i1].storedqty);
                            NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + rawmatrial[i1].vat_code + "' and party_code='" + rawmatrial[0].party_code + "'", cn);
                            cmd7.ExecuteNonQuery();
                        }

                    }
                   
                        val = "0";
                    trn.Commit();
                }
                catch (Exception ex)
                {
                    val = ex.Message;
                    trn.Commit();
                }
                return val;
            }
        }

        public static object AdminUpdateGrain(List<RawMaterialReceipt> rawmaterial)
        {
            string val = "";

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd;
                    StringBuilder str = new StringBuilder();
                    if (rawmaterial[0].suppliername != "" && rawmaterial[0].suppliername != null)
                    {
                   cmd = new NpgsqlCommand("update exciseautomation.rawmaterial_receipt set rmr_entrydate='" + rawmaterial[0].rmr_entrydate + "', grossweight='" + rawmaterial[0].grossweight + "' ,vehicleno='" + rawmaterial[0].vehicleno + "', passno='" + rawmaterial[0].passno + "',passqty='" + rawmaterial[0].passqty + "',passissuedate='" + rawmaterial[0].passissuedate + "', netweight='" + rawmaterial[0].netweight + "',  lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "',supplier_party_code='" + rawmaterial[0].supplier + "',attribute1='" + rawmaterial[0].rawmaterial + "',attribute2='" + rawmaterial[0].suppliertype + "',attribute3='" + rawmaterial[0].suppliername + "' ,attribute4='" + rawmaterial[0].uom + "' where rawmaterial_receipt_id='" + rawmaterial[0].rawmaterial_receipt_id + "' and financial_year='" + rawmaterial[0].financial_year + "' ", cn);
                        cmd.ExecuteNonQuery();
                      
                    }
                    else
                    {
                       
                    cmd = new NpgsqlCommand("update exciseautomation.rawmaterial_receipt set rmr_entrydate='" + rawmaterial[0].rmr_entrydate + "', grossweight='" + rawmaterial[0].grossweight + "' ,vehicleno='" + rawmaterial[0].vehicleno + "', passno='" + rawmaterial[0].passno + "',passqty='" + rawmaterial[0].passqty + "',passissuedate='" + rawmaterial[0].passissuedate + "', netweight='" + rawmaterial[0].netweight + "',  lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "',supplier_party_code='" + rawmaterial[0].supplier + "',attribute1='" + rawmaterial[0].rawmaterial + "',attribute2='" + rawmaterial[0].suppliertype + "',attribute3='" + rawmaterial[0].suppliername + "',attribute4='" + rawmaterial[0].uom + "' where rawmaterial_receipt_id='" + rawmaterial[0].rawmaterial_receipt_id + "' and financial_year='" +rawmaterial[0].financial_year + "' ", cn);
                        cmd.ExecuteNonQuery();
                    }
                    for (int i1 = 0; i1 < rawmaterial.Count; i1++)
                    {
                        if (rawmaterial[i1].rmstorageid == "")
                        {
                           
                            str.Append("INSERT INTO exciseautomation.rmreceipt_storage(rawmaterial_receipt_id, vat_code, storedqty,opening_dips, creation_date,financial_year)");
                            str.Append("Values('" + rawmaterial[0].rawmaterial_receipt_id + "', '" + rawmaterial[i1].vat_code + "','" + rawmaterial[i1].storedqty + "','" + rawmaterial[i1].opening_dips + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+rawmaterial[0].financial_year+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            int r = cmd3.ExecuteNonQuery();
                            if (rawmaterial[0].record_status == "A")
                            {
                                NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + rawmaterial[i1].vat_code + "' and party_code='" + rawmaterial[0].party_code + "'", cn);
                                double k = Convert.ToDouble(cmd6.ExecuteScalar());
                                double v = Convert.ToDouble(k) + Convert.ToDouble(rawmaterial[i1].storedqty);
                                NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + rawmaterial[i1].vat_code + "' and party_code='" + rawmaterial[0].party_code + "'", cn);
                                cmd7.ExecuteNonQuery();
                            }

                        }
                        else
                        {
                           
                            str.Append("update exciseautomation.rmreceipt_storage set rawmaterial_receipt_id='" + rawmaterial[0].rawmaterial_receipt_id + "', vat_code='" + rawmaterial[i1].vat_code + "', storedqty='" + rawmaterial[i1].storedqty + "',opening_dips='" + rawmaterial[i1].opening_dips + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "' where rmreceipt_storage_id='" + rawmaterial[i1].rmstorageid + "' and financial_year='" + rawmaterial[i1].financial_year + "'");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            cmd3.ExecuteNonQuery();
                            if (rawmaterial[0].record_status == "A")
                            {
                                NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + rawmaterial[i1].vat_code + "' and party_code='" + rawmaterial[0].party_code + "'", cn);
                                double k = Convert.ToDouble(cmd6.ExecuteScalar());
                                double v = Convert.ToDouble(k) + Convert.ToDouble(rawmaterial[i1].storedqty);
                                NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + rawmaterial[i1].vat_code + "' and party_code='" + rawmaterial[0].party_code + "'", cn);
                                cmd7.ExecuteNonQuery();
                            }
                        }
                        str = new StringBuilder();
                        str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                        str.Append("'" + rawmaterial[0].rawmaterial_receipt_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy") + "','RMR','Updated by Admin','Updated by Admin','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + rawmaterial[0].user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + rawmaterial[0].user_id + "','"+rawmaterial[0].financial_year+"','"+rawmaterial[0].party_code+"')");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        int n = cmd.ExecuteNonQuery();
                    }
                    val = "0";
                    trn.Commit();
                }
                catch (Exception ex)
                {
                    val = ex.Message;
                    trn.Commit();
                }
                return val;
            }
        }
        public static object UpdateGrain(List<RawMaterialReceipt> rawmaterial)
        {
            string val = "";

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    if (rawmaterial[0].suppliername != "" && rawmaterial[0].suppliername != null)
                    {

                        //NpgsqlCommand cmd1 = new NpgsqlCommand("Select attribute2 from exciseautomation.rawmaterial_receipt where rawmaterial_receipt_id='"+rawmaterial[0].rawmaterial_receipt_id+"' ", cn);
                        //string other =cmd1.ExecuteScalar().ToString();
                        //if(other !="" && other != null)
                        //{
                        //    NpgsqlCommand cmd2 = new NpgsqlCommand("update   exciseautomation.rawmaterial_receipt set attribute2=''  where rawmaterial_receipt_id='" + rawmaterial[0].rawmaterial_receipt_id + "' ", cn);
                        //   cmd2.ExecuteNonQuery();
                        //    NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.rawmaterial_receipt set rmr_entrydate='" + rawmaterial[0].rmr_entrydate + "', grossweight='" + rawmaterial[0].grossweight + "' ,vehicleno='" + rawmaterial[0].vehicleno + "', passno='" + rawmaterial[0].passno + "',passqty='" + rawmaterial[0].passqty + "',passissuedate='" + rawmaterial[0].passissuedate + "', netweight='" + rawmaterial[0].netweight + "', remarks='" + rawmaterial[0].remarks + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "', user_id='" + rawmaterial[0].user_id + "',record_status='" + rawmaterial[0].record_status + "',attribute1='" + rawmaterial[0].rawmaterial + "',attribute2='" + rawmaterial[0].suppliertype + "',attribute3='" + rawmaterial[0].suppliername + "' ,attribute4='" + rawmaterial[0].uom + "' where rawmaterial_receipt_id='" + rawmaterial[0].rawmaterial_receipt_id + "' ", cn);
                        //    cmd.ExecuteNonQuery();
                        //}
                        //else
                        //{
                            NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.rawmaterial_receipt set rmr_entrydate='" + rawmaterial[0].rmr_entrydate + "', grossweight='" + rawmaterial[0].grossweight + "' ,vehicleno='" + rawmaterial[0].vehicleno + "', passno='" + rawmaterial[0].passno + "',passqty='" + rawmaterial[0].passqty + "',passissuedate='" + rawmaterial[0].passissuedate + "', netweight='" + rawmaterial[0].netweight + "', remarks='" + rawmaterial[0].remarks + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "', user_id='" + rawmaterial[0].user_id + "',record_status='" + rawmaterial[0].record_status + "',supplier_party_code='" + rawmaterial[0].supplier + "',attribute1='" + rawmaterial[0].rawmaterial + "',attribute2='" + rawmaterial[0].suppliertype + "',attribute3='" + rawmaterial[0].suppliername + "' ,attribute4='" + rawmaterial[0].uom + "' where rawmaterial_receipt_id='" + rawmaterial[0].rawmaterial_receipt_id + "'  and financial_year='" + rawmaterial[0].financial_year + "'", cn);
                            cmd.ExecuteNonQuery();
                       // }
                      
                    }
                    else
                    {
                        //NpgsqlCommand cmd1 = new NpgsqlCommand("Select supplier_party_code from exciseautomation.rawmaterial_receipt where rawmaterial_receipt_id='" + rawmaterial[0].rawmaterial_receipt_id + "' ", cn);
                        //string other = cmd1.ExecuteScalar().ToString();
                        //if (other != "" && other != null)
                        //{
                        //    NpgsqlCommand cmd2 = new NpgsqlCommand("update   exciseautomation.rawmaterial_receipt set supplier_party_code=''  where rawmaterial_receipt_id='" + rawmaterial[0].rawmaterial_receipt_id + "' ", cn);
                        //    cmd2.ExecuteNonQuery();

                        //    NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.rawmaterial_receipt set rmr_entrydate='" + rawmaterial[0].rmr_entrydate + "', grossweight='" + rawmaterial[0].grossweight + "' ,vehicleno='" + rawmaterial[0].vehicleno + "', passno='" + rawmaterial[0].passno + "',passqty='" + rawmaterial[0].passqty + "',passissuedate='" + rawmaterial[0].passissuedate + "', netweight='" + rawmaterial[0].netweight + "', remarks='" + rawmaterial[0].remarks + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "', user_id='" + rawmaterial[0].user_id + "',record_status='" + rawmaterial[0].record_status + "',supplier_party_code='" + rawmaterial[0].supplier + "',attribute1='" + rawmaterial[0].rawmaterial + "',attribute2='" + rawmaterial[0].suppliertype + "',attribute3='" + rawmaterial[0].suppliername + "',attribute4='" + rawmaterial[0].uom + "' where rawmaterial_receipt_id='" + rawmaterial[0].rawmaterial_receipt_id + "' ", cn);
                        //    cmd.ExecuteNonQuery();
                        //}
                        //else
                        //{
                            NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.rawmaterial_receipt set rmr_entrydate='" + rawmaterial[0].rmr_entrydate + "', grossweight='" + rawmaterial[0].grossweight + "' ,vehicleno='" + rawmaterial[0].vehicleno + "', passno='" + rawmaterial[0].passno + "',passqty='" + rawmaterial[0].passqty + "',passissuedate='" + rawmaterial[0].passissuedate + "', netweight='" + rawmaterial[0].netweight + "', remarks='" + rawmaterial[0].remarks + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "', user_id='" + rawmaterial[0].user_id + "',record_status='" + rawmaterial[0].record_status + "',supplier_party_code='" + rawmaterial[0].supplier + "',attribute1='" + rawmaterial[0].rawmaterial + "',attribute2='" + rawmaterial[0].suppliertype + "',attribute3='" + rawmaterial[0].suppliername + "',attribute4='" + rawmaterial[0].uom + "' where rawmaterial_receipt_id='" + rawmaterial[0].rawmaterial_receipt_id + "' and financial_year='" + rawmaterial[0].financial_year + "' ", cn);
                            cmd.ExecuteNonQuery();
                       // }
                           
                    }
                   

                    for (int i1 = 0; i1 < rawmaterial.Count; i1++)
                    {
                        if (rawmaterial[i1].rmstorageid == "")
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.rmreceipt_storage(rawmaterial_receipt_id, vat_code, storedqty,opening_dips,user_id, creation_date,financial_year)");
                            str.Append("Values('" + rawmaterial[0].rawmaterial_receipt_id + "', '" + rawmaterial[i1].vat_code + "','" + rawmaterial[i1].storedqty + "','" + rawmaterial[i1].opening_dips + "','" + rawmaterial[0].user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+rawmaterial[0].financial_year+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            int r = cmd3.ExecuteNonQuery();
                            if (rawmaterial[0].record_status == "A")
                            {
                                NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + rawmaterial[i1].vat_code + "' and party_code='" + rawmaterial[0].party_code + "'", cn);
                                double k = Convert.ToDouble(cmd6.ExecuteScalar());
                                double v = Convert.ToDouble(k) + Convert.ToDouble(rawmaterial[i1].storedqty);
                                NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + rawmaterial[i1].vat_code + "' and party_code='" + rawmaterial[0].party_code + "'", cn);
                                cmd7.ExecuteNonQuery();
                            }

                        }
                        else
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append("update exciseautomation.rmreceipt_storage set rawmaterial_receipt_id='" + rawmaterial[0].rawmaterial_receipt_id + "', vat_code='" + rawmaterial[i1].vat_code + "', storedqty='" + rawmaterial[i1].storedqty + "',opening_dips='" + rawmaterial[i1].opening_dips + "',user_id='" + rawmaterial[0].user_id + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "' where rmreceipt_storage_id='" + rawmaterial[i1].rmstorageid + "'  and financial_year='" + rawmaterial[0].financial_year + "'");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            cmd3.ExecuteNonQuery();
                            if (rawmaterial[0].record_status == "A")
                            {
                                NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + rawmaterial[i1].vat_code + "' and party_code='" + rawmaterial[0].party_code + "'", cn);
                                double k = Convert.ToDouble(cmd6.ExecuteScalar());
                                double v = Convert.ToDouble(k) + Convert.ToDouble(rawmaterial[i1].storedqty);
                                NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + rawmaterial[i1].vat_code + "' and party_code='" + rawmaterial[0].party_code + "'", cn);
                                cmd7.ExecuteNonQuery();
                            }
                        }
                    }
                    val = "0";
                    trn.Commit();
                }
                catch (Exception ex)
                {
                    val = ex.Message;
                    trn.Commit();
                }
                return val;
            }
        }
        public static List<RawMaterialReceipt> GetGrainRawMaterial(string userid)
        {
            List<RawMaterialReceipt> rawmaterial = new List<RawMaterialReceipt>();

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name from exciseautomation.rawmaterial_receipt a  left join exciseautomation.party_master b on a.supplier_party_code=b.party_code where a.attribute1 !='' and record_active='true'  order by rmr_entrydate desc", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        rawmaterial = new List<RawMaterialReceipt>();
                        while (dr.Read())
                        {
                            RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
                            rawmaterials.rawmaterial_receipt_id = dr["rawmaterial_receipt_id"].ToString();
                            rawmaterials.financial_year = dr["financial_year"].ToString();
                            rawmaterials.party_code = dr["party_code"].ToString();
                            rawmaterials.party_name = dr["party_name"].ToString();
                            if (dr["rmr_entrydate"].ToString() != "")
                                rawmaterials.rmr_entrydate = dr["rmr_entrydate"].ToString().Substring(0, 10).Replace("/", "-");
                            rawmaterials.passissuedate = dr["passissuedate"].ToString();
                            rawmaterials.passno = dr["passno"] .ToString();
                            rawmaterials.vehicleno = dr["vehicleno"].ToString();
                            if (dr["passqty"].ToString() == "0")
                                rawmaterials.passqty = 0;
                            else
                                rawmaterials.passqty = Convert.ToDouble(dr["passqty"].ToString());
                            rawmaterials.vehicleno = dr["vehicleno"].ToString();
                            if (dr["grossweight"].ToString() == "")
                                rawmaterials.grossweight = 0;
                            else
                                rawmaterials.grossweight = Convert.ToDouble(dr["grossweight"].ToString());
                            if (dr["tankerweight"].ToString() == "")
                                rawmaterials.tankerweight = 0;
                            else
                                rawmaterials.tankerweight = Convert.ToDouble(dr["tankerweight"].ToString());
                            if (dr["supplierweight"].ToString() == "")
                                rawmaterials.supplierweight = 0;
                            else
                                rawmaterials.supplierweight = Convert.ToDouble(dr["supplierweight"].ToString());
                            if (dr["transitweight"].ToString() == "")
                                rawmaterials.transitweight = 0;
                            else
                                rawmaterials.transitweight = Convert.ToDouble(dr["transitweight"].ToString());
                            if (dr["netweight"].ToString() == "")
                                rawmaterials.netweight = 0;
                            else
                                rawmaterials.netweight = Convert.ToDouble(dr["netweight"].ToString());
                            rawmaterials.suppliername = dr["attribute3"].ToString();
                            rawmaterials.rawmaterial = dr["attribute1"].ToString();
                            rawmaterials.suppliertype = dr["attribute2"].ToString();
                            rawmaterials.supplier = dr["supplier_party_code"].ToString();
                            rawmaterials.record_status = dr["record_status"].ToString();
                            rawmaterials.user_id = userid;
                            rawmaterials.remarks = dr["remarks"].ToString();
                            rawmaterial.Add(rawmaterials);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return rawmaterial;
            }
        }
        public static List<RawMaterialReceipt> Search2(string tablename, string column, string value)
        {
            List<RawMaterialReceipt> mir = new List<RawMaterialReceipt>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name from exciseautomation.rawmaterial_receipt a  left join exciseautomation.party_master b on a.supplier_party_code=b.party_code where a.attribute1 !='' and " + column + " Ilike '%" + value + "%'  order by   rmr_entrydate desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<RawMaterialReceipt>();
                            while (dr.Read())
                            {
                                RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
                                rawmaterials.rawmaterial_receipt_id = dr["rawmaterial_receipt_id"].ToString();
                                rawmaterials.party_code = dr["party_code"].ToString();
                                rawmaterials.party_name = dr["party_name"].ToString();
                                if (dr["rmr_entrydate"].ToString() != "")
                                    rawmaterials.rmr_entrydate = dr["rmr_entrydate"].ToString().Substring(0, 10).Replace("/", "-");
                                rawmaterials.passissuedate = dr["passissuedate"].ToString();
                                rawmaterials.passno = dr["passno"].ToString();
                                rawmaterials.vehicleno = dr["vehicleno"].ToString();
                                if (dr["passqty"].ToString() == "0")
                                    rawmaterials.passqty = 0;
                                else
                                    rawmaterials.passqty = Convert.ToDouble(dr["passqty"].ToString());
                                rawmaterials.vehicleno = dr["vehicleno"].ToString();
                                if (dr["grossweight"].ToString() == "")
                                    rawmaterials.grossweight = 0;
                                else
                                    rawmaterials.grossweight = Convert.ToDouble(dr["grossweight"].ToString());
                                if (dr["tankerweight"].ToString() == "")
                                    rawmaterials.tankerweight = 0;
                                else
                                    rawmaterials.tankerweight = Convert.ToDouble(dr["tankerweight"].ToString());
                                if (dr["supplierweight"].ToString() == "")
                                    rawmaterials.supplierweight = 0;
                                else
                                    rawmaterials.supplierweight = Convert.ToDouble(dr["supplierweight"].ToString());
                                if (dr["transitweight"].ToString() == "")
                                    rawmaterials.transitweight = 0;
                                else
                                    rawmaterials.transitweight = Convert.ToDouble(dr["transitweight"].ToString());
                                if (dr["netweight"].ToString() == "")
                                    rawmaterials.netweight = 0;
                                else
                                    rawmaterials.netweight = Convert.ToDouble(dr["netweight"].ToString());
                                rawmaterials.suppliername = dr["attribute3"].ToString();
                                rawmaterials.rawmaterial = dr["attribute1"].ToString();
                                rawmaterials.suppliertype = dr["attribute2"].ToString();
                                rawmaterials.supplier = dr["supplier_party_code"].ToString();
                                rawmaterials.record_status = dr["record_status"].ToString();
                                rawmaterials.user_id = dr["user_id"].ToString();
                                rawmaterials.remarks = dr["remarks"].ToString();
                                mir.Add(rawmaterials);

                            }
                        }
                    }
                    cn.Close();

                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            return mir;
        }
        public static RawMaterialReceipt GetGainList(int receipt_id, string financial_year)
        {
            RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name from exciseautomation.rawmaterial_receipt a  left join exciseautomation.party_master b on a.supplier_party_code=b.party_code where  a.rawmaterial_receipt_id='" + receipt_id + "' and a.financial_year='"+financial_year+"'", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        rawmaterials.rawmaterial_receipt_id = dr["rawmaterial_receipt_id"].ToString();
                        rawmaterials.party_code = dr["party_code"].ToString();
                        rawmaterials.party_name = dr["party_name"].ToString();
                        if (dr["rmr_entrydate"].ToString() != "")
                            rawmaterials.rmr_entrydate = dr["rmr_entrydate"].ToString().Substring(0, 10).Replace("/", "-");
                        rawmaterials.passissuedate = dr["passissuedate"].ToString();
                         rawmaterials.rmrpassno= dr["passno"].ToString();
                        if (dr["passqty"].ToString() == "0")
                            rawmaterials.passqty = 0;
                        else
                            rawmaterials.passqty = Convert.ToDouble(dr["passqty"].ToString());
                        rawmaterials.vehicleno = dr["vehicleno"].ToString();
                        if (dr["grossweight"].ToString() == "")
                            rawmaterials.grossweight = 0;
                        else
                            rawmaterials.grossweight = Convert.ToDouble(dr["grossweight"].ToString());
                        if (dr["tankerweight"].ToString() == "")
                            rawmaterials.tankerweight = 0;
                        else
                            rawmaterials.tankerweight = Convert.ToDouble(dr["tankerweight"].ToString());
                        if (dr["supplierweight"].ToString() == "")
                            rawmaterials.supplierweight = 0;
                        else
                            rawmaterials.supplierweight = Convert.ToDouble(dr["supplierweight"].ToString());
                        if (dr["transitweight"].ToString() == "")
                            rawmaterials.transitweight = 0;
                        else
                            rawmaterials.transitweight = Convert.ToDouble(dr["transitweight"].ToString());
                        if (dr["netweight"].ToString() == "")
                            rawmaterials.netweight = 0;
                        else
                            rawmaterials.netweight = Convert.ToDouble(dr["netweight"].ToString());
                        rawmaterials.suppliername = dr["attribute3"].ToString();
                        rawmaterials.rawmaterial = dr["attribute1"].ToString();
                        rawmaterials.suppliertype = dr["attribute2"].ToString();
                        rawmaterials.uom = dr["attribute4"].ToString();
                        rawmaterials.supplier = dr["supplier_party_code"].ToString();
                        rawmaterials.record_status = dr["record_status"].ToString();
                        rawmaterials.remarks = dr["remarks"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return rawmaterials;
            }
        }

        public static List<RawMaterialReceipt> GetGrainvatsList(string party_code, string receipt_id,string product,string financial_year)
        {
            List<RawMaterialReceipt> rawmaterial = new List<RawMaterialReceipt>();

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    if (party_code == "ALL")
                    {
                        cmd = new NpgsqlCommand("select a.rawmaterial_receipt_id,b.vat_totalcapacity,a.opening_dips,a.rmreceipt_storage_id,case when a.storedqty is null then 0 else storedqty end as storedqty,b.vat_code,b.vat_name,b.party_code from   exciseautomation.vat_master b left join exciseautomation.rmreceipt_storage a on a.vat_code=b.vat_code left join exciseautomation.rawmaterial_master d on b.product_type_code=d.product_type_code where  b.vat_type_code='MST' and a.rawmaterial_receipt_id='" + receipt_id + "' and a.financial_year='"+financial_year+"' order by b.vat_code", cn);
                    }
                    else
                    {
                        cmd = new NpgsqlCommand("select a.rawmaterial_receipt_id,b.vat_totalcapacity,a.opening_dips,a.rmreceipt_storage_id,case when a.storedqty is null then 0 else storedqty end as storedqty,b.vat_code,b.vat_name,b.party_code from   exciseautomation.vat_master b left join exciseautomation.rmreceipt_storage a on a.vat_code=b.vat_code left join exciseautomation.rawmaterial_master d on b.product_type_code=d.product_type_code where  b.vat_type_code='MST' and d.product_type_code='" + product + "' and b.party_code='" + party_code + "' and a.rawmaterial_receipt_id='" + receipt_id + "' and a.financial_year='" + financial_year + "' order by b.vat_code", cn);
                    }
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        rawmaterial = new List<RawMaterialReceipt>();
                        while (dr.Read())
                        {
                            RawMaterialReceipt raw = new RawMaterialReceipt();
                            raw.rawmaterial_receipt_id = dr["rawmaterial_receipt_id"].ToString();
                            raw.rmstorageid = dr["rmreceipt_storage_id"].ToString();
                            raw.vat_name = dr["vat_name"].ToString();
                            raw.vat_code = dr["vat_code"].ToString();
                            raw.party_code = dr["party_code"].ToString();
                            raw.storedqty = Convert.ToDouble(dr["storedqty"].ToString());
                            raw.vat_totalcapacity = Convert.ToDouble(dr["vat_totalcapacity"].ToString());
                            if (dr["opening_dips"].ToString() == "")
                                raw.opening_dips = 0;
                            else
                                raw.opening_dips = Convert.ToDouble(dr["opening_dips"].ToString());
                            rawmaterial.Add(raw);
                        }
                    }
                    //else
                    //{
                    //    dr.Close();
                    //    NpgsqlCommand cmd1 = new NpgsqlCommand("select vat_code,vat_name,party_code,product_type_code from   exciseautomation.vat_master where  vat_type_code='MST' and product_type_code='"+product+"'  and party_code='" + party_code + "'  order by vat_code", cn);
                    //    NpgsqlDataReader dr1 = cmd1.ExecuteReader();
                    //    if (dr1.HasRows)
                    //    {
                    //        rawmaterial = new List<RawMaterialReceipt>();
                    //        while (dr1.Read())
                    //        {
                    //            RawMaterialReceipt raw = new RawMaterialReceipt();
                    //            raw.rawmaterial_receipt_id = receipt_id;
                    //            raw.rmstorageid = "";
                    //            raw.vat_name = dr1["vat_name"].ToString();
                    //            raw.vat_code = dr1["vat_code"].ToString();
                    //            raw.party_code = dr1["party_code"].ToString();
                    //            raw.product_type_code = dr1["product_type_code"].ToString();
                    //            raw.storedqty = 0;
                    //            // if (dr["opening_dips"].ToString() == "")
                    //            raw.opening_dips = 0;
                    //            //else
                    //            // raw.opening_dips = Convert.ToDouble(dr["opening_dips"].ToString());
                    //            rawmaterial.Add(raw);
                    //        }
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return rawmaterial;

        }
        //public static List<RawMaterialReceipt> GetGrainVats(string party_code, string receipt_id)
        //{

        //}

        public static List<RawMaterialReceipt> GetvatsList(string party_code,string receipt_id,string financial_year)
        {
            List<RawMaterialReceipt> rawmaterial = new List<RawMaterialReceipt>();

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    if(party_code=="ALL")
                    {
                        cmd = new NpgsqlCommand("select a.rawmaterial_receipt_id,b.vat_totalcapacity,a.opening_dips,a.rmreceipt_storage_id,case when a.storedqty is null then 0 else storedqty end as storedqty,b.vat_code,b.vat_name,b.party_code from   exciseautomation.vat_master b left join exciseautomation.rmreceipt_storage a on a.vat_code=b.vat_code where  b.vat_type_code='MST' and  a.rawmaterial_receipt_id='" + receipt_id + "' and a.financial_year='"+financial_year+"' order by b.vat_code", cn);
                    }
                    else
                    {
                        cmd = new NpgsqlCommand("select a.rawmaterial_receipt_id,b.vat_totalcapacity,a.opening_dips,a.rmreceipt_storage_id,case when a.storedqty is null then 0 else storedqty end as storedqty,b.vat_code,b.vat_name,b.party_code from   exciseautomation.vat_master b left join exciseautomation.rmreceipt_storage a on a.vat_code=b.vat_code where  b.vat_type_code='MST' and b.party_code='" + party_code + "' and a.rawmaterial_receipt_id='" + receipt_id + "'  and a.financial_year='" + financial_year + "'  order by b.vat_code", cn);
                    }
             
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        rawmaterial = new List<RawMaterialReceipt>();
                        while (dr.Read())
                        {
                            RawMaterialReceipt raw = new RawMaterialReceipt();
                            raw.rawmaterial_receipt_id = dr["rawmaterial_receipt_id"].ToString();
                            raw.rmstorageid = dr["rmreceipt_storage_id"].ToString();
                            raw.vat_name = dr["vat_name"].ToString();
                            raw.vat_code = dr["vat_code"].ToString();
                            raw.party_code = dr["party_code"].ToString();
                            raw.storedqty = Convert.ToDouble(dr["storedqty"].ToString());
                            raw.vat_totalcapacity = Convert.ToDouble(dr["vat_totalcapacity"].ToString());
                            if (dr["opening_dips"].ToString() == "")
                                raw.opening_dips = 0;
                            else
                                raw.opening_dips = Convert.ToDouble(dr["opening_dips"].ToString());
                            rawmaterial.Add(raw);
                        }
                    }
                    else
                    {
                        dr.Close();
                        cmd = new NpgsqlCommand("select vat_code,vat_name,party_code,product_type_code from   exciseautomation.vat_master where  vat_type_code='MST' and party_code='" + party_code + "'  order by vat_code", cn);
                        dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            rawmaterial = new List<RawMaterialReceipt>();
                            while (dr.Read())
                            {
                                RawMaterialReceipt raw = new RawMaterialReceipt();
                                raw.rawmaterial_receipt_id =receipt_id;
                                raw.rmstorageid = "";
                                raw.vat_name = dr["vat_name"].ToString();
                                raw.vat_code = dr["vat_code"].ToString();
                                raw.party_code = dr["party_code"].ToString();
                                raw.product_type_code = dr["product_type_code"].ToString();
                                raw.storedqty =0;
                               // if (dr["opening_dips"].ToString() == "")
                                    raw.opening_dips = 0;
                                //else
                                   // raw.opening_dips = Convert.ToDouble(dr["opening_dips"].ToString());
                                rawmaterial.Add(raw);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
                return rawmaterial;
            
        }
        public static List<RawMaterialReceipt> GetvatsList1(string party_code, string receipt_id, string product)
        {
            List<RawMaterialReceipt> rawmaterial = new List<RawMaterialReceipt>();

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    //NpgsqlCommand cmd = new NpgsqlCommand("select a.rawmaterial_receipt_id,b.vat_totalcapacity,a.opening_dips,a.rmreceipt_storage_id,case when a.storedqty is null then 0 else storedqty end as storedqty,b.vat_code,b.vat_name,b.party_code from   exciseautomation.vat_master b left join exciseautomation.rmreceipt_storage a on a.vat_code=b.vat_code where  b.vat_type_code='MST' and b.party_code='" + party_code + "' and a.rawmaterial_receipt_id='" + receipt_id + "' order by b.vat_code", cn);
                    //NpgsqlDataReader dr = cmd.ExecuteReader();
                    //if (dr.HasRows)
                    //{
                    //    rawmaterial = new List<RawMaterialReceipt>();
                    //    while (dr.Read())
                    //    {
                    //        RawMaterialReceipt raw = new RawMaterialReceipt();
                    //        raw.rawmaterial_receipt_id = dr["rawmaterial_receipt_id"].ToString();
                    //        raw.rmstorageid = dr["rmreceipt_storage_id"].ToString();
                    //        raw.vat_name = dr["vat_name"].ToString();
                    //        raw.vat_code = dr["vat_code"].ToString();
                    //        raw.party_code = dr["party_code"].ToString();
                    //        raw.storedqty = Convert.ToDouble(dr["storedqty"].ToString());
                    //        raw.vat_totalcapacity = Convert.ToDouble(dr["vat_totalcapacity"].ToString());
                    //        if (dr["opening_dips"].ToString() == "")
                    //            raw.opening_dips = 0;
                    //        else
                    //            raw.opening_dips = Convert.ToDouble(dr["opening_dips"].ToString());
                    //        rawmaterial.Add(raw);
                    //    }
                    //}
                    //else
                    //{
                    //    dr.Close();
                    NpgsqlCommand cmd = new NpgsqlCommand("select vat_code,vat_name,party_code,product_type_code from   exciseautomation.vat_master where  vat_type_code='MST' and product_type_code='" + product + "'  and party_code='" + party_code + "'  order by vat_code", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            rawmaterial = new List<RawMaterialReceipt>();
                            while (dr.Read())
                            {
                                RawMaterialReceipt raw = new RawMaterialReceipt();
                                raw.rawmaterial_receipt_id = receipt_id;
                                raw.rmstorageid = "";
                                raw.vat_name = dr["vat_name"].ToString();
                                raw.vat_code = dr["vat_code"].ToString();
                                raw.party_code = dr["party_code"].ToString();
                                raw.product_type_code = dr["product_type_code"].ToString();
                                raw.storedqty = 0;
                                // if (dr["opening_dips"].ToString() == "")
                                raw.opening_dips = 0;
                                //else
                                // raw.opening_dips = Convert.ToDouble(dr["opening_dips"].ToString());
                                rawmaterial.Add(raw);
                            }
                        }
                    
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return rawmaterial;

        }
        public static string InsertRaw(List<RawMaterialReceipt> rawmatrial)
        {
           string val = "";

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                   
                    NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.rawmaterial_receipt set rmr_entrydate='" + rawmatrial[0].rmr_entrydate + "', grossweight='" + rawmatrial[0].grossweight + "', tankerweight='" + rawmatrial[0].tankerweight + "', transitweight='" + rawmatrial[0].transitweight + "', supplierweight='" + rawmatrial[0].supplierweight + "', netweight='" + rawmatrial[0].netweight + "', remarks='" + rawmatrial[0].remarks + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "', user_id='" + rawmatrial[0].user_id + "',record_status='" + rawmatrial[0].record_status + "', financial_year='"+rawmatrial[0].financial_year+"' where rawmaterial_receipt_id='" + rawmatrial[0].rawmaterial_receipt_id + "'  and financial_year='" + rawmatrial[0].financial_year + "' ", cn);
                    cmd.ExecuteNonQuery();
                    for (int i1 = 0; i1 < rawmatrial.Count; i1++)
                    {
                        
                            StringBuilder str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.rmreceipt_storage(rawmaterial_receipt_id, vat_code, storedqty,opening_dips,user_id, creation_date,financial_year)");
                            str.Append("Values('" + rawmatrial[0].rawmaterial_receipt_id + "', '" + rawmatrial[i1].vat_code + "','" + rawmatrial[i1].storedqty + "','" + rawmatrial[i1].opening_dips + "','" + rawmatrial[0].user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+rawmatrial[0].financial_year+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            int r = cmd3.ExecuteNonQuery();
                        
                    }
                    val = "0";
                    trn.Commit();
                }
                catch (Exception ex)
                {
                    val = ex.Message;
                    trn.Commit();
                }
                return val;
            }
        }


        public static int GetGrainData(string party_code)
        {
            int value1 = 0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;

                    cmd = new NpgsqlCommand("select  grain_based_distillery from exciseautomation.party_master where party_code='" + party_code + "'", cn);
                    string re = cmd.ExecuteScalar().ToString();
                    if (re == "True")
                    {
                        value1 = 1;
                     
                    }
                    else 
                    {
                        if (re != "")
                            value1 = Convert.ToInt32(re);
                       
                    }

                }
                catch (Exception ex)
                {
                   
                    value1 = 0;
                }
            }
            return value1;
        }

        public static int GetData(string party_code)
        {
            int value1 = 0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;

                    cmd = new NpgsqlCommand("select  count(b.rawmaterial_code) from   exciseautomation.vat_master a inner join exciseautomation.rawmaterial_master b on a.product_type_code=b.product_type_code where a.party_code='"+party_code+"'", cn);
                    string re = cmd.ExecuteScalar().ToString();
                    if (re != "0")
                    {
                        value1 = 1;

                    }
                    else
                    {
                        if (re != "")
                            value1 = Convert.ToInt32(re);

                    }

                }
                catch (Exception ex)
                {

                    value1 = 0;
                }
            }
            return value1;
        }

        public static string Adminupdate(List<RawMaterialReceipt> rawmaterial)
        {
            string val = "";

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.rawmaterial_receipt set rmr_entrydate='" + rawmaterial[0].rmr_entrydate + "', grossweight='" + rawmaterial[0].grossweight + "', tankerweight='" + rawmaterial[0].tankerweight + "', transitweight='" + rawmaterial[0].transitweight + "', supplierweight='" + rawmaterial[0].passqty + "', netweight='" + rawmaterial[0].netweight + "' where rawmaterial_receipt_id='" + rawmaterial[0].rawmaterial_receipt_id + "'  and financial_year='" +rawmaterial[0].financial_year + "' ", cn);
                    cmd.ExecuteNonQuery();
                    StringBuilder str = new StringBuilder();
                    for (int i1 = 0; i1 < rawmaterial.Count; i1++)
                    {
                        if (rawmaterial[i1].rmstorageid == "")
                        {
                           
                            str.Append("INSERT INTO exciseautomation.rmreceipt_storage(rawmaterial_receipt_id, vat_code, storedqty,opening_dips, creation_date,financial_year)");
                            str.Append("Values('" + rawmaterial[0].rawmaterial_receipt_id + "', '" + rawmaterial[i1].vat_code + "','" + rawmaterial[i1].storedqty + "','" + rawmaterial[i1].opening_dips + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+rawmaterial[0].financial_year+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            int r = cmd3.ExecuteNonQuery();
                        }
                        else
                        {
                           
                            str.Append("update exciseautomation.rmreceipt_storage set rawmaterial_receipt_id='" + rawmaterial[0].rawmaterial_receipt_id + "', vat_code='" + rawmaterial[i1].vat_code + "', storedqty='" + rawmaterial[i1].storedqty + "',opening_dips='" + rawmaterial[i1].opening_dips + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "' where rmreceipt_storage_id='" + rawmaterial[i1].rmstorageid + "' and financial_year='" + rawmaterial[0].financial_year + "'");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            cmd3.ExecuteNonQuery();
                        }
                    }
                    str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year) VALUES(");
                    str.Append("'" + rawmaterial[0].rawmaterial_receipt_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy") + "','RMR','Updated by Admin','Updated by Admin','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + rawmaterial[0].user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + rawmaterial[0].user_id + "','"+rawmaterial[0].financial_year+"')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                   int n = cmd.ExecuteNonQuery();
                    val = "0";
                    trn.Commit();
                }
                catch (Exception ex)
                {
                    val = ex.Message;
                    trn.Commit();
                }
                return val;
            }
        }
        public static string UpdateRawMaterial(List<RawMaterialReceipt> rawmaterial)
        {
            string val = "";

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.rawmaterial_receipt set rmr_entrydate='" + rawmaterial[0].rmr_entrydate + "', grossweight='" + rawmaterial[0].grossweight + "', tankerweight='" + rawmaterial[0].tankerweight + "', transitweight='" + rawmaterial[0].transitweight + "', supplierweight='" + rawmaterial[0].passqty + "', netweight='" + rawmaterial[0].netweight + "', remarks='" + rawmaterial[0].remarks + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "', user_id='" + rawmaterial[0].user_id + "',record_status='" + rawmaterial[0].record_status + "' where rawmaterial_receipt_id='" + rawmaterial[0].rawmaterial_receipt_id + "' and financial_year='"+rawmaterial[0].financial_year+"' ", cn);
                    cmd.ExecuteNonQuery();
                    for (int i1 = 0; i1 < rawmaterial.Count; i1++)
                    {
                        if (rawmaterial[i1].rmstorageid == "")
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.rmreceipt_storage(rawmaterial_receipt_id, vat_code, storedqty,opening_dips,user_id, creation_date,financial_year)");
                            str.Append("Values('" + rawmaterial[0].rawmaterial_receipt_id + "', '" + rawmaterial[i1].vat_code + "','" + rawmaterial[i1].storedqty + "','" + rawmaterial[i1].opening_dips + "','" + rawmaterial[0].user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+rawmaterial[0].financial_year+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            int r = cmd3.ExecuteNonQuery();
                        }
                        else
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append("update exciseautomation.rmreceipt_storage set rawmaterial_receipt_id='" + rawmaterial[0].rawmaterial_receipt_id + "', vat_code='" + rawmaterial[i1].vat_code + "', storedqty='" + rawmaterial[i1].storedqty + "',opening_dips='" + rawmaterial[i1].opening_dips + "',user_id='" + rawmaterial[0].user_id + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "' where rmreceipt_storage_id='" + rawmaterial[i1].rmstorageid + "' and financial_year='" + rawmaterial[0].financial_year + "'");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            cmd3.ExecuteNonQuery();
                        }
                    }
                    val = "0";
                    trn.Commit();
                }
                catch (Exception ex)
                {
                    val = ex.Message;
                    trn.Commit();
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
        public static bool InsertRMRStorage(RawMaterialReceipt RMRStorage)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cm = new NpgsqlCommand("select count(rawmaterial_receipt_id) from exciseautomation.rawmaterial_receipt", cn);
                    int m = Convert.ToInt32(cm.ExecuteScalar());
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.rmreceipt_storage(rawmaterial_receipt_id, vat_code, storedqty, lastmodified_date, user_id, creation_date) VALUES('" + m + "', '" + RMRStorage.vat_code + "', '" + RMRStorage.storedqty + "', '" + DateTime.Now.ToShortDateString() + "', '" + RMRStorage.user_id + "','" + RMRStorage.creation_date + "'  ) ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                    }
                    else
                    {
                        value = false;
                    }
                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);
                }
                return value;
            }
        }
        public static RawMaterialReceipt GetList(int receipt_id,string financial_year)
        {
            RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.pass,d.vehicle_no as vehicle_no1,d.pass_issueno,d.financial_year as passfinancial_year  from exciseautomation.rawmaterial_receipt a  right join exciseautomation.party_master b on a.supplier_party_code=b.party_code inner join exciseautomation.document_format_master c on c.party_code=a.supplier_party_code inner join exciseautomation.pass d on a.passno=d.pass_id where  a.rawmaterial_receipt_id='" + receipt_id + "' and a.financial_year='"+financial_year+"'", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        rawmaterials.rawmaterial_receipt_id = dr["rawmaterial_receipt_id"].ToString();
                        rawmaterials.party_code = dr["party_code"].ToString();
                        rawmaterials.party_name = dr["party_name"].ToString();
                      if(dr["rmr_entrydate"].ToString()!="")
                        rawmaterials.rmr_entrydate = dr["rmr_entrydate"].ToString().Substring(0,10).Replace("/","-");
                        rawmaterials.passissuedate = dr["passissuedate"].ToString();
                        rawmaterials.financial_year = dr["passfinancial_year"].ToString();
                        rawmaterials.passno =dr["pass"]+ dr["passfinancial_year"].ToString() + "/"+ dr["pass_issueno"].ToString();
                       // rawmaterials.rmrpassno= dr["passno"].ToString();
                        if (dr["passqty"].ToString() == "0")
                            rawmaterials.passqty = 0;
                        else
                            rawmaterials.passqty = Convert.ToDouble(dr["passqty"].ToString());
                        rawmaterials.vehicleno = dr["vehicle_no1"].ToString();
                        if (dr["grossweight"].ToString() == "")
                            rawmaterials.grossweight = 0;
                        else
                            rawmaterials.grossweight = Convert.ToDouble(dr["grossweight"].ToString());
                        if (dr["tankerweight"].ToString() == "")
                            rawmaterials.tankerweight = 0;
                        else
                            rawmaterials.tankerweight = Convert.ToDouble(dr["tankerweight"].ToString());
                        if (dr["supplierweight"].ToString() == "")
                            rawmaterials.supplierweight = 0;
                        else
                            rawmaterials.supplierweight = Convert.ToDouble(dr["supplierweight"].ToString());
                        if (dr["transitweight"].ToString() == "")
                            rawmaterials.transitweight = 0;
                        else
                            rawmaterials.transitweight = Convert.ToDouble(dr["transitweight"].ToString());
                        if (dr["netweight"].ToString() == "")
                            rawmaterials.netweight = 0;
                        else
                            rawmaterials.netweight = Convert.ToDouble(dr["netweight"].ToString());
                        //rawmaterials.suppliername= dr["attribute3"].ToString();
                        //rawmaterials.rawmaterial= dr["attribute1"].ToString();
                        //rawmaterials.suppliertype= dr["attribute2"].ToString();
                        //rawmaterials.supplier= dr["supplier_party_code"].ToString();
                        rawmaterials.record_status = dr["record_status"].ToString();
                        rawmaterials.remarks = dr["remarks"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return rawmaterials;
            }
        }
        //public static List<RmrReceiptStorage> GetStorage(int receipt_id)
        //{
        //    List<RmrReceiptStorage> str = new List<RmrReceiptStorage>();
        //    using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
        //    {
        //        cn.Open();
        //        try
        //        {
        //            NpgsqlCommand cmd = new NpgsqlCommand("SELECT  a.*,b.vat_name FROM exciseautomation.rmreceipt_storage a inner join exciseautomation.vat_master b on a.vat_code = b.vat_code where rawmaterial_receipt_id = '" + receipt_id + "'", cn);
        //            NpgsqlDataReader dr = cmd.ExecuteReader();
        //            if (dr.HasRows)
        //            {
        //                str = new List<RmrReceiptStorage>();
        //                while (dr.Read())
        //                {
        //                    RmrReceiptStorage storage = new RmrReceiptStorage();
        //                    storage.vat_name = dr["vat_name"].ToString();
        //                    storage.vat_code = dr["vat_code"].ToString();
        //                    storage.storedqty = Convert.ToDouble(dr["storedqty"].ToString());
        //                    str.Add(storage);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw (ex);
        //        }
        //        return str;
        //    }
        //}



        public static string Approve(List<RawMaterialReceipt> RAW)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    if (RAW[0].record_status == "A")
                    {
                        for (int i = 0; i < RAW.Count; i++)
                        {
                            NpgsqlCommand cmd2 = new NpgsqlCommand("select case when vat_availablecapacity is null then 0 else vat_availablecapacity end vat_availablecapacity from  exciseautomation.vat_master  WHERE party_code='" + RAW[i].party_code + "' and vat_code='" + RAW[i].vat_code + "'", cn);
                            double available = Convert.ToDouble(cmd2.ExecuteScalar()) + RAW[i].storedqty;
                            cmd2 = new NpgsqlCommand("UPDATE exciseautomation.vat_master SET  vat_availablecapacity='" + available + "' WHERE party_code='" + RAW[i].party_code + "' and vat_code='" + RAW[i].vat_code + "'", cn);
                            int G = cmd2.ExecuteNonQuery();
                            StringBuilder str2 = new StringBuilder();
                            str2.Append("INSERT INTO exciseautomation.transactionhistory(transaction_id, transaction_date, transaction_type, transaction_format, party_code, from_vat,  minus_qty, user_id, creation_date,financial_year)Values(");
                            str2.Append("'" + RAW[0].rawmaterial_receipt_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','RMR','" + RAW[i].rawmaterial_receipt_id + "','" + RAW[i].party_code + "','" + RAW[i].vat_code + "','" + RAW[i].passqty + "','" + RAW[i].user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+RAW[i].financial_year+"')");
                            cmd2 = new NpgsqlCommand(str2.ToString(), cn);
                            cmd2.ExecuteNonQuery();
                        }
                    }
                    StringBuilder str = new StringBuilder();
                    str.Append("update exciseautomation.rawmaterial_receipt set  record_status='" + RAW[0].record_status + "' where rawmaterial_receipt_id='" + RAW[0].rawmaterial_receipt_id + "' and financial_year='" +RAW[0].financial_year + "' ");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (RAW[0].record_status == "A")
                    {
                        RAW[0].record_status = "Approved By Bond Officer";
                    }
                    else
                    {
                        RAW[0].record_status = "Rejected By Bond Officer";
                    }
                    str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + RAW[0].rawmaterial_receipt_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy") + "','RMR','" + RAW[0].record_status + "','" + RAW[0].remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + RAW[0].user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + RAW[0].user_id + "','"+RAW[0].financial_year+"','"+RAW[0].party_code+"')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                    n = cmd.ExecuteNonQuery();
                    VAL = "0";
                    trn.Commit();
                    cn.Close();
                }
                catch (Exception ex1)
                {
                    VAL = ex1.Message;
                    trn.Rollback();
                }
            }
            return VAL;
        }
        }
}
