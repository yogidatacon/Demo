using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_cm_seiz_Accused_Status
    {
        public static List<cm_seiz_Accused_Status> GetList()
        {
            return DL_cm_seiz_Accused_Status.GetList();


        }
    }
}
