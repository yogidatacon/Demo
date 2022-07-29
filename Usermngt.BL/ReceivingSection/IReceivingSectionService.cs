using System;
using System.Collections.Generic;
using Usermngt.Entities;

namespace Usermngt.BL.ReceivingSection
{
    public interface IReceivingSectionService
    {
        IDictionary<string, string> TypesList(string type);
        IDictionary<string, string> PoliceOfficerDesignation(string type);
        IDictionary<int, string> Compactors();
        Tuple<bool, string> SaveReceivingSection(ReceivingSectionContext context);
        List<ReceivingSectionList1> LoadReceivingSectionDetails();
        ReceivingSectionContext GetReceivingSectionDetailById(string receivingSectionId, string exhibitFrom);
    }
}
