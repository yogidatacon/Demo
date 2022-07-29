using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;
using Usermngt.Entities.CaseMgmt;

namespace Usermngt.BL
{
    #region BL_cm_seiz_BasicIformation
    public class BL_cm_seiz_BasicIformation
    {
        public static bool InsertBasicIformation(cm_seiz_BasicIformation BasicIformation)
        {
            return DL_cm_seiz_BasicIformation.InsertSeiz_BasicIformation(BasicIformation);
        }

        public static List<cm_article_category> GetList()
        {
            return DL_cm_article_category.GetList();
        }

        public static bool Update(cm_seiz_BasicIformation cm_obj)
        {
            return DL_cm_seiz_BasicIformation.Update_BasicIformation(cm_obj);
        }

        public static List<cm_seiz_BasicIformation> GetSubmittedSeizureListByName(string username)
        {
            return DL_cm_seiz_BasicIformation.GetSubmittedSeizureListByName(username);
        }

        public static List<cm_seiz_BasicIformation> GetSubmittedSeizureList(string _seizureNo, string prfirno, string Thana)
        {
            return DL_cm_seiz_BasicIformation.GetSubmittedSeizureList(_seizureNo,prfirno,Thana);
        }

        public static List<Call_Complaints> GetComplaintList(string userid)
        {
            return DL_cm_seiz_BasicIformation.GetComplaintList(userid);
        }

        public static List<cm_seiz_BasicIformation> GetRaidList()
        {
            return DL_cm_seiz_BasicIformation.GetRaidList();
        }
        public static List<cm_seiz_BasicIformation> GetThanaList()
        {
            return DL_cm_seiz_BasicIformation.GetThanaList();
        }
        public static List<cm_seiz_BasicIformation> GetUnSubmittedSeizureList(string username)
        {
            return DL_cm_seiz_BasicIformation.GetUnSubmittedSeizureList(username);
        }

        public static List<cm_seiz_BasicIformation> GetUnsubmittedList(string _thana, string _raidDate, string _seizureNo)
        {
            return DL_cm_seiz_BasicIformation.GetUnsubmittedList(_thana, _raidDate, _seizureNo);
        }

        public static cm_seiz_BasicIformation ViewDetails(string seizureNo)
        {
            return DL_cm_seiz_BasicIformation.ViewDetails(seizureNo);
        }

        public static cm_seizure GetSeizureDetails(string seizureNo)
        {
            return DL_cm_seiz_BasicIformation.GetSeizureDetails(seizureNo);
        }

        public static List<cm_seiz_BasicIformation> GetSubmittedSeizureListByALL( string rb)
        {
            return DL_cm_seiz_BasicIformation.GetSubmittedSeizureListByALL(rb);
        }

        public static List<cm_seiz_BasicIformation> GetUnSubmittedSeizureAllList(string rb)
        {
            return DL_cm_seiz_BasicIformation.GetUnSubmittedSeizureAllList(rb);
        }
    }
    #endregion BL_cm_seiz_BasicIformation
}
