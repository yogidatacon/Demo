using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_WorkFlow
    {
        public static List<WorkFlow> GetSubModules()
        {
            return DL_WorkFlow.GetSubModules();
        }
        public static List<WorkFlow> SearchWorkFlow(string tablename, string column, string value)
        {
            return DL_WorkFlow.SearchWorkFlow(tablename, column, value);
        }
        public static List<WorkFlow> GetTabNames(string submodule_code)
        {
            return DL_WorkFlow.GetTabNames(submodule_code);
        }

        public static List<Reportmaster> GetReportsList()
        {
            return DL_WorkFlow.GetReportlist();
        }
        public static List<Reportmaster> Search(string tablename, string column, string value)
        {
            return DL_WorkFlow.Search(tablename,column,value);
        }
        public static List<Reportmaster> GetReports(string username)
        {
            return DL_WorkFlow.GetReports(username);
        }

        public static List<WorkFlow> GetRoleNames()
        {
            return DL_WorkFlow.GetRoleNames();
        }

        public static List<WorkFlow> Getworkflowlist(string username)
        {
            return DL_WorkFlow.Getworkflowlist(username);
        }

        public static List<WorkFlow> GetUserNames(int role_name_code)
        {
            return DL_WorkFlow.GetUserNames(role_name_code);
        }

        public static List<WorkFlow> GetDistricts()
        {
            return DL_WorkFlow.GetDistricts();
        }

        public static string InsertWorkFlow(List<WorkFlow> workflow)
        {
            return DL_WorkFlow.InsertWorkFlow(workflow);
        }

        public static List<WorkFlow> Getworkflow(int txn)
        {
            return DL_WorkFlow.Getworkflow(txn);
        }

        public static List<WorkFlow> Checkworkflow(string submodule_code, string tab_id, string role_name_code, string district_code,string userid)
        {
            return DL_WorkFlow.Checkworkflow(submodule_code, tab_id, role_name_code, district_code, userid);
        }
        public static List<WorkFlow> ApprovelLevels(string submodule_code, string tab_id)
        {
            return DL_WorkFlow.ApprovelLevels(submodule_code, tab_id);
        }

        public static string InsertReport(Reportmaster reportmaster)
        {
            return DL_WorkFlow.InsertReport(reportmaster);
        }
        public static string UpdateReport(Reportmaster reportmaster)
        {
            return DL_WorkFlow.UpdateReport(reportmaster);
        }
        public static string UpdateWorkFlow(List<WorkFlow> workflows)
        {
            return DL_WorkFlow.UpdateWorkFlow(workflows);
        }
    }
}
