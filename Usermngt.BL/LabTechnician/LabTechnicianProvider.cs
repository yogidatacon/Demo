using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.Entities;
using Usermngt.BL.DataUtility;

namespace Usermngt.BL.LabTechnician
{
    public class LabTechnicianProvider : BaseDataLayer,ILabTechnicianService
    {
        #region Methods
        public List<LabTechnicianList> LoadLabTechnicianDetails()
        {
            List<LabTechnicianList> labTechnicianList = new List<LabTechnicianList>();
            var query = @"SELECT 
                          R.receiving_section_id, 
                          R.exhibit_from, 
                          L.type_of_liquor_name, 
                          S.liquor_sub_name, 
                          SM.size_master_name, 
                          R.quantity, 
                          B.brand_master_name, 
                          R.batch_no, 
                          R.address, 
                          C.compactor_name, 
                          LR.status,
                          LR.lab_report_id
                        FROM 
                          exciseautomation.receiving_section R 
                          LEFT JOIN exciseautomation.type_of_liquor L ON L.type_of_liquor_id = R.type_of_liquor_id 
                          LEFT JOIN exciseautomation.lab_report LR ON LR.receiving_section_id = R.receiving_section_id
                          LEFT JOIN exciseautomation.liquor_sub_type S ON S.liquor_sub_type_id = R.liquor_sub_type_id 
                          LEFT JOIN exciseautomation.size_master SM ON SM.size_master_id = R.size_master_id 
                          LEFT JOIN exciseautomation.brand_master B ON B.brand_master_id = R.brand_master_id 
                          LEFT JOIN exciseautomation.compactor C ON C.compactor_id = R.compactor_id
                          Where R.issaved=true
                          ORDER BY R.receiving_section_id DESC";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        LabTechnicianList labTechnician = MapLabTechnicianList(dr);
                        labTechnicianList.Add(labTechnician);
                    }
                }
                command.Connection.Close();
            }
            return labTechnicianList;
        }

        public LabTechReport GetLabTechReportDetailById(string receivingSectionId, string exhibitFrom)
        {
            LabTechReport labtechreport = new LabTechReport();
            var query = $@"SELECT 
                          R.receiving_section_id, 
                          R.exhibit_from, 
                          L.type_of_liquor_name, 
                          S.liquor_sub_name, 
                          SM.size_master_name, 
                          R.quantity, 
                          B.brand_master_name, 
                          R.batch_no, 
                          R.address, 
                          R.compactor_id, 
                          LR.status,
                          LR.lab_report_id,
                          R.letter_no,
                          R.letter_date,
                          R.receiving_date
                        FROM 
                          exciseautomation.receiving_section R 
                          LEFT JOIN exciseautomation.type_of_liquor L ON L.type_of_liquor_id = R.type_of_liquor_id 
                          LEFT JOIN exciseautomation.lab_report LR ON LR.receiving_section_id = R.receiving_section_id
                          LEFT JOIN exciseautomation.liquor_sub_type S ON S.liquor_sub_type_id = R.liquor_sub_type_id 
                          LEFT JOIN exciseautomation.size_master SM ON SM.size_master_id = R.size_master_id 
                          LEFT JOIN exciseautomation.brand_master B ON B.brand_master_id = R.brand_master_id 
                          Where R.receiving_section_id='{receivingSectionId}'";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        labtechreport = MapLabtechreport(dr);
                    }
                    switch (exhibitFrom)
                    {
                        case "Excise":
                            var excise_receiving = $"Select remark from exciseautomation.excise_receiving where receiving_section_id={receivingSectionId} LIMIT 1";
                            using (var receivingCommnad = GetSqlCommand(excise_receiving))
                            {
                                receivingCommnad.Connection.Open();
                                NpgsqlDataReader dp = receivingCommnad.ExecuteReader();
                                while (dp.Read())
                                {
                                    labtechreport.remark = GetString(dp, nameof(Entities.LabTechReport.remark));
                                }
                                receivingCommnad.Connection.Close();
                            }
                            break;
                        default:
                            var distilleryRemark = $"Select remark from exciseautomation.distillery_receiving WHERE receiving_section_id={receivingSectionId} LIMIT 1";
                            using (var receivingCommnad = GetSqlCommand(distilleryRemark))
                            {
                                receivingCommnad.Connection.Open();
                                NpgsqlDataReader ds = receivingCommnad.ExecuteReader();
                                while (ds.Read())
                                {
                                    labtechreport.remark = GetString(ds, nameof(Entities.LabTechReport.remark));
                                }
                                receivingCommnad.Connection.Close();
                            }
                            break;
                    }
                    command.Connection.Close();
                }
            }
            return labtechreport;
        }

        public Tuple<bool, string> SaveLabTechReport(LabTechReportContext context)
        {
            Tuple<bool, string> isLabTechReportSaved = default(Tuple<bool, string>);

            string labReportSql = $@" UPDATE 
                                              exciseautomation.lab_report 
                                            SET
                                                smell= '{context.smell}',
                                                proofstrength1= '{context.proofstrength1}',
                                                proofstrength2= '{context.proofstrength2}',
                                                color= '{context.color}',
                                                remarks1= '{context.remarks1}',
                                                deviceused= '{context.deviceused}',
                                                hydrometerindication= '{context.hydrometerindication}',
                                                hydrometertempearature= '{context.hydrometertempearature}',
                                                pyknomenterweight= '{context.pyknomenterweight}',
                                                dmpyknometerweight= '{context.dmpyknometerweight}',
                                                samplepyknometerweight= '{context.samplepyknometerweight}',
                                                pyknomentertemperature= '{context.pyknomentertemperature}',
                                                passtestaceticacid= '{context.passtestaceticacid}',
                                                passtestresidue= '{context.passtestresidue}',
                                                passtestmethylalcohol= '{context.passtestmethylalcohol}',
                                                passtestamylalcohol= '{context.passtestamylalcohol}',
                                                passtestfurfural= '{context.passtestfurfural}',
                                                passtestethylacetate= '{context.passtestethylacetate}',
                                                passtestcopper= '{context.passtestcopper}',
                                                passtestaldehyes= '{context.passtestaldehyes}',
                                                passtestbyvalueaceticacid= '{context.passtestbyvalueaceticacid}',
                                                passtestbyvalueresidue= '{context.passtestbyvalueresidue}',
                                                passtestbyvaluemethylalcohol= '{context.passtestbyvaluemethylalcohol}',
                                                passtestbyvalueamylalcohol= '{context.passtestbyvalueamylalcohol}',
                                                passtestbyvaluefurfural= '{context.passtestbyvaluefurfural}',
                                                passtestbyvalueethylacetate= '{context.passtestbyvalueethylacetate}',
                                                passtestbyvaluecopper= '{context.passtestbyvaluecopper}',
                                                passtestbyvaluealdehyes= '{context.passtestbyvaluealdehyes}',
                                                testresult= '{context.testresult}',
                                                remarks2= '{context.remarks2}',
                                                status= '{context.status}',
                                                created_on='{DateTime.Now}',
                                                created_by='{context.created_by}'
                                                WHERE 
                                              receiving_section_id='{context.receiving_section_id}'";
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(labReportSql, transaction))
                    {
                        var recordsUpdated = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    isLabTechReportSaved = new Tuple<bool, string>(true, "Recieving Section Saved Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //throw ex;
                    isLabTechReportSaved = new Tuple<bool, string>(false, labReportSql);
                }
            }

            return isLabTechReportSaved;
        }
        #endregion
        #region Private Methods
        private LabTechnicianList MapLabTechnicianList(NpgsqlDataReader dr)
        {
            return new LabTechnicianList()
            {
                receiving_section_id = GetInt(dr, nameof(LabTechnicianList.receiving_section_id)),
                exhibit_from = GetString(dr, nameof(LabTechnicianList.exhibit_from)),
                type_of_liquor_name = GetString(dr, nameof(LabTechnicianList.type_of_liquor_name)),
                liquor_sub_name = GetString(dr, nameof(LabTechnicianList.liquor_sub_name)),
                size_master_name = GetString(dr, nameof(LabTechnicianList.size_master_name)),
                quantity = GetString(dr, nameof(LabTechnicianList.quantity)),
                brand_master_name = GetString(dr, nameof(LabTechnicianList.brand_master_name)),
                batch_no = GetString(dr, nameof(LabTechnicianList.batch_no)),
                address = GetString(dr, nameof(LabTechnicianList.address)),
                compactor_name = GetString(dr, nameof(LabTechnicianList.compactor_name)),
                status = GetString(dr, nameof(LabTechnicianList.status)),
                lab_report_id= GetInt(dr, nameof(LabTechnicianList.lab_report_id))
            };
        }

        private Entities.LabTechReport MapLabtechreport(NpgsqlDataReader dr)
        {
            return new Entities.LabTechReport()
            {
                receiving_section_id = GetInt(dr, nameof(Entities.LabTechReport.receiving_section_id)),
                receiving_date = GetDateTime(dr, nameof(Entities.LabTechReport.receiving_date)),
                letter_no = GetString(dr, nameof(Entities.LabTechReport.letter_no)),
                letter_date = GetDateTime(dr, nameof(Entities.LabTechReport.letter_date)),
                exhibit_from = GetString(dr, nameof(Entities.LabTechReport.exhibit_from)),
                quantity = GetString(dr, nameof(Entities.LabTechReport.quantity)),
                batch_no = GetString(dr, nameof(Entities.LabTechReport.batch_no)),
                address = GetString(dr, nameof(Entities.LabTechReport.address)),
                status = GetString(dr, nameof(LabTechReport.status)),
                lab_report_id = GetInt(dr, nameof(LabTechReport.lab_report_id)),
                type_of_liquor_name = GetString(dr, nameof(LabTechReport.type_of_liquor_name)),
                liquor_sub_name = GetString(dr, nameof(LabTechReport.liquor_sub_name)),
                size_master_name = GetString(dr, nameof(LabTechReport.size_master_name)),
                brand_master_name = GetString(dr, nameof(LabTechReport.brand_master_name)),
                compactor_id = GetInt(dr, nameof(Entities.LabTechReport.compactor_id))
            };
        }
        #endregion
    }
}
