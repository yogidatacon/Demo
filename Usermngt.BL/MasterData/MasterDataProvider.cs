using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Usermngt.BL.DataUtility;
using Usermngt.BL.Service;
using Usermngt.DAL.Utilities;
using Usermngt.Entities;

namespace Usermngt.BL.MasterData
{
    public class MasterDataProvider : BaseDataLayer, IMasterDataService
    {
        #region District Users
        public List<DistrictUser> DistrictUsers(string userId)
        {
            List<DistrictUser> districtUsers = new List<DistrictUser>();
            try
            {

                //Bhavin

                //string query = @"select *,dept.department_name,dist.district_name from exciseautomation.district_login as district
                //                INNER JOIN exciseautomation.department_master as dept ON dept.department_code=district.department_code
                //                INNER JOIN exciseautomation.district_master as dist ON dist.district_code=district.district_code";

                //old working
                //string query = @"select *,dept.department_name,dist.district_name from exciseautomation.district_login as district
                //                INNER JOIN exciseautomation.department_master as dept ON dept.department_master_id=district.department_id
                //                INNER JOIN exciseautomation.district_master as dist ON dist.district_master_id=district.dist_id
                //                order by dist.district_name,dept.department_name asc";


                string query = @"select *,dept.department_name,dist.district_name from exciseautomation.district_login as district
                                INNER JOIN exciseautomation.department_master as dept ON dept.department_master_id=district.department_id
                                INNER JOIN exciseautomation.district_master as dist ON dist.tab_district_id=district.dist_id
                                order by dist.district_name,dept.department_name asc";


                //string query = @"select *,dept.department_name,dist.district_name from exciseautomation.district_master as district
                //                INNER JOIN exciseautomation.department_master as dept ON dept.department_code=district.department_code
                //                INNER JOIN exciseautomation.district_master as dist ON dist.district_code=district.district_code";

                //End

                using (var command = GetSqlCommand(query))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            DistrictUser districtUser = MapDistrictUser(dr);
                            districtUsers.Add(districtUser);
                        }
                    }
                    command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return districtUsers;
        }

        public Tuple<bool, string> SaveDistrictUser(DistrictUser districtUser)
        {
            Tuple<bool, string> isDistrictSaveSuccessful = default(Tuple<bool, string>);
            const int access_type_code = Constants.DistrictUserAccessCode;
            var isUserExists = CheckWhetherUserAlreadyExists(districtUser);
            //string districtUserLoginQuery = $@"INSERT INTO exciseautomation.district_login (district_code,department_code,user_id,user_password,email_id,email_id2,email_id3,
            //                                                                                mobile_no,mobile_no2,mobile_no3,full_name,created_on,created_by) 
            //                                 VALUES ('{districtUser.district_code}','{districtUser.department_code}','{districtUser.user_id}','{districtUser.user_password}',
            //                                         '{districtUser.email_id}','{districtUser.email_id2}','{districtUser.email_id3}',
            //                                         '{districtUser.mobile_no}','{districtUser.mobile_no2}','{districtUser.mobile_no3}',
            //                                         '{districtUser.full_name}','{DateTime.Now}','{districtUser.created_by}')";




            string districtUserLoginQuery = $@"INSERT INTO exciseautomation.district_login (dist_id,department_id,userid,upwd,emailid,
                                                                                            mobileno,fullname,dist_name) 
                                             VALUES ('{districtUser.district_code}','{districtUser.department_code}','{districtUser.user_id}','{districtUser.user_password}',
                                                     '{districtUser.email_id + ',' + districtUser.email_id2 + ',' + districtUser.email_id3}',
                                                     '{Convert.ToString(districtUser.mobile_no) + ',' + Convert.ToString(districtUser.mobile_no2) + ',' + Convert.ToString(districtUser.mobile_no3) }',
                                                     '{districtUser.full_name}', '{districtUser.district_name}')";


            //string user_registrationQuery = $@"INSERT INTO exciseautomation.user_registration (access_type_code,district_code,email_id,mobile,user_password,user_id,record_status)
            //                                VALUES ('{access_type_code}','{districtUser.district_code}','{districtUser.email_id}','{districtUser.mobile_no}',
            //                                        '{EncryptionDecryption.Encrypt(districtUser.user_password)}',
            //                                        '{districtUser.user_id}','{1}')";
            if (isUserExists)
            {
                isDistrictSaveSuccessful = new Tuple<bool, string>(default(bool), $"User {districtUser.user_id} for district {districtUser.district_code} already exists");
                return isDistrictSaveSuccessful;
            }
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(districtUserLoginQuery, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                        if (rowAffected > default(int))
                        {
                            using (var command1 = GetSqlCommandWithTransaction(districtUserLoginQuery, transaction))
                            {
                                //command1.CommandText = user_registrationQuery;
                                //command1.ExecuteNonQuery();
                            }
                        }
                    }
                    transaction.Commit();
                    connection.Close();
                    isDistrictSaveSuccessful = new Tuple<bool, string>(true, "District User Saved Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isDistrictSaveSuccessful;
        }

