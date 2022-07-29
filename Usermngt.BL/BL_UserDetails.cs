using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_UserDetails
    {
        public static List<UserDetails> GetUserList(string userid)
        {
            return DL_UserDetails.GetUserList(userid);
        }
        public static List<UserDetails> AllUserList(string userid)
        {
            return DL_UserDetails.AllUserList(userid);
        }
        public static bool UpdateUser(UserDetails user)
        {
            return DL_UserDetails.UpdateUser(user);
        }
        public static List<UserDetails> SearchUserDetails(string tablename, string column, string value)
        {
            return DL_UserDetails.SearchUserDetails(tablename, column, value);
        }
        public static List<UserDetails> SearchUserpermission(string tablename, string column, string value)
        {
            return DL_UserDetails.SearchUserpermission(tablename, column, value);
        }
        public static bool InsertUser(UserDetails user)
        {
            return DL_UserDetails.InsertUser(user);
        }
        public static List<UserDetails> GetUsers(string userid)
        {
            return DL_UserDetails.GetUsers(userid);
        }
        public static UserDetails GetUser(string id)
        {
            return DL_UserDetails.GetUser(id);
        }
        public static UserDetails GetProfileUser(string userid, string mobile)
        {
            return DL_UserDetails.GetProfileUser(userid, mobile);

        }
        public static List<UserDetails> GetUserPermissins(string userid)
        {
            return DL_UserDetails.GetUserPermissins(userid);
        }

        public static UserDetails CheckUser(string userid)
        {
            return DL_UserDetails.CheckUser(userid);
        }

        public static UserDetails GetUserDetails(string id)
        {
            return DL_UserDetails.GetUserDetails(id);
        }
    }
}
