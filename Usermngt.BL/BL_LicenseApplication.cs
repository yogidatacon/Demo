using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_LicenseApplication
    {

        public static string Insert(LicenseApplication license)
        {
            return DL_LicenseApplication.Insert(license);
        }
        public static int GetExistsData(string year, string party,string party_code)
        {
            return DL_LicenseApplication.GetExistsData(year, party,party_code);
        }
        public static List<LicenseApplication> Getlic(int id)
        {
            return DL_LicenseApplication.Getlic(id);
        }

        public static string Update(LicenseApplication license)
        {
            return DL_LicenseApplication.Update(license);
        }
        public static LicenseApplication GetDetails(int lic_application_id,string financial_year)
        {
            return DL_LicenseApplication.GetDetails(lic_application_id,financial_year);
        }
        public static List<aplliedfor> getdetail(int lic)
        {
            return DL_LicenseApplication.getdetail(lic);
        }
        public static List<LicenseApplication> Getlicense()
        {
            return DL_LicenseApplication.Getlicense();
        }
        public static List<District> GetDistrictList(string division_code)
        {
            return DL_LicenseApplication.GetDistrictList(division_code);
        }
        public static LicenseFee GetAval(string code)
        {
            return DL_LicenseApplication.GetAval(code);
        }

        public static string Approve(LicenseApplication DDC)
        {
            return DL_LicenseApplication.Approve(DDC);
        }
        }
    }
