using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_DigitalSignature
    {
        public static bool InserDigitalSignature(DigitalSignature DigitalSignature)
        {
            return DL_DigitalSignature.InserDigitalSignature(DigitalSignature);
        }

        public static bool UpdateDigitalSignature(DigitalSignature DigitalSignature)
        {
            return DL_DigitalSignature.UpdateDigitalSignature(DigitalSignature);
        }

        public static List<DigitalSignature> GetList()
        {
            return DL_DigitalSignature.GetList();
        }

        public static DigitalSignature GetRolename(int id)
        {
            return DL_DigitalSignature.GetRolename(id);
        }

        public static List<DigitalSignature> GetEmpList()
        {
            return DL_DigitalSignature.GetEmpList();
        }

        public static DigitalSignature GetDetails(string id)
        {
            return DL_DigitalSignature.GetDetails(id);
        }
    }
}
