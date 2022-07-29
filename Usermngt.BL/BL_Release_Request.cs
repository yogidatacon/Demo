using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_Release_Request
    {
        public static List<Release_Request> GetList()
        {
            return DL_Release_Request.GetList();
        }
        public static List<Release_Request> Search(string tablename, string column, string value)
        {
            return DL_Release_Request.Search(tablename, column, value);
        }

        public static string GetMax(string party_code)
        {
            return DL_Release_Request.GetMax(party_code);
        }

        public static string Insert(Release_Request rr)
        {
            return DL_Release_Request.Insert(rr);
        }

        public static string Update(Release_Request rr)
        {
            return DL_Release_Request.Update(rr);
        }

        public static List<Release_Request> GetRRList()
        {
            return DL_Release_Request.GetRRList();
        }
        public static List<Release_Request> Search1(string tablename, string column, string value)
        {
            return DL_Release_Request.Search1(tablename, column, value);
        }
        public static Release_Request GetDetails(string rrno,string financial_year)
        {
            return DL_Release_Request.GetDetails(rrno,financial_year);
        }

        public static string Approve(Release_Request rr)
        {
            return DL_Release_Request.Approve(rr);
        }
        public static string Adminupdate(Release_Request rr)
        {
            return DL_Release_Request.Adminupdate(rr);
        }


        public static string GetRRMax(string party_code)
        {
            throw new NotImplementedException();
        }
    }
}
