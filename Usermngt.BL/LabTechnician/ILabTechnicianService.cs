using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.Entities;

namespace Usermngt.BL.LabTechnician
{
   public  interface ILabTechnicianService
    {
        List<LabTechnicianList> LoadLabTechnicianDetails();

        LabTechReport GetLabTechReportDetailById(string receivingSectionId, string exhibitFrom);

        Tuple<bool, string> SaveLabTechReport(LabTechReportContext context);
    }
}
