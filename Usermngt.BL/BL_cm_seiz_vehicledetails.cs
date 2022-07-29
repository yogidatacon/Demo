using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;
using System.Configuration;

namespace Usermngt.BL
{
    #region BL_cm_seiz_OtherExcisableArticles
    /// <summary>
    /// exciseautomation.seizure_vehicledetails
    /// </summary>
    public class BL_cm_seiz_vehicledetails
    {
        public static List<cm_seiz_vehicledetails> GetList(string seizureNo)
        {
            return DL_cm_seiz_vehicledetails.GetList(seizureNo);
        }

        public static bool Insertvehicledetails(cm_seiz_vehicledetails obj)
        {
            return DL_cm_seiz_vehicledetails.InsertSeiz_OtherExcisableArticles(obj);
        }

        public static bool Update(cm_seiz_vehicledetails cm_obj)
        {
            return DL_cm_seiz_vehicledetails.Update_OtherExcisableArticles(cm_obj);
        }

        public static List<cm_seiz_vehicledetails> VehicleSearch(string vno, string vType)
        {
            return DL_cm_seiz_vehicledetails.VehicleSearch(vno,vType);
        }

        public static cm_seiz_vehicledetails GetDetails(string tableId)
        {
            return DL_cm_seiz_vehicledetails.GetDetails(tableId);
        }

        public static List<cm_seiz_vehicledetails> GetvehicleList()
        {
            return DL_cm_seiz_vehicledetails.GetvehicleList();
        }
    }
    #endregion BL_cm_seiz_OtherExcisableArticles
}
