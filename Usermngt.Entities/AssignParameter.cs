using System;
using System.Collections.Generic;
using System.Linq;

namespace Usermngt.Entities
{
    public class AssignParameter
    {
        public int assign_parameter_id { get; set; }
        public int type_of_liquor_id { get; set; }
        public string type_of_liquor_name { get; set; }
        public int liquor_sub_type_id { get; set; }
        public string liquor_sub_type_name { get; set; }
        public List<AssignParameterDetail> assign_parameter_assigned_list { get; set; }
        public string assign_parameter_assigned_list_display => assign_parameter_assigned_list?.Any() != null ?
                                                                    string.Join(",", assign_parameter_assigned_list.Select(x => x.parameter_master_name)) : default(string);
        public DateTime? created_on { get; set; }
        public string created_by { get; set; }
        public DateTime? updated_on { get; set; }
        public string updated_by { get; set; }
    }

    public class AssignParameterDetail
    {
        public int assign_parameter_assigned_list_id { get; set; }
        public int assign_parameter_id { get; set; }
        public int parameter_master_id { get; set; }
        public string parameter_master_name { get; set; }
    }

    public class AssignUnAssignParameter
    {
        public List<CustomDictionary> AssignedParameters { get; set; }
        public List<CustomDictionary> UnAssignedParameters { get; set; }
    }

    public class CustomDictionary
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }


    public class AssignParameterPostInput
    {
        public int AssignedParameterId { get; set; }
        public int LiquorTypeId { get; set; }
        public int SubLiquorTypeId { get; set; }
        public IEnumerable<CustomDictionary> AssignedParameter { get; set; }
    }
}