        public Tuple<bool, string> UpdateDistrictUser(DistrictUser districtUser)
        {
            Tuple<bool, string> isDistrictSaveSuccessful = default(Tuple<bool, string>);

            //Bhavin
            //string districtUserLoginQuery = $@"UPDATE exciseautomation.district_login SET user_password='{districtUser.user_password}',full_name='{districtUser.full_name}',
            //                                                       email_id='{districtUser.email_id}',email_id2='{districtUser.email_id2}',email_id3='{districtUser.email_id3}',
            //                                                       mobile_no='{districtUser.mobile_no}',mobile_no2='{districtUser.mobile_no2}',mobile_no3='{districtUser.mobile_no3}'
            //                                                       ,updated_on='{DateTime.Now}',updated_by='{districtUser.created_by}'
            //                                                       WHERE user_id= '{districtUser.user_id}'";


            string districtUserLoginQuery = $@"UPDATE exciseautomation.district_login SET upwd='{districtUser.user_password}',fullname='{districtUser.full_name}',
                                                                   emailid='{districtUser.email_id + ',' + districtUser.email_id2 + ',' + districtUser.email_id3}',
                                                                   mobileno='{Convert.ToString(districtUser.mobile_no) + ',' + Convert.ToString(districtUser.mobile_no2) + ',' + Convert.ToString(districtUser.mobile_no3) }',
                                                                   userid='{districtUser.user_id}'
                                                                   WHERE district_log_id= '{districtUser.district_login_id}'";


            //string user_registrationQuery = $@"UPDATE exciseautomation.user_registration SET email_id='{districtUser.email_id}',mobile='{districtUser.mobile_no}',
            //                                                       user_password='{EncryptionDecryption.Encrypt(districtUser.user_password)}',user_id='{districtUser.user_id}'
            //                                                                  WHERE user_id= '{districtUser.user_id}'";

            //End

            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(districtUserLoginQuery, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                        if (rowAffected > default(int))
                        {
                            using (var command1 = GetSqlCommandWithTransaction(districtUserLoginQuery, transaction))
                            {
                                //Bhavin
                                // command1.CommandText = user_registrationQuery;
                                // command1.ExecuteNonQuery();

                                //End
                            }
                        }
                    }
                    transaction.Commit();
                    connection.Close();
                    isDistrictSaveSuccessful = new Tuple<bool, string>(true, "District User Updated Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isDistrictSaveSuccessful;
        }

        public DistrictUser LoadDistrictUserDetails(int district_userId)
        {
            var districtUser = new DistrictUser();
            //Bhavin
            //string query = $"select * from exciseautomation.district_login where district_login_id={district_userId} LIMIT 1";
            // string query = $"select * from exciseautomation.district_login where district_log_id={district_userId} LIMIT 1";

            //string query = $@"select *,dept.department_name,dist.district_name from exciseautomation.district_login as district
            //                    INNER JOIN exciseautomation.department_master as dept ON dept.department_master_id=district.department_id
            //                    INNER JOIN exciseautomation.district_master as dist ON dist.district_master_id=district.dist_id
            //                       where district.district_log_id='{district_userId}' LIMIT 1";

            string query = $@"select *,dept.department_name,dist.district_name from exciseautomation.district_login as district
                                INNER JOIN exciseautomation.department_master as dept ON dept.department_master_id=district.department_id
                                INNER JOIN exciseautomation.district_master as dist ON dist.tab_district_id=district.dist_id
                                   where district.district_log_id='{district_userId}' LIMIT 1";

            //End

            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        districtUser = MapDistrictUser(dr);
                    }
                }
                command.Connection.Close();
            }
            return districtUser;
        }

        #endregion

        #region Thana
        public List<Thana> ThanaListByDistrictCode(string district_code)
        {
            List<Thana> districtUsers = new List<Thana>();
            if (!string.IsNullOrWhiteSpace(district_code))
            {
                string query = $@"select *,D.dist_name from exciseautomation.tab_thana as T
                            INNER JOIN exciseautomation.tab_district as D ON D.dist_id=T.dist_id
                            WHERE D.dist_id='{district_code}'";

                //string query = $@"select *,D.district_name from exciseautomation.thana_master as T
                //              INNER JOIN exciseautomation.district_master as D ON D.district_code=T.district_code
                //              WHERE D.district_code='{district_code}' order by thana_name asc";

                using (var command = GetSqlCommand(query))
                {
                    command.Connection.Open();

                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Thana districtUser = MapThana(dr);
                            districtUsers.Add(districtUser);
                        }
                    }
                    command.Connection.Close();
                }
            }
            return districtUsers;
        }


        public List<Compactor> CompactorList(string userId)
        {
            List<Compactor> compactorUsers = new List<Compactor>();
            string query = @"select comptech.compactor_id, comptech.tech_id, Comp.comp_name, tech.user_id from exciseautomation.tab_compactor_id_tech as comptech
                                INNER JOIN exciseautomation.tab_compactor as comp ON comp.comp_id = comptech.compactor_id
                                INNER JOIN exciseautomation.user_registration as tech ON tech.user_registration_id = comptech.tech_id";

            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Compactor compactorUser = MapCompactor(dr);
                        compactorUsers.Add(compactorUser);
                    }
                }
                command.Connection.Close();
            }
            return compactorUsers;
        }

        public List<Thana> ThanaList(string userId)
        {
            List<Thana> districtUsers = new List<Thana>();
            string query = @"select *,D.dist_name from exciseautomation.tab_thana as T
                            INNER JOIN exciseautomation.tab_district as D ON D.dist_id=T.dist_id order by T.dist_id asc";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Thana districtUser = MapThana(dr);
                        districtUsers.Add(districtUser);
                    }
                }
                command.Connection.Close();
            }
            return districtUsers;
        }

        public Tuple<bool, string> SaveCompactor(Compactor compactor)
        {
            Tuple<bool, string> isCompactorSaveSuccessful = default(Tuple<bool, string>);
            bool isCompactorExists = CheckWhetherCompactorExists(compactor, true);


            string compactorMasterQuery = $@"INSERT INTO exciseautomation.tab_compactor_id_tech (compactor_id,tech_id) 
                                             VALUES ('{compactor.compactor_id}','{compactor.tech_id}')";

            if (isCompactorExists)
            {
                isCompactorSaveSuccessful = new Tuple<bool, string>(default(bool), $"Compactor: {compactor.compactor_id} already exists");
                return isCompactorSaveSuccessful;
            }

            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(compactorMasterQuery, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    isCompactorSaveSuccessful = new Tuple<bool, string>(true, "Compactor Saved Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isCompactorSaveSuccessful;
        }

        public Tuple<bool, string> UpdateCompactor(Compactor compactor)
        {
            Tuple<bool, string> isCompactorUpdated = default(Tuple<bool, string>);
            string updateCompactorQuery = $@"UPDATE exciseautomation.tab_compactor_id_tech SET  tech_id='{compactor.tech_id}'                                                                   
                                                                    WHERE compactor_id= '{compactor.compactor_id}'";

            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(updateCompactorQuery, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    isCompactorUpdated = new Tuple<bool, string>(true, "Compactor Updated Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isCompactorUpdated;
        }


        public Tuple<bool, string> SaveThana(Thana thana)
        {
            Tuple<bool, string> isDistrictSaveSuccessful = default(Tuple<bool, string>);
            bool isUserExists = CheckWhetherThanaExists(thana, true);
            string thanaMasterQuery = $@"INSERT INTO exciseautomation.tab_thana (dist_id,thana_name) 
                                             VALUES ('{thana.district_code}','{thana.thana_name}')";
            if (isUserExists)
            {
                isDistrictSaveSuccessful = new Tuple<bool, string>(default(bool), $"Thana: {thana.thana_name} for district {thana.district_code} already exists");
                return isDistrictSaveSuccessful;
            }
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(thanaMasterQuery, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    isDistrictSaveSuccessful = new Tuple<bool, string>(true, "Thana Saved Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isDistrictSaveSuccessful;
        }

        public Tuple<bool, string> UpdateThana(Thana thana)
        {
            Tuple<bool, string> isThanaUpdated = default(Tuple<bool, string>);
            string updateThanaQuery = $@"UPDATE exciseautomation.tab_thana SET  thana_name='{thana.thana_name}',
                                                                   dist_id='{thana.district_code}'
                                                                    WHERE thana_id= '{thana.thana_master_id}'";
            bool isUserExists = CheckWhetherThanaExists(thana, false);
            if (isUserExists)
            {
                isThanaUpdated = new Tuple<bool, string>(default(bool), $"Thana: {thana.thana_name} for district {thana.district_code} already exists for other record");
                return isThanaUpdated;
            }
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(updateThanaQuery, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    isThanaUpdated = new Tuple<bool, string>(true, "Thana Updated Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isThanaUpdated;
        }

        public Compactor LoadCompactorDetails(int compactor_id)
        {
            var compactor = new Compactor();
            string query = $"select * from exciseautomation.tab_compactor_id_tech where compactor_id={compactor_id} LIMIT 1";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        compactor = MapCompactorDetails(dr);
                    }
                }
                command.Connection.Close();
            }
            return compactor;
        }

        public Thana LoadThanaDetails(int thana_id)
        {
            var thana = new Thana();
            string query = $"select * from exciseautomation.tab_thana where thana_id={thana_id} LIMIT 1";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        thana = MapThanaWithoutDistrict(dr);
                    }
                }
                command.Connection.Close();
            }
            return thana;
        }
        public bool deleteCompactor(string compactor_id)
        {
            string deleteQuery = $"delete from exciseautomation.tab_compactor_id_tech where compactor_id='{compactor_id}'";

            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(deleteQuery, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // throw;
                    return false;
                }
            }
        }
        public bool deleteThana(string thana_id)
        {
            string deleteQuery = $"delete from exciseautomation.tab_thana where thana_id='{thana_id}'";
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(deleteQuery, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // throw;
                    return false;
                }
            }
        }
        #endregion

        #region Distillery
        public List<Distillery> DistilleryList(string userId)
        {
            List<Distillery> distilleries = new List<Distillery>();
            string query = @"SELECT * FROM exciseautomation.tab_distill ORDER BY distill_id ASC";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Distillery distillery = MapDistillery(dr);
                        distilleries.Add(distillery);
                    }
                }
                command.Connection.Close();
            }
            return distilleries;
        }

        public Tuple<bool, string> SaveDistillery(Distillery distillery)
        {
            Tuple<bool, string> isDistllerySaveSuccess = default(Tuple<bool, string>);
            bool isUserExists = checkWhetherDistilleryExists(distillery, true);
            string distilleryMasterQuery = $@"INSERT INTO exciseautomation.tab_distill (distill_id,distill_name) 
                                             VALUES ('{distillery.distillery_code}','{distillery.distillery_name}')";
            if (isUserExists)
            {
                isDistllerySaveSuccess = new Tuple<bool, string>(default(bool), $"Distillery: {distillery.distillery_name} or Distillery ID : {distillery.distillery_code} already exists");
                return isDistllerySaveSuccess;
            }
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(distilleryMasterQuery, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    isDistllerySaveSuccess = new Tuple<bool, string>(true, "Distillery Saved Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isDistllerySaveSuccess;
        }

        public Tuple<bool, string> UpdateDistillery(Distillery distillery)
        {
            Tuple<bool, string> isDistilleryUpdated = default(Tuple<bool, string>);
            string updateThanaQuery = $@"UPDATE exciseautomation.tab_distill SET  distill_name='{distillery.distillery_name}'
                                                                   WHERE distill_id='{distillery.distillery_code}'";
            bool isUserExists = checkWhetherDistilleryExists(distillery, false);
            if (isUserExists)
            {
                isDistilleryUpdated = new Tuple<bool, string>(default(bool), $"Distillery: {distillery.distillery_name} or Distillery ID : {distillery.distillery_code} already exists for other record");
                return isDistilleryUpdated;
            }
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(updateThanaQuery, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    isDistilleryUpdated = new Tuple<bool, string>(true, "Distillery Updated Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isDistilleryUpdated;
        }

        public Distillery LoadDistilleryDetails(int distillery_id)
        {
            var distillery = new Distillery();
            string query = $"select * from exciseautomation.tab_distill where distill_id={distillery_id} LIMIT 1";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        distillery = MapDistillery(dr);
                    }
                }
                command.Connection.Close();
            }
            return distillery;
        }
        #endregion

        #region Type of Liquor
        public List<TypeOfLiquor> TypeOfLiquorList(string userId)
        {
            List<TypeOfLiquor> typeofLiquors = new List<TypeOfLiquor>();
            //Bhavin
            //string query = @"select * from exciseautomation.tab_liq_type";
            string query = @"select * from exciseautomation.tab_liq_type order by liq_type asc";
            //string query = @"select * from exciseautomation.type_of_liquor order by type_of_liquor_name asc";

            //End

            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TypeOfLiquor typeOfLiquor = MapTypeOfLiquor(dr);
                        typeofLiquors.Add(typeOfLiquor);
                    }
                }
                command.Connection.Close();
            }
            return typeofLiquors;
        }

        public Tuple<bool, string> SaveTypeOfLiquor(TypeOfLiquor typeOfLiquor)
        {
            Tuple<bool, string> isTypeOfLiquorSaved = default(Tuple<bool, string>);
            bool isTypeofLiquorExists = checkWhetherDistilleryExists(typeOfLiquor, true);
            string typeOfSQL = $@"INSERT INTO exciseautomation.tab_liq_type (liq_type) 
                                             VALUES ('{typeOfLiquor.type_of_liquor_name}')";

            //string typeOfSQL = $@"INSERT INTO exciseautomation.type_of_liquor (type_of_liquor_name) 
            //                                 VALUES ('{typeOfLiquor.type_of_liquor_name}')";


            if (isTypeofLiquorExists)
            {
                isTypeOfLiquorSaved = new Tuple<bool, string>(default(bool), $"Type of Liquor: {typeOfLiquor.type_of_liquor_name} already exists");
                return isTypeOfLiquorSaved;
            }
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(typeOfSQL, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    isTypeOfLiquorSaved = new Tuple<bool, string>(true, "Type of Liquor Saved Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isTypeOfLiquorSaved;
        }

        public Tuple<bool, string> UpdateTypeOfLiquor(TypeOfLiquor typeofLiquor)
        {
            Tuple<bool, string> isTypeofLiquorUpdated = default(Tuple<bool, string>);
            string updateTypeofLiquorSql = $@"UPDATE exciseautomation.tab_liq_type SET  liq_type='{typeofLiquor.type_of_liquor_name}'
                                                                   WHERE liq_id= '{typeofLiquor.type_of_liquor_id}'";

            //string updateTypeofLiquorSql = $@"UPDATE exciseautomation.type_of_liquor SET  type_of_liquor_name='{typeofLiquor.type_of_liquor_name}'
            //                                                       WHERE type_of_liquor_id= '{typeofLiquor.type_of_liquor_id}'";


            bool isExists = checkWhetherDistilleryExists(typeofLiquor, false);
            if (isExists)
            {
                isTypeofLiquorUpdated = new Tuple<bool, string>(default(bool), $"Type of Liquor: {typeofLiquor.type_of_liquor_name} already exists for other record");
                return isTypeofLiquorUpdated;
            }
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(updateTypeofLiquorSql, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    isTypeofLiquorUpdated = new Tuple<bool, string>(true, "Type of Liquor Updated Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isTypeofLiquorUpdated;
        }

        public TypeOfLiquor LoadTypeOfLiquorDetails(int typeofLiquor_id)
        {
            var typeOfLiquor = new TypeOfLiquor();
            string query = $"select * from exciseautomation.tab_liq_type where liq_id={typeofLiquor_id} LIMIT 1";
            //string query = $"select * from exciseautomation.type_of_liquor where type_of_liquor_id={typeofLiquor_id} LIMIT 1";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //Bhavin 
                        //typeOfLiquor = MapTypeOfLiquor(dr);
                        typeOfLiquor = MapTypeOfLiquorList(dr);
                    }
                }
                command.Connection.Close();
            }
            return typeOfLiquor;
        }

        public bool deleteLiqType(string typeofLiquor_id)
        {
            string deleteQuery = $"delete from exciseautomation.tab_liq_type where liq_id='{typeofLiquor_id}'";
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(deleteQuery, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //throw;
                    return false;
                }
            }
        }
        #endregion

        #region Sub Liquor Type
        public List<SubLiquor> SubLiquorList(string userId)
        {
            List<SubLiquor> subLiquors = new List<SubLiquor>();
            //Bhavin
            //string query = @"select * from exciseautomation.tab_liq_subtype order by liq_id asc";
             string query = @"select * from exciseautomation.tab_liq_subtype order by liq_type, liq_sub_type_name asc";
            //string query = @"select s.liquor_sub_type_id, s.type_of_liquor_id, s.liquor_sub_name, l.type_of_liquor_name
            //                  from liquor_sub_type s inner join type_of_liquor l on s.type_of_liquor_id = l.type_of_liquor_id
            //                   order by s.liquor_sub_name";
            //End

            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        SubLiquor districtUser = MapSubLiquorList(dr);
                        subLiquors.Add(districtUser);
                    }
                }
                command.Connection.Close();
            }
            return subLiquors;
        }

        public List<SubLiquor> SubLiquorListByLiquorType(int type_of_liquor_id)
        {
            List<SubLiquor> subLiquors = new List<SubLiquor>();
            //Bhavin
            //string query = $@"select * from exciseautomation.tab_liq_subtype WHERE liq_id={type_of_liquor_id}";
            string query = $@"select * from exciseautomation.tab_liq_subtype WHERE liq_id={type_of_liquor_id} order by liq_sub_type_name";
           // string query = $@"select * from exciseautomation.liquor_sub_type WHERE type_of_liquor_id={type_of_liquor_id} order by liquor_sub_name";
            //End

            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        SubLiquor districtUser = MapSubLiquor(dr);
                        subLiquors.Add(districtUser);
                    }
                }
                command.Connection.Close();
            }
            return subLiquors;
        }



        public int GetliquorsubtypeId()
        {
            string query = @"select Max(liquor_sub_type_id) as liquor_sub_type_id from exciseautomation.liquor_sub_type";
            int Id = 0;

            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                       Id = dr.GetInt32(dr.GetOrdinal("liquor_sub_type_id"));
                    }
                }
                command.Connection.Close();
            }
            return Id;

        }



        public Tuple<bool, string> SaveSubLiquor(SubLiquor subLiquor)
        {
            Tuple<bool, string> isSubLiquorSaved = default(Tuple<bool, string>);
            bool isSubLiquorExists = CheckWhetherSubLiquorExists(subLiquor, true);
            int Id = GetliquorsubtypeId();


            string subLiquorSQL = $@"INSERT INTO exciseautomation.tab_liq_subtype (liq_id,liq_sub_type_name, liq_type) 
                                             VALUES ('{subLiquor.type_of_liquor_id}','{subLiquor.liquor_sub_name}','{subLiquor.type_of_liquor_name}')";

            //string subLiquorSQL = $@"INSERT INTO exciseautomation.liquor_sub_type (type_of_liquor_id,liquor_sub_name) 
            //                                 VALUES ('{subLiquor.type_of_liquor_id}','{subLiquor.liquor_sub_name}')";
            if (isSubLiquorExists)
            {
                isSubLiquorSaved = new Tuple<bool, string>(default(bool), $"Sub Liquor: {subLiquor.liquor_sub_name} for liquor code : {subLiquor.type_of_liquor_id} already exists");
                return isSubLiquorSaved;
            }
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(subLiquorSQL, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    isSubLiquorSaved = new Tuple<bool, string>(true, "Sub Liquor Saved Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isSubLiquorSaved;
        }

        public Tuple<bool, string> UpdateSubLiquor(SubLiquor subLiquor)
        {
            Tuple<bool, string> isSubLiquorUpdated = default(Tuple<bool, string>);
            string updateSubLiquorSQL = $@"UPDATE exciseautomation.tab_liq_subtype SET  liq_sub_type_name='{subLiquor.liquor_sub_name}',
                                                                   liq_id='{subLiquor.type_of_liquor_id}', 
                                                                   liq_type='{subLiquor.type_of_liquor_name}'
                                                                   WHERE liq_sub_type_id= '{subLiquor.liquor_sub_type_id}'";

            //string updateSubLiquorSQL = $@"UPDATE exciseautomation.liquor_sub_type SET  liquor_sub_name='{subLiquor.liquor_sub_name}',
            //                                                       type_of_liquor_id='{subLiquor.type_of_liquor_id}'

            //                                                       WHERE liquor_sub_type_id= '{subLiquor.liquor_sub_type_id}'";

            bool isSubLiquorExists = CheckWhetherSubLiquorExists(subLiquor, true);
            if (isSubLiquorExists)
            {
                isSubLiquorUpdated = new Tuple<bool, string>(default(bool), $"Sub Liquor: {subLiquor.liquor_sub_name} for liquor code : {subLiquor.type_of_liquor_id} already exists for other record");
                return isSubLiquorUpdated;
            }
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(updateSubLiquorSQL, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    isSubLiquorUpdated = new Tuple<bool, string>(true, "Sub Liquor Updated Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isSubLiquorUpdated;
        }

        public SubLiquor LoadSubLiquorDetails(int typeofLiquor_id)
        {
            var subLiquor = new SubLiquor();
            string query = $"select * from exciseautomation.tab_liq_subtype where liq_sub_type_id={typeofLiquor_id} LIMIT 1";
           // string query = $"select * from exciseautomation.liquor_sub_type where liquor_sub_type_id = {typeofLiquor_id} LIMIT 1";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //Bhavin 
                        // subLiquor = MapSubLiquor(dr);
                        subLiquor = MapSubLiquorList(dr);
                    }
                }
                command.Connection.Close();
            }
            return subLiquor;
        }

        public bool DeleteSubType(string sub_type_id)
        {
            string deleteQuery = $"delete from exciseautomation.tab_liq_subtype where liq_sub_type_id='{sub_type_id}'";
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(deleteQuery, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //throw;
                    return false;
                }
            }
        }

        #endregion

        #region Parameter

        //Bhavin 
        public bool deleteParameter(string parameter_master_id)
        {
            string deleteQuery = $"delete from exciseautomation.parameter_master where parameter_master_id='{parameter_master_id}'";
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(deleteQuery, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // throw;
                    return false;
                }
            }
        }
        //End


        public List<ParameterMaster> ParameterList(string userId)
        {
            List<ParameterMaster> parameters = new List<ParameterMaster>();
            string query = @"select *  from exciseautomation.parameter_master";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ParameterMaster parameter = MapParameter(dr);
                        parameters.Add(parameter);
                    }
                }
                command.Connection.Close();
            }
            return parameters;
        }

        public Tuple<bool, string> SaveParameter(ParameterMaster parameterMaster)
        {
            Tuple<bool, string> isParameterSaved = default(Tuple<bool, string>);
            bool isParameterPresent = checkWhetherParameterExists(parameterMaster, true);
            string typeOfSQL = $@"INSERT INTO exciseautomation.parameter_master (parameter_master_name,created_on,created_by) 
                                             VALUES ('{parameterMaster.parameter_master_name}','{DateTime.Now}','{parameterMaster.created_by}')";
            if (isParameterPresent)
            {
                isParameterSaved = new Tuple<bool, string>(default(bool), $"Parameter Name: {parameterMaster.parameter_master_name} already exists");
                return isParameterSaved;
            }
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(typeOfSQL, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    isParameterSaved = new Tuple<bool, string>(true, "Parameter Saved Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isParameterSaved;
        }

        public Tuple<bool, string> UpdateParameter(ParameterMaster parameter)
        {
            Tuple<bool, string> isParameterUpdated = default(Tuple<bool, string>);
            string parameterSql = $@"UPDATE exciseautomation.parameter_master SET  parameter_master_name='{parameter.parameter_master_name}', 
                                                                   updated_on='{DateTime.Now}',updated_by='{parameter.created_by}'
                                                                   WHERE parameter_master_id= '{parameter.parameter_master_id}'";
            bool isParameterPresent = checkWhetherParameterExists(parameter, false);
            if (isParameterPresent)
            {
                isParameterUpdated = new Tuple<bool, string>(default(bool), $"Parameter Name: {parameter.parameter_master_name} already exists for other record");
                return isParameterUpdated;
            }
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(parameterSql, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    isParameterUpdated = new Tuple<bool, string>(true, "Parameter Updated Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isParameterUpdated;
        }

        public ParameterMaster LoadParameterDetails(int parameter_id)
        {
            var param = new ParameterMaster();
            string query = $"select * from exciseautomation.parameter_master where parameter_master_id={parameter_id} LIMIT 1";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        param = MapParameter(dr);
                    }
                }
                command.Connection.Close();
            }
            return param;
        }
        #endregion

        #region Size
        public List<SizeMaster> SizeMasterList(string userId)
        {
            List<SizeMaster> sizes = new List<SizeMaster>();

            //Bhavin
            //string query = @"select * from exciseautomation.tab_liq_size";
            string query = @"select * from exciseautomation.tab_liq_size order by size_name";
            //End

            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        SizeMaster size = MapSize(dr);
                        sizes.Add(size);
                    }
                }
                command.Connection.Close();
            }
            return sizes;
        }

        public Tuple<bool, string> SaveSize(SizeMaster size)
        {
            Tuple<bool, string> isSizeSaved = default(Tuple<bool, string>);
            bool isSizeExists = checkWhetherSizeExists(size, true);
            string typeOfSQL = $@"INSERT INTO exciseautomation.tab_liq_size (size_name) 
                                             VALUES ('{size.size_master_name}')";
            if (isSizeExists)
            {
                isSizeSaved = new Tuple<bool, string>(default(bool), $"Size Name: {size.size_master_name} already exists");
                return isSizeSaved;
            }
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(typeOfSQL, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    isSizeSaved = new Tuple<bool, string>(true, "Sized Saved Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isSizeSaved;
        }

        public Tuple<bool, string> UpdateSize(SizeMaster size)
        {
            Tuple<bool, string> isSizedUpdated = default(Tuple<bool, string>);
            string updateSizeSql = $@"UPDATE exciseautomation.tab_liq_size SET  size_name='{size.size_master_name}'
                                                                   WHERE size_id= '{size.size_master_id}'";
            bool isSizeExists = checkWhetherSizeExists(size, false);
            if (isSizeExists)
            {
                isSizedUpdated = new Tuple<bool, string>(default(bool), $"Size Name: {size.size_master_name} already exists for other record");
                return isSizedUpdated;
            }
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(updateSizeSql, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    isSizedUpdated = new Tuple<bool, string>(true, "Size Updated Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isSizedUpdated;
        }

        public SizeMaster LoadSizeDetails(int size_master_id)
        {
            var size = new SizeMaster();
            string query = $"select * from exciseautomation.tab_liq_size where size_id={size_master_id} LIMIT 1";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        size = MapSize(dr);
                    }
                }
                command.Connection.Close();
            }
            return size;
        }

        public bool deleteSize(string size_id)
        {
            string deleteQuery = $"delete from exciseautomation.tab_liq_size where size_id='{size_id}'";
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(deleteQuery, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //throw;
                    return false;
                }
            }
        }
        #endregion

        #region Brand
        public List<BrandMaster> BrandMasterList(string userId)
        {
            List<BrandMaster> brands = new List<BrandMaster>();
            //Bhavin
            //string query = @"select * from exciseautomation.tab_liq_brand_name";
            string query = @"select * from exciseautomation.tab_liq_brand_name order by brand_name";
            //End

            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        BrandMaster brand = MapBrand(dr);
                        brands.Add(brand);
                    }
                }
                command.Connection.Close();
            }
            return brands;
        }

        public Tuple<bool, string> SaveBrand(BrandMaster brand)
        {
            Tuple<bool, string> isBrandSaved = default(Tuple<bool, string>);
            bool isBrandExists = checkWhetherBrandExists(brand, true);
            string brandSaveSql = $@"INSERT INTO exciseautomation.tab_liq_brand_name (brand_name) 
                                             VALUES ('{brand.brand_master_name}')";
            if (isBrandExists)
            {
                isBrandSaved = new Tuple<bool, string>(default(bool), $"Brand Name: {brand.brand_master_name} already exists");
                return isBrandSaved;
            }
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(brandSaveSql, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    isBrandSaved = new Tuple<bool, string>(true, "Brand Saved Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isBrandSaved;
        }

        public Tuple<bool, string> UpdateBrand(BrandMaster brand)
        {
            Tuple<bool, string> isBrandUpdated = default(Tuple<bool, string>);
            string updateBrandSql = $@"UPDATE exciseautomation.tab_liq_brand_name SET  brand_name='{brand.brand_master_name}'
                                                                   WHERE brand_id= '{brand.brand_master_id}'";
            bool isBrandExists = checkWhetherBrandExists(brand, false);
            if (isBrandExists)
            {
                isBrandUpdated = new Tuple<bool, string>(default(bool), $"Brand Name: {brand.brand_master_name} already exists for other record");
                return isBrandUpdated;
            }
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(updateBrandSql, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    isBrandUpdated = new Tuple<bool, string>(true, "Brand Updated Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isBrandUpdated;
        }

        public BrandMaster LoadBrandDetails(int brand_master_id)
        {
            var brand = new BrandMaster();
            string query = $"select * from exciseautomation.tab_liq_brand_name where brand_id={brand_master_id} LIMIT 1";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        brand = MapBrand(dr);
                    }
                }
                command.Connection.Close();
            }
            return brand;
        }

        public bool deleteBrand(string brand_id)
        {
            string deleteQuery = $"delete from exciseautomation.tab_liq_brand_name where brand_id='{brand_id}'";
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(deleteQuery, transaction))
                    {
                        var rowAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //throw;
                    return false;
                }
            }
        }
        #endregion

        #region Assign Parameter
        public List<AssignParameter> ListAssignedParameters(string userId)
        {
            List<AssignParameter> assignedParameterList = new List<AssignParameter>();
            //string query = @"select header.*,liquor.type_of_liquor_name,liquor_subtype.liquor_sub_name from exciseautomation.assign_parameter as header
            //                    INNER JOIN exciseautomation.type_of_liquor as liquor ON header.type_of_liquor_id=liquor.type_of_liquor_id
            //                    INNER JOIN exciseautomation.liquor_sub_type as liquor_subtype ON header.liquor_sub_type_id=liquor_subtype.liquor_sub_type_id";




            //string query = @"select header.*,liquor.liq_type,liquor_subtype.liquor_sub_name from exciseautomation.assign_parameter as header
            //                    INNER JOIN exciseautomation.tab_liq_type as liquor ON header.type_of_liquor_id=liquor.liq_id
            //                    INNER JOIN exciseautomation.liquor_sub_type as liquor_subtype ON header.liquor_sub_type_id=liquor_subtype.liquor_sub_type_id";

            //Jay Start
            string query = @"select header.*,liquor.liq_type,liquor_subtype.liq_sub_type_name as liquor_sub_name from exciseautomation.assign_parameter as header
                                INNER JOIN exciseautomation.tab_liq_type as liquor ON header.type_of_liquor_id=liquor.liq_id
                                INNER JOIN exciseautomation.tab_liq_subtype as liquor_subtype ON header.liquor_sub_type_id=liquor_subtype.liq_sub_type_id";

            //Jay End
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        AssignParameter assignParameterHeader = MapAssignParameterHeader(dr);
                        assignedParameterList.Add(assignParameterHeader);
                    }
                }
                command.Connection.Close();
            }
            if (assignedParameterList?.Any() ?? default(bool))
            {
                List<AssignParameterDetail> parameterDetailList = new List<AssignParameterDetail>();
                var assign_list_id = string.Join(",", assignedParameterList.Select(x => x.assign_parameter_id));
                query = $@"select AP.*,PM.parameter_master_name from exciseautomation.assign_parameter_assigned_list AP 
                           INNER JOIN exciseautomation.parameter_master PM ON AP.parameter_master_id=PM.parameter_master_id
                            where assign_parameter_id IN ({assign_list_id})";
                using (var command = GetSqlCommand(query))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            AssignParameterDetail parameterDetail = MapAssignParameterDetail(dr);
                            parameterDetailList.Add(parameterDetail);
                        }
                    }
                    command.Connection.Close();
                }
                foreach (var assignedParameter in assignedParameterList)
                {
                    assignedParameter.assign_parameter_assigned_list = parameterDetailList.Where(x => x.assign_parameter_id == assignedParameter.assign_parameter_id).ToList();
                }
            }
            return assignedParameterList;
        }

        public Tuple<bool, string> SaveAssignedParameter(AssignParameterPostInput parameterInfo, string user)
        {
            Tuple<bool, string> isParameterAssignmentSuccess = default(Tuple<bool, string>);
            DeleteAssignmentIfExists(parameterInfo);

            //string headerAssignmentQuery = $@"INSERT INTO exciseautomation.assign_parameter (type_of_liquor_id,liquor_sub_type_id,created_by,created_on) 
            //                                 VALUES ('{parameterInfo.LiquorTypeId}','{parameterInfo.SubLiquorTypeId}','{user}','{DateTime.Now}') RETURNING assign_parameter_id";

            //Jay Start
            string headerAssignmentQuery = $@"INSERT INTO exciseautomation.assign_parameter (type_of_liquor_id,liquor_sub_type_id,created_by,created_on) 
                                             VALUES ('{parameterInfo.LiquorTypeId}','{parameterInfo.SubLiquorTypeId}','{user}','{DateTime.Now}') RETURNING assign_parameter_id";
            //Jay End
            string detailAssignmentQuery = @"INSERT INTO exciseautomation.assign_parameter_assigned_list (assign_parameter_id,parameter_master_id)
                                            VALUES ({0},{1});";
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = GetSqlCommandWithTransaction(headerAssignmentQuery, transaction))
                    {
                        var identity = (int)command.ExecuteScalar();
                        if (identity > default(int))
                        {
                            var stringBuilder = new StringBuilder();
                            foreach (var parameter in parameterInfo.AssignedParameter)
                            {
                                stringBuilder.Append(string.Format(detailAssignmentQuery, identity, parameter.Id));
                            }
                            using (var command1 = GetSqlCommandWithTransaction(stringBuilder.ToString(), transaction))
                            {
                                command1.ExecuteNonQuery();
                            }
                        }
                    }
                    transaction.Commit();
                    connection.Close();
                    isParameterAssignmentSuccess = new Tuple<bool, string>(true, "Parameter Saved Successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return isParameterAssignmentSuccess;
        }

        public Tuple<bool, string> UpdateAssignedParameter(AssignParameterPostInput parameterInfo, string user)
        {
            Tuple<bool, string> isParameterAssignmentSuccess = default(Tuple<bool, string>);
            DeleteAssignmentIfExists(parameterInfo.AssignedParameterId);

            string detailAssignmentQuery = @"INSERT INTO exciseautomation.assign_parameter_assigned_list (assign_parameter_id,parameter_master_id)
                                            VALUES ({0},{1});";
            try
            {
                var stringBuilder = new StringBuilder();
                foreach (var parameter in parameterInfo.AssignedParameter)
                {
                    stringBuilder.Append(string.Format(detailAssignmentQuery, parameterInfo.AssignedParameterId, parameter.Id));
                }
                using (var command = GetSqlCommand(stringBuilder.ToString()))
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                isParameterAssignmentSuccess = new Tuple<bool, string>(true, "Parameter Saved Successfully");
            }
            catch (Exception ex)
            {
                throw;
            }
            return isParameterAssignmentSuccess;
        }

        public AssignUnAssignParameter GetAssignedParameterInfo(int liquorId, int subLiquorId)
        {
            AssignUnAssignParameter paramList = new AssignUnAssignParameter();
            //string query = $@"select detail.parameter_master_id, param.parameter_master_name from exciseautomation.assign_parameter as header
            //                  INNER JOIN  exciseautomation.assign_parameter_assigned_list detail ON header.assign_parameter_id = detail.assign_parameter_id
            //                  INNER JOIN exciseautomation.parameter_master param ON param.parameter_master_id = detail.parameter_master_id
            //                  WHERE header.type_of_liquor_id='{liquorId}' and  header.liquor_sub_type_id='{subLiquorId}'";

            //Jay Start
            string query = $@"select detail.parameter_master_id, param.parameter_master_name from exciseautomation.assign_parameter as header
                              INNER JOIN  exciseautomation.assign_parameter_assigned_list detail ON header.assign_parameter_id = detail.assign_parameter_id
                              INNER JOIN exciseautomation.parameter_master param ON param.parameter_master_id = detail.parameter_master_id
                              WHERE header.type_of_liquor_id='{liquorId}' and  header.liquor_sub_type_id='{subLiquorId}'";

            //Jay End
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    paramList.AssignedParameters = new List<CustomDictionary>();
                    while (dr.Read())
                    {
                        int paramId = GetInt(dr, "parameter_master_id");
                        string paramName = GetString(dr, "parameter_master_name");
                        paramList.AssignedParameters.Add(new CustomDictionary() { Id = paramId, Value = paramName });
                    }
                }
                command.Connection.Close();
            }

            var allAssignedParamIds = paramList?.AssignedParameters?.Select(x => x.Id)?.ToList();

            string containsList = default(string);
            if (allAssignedParamIds?.Any() ?? default(bool))
                containsList = string.Join(",", allAssignedParamIds);
            query = allAssignedParamIds?.Any() ?? default(bool) ?
                                                 $@"select parameter_master_id,parameter_master_name FROM exciseautomation.parameter_master WHERE parameter_master_id NOT IN ({containsList})"
                                                 : $@"select parameter_master_id,parameter_master_name FROM exciseautomation.parameter_master";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    paramList.UnAssignedParameters = new List<CustomDictionary>();
                    while (dr.Read())
                    {
                        int paramId = GetInt(dr, "parameter_master_id");
                        string paramName = GetString(dr, "parameter_master_name");
                        paramList.UnAssignedParameters.Add(new CustomDictionary() { Id = paramId, Value = paramName });
                    }
                }
                command.Connection.Close();
            }
            return paramList;
        }

        private void DeleteAssignmentIfExists(AssignParameterPostInput parameterInfo)
        {
            string isExistsQuery = $@"SELECT H.assign_parameter_id FROM exciseautomation.assign_parameter H 
                                    WHERE type_of_liquor_id={parameterInfo.LiquorTypeId} AND liquor_sub_type_id={parameterInfo.SubLiquorTypeId}";
            List<int> assignedheaders = new List<int>();
            using (var command = GetSqlCommand(isExistsQuery))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        int id = GetInt(dr, "assign_parameter_id");
                        assignedheaders.Add(id);
                    }
                }
                command.Connection.Close();

            }

            if (assignedheaders?.Any() ?? default(bool))
            {
                var deleteQuery = $@"DELETE FROM exciseautomation.assign_parameter_assigned_list WHERE assign_parameter_id IN ({string.Join(",", assignedheaders.Distinct())});
                                     DELETE FROM  exciseautomation.assign_parameter WHERE assign_parameter_id IN ({string.Join(",", assignedheaders.Distinct())})";
                try
                {
                    using (var command = GetSqlCommand(deleteQuery))
                    {
                        command.Connection.Open();
                        var rowAffected = command.ExecuteNonQuery();
                        command.Connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        private void DeleteAssignmentIfExists(int assignedParameterId)
        {
            var deleteQuery = $@"DELETE FROM  exciseautomation.assign_parameter_assigned_list WHERE assign_parameter_id IN ({assignedParameterId})";
            try
            {
                using (var command = GetSqlCommand(deleteQuery))
                {
                    command.Connection.Open();
                    var rowAffected = command.ExecuteNonQuery();
                    command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Lookup Values
        public IDictionary<string, string> DepartmentList()
        {
            Dictionary<string, string> departments = new Dictionary<string, string>();
            string query = "select department_code,department_name,* from exciseautomation.department_master";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        departments.Add(GetString(dr, "department_code"), GetString(dr, "department_name"));
                    }
                }
                command.Connection.Close();
            }
            return departments;
        }



        public IDictionary<int, string> UserList()
        {
            Dictionary<int, string> users = new Dictionary<int, string>();
            string query = "select user_registration_id, user_id from exciseautomation.user_registration order by user_id asc";

            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {


                        users.Add(GetInt(dr, "user_registration_id"), GetString(dr, "user_id"));
                    }
                }
                command.Connection.Close();
            }
            return users;
        }

        public IDictionary<string, string> DistrictList()
        {
            Dictionary<string, string> departments = new Dictionary<string, string>();
            //Bhavin
            string query = "select dist_id, dist_name from exciseautomation.tab_district";
           // string query = "select district_master_id,district_code, district_name from exciseautomation.district_master order by district_name asc ";
            //End

            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                         departments.Add(GetString(dr, "dist_id"), GetString(dr, "dist_name"));
                        // departments.Add(GetString(dr, "district_code"), GetString(dr, "district_name"));
                       // departments.Add(GetString(dr, "district_master_id"), GetString(dr, "district_name"));
                    }
                }
                command.Connection.Close();
            }
            return departments;
        }
        #endregion

        #region Private Methods
        private bool isPresentScalar(string Sql)
        {
            bool isExists = default(bool);
            using (var command = GetSqlCommand(Sql))
            {
                command.Connection.Open();
                var isPresent = command.ExecuteScalar();
                if (isPresent != null)
                {
                    isExists = true;
                }
                command.Connection.Close();
            }
            return isExists;
        }
        private DistrictUser MapDistrictUser(NpgsqlDataReader dr)
        {

            //Bhavin
            string emailid_2 = "", emailid_3 = "";
            Int64 mobileno_2 = 0, mobileno_3 = 0;

            if (GetString(dr, "emailid").Split(',').Count() > 1)
            {
                emailid_2 = GetString(dr, "emailid").Split(',')[1];
            }
            if (GetString(dr, "emailid").Split(',').Count() > 2)
            {
                emailid_3 = GetString(dr, "emailid").Split(',')[2];
            }

            if (GetString(dr, "mobileno").Split(',').Count() > 1)
            {
                mobileno_2 = Convert.ToInt64(GetString(dr, "mobileno").Split(',')[1]);
            }

            if (GetString(dr, "mobileno").Split(',').Count() > 2)
            {
                mobileno_3 = Convert.ToInt64(GetString(dr, "mobileno").Split(',')[1]);
            }


            return new DistrictUser
            {
                //district_login_id = GetInt(dr, "district_log_id"),
                //district_code = GetString(dr, "district_code"),
                //department_code = GetString(dr, "department_code"),
                //user_id = GetString(dr, "user_id"),
                //user_password = GetString(dr, "upwd"),
                //email_id = GetString(dr, "emailid"),
                //email_id2 = GetString(dr, "email_id2"),
                //email_id3 = GetString(dr, "email_id3"),
                //mobile_no = GetInt64(dr, "mobile_no"),
                //mobile_no2 = GetInt64(dr, "mobile_no2"),
                //mobile_no3 = GetInt64(dr, "mobile_no3"),
                //full_name = GetString(dr, "full_name"),
                //created_on = GetDateTime(dr, "created_on"),
                //created_by = GetString(dr, "created_by"),
                //department_name = GetString(dr, "department_name", true),
                //district_name = GetString(dr, "district_name", true),



                district_login_id = GetInt(dr, "district_log_id"),
                district_code = GetString(dr, "district_code"),
                department_code = GetString(dr, "department_code"),
                user_id = GetString(dr, "user_id"),
                user_password = GetString(dr, "upwd"),
                email_id = GetString(dr, "emailid").Split(',')[0],
                email_id2 = emailid_2,
                email_id3 = emailid_3,
                mobile_no = Convert.ToInt64(GetString(dr, "mobileno").Split(',')[0]),
                mobile_no2 = mobileno_2,
                mobile_no3 = mobileno_3,
                full_name = GetString(dr, "fullname"),
                created_on = GetDateTime(dr, "creation_date"),
                created_by = GetString(dr, "user_id"),
                department_name = GetString(dr, "department_name", true),
                district_name = GetString(dr, "district_name", true),
                userid = GetString(dr, "userid"),
                dist_id = GetString(dr, "dist_id"),

                //End
            };
        }

        private bool CheckWhetherUserAlreadyExists(DistrictUser districtUser)
        {
            //Bhavin
            // string query = $"SELECT district_login_id FROM exciseautomation.district_login WHERE user_id='{districtUser.user_id}' AND district_code='{districtUser.district_code}' LIMIT 1";
            string query = $"SELECT district_log_id FROM exciseautomation.district_login WHERE userid='{districtUser.user_id}' AND dist_id='{districtUser.district_code}' LIMIT 1";
            return isPresentScalar(query);
        }

        private Thana MapThana(NpgsqlDataReader dr)
        {
            return new Thana()
            {
                thana_master_id = GetInt(dr, "thana_id"),
                thana_name = GetString(dr, "thana_name"),
                district_code = GetString(dr, "dist_id"),
                district_name = GetString(dr, "dist_name"),
                //thana_master_id = GetInt(dr, "thana_master_id"),
                //thana_name = GetString(dr, "thana_name"),
                //district_code = GetString(dr, "district_code"),
                //district_name = GetString(dr, "district_name"),
            };
        }

        private Compactor MapCompactor(NpgsqlDataReader dr)
        {
            return new Compactor()
            {
                compactor_id = GetInt(dr, "compactor_id"),
                tech_id = GetInt(dr, "tech_id"),
                comp_name = GetString(dr, "comp_name"),
                user_id = GetString(dr, "user_id"),
            };
        }


        private Compactor MapCompactorDetails(NpgsqlDataReader dr)
        {
            return new Compactor()
            {
                compactor_id = GetInt(dr, "compactor_id"),
                tech_id = GetInt(dr, "tech_id"),

            };
        }


        private Thana MapThanaWithoutDistrict(NpgsqlDataReader dr)
        {
            return new Thana()
            {
                thana_master_id = GetInt(dr, "thana_id"),
                thana_name = GetString(dr, "thana_name"),
                district_code = GetString(dr, "dist_id"),
                //thana_master_id = GetInt(dr, " thana_master_id"),
                //thana_name = GetString(dr, "thana_name"),
                //district_code = GetString(dr, "district_code"),
            };
        }

        private bool CheckWhetherCompactorExists(Compactor compactor, bool isAddMode)
        {

            string query = $"SELECT compactor_id FROM exciseautomation.tab_compactor_id_tech WHERE compactor_id='{compactor.compactor_id}' LIMIT 1";
            return isPresentScalar(query);

        }

        private bool CheckWhetherThanaExists(Thana thana, bool isAddMode)
        {
            string query = isAddMode ?
                $"SELECT thana_id FROM exciseautomation.tab_thana WHERE thana_name='{thana.thana_name}' AND dist_id='{thana.district_code}' LIMIT 1" :
                $@"SELECT thana_id FROM exciseautomation.tab_thana WHERE thana_name='{thana.thana_name}' AND dist_id='{thana.district_code}' 
                                                                                    AND  thana_id<>'{thana.thana_master_id}' LIMIT 1";
            return isPresentScalar(query);
        }

        private Distillery MapDistillery(NpgsqlDataReader dr)
        {
            return new Distillery()
            {
                distillery_code = GetInt(dr, "distill_id"),
                distillery_name = GetString(dr, "distill_name")
            };
        }

        private bool checkWhetherDistilleryExists(Distillery distillery, bool isAddMode)
        {
            if (!isAddMode)
            {
                string query = $"SELECT distill_id FROM exciseautomation.tab_distill WHERE distill_name='{distillery.distillery_name}' LIMIT 1";
                return isPresentScalar(query);
            }
            else
            {
                string query = $"SELECT distill_id FROM exciseautomation.tab_distill WHERE distill_name='{distillery.distillery_name}' OR distill_id='{distillery.distillery_code}' LIMIT 1";
                return isPresentScalar(query);
            }
        }

        private TypeOfLiquor MapTypeOfLiquor(NpgsqlDataReader dr)
        {
            return new TypeOfLiquor()
            {
                type_of_liquor_id = GetInt(dr, "liq_id"),
                type_of_liquor_name = GetString(dr, "liq_type")

                //type_of_liquor_id = GetInt(dr, "type_of_liquor_id"),
                //type_of_liquor_name = GetString(dr, "type_of_liquor_name")
            };
        }

        //Bhavin
        private TypeOfLiquor MapTypeOfLiquorList(NpgsqlDataReader dr)
        {
            return new TypeOfLiquor()
            {
                //Bhavin
                //type_of_liquor_id = GetInt(dr, "type_of_liquor_id"),
                //type_of_liquor_name = GetString(dr, "type_of_liquor_name")

                type_of_liquor_id = GetInt(dr, "liq_id"),
                type_of_liquor_name = GetString(dr, "liq_type")
            };
        }


        private bool checkWhetherDistilleryExists(TypeOfLiquor typeOfLiquor, bool isAddMode)
        {
            string query = isAddMode ?
                $"SELECT liq_id FROM exciseautomation.tab_liq_type WHERE liq_type='{typeOfLiquor.type_of_liquor_name}' LIMIT 1" :
                $@"SELECT liq_id FROM exciseautomation.tab_liq_type WHERE liq_type='{typeOfLiquor.type_of_liquor_name}' 
                                                                            AND liq_id<>'{typeOfLiquor.type_of_liquor_id}' LIMIT 1";
            return isPresentScalar(query);
        }

        private SubLiquor MapSubLiquor(NpgsqlDataReader dr)
        {
            return new SubLiquor()
            {
                liquor_sub_type_id = GetInt(dr, "liq_sub_type_id"),
                type_of_liquor_id = GetInt(dr, "liq_id"),
                liquor_sub_name = GetString(dr, "liq_sub_type_name"),
                type_of_liquor_name = GetString(dr, "liq_type", true),

                //liquor_sub_type_id = GetInt(dr, "liquor_sub_type_id"),
                //type_of_liquor_id = GetInt(dr, "type_of_liquor_id"),
                //liquor_sub_name = GetString(dr, "liquor_sub_name"),
                //// type_of_liquor_name = GetString(dr, "liq_type", true),
            };
        }

        //Bhavin
        private SubLiquor MapSubLiquorList(NpgsqlDataReader dr)
        {
            return new SubLiquor()
            {
                //liquor_sub_type_id = GetInt(dr, "liquor_sub_type_id"),
                //type_of_liquor_id = GetInt(dr, "type_of_liquor_id"),
                //liquor_sub_name = GetString(dr, "liquor_sub_name"),
                //type_of_liquor_name = GetString(dr, "type_of_liquor_name", true),


                liquor_sub_type_id = GetInt(dr, "liq_sub_type_id"),
                type_of_liquor_id = GetInt(dr, "liq_id"),
                liquor_sub_name = GetString(dr, "liq_sub_type_name"),
                type_of_liquor_name = GetString(dr, "liq_type", true),


            };
        }

        private bool CheckWhetherSubLiquorExists(SubLiquor subLiquor, bool isAddMode)
        {
            string query = isAddMode ?
                $"SELECT liq_sub_type_id FROM exciseautomation.tab_liq_subtype WHERE liq_id='{subLiquor.type_of_liquor_id}' AND liq_sub_type_name='{subLiquor.liquor_sub_name}' LIMIT 1" :
                $@"SELECT liq_sub_type_id FROM exciseautomation.tab_liq_subtype WHERE liq_id='{subLiquor.type_of_liquor_id}' 
                                                    AND liq_sub_type_name='{subLiquor.liquor_sub_name}' AND liq_sub_type_id<>'{subLiquor.liquor_sub_type_id}' LIMIT 1";
            return isPresentScalar(query);
        }

        private ParameterMaster MapParameter(NpgsqlDataReader dr)
        {
            return new ParameterMaster()
            {
                parameter_master_id = GetInt(dr, "parameter_master_id"),
                parameter_master_name = GetString(dr, "parameter_master_name"),
                created_by = GetString(dr, "created_by"),
                created_on = GetDateTime(dr, "created_on")
            };
        }

        private bool checkWhetherParameterExists(ParameterMaster parameterMaster, bool isAddMode)
        {
            string query = isAddMode ?
                $"SELECT parameter_master_id FROM exciseautomation.parameter_master WHERE parameter_master_name='{parameterMaster.parameter_master_name}' LIMIT 1" :
                $@"SELECT parameter_master_id FROM exciseautomation.parameter_master WHERE parameter_master_name='{parameterMaster.parameter_master_name}'
                                                                                    AND parameter_master_id<>'{parameterMaster.parameter_master_id}' LIMIT 1";
            return isPresentScalar(query);
        }

        private SizeMaster MapSize(NpgsqlDataReader dr)
        {
            return new SizeMaster()
            {
                size_master_id = GetInt(dr, "size_id"),
                size_master_name = GetString(dr, "size_name")
            };
        }

        private bool checkWhetherSizeExists(SizeMaster size, bool isAddMode)
        {
            string query = isAddMode ?
                $"SELECT size_id FROM exciseautomation.tab_liq_size WHERE size_name='{size.size_master_name}' LIMIT 1" :
                $@"SELECT size_id FROM exciseautomation.tab_liq_size WHERE size_name='{size.size_master_name}' AND size_id<>'{size.size_master_id}' LIMIT 1";
            return isPresentScalar(query);
        }

        private BrandMaster MapBrand(NpgsqlDataReader dr)
        {
            return new BrandMaster()
            {
                brand_master_id = GetInt(dr, "brand_id"),
                brand_master_name = GetString(dr, "brand_name")
            };
        }

        private bool checkWhetherBrandExists(BrandMaster brand, bool isAddMode)
        {
            string query = isAddMode ?
                $"SELECT brand_id FROM exciseautomation.tab_liq_brand_name WHERE brand_name='{brand.brand_master_name}' LIMIT 1" :
                $@"SELECT brand_id FROM exciseautomation.tab_liq_brand_name WHERE brand_name='{brand.brand_master_name}' AND brand_id<>'{brand.brand_master_id}' LIMIT 1";
            return isPresentScalar(query);
        }

        private AssignParameter MapAssignParameterHeader(NpgsqlDataReader dr)
        {
            return new AssignParameter()
            {

                //Bhavin
                //assign_parameter_id = GetInt(dr, "assign_parameter_id"),
                //type_of_liquor_id = GetInt(dr, "type_of_liquor_id"),
                //liquor_sub_type_id = GetInt(dr, "liquor_sub_type_id"),
                //type_of_liquor_name = GetString(dr, "type_of_liquor_name"),
                //liquor_sub_type_name = GetString(dr, "liquor_sub_name"),
                //created_by = GetString(dr, "created_by"),
                //created_on = GetDateTime(dr, "created_on")

                assign_parameter_id = GetInt(dr, "assign_parameter_id"),
                type_of_liquor_id = GetInt(dr, "type_of_liquor_id"),
                liquor_sub_type_id = GetInt(dr, "liquor_sub_type_id"),
                type_of_liquor_name = GetString(dr, "liq_type"),
                liquor_sub_type_name = GetString(dr, "liquor_sub_name"),
                created_by = GetString(dr, "created_by"),
                created_on = GetDateTime(dr, "created_on")

                //End
            };
        }

        private AssignParameterDetail MapAssignParameterDetail(NpgsqlDataReader dr)
        {
            return new AssignParameterDetail()
            {
                assign_parameter_assigned_list_id = GetInt(dr, "assign_parameter_assigned_list_id"),
                assign_parameter_id = GetInt(dr, "assign_parameter_id"),
                parameter_master_id = GetInt(dr, "parameter_master_id"),
                parameter_master_name = GetString(dr, "parameter_master_name")
            };
        }

        private bool CheckWhetherAlreadyParameterAssigned(AssignParameter assignParameter)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
