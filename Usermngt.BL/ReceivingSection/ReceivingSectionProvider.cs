using Npgsql;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Usermngt.BL.DataUtility;
using Usermngt.Entities;

namespace Usermngt.BL.ReceivingSection
{
    public class ReceivingSectionProvider : BaseDataLayer, IReceivingSectionService

    {
        #region Methods
        public Tuple<bool, string> SaveReceivingSection(ReceivingSectionContext context)
        {
            Tuple<bool, string> isReceivingSectionSaved = default(Tuple<bool, string>);
            if (context.ReceivingSection.receiving_section_id == default(int))
            {
                string receivingSectionSql = $@"INSERT INTO exciseautomation.receiving_section(
                                          type_of_liquor_id, 
                                          liquor_sub_type_id, size_master_id, 
                                          brand_master_id, compactor_id, receiving_date, 
                                          letter_no, letter_date, exhibit_from, 
                                          sealed, quantity, batch_no, address, 
                                          issaved,issealed_text
                                        ) 
                                        VALUES 
                                          ( '{context.ReceivingSection.type_of_liquor_id}', '{context.ReceivingSection.liquor_sub_type_id}','{context.ReceivingSection.size_master_id}',
                                            '{context.ReceivingSection.brand_master_id}','{context.ReceivingSection.compactor_id}','{context.ReceivingSection.receiving_date}', 
                                            '{context.ReceivingSection.letter_no}','{context.ReceivingSection.letter_date}', '{context.ReceivingSection.exhibit_from}',
                                            '{context.ReceivingSection.is_sealed}', '{context.ReceivingSection.quantity}', '{context.ReceivingSection.batch_no}',
                                            '{context.ReceivingSection.address}', '{context.ReceivingSection.issaved}', '{context.ReceivingSection.issealed_text}')
                                            RETURNING receiving_section_id";
              
                using (var connection = GetSqlConnection())
                {
                    connection.Open();
                    var transaction = connection.BeginTransaction();
                    try
                    {
                        using (var command = GetSqlCommandWithTransaction(receivingSectionSql, transaction))
                        {
                            context.ReceivingSection.receiving_section_id = (int)command.ExecuteScalar();
                            string lab_reportSql = $@"INSERT INTO exciseautomation.lab_report(
                                                    receiving_section_id) 
                                                    VALUES
                                                    ('{context.ReceivingSection.receiving_section_id}')
                                                       ";
                            if (context.ReceivingSection.receiving_section_id > default(int))
                            {
                                var relatedDataSql = GetRelatedDataSql(context, true);

                                using (var command1 = GetSqlCommandWithTransaction(relatedDataSql, transaction))
                                {
                                    command1.CommandText = relatedDataSql;
                                    command1.ExecuteNonQuery();
                                }
                                using (var command2 = GetSqlCommandWithTransaction(lab_reportSql, transaction))
                                {
                                    command2.CommandText = lab_reportSql;
                                    command2.ExecuteNonQuery();
                                }
                            }
                        }
                        transaction.Commit();
                        connection.Close();
                        isReceivingSectionSaved = new Tuple<bool, string>(true, "Recieving Section Saved Successfully");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            else
            {
                string receivingSectionSql = $@" UPDATE 
                                              exciseautomation.receiving_section 
                                            SET  
                                              type_of_liquor_id = '{context.ReceivingSection.type_of_liquor_id}', 
                                              liquor_sub_type_id = '{context.ReceivingSection.liquor_sub_type_id}', 
                                              size_master_id = '{context.ReceivingSection.size_master_id}', 
                                              brand_master_id = '{context.ReceivingSection.brand_master_id}', 
                                              compactor_id = '{context.ReceivingSection.compactor_id}', 
                                              receiving_date = '{context.ReceivingSection.receiving_date}', 
                                              letter_no = '{context.ReceivingSection.letter_no}', 
                                              letter_date ='{context.ReceivingSection.letter_date}', 
                                              exhibit_from = '{context.ReceivingSection.exhibit_from}', 
                                              sealed = '{context.ReceivingSection.is_sealed}', 
                                              quantity = '{context.ReceivingSection.quantity}', 
                                              batch_no = '{context.ReceivingSection.batch_no}', 
                                              address ='{context.ReceivingSection.address}', 
                                              issaved = '{context.ReceivingSection.issaved}',  
                                              updated_on = '{DateTime.Now}', 
                                              updated_by = '{context.ReceivingSection.created_by}', 
                                              issealed_text = '{context.ReceivingSection.issealed_text}'
                                            WHERE 
                                              receiving_section_id='{context.ReceivingSection.receiving_section_id}';";

                using (var connection = GetSqlConnection())
                {
                    connection.Open();
                    var transaction = connection.BeginTransaction();
                    try
                    {
                        using (var command = GetSqlCommandWithTransaction(receivingSectionSql, transaction))
                        {
                            var recordsUpdated = command.ExecuteNonQuery();
                            if (recordsUpdated == 1)
                            {
                                var relatedDataSql = GetRelatedDataSql(context, false);

                                using (var command1 = GetSqlCommandWithTransaction(relatedDataSql, transaction))
                                {
                                    command1.CommandText = relatedDataSql;
                                    command1.ExecuteNonQuery();
                                }
                            }
                        }
                        transaction.Commit();
                        connection.Close();
                        isReceivingSectionSaved = new Tuple<bool, string>(true, "Recieving Section Saved Successfully");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            return isReceivingSectionSaved;
        }

        public List<ReceivingSectionList1> LoadReceivingSectionDetails()
        {
            List<ReceivingSectionList1> receivingSectionList = new List<ReceivingSectionList1>();
            //var query = @"SELECT 
            //              R.receiving_section_id, 
            //              R.exhibit_from, 
            //              L.type_of_liquor_name, 
            //              S.liquor_sub_name, 
            //              SM.size_master_name, 
            //              R.quantity, 
            //              B.brand_master_name, 
            //              R.batch_no, 
            //              R.address, 
            //              C.compactor_name, 
            //              R.issaved 
            //            FROM 
            //              exciseautomation.receiving_section R 
            //              LEFT JOIN exciseautomation.type_of_liquor L ON L.type_of_liquor_id = R.type_of_liquor_id 
            //              LEFT JOIN exciseautomation.liquor_sub_type S ON S.liquor_sub_type_id = R.liquor_sub_type_id 
            //              LEFT JOIN exciseautomation.size_master SM ON SM.size_master_id = R.size_master_id 
            //              LEFT JOIN exciseautomation.brand_master B ON B.brand_master_id = R.brand_master_id 
            //              LEFT JOIN exciseautomation.compactor C ON C.compactor_id = R.compactor_id 
            //              ORDER BY R.receiving_section_id DESC";
            var query = $@"SELECT 
                          A.quant_received_id, 
                          A.quantity,
                          A.batch_no,
                          A.address,
                          A.status,
                          B.liq_type,
                          C.liq_sub_type_name,
                          D.size_name,
                          E.brand_name,
                          F.comp_id,
                          G.form_no
                        FROM 
                          exciseautomation.tab_quant_received A
                          LEFT JOIN exciseautomation.tab_liq_type B ON A.liq_type_id=B.liq_id
                          LEFT JOIN exciseautomation.tab_liq_subtype C ON A.sub_type_id=C.liq_sub_type_id
                          LEFT JOIN exciseautomation.tab_liq_size D ON A.size_id=D.size_id
                          LEFT JOIN exciseautomation.tab_liq_brand_name E ON A.brand_name_id=E.brand_id
                          LEFT JOIN exciseautomation.tab_compactor F ON A.compactor_id=F.comp_id
                          LEFT JOIN exciseautomation.tab_quant_form_no G ON A.quant_received_id=G.quant_received_id
                          WHERE (A.status='retest' OR A.status='untested') and A.Compactor_id in (select Compactor_id from exciseautomation.tab_compactor_id_tech  )
                          ORDER BY A.quant_received_id DESC";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ReceivingSectionList1 receivingSection = MapReceivingSectionList1(dr);
                        receivingSectionList.Add(receivingSection);
                    }
                }
                command.Connection.Close();
            }
            return receivingSectionList;
        }


        public ReceivingSectionContext GetReceivingSectionDetailById(string receivingSectionId, string exhibitFrom)
        {
            ReceivingSectionContext receivingSectionContext = new ReceivingSectionContext();
            var query = $@"SELECT * 
                            FROM exciseautomation.receiving_section  
                            WHERE receiving_section_id={receivingSectionId} LIMIT 1";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        receivingSectionContext.ReceivingSection = MapReceivingSection(dr);
                    }
                }
                command.Connection.Close();
            }
            switch (exhibitFrom)
            {
                case "police":
                    query = $"Select * from exciseautomation.police_receiving where receiving_section_id={receivingSectionContext?.ReceivingSection.receiving_section_id} LIMIT 1";
                    receivingSectionContext.PoliceReceiving = GetPoliceReceiving(query);
                    break;
                case "excise":
                    query = $"Select * from exciseautomation.excise_receiving where receiving_section_id={receivingSectionContext?.ReceivingSection.receiving_section_id} LIMIT 1";
                    receivingSectionContext.ExciseReceiving = GetExciseReceiving(query);
                    break;
                default:
                    query = $"Select * from exciseautomation.distillery_receiving WHERE receiving_section_id={receivingSectionContext?.ReceivingSection.receiving_section_id} LIMIT 1";
                    receivingSectionContext.DistilleryReceiving = GetDistilleryReceiving(query);
                    break;
            }
            return receivingSectionContext;
        }
        #endregion

        #region Lookups
        public IDictionary<string, string> TypesList(string type)
        {
            if (type == "police")
                return new Dictionary<string, string>()
            {
                {"GRP","GRP" },
                {"Others","Others" },
                {"Police","Police" },
                {"SSB","SSB" },
            };
            else
                return new Dictionary<string, string>()
            {
                {"Excise","Excise" }
            };
        }

        public IDictionary<string, string> PoliceOfficerDesignation(string type)
        {
            IDictionary<string, string> keyValuePairs = default(Dictionary<string, string>);
            switch (type)
            {
                case "excise":
                    keyValuePairs = new Dictionary<string, string>()
                                    {
                                        {"Excise Assistant Commissioner","Excise Assistant Commissioner" },
                                        {"Excise Asst. Sub Inspector","Excise Asst. Sub Inspector" },
                                        {"Excise Dupty Commissioner","Excise Dupty Commissioner" },
                                        {"Excise Inspector","Excise Inspector" },
                                        {"Excise Sub Inspector","Excise Sub Inspector" },
                                        {"Excise Supritendent","Excise Supritendent" },
                                    };
                    break;
                case "distillery":
                    keyValuePairs = new Dictionary<string, string>()
                                    {
                                        {"Excise Officer Incharge","Excise Officer Incharge" },
                                        {"Excise Supritendent","Excise Supritendent" },
                                    };
                    break;
                default:
                    keyValuePairs = new Dictionary<string, string>()
                                    {
                                        {"Asst. Sub Inspector","Asst. Sub Inspector" },
                                        {"Inspector","Inspector" },
                                        {"Op incharge","Op incharge" },
                                        {"Sub Inspector","Sub Inspector" },
                                    };
                    break;
            }
            return keyValuePairs;
        }

        public IDictionary<int, string> Compactors()
        {
            IDictionary<int, string> compactors = new Dictionary<int, string>();
            string query = "select * from exciseautomation.tab_compactor";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        compactors.Add(GetInt(dr, "comp_id"), GetString(dr, "comp_name"));
                    }
                }
                command.Connection.Close();
            }
            return compactors;
        }

        #endregion

        #region Private Methods
        private static string GetRelatedDataSql(ReceivingSectionContext context, bool isAddMode)
        {
            var relatedDataSql = string.Empty;
            switch (context.ReceivingSection?.exhibit_from)
            {
                case "police":
                    relatedDataSql = $@"INSERT INTO exciseautomation.police_receiving(
	                                            receiving_section_id, district_code, thana_master_id, police_type,
                                                designation, fir_no, fir_date, court_order, court_order_text, fir_copy, seizure_list, seizure_list_text)
	                                     VALUES ('{context.ReceivingSection?.receiving_section_id}', '{context.PoliceReceiving?.district_code}', 
                                                 '{context.PoliceReceiving?.thana_master_id}', '{context.PoliceReceiving?.police_type}', 
                                                 '{context.PoliceReceiving?.designation}', '{context.PoliceReceiving?.fir_no}', '{context.PoliceReceiving?.fir_date}', 
                                                 '{context.PoliceReceiving?.court_order}', '{context.PoliceReceiving?.court_order_text}', 
                                                 '{context.PoliceReceiving?.fir_copy}', '{context.PoliceReceiving?.seizure_list}',
                                                 '{context.PoliceReceiving?.seizure_list_text}');";
                    if (!isAddMode)
                        relatedDataSql = $@"UPDATE 
                                          exciseautomation.police_receiving 
                                        SET    
                                          district_code = '{context.PoliceReceiving?.district_code}', 
                                          thana_master_id = '{context.PoliceReceiving?.thana_master_id}', 
                                          police_type = '{context.PoliceReceiving?.police_type}', 
                                          designation = '{context.PoliceReceiving?.designation}', 
                                          fir_no = '{context.PoliceReceiving?.fir_no}', 
                                          fir_date = '{context.PoliceReceiving?.fir_date}', 
                                          court_order = '{context.PoliceReceiving?.court_order}', 
                                          court_order_text = '{context.PoliceReceiving?.court_order_text}', 
                                          fir_copy ='{context.PoliceReceiving?.fir_copy}', 
                                          seizure_list = '{context.PoliceReceiving?.seizure_list}', 
                                          seizure_list_text = '{context.PoliceReceiving?.seizure_list_text}' 
                                        WHERE 
                                         receiving_section_id = '{context.ReceivingSection?.receiving_section_id}';";
                    break;
                case "excise":
                    relatedDataSql = $@"INSERT INTO exciseautomation.excise_receiving(
	                                    receiving_section_id, district_code, excise_type, designation, case_no, case_date, remark, prno, statevs)
	                                    VALUES ('{context.ReceivingSection?.receiving_section_id}', '{context.ExciseReceiving?.district_code}', 
                                                '{context.ExciseReceiving?.excise_type}', '{context.ExciseReceiving?.designation}', '{context.ExciseReceiving?.case_no}', 
                                                '{context.ExciseReceiving?.case_date}', '{context.ExciseReceiving?.remark}', '{context.ExciseReceiving?.prno}', 
                                                '{context.ExciseReceiving?.statevs}');";
                    if (!isAddMode)
                        relatedDataSql = $@"UPDATE 
                                              exciseautomation.excise_receiving 
                                            SET   
                                              district_code = '{context.ExciseReceiving?.district_code}', 
                                              excise_type = '{context.ExciseReceiving?.excise_type}', 
                                              designation = '{context.ExciseReceiving?.designation}', 
                                              case_no = '{context.ExciseReceiving?.case_no}', 
                                              case_date ='{context.ExciseReceiving?.case_date}', 
                                              remark = '{context.ExciseReceiving?.remark}', 
                                              prno = '{context.ExciseReceiving?.prno}', 
                                              statevs = '{context.ExciseReceiving?.statevs}'
                                            WHERE 
                                               receiving_section_id = '{context.ReceivingSection?.receiving_section_id}';";
                    break;
                default:
                    relatedDataSql = $@"INSERT INTO exciseautomation.distillery_receiving(
	                                    receiving_section_id, district_code, distillery_code, designation, vat_no, denatured_date, remark)
	                                    VALUES ('{context.ReceivingSection?.receiving_section_id}', '{context.DistilleryReceiving?.district_code}', 
                                                '{context.DistilleryReceiving?.distillery_code}', '{context.DistilleryReceiving?.designation}', 
                                                '{context.DistilleryReceiving?.vat_no}', '{context.DistilleryReceiving?.denatured_date}', '{context.DistilleryReceiving?.remark}');";
                    if (!isAddMode)
                        relatedDataSql = $@"UPDATE 
                                          exciseautomation.distillery_receiving 
                                        SET  
                                          district_code = '{context.DistilleryReceiving?.district_code}', 
                                          distillery_code = '{context.DistilleryReceiving?.distillery_code}', 
                                          designation = '{context.DistilleryReceiving?.designation}', 
                                          vat_no = '{context.DistilleryReceiving?.vat_no}', 
                                          denatured_date = '{context.DistilleryReceiving?.denatured_date}', 
                                          remark = '{context.DistilleryReceiving?.remark}' 
                                        WHERE 
                                         receiving_section_id = '{context.ReceivingSection?.receiving_section_id}';";
                    break;
            }
            return relatedDataSql;
        }

        private ReceivingSectionList MapReceivingSectionList(NpgsqlDataReader dr)
        {
            return new ReceivingSectionList()
            {
                receiving_section_id = GetInt(dr, nameof(ReceivingSectionList.receiving_section_id)),
                exhibit_from = GetString(dr, nameof(ReceivingSectionList.exhibit_from)),
                type_of_liquor_name = GetString(dr, nameof(ReceivingSectionList.type_of_liquor_name)),
                liquor_sub_name = GetString(dr, nameof(ReceivingSectionList.liquor_sub_name)),
                size_master_name = GetString(dr, nameof(ReceivingSectionList.size_master_name)),
                quantity = GetString(dr, nameof(ReceivingSectionList.quantity)),
                brand_master_name = GetString(dr, nameof(ReceivingSectionList.brand_master_name)),
                batch_no = GetString(dr, nameof(ReceivingSectionList.batch_no)),
                address = GetString(dr, nameof(ReceivingSectionList.address)),
                compactor_name = GetString(dr, nameof(ReceivingSectionList.compactor_name)),
                issaved = GetBoolean(dr, nameof(ReceivingSectionList.issaved))
            };
        }
        //20220301
        private ReceivingSectionList1 MapReceivingSectionList1(NpgsqlDataReader dr)
        {
            return new ReceivingSectionList1()
            {

                quant_received_id = GetInt(dr, nameof(ReceivingSectionList1.quant_received_id)),
                form_no = GetInt(dr, nameof(ReceivingSectionList1.form_no)),
                liq_type = GetString(dr, nameof(ReceivingSectionList1.liq_type)),
                liq_sub_type_name = GetString(dr, nameof(ReceivingSectionList1.liq_sub_type_name)),
                //size_master_name = GetString(dr, nameof(ReceivingSectionList1.size_master_name)),
                quantity = GetString(dr, nameof(ReceivingSectionList1.quantity)),
                brand_name = GetString(dr, nameof(ReceivingSectionList1.brand_name)),
                batch_no = GetString(dr, nameof(ReceivingSectionList1.batch_no)),
                address = GetString(dr, nameof(ReceivingSectionList1.address)),
                comp_id = GetInt(dr, nameof(ReceivingSectionList1.comp_id)),
                status = GetString(dr, nameof(ReceivingSectionList1.status))
            };
        }


        private DistilleryReceiving GetDistilleryReceiving(string query)
        {
            DistilleryReceiving distilleryReceiving = new DistilleryReceiving();
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        distilleryReceiving = new DistilleryReceiving()
                        {
                            distillery_receiving_id = GetInt(dr, nameof(DistilleryReceiving.distillery_receiving_id)),
                            distillery_code = GetString(dr, nameof(DistilleryReceiving.distillery_code)),
                            district_code = GetString(dr, nameof(DistilleryReceiving.district_code)),
                            designation = GetString(dr, nameof(DistilleryReceiving.designation)),
                            vat_no = GetString(dr, nameof(DistilleryReceiving.vat_no)),
                            denatured_date = GetDateTime(dr, nameof(DistilleryReceiving.denatured_date)),
                            remark = GetString(dr, nameof(DistilleryReceiving.remark)),
                        };
                    }
                }
                command.Connection.Close();
            }
            return distilleryReceiving;
        }

        private ExciseReceiving GetExciseReceiving(string query)
        {
            ExciseReceiving exciseReceiving = new ExciseReceiving();
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        exciseReceiving = new ExciseReceiving()
                        {
                            excise_receiving_id = GetInt(dr, nameof(ExciseReceiving.excise_receiving_id)),
                            district_code = GetString(dr, nameof(ExciseReceiving.district_code)),
                            designation = GetString(dr, nameof(ExciseReceiving.designation)),
                            case_no = GetString(dr, nameof(ExciseReceiving.case_no)),
                            case_date = GetDateTime(dr, nameof(ExciseReceiving.case_date)),
                            remark = GetString(dr, nameof(ExciseReceiving.remark)),
                            prno = GetString(dr, nameof(ExciseReceiving.prno)),
                            statevs = GetString(dr, nameof(ExciseReceiving.statevs)),
                            excise_type = GetString(dr, nameof(ExciseReceiving.excise_type)),
                        };
                    }
                }
                command.Connection.Close();
            }
            return exciseReceiving;
        }

        private PoliceReceiving GetPoliceReceiving(string query)
        {
            PoliceReceiving policeReceiving = new PoliceReceiving();
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        policeReceiving = new PoliceReceiving()
                        {
                            police_receiving_id = GetInt(dr, nameof(PoliceReceiving.police_receiving_id)),
                            district_code = GetString(dr, nameof(PoliceReceiving.district_code)),
                            thana_master_id = GetInt(dr, nameof(PoliceReceiving.thana_master_id)),
                            police_type = GetString(dr, nameof(PoliceReceiving.police_type)),
                            designation = GetString(dr, nameof(PoliceReceiving.designation)),
                            fir_no = GetString(dr, nameof(PoliceReceiving.fir_no)),
                            fir_date = GetDateTime(dr, nameof(PoliceReceiving.fir_date)),
                            court_order = GetBoolean(dr, nameof(PoliceReceiving.court_order)),
                            court_order_text = GetString(dr, nameof(PoliceReceiving.court_order_text)),
                            fir_copy = GetBoolean(dr, nameof(PoliceReceiving.fir_copy)),
                            seizure_list = GetBoolean(dr, nameof(PoliceReceiving.seizure_list)),
                            seizure_list_text = GetString(dr, nameof(PoliceReceiving.seizure_list_text)),
                        };
                    }
                }
                command.Connection.Close();
            }
            return policeReceiving;
        }

        private Entities.ReceivingSection MapReceivingSection(NpgsqlDataReader dr)
        {
            return new Entities.ReceivingSection()
            {
                receiving_section_id = GetInt(dr, nameof(Entities.ReceivingSection.receiving_section_id)),
                type_of_liquor_id = GetInt(dr, nameof(Entities.ReceivingSection.type_of_liquor_id)),
                liquor_sub_type_id = GetInt(dr, nameof(Entities.ReceivingSection.liquor_sub_type_id)),
                size_master_id = GetInt(dr, nameof(Entities.ReceivingSection.size_master_id)),
                brand_master_id = GetInt(dr, nameof(Entities.ReceivingSection.brand_master_id)),
                compactor_id = GetInt(dr, nameof(Entities.ReceivingSection.compactor_id)),
                receiving_date = GetDateTime(dr, nameof(Entities.ReceivingSection.receiving_date)),
                letter_no = GetString(dr, nameof(Entities.ReceivingSection.letter_no)),
                letter_date = GetDateTime(dr, nameof(Entities.ReceivingSection.letter_date)),
                exhibit_from = GetString(dr, nameof(Entities.ReceivingSection.exhibit_from)),
                is_sealed = GetBoolean(dr, "sealed"),
                quantity = GetString(dr, nameof(Entities.ReceivingSection.quantity)),
                batch_no = GetString(dr, nameof(Entities.ReceivingSection.batch_no)),
                address = GetString(dr, nameof(Entities.ReceivingSection.address)),
                issaved = GetBoolean(dr, nameof(Entities.ReceivingSection.issaved)),
                issealed_text = GetString(dr, nameof(Entities.ReceivingSection.issealed_text)),
            };
        }
        #endregion
    }
}
