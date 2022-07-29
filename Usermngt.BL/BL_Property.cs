using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
  public  class BL_Property
    {
        public static List<ThanaMaster> GetThana()
        {
            return DL_Property.GetThana();
        }

        public static string Insert(cm_seiz_Property Property)
        {
            return DL_Property.Insert(Property);
        }
        public static string Update(cm_seiz_Property Property)
        {
            return DL_Property.Update(Property);
        }


        public static List<cm_seiz_Property> GetList(string seizureNo)
        {
            return DL_Property.GetList(seizureNo);
        }

        public static cm_seiz_Property GetDetails(string userid, int Propertyid)
        {
            return DL_Property.GetDetails(userid, Propertyid);
        }

    }
}
