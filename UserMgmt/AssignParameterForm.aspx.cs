using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Usermngt.BL.MasterData;
using Usermngt.BL.Service;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class AssignParameterForm : System.Web.UI.Page
    {

        private static readonly IMasterDataService masterDataService = new MasterDataProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            hdnBaseUrl.Value = baseUrl;
            if (!IsPostBack)
            {
                if (Request.QueryString.AllKeys.Contains("QMode"))
                {
                    var assignedParameterId = Request.QueryString.Get("QId");
                    var liquorTypeId = Request.QueryString.Get("QliquorId");
                    var subLiquorTypeId = Request.QueryString.Get("QsubliquorId");
                    hdnAssignedParameterId.Value = assignedParameterId;
                    hdnLiquourId.Value = liquorTypeId;
                    hdnSubLiqourId.Value = subLiquorTypeId;
                }
            }
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/AssignParameterList.aspx");
        }

        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static IEnumerable<TypeOfLiquor> GetLiquorTypes()
        {
            var sessionId = HttpContext.Current.Session["UserID"].ToString();
            var liquorTypes = masterDataService.TypeOfLiquorList(sessionId);
            return liquorTypes;
        }

        [WebMethod(EnableSession = true)]
        public static IEnumerable<SubLiquor> GetSubLiquorTypes(int selectedLiquor)
        {
            var subLiquorTypes = masterDataService.SubLiquorListByLiquorType(selectedLiquor);
            return subLiquorTypes;
        }

        [WebMethod(EnableSession = true)]
        public static AssignUnAssignParameter GetAssignUnAssignParameter(int liquor, int subLiquor)
        {
            var assignedUnAssignedParameter = masterDataService.GetAssignedParameterInfo(liquor, subLiquor);
            return assignedUnAssignedParameter;
        }


        [WebMethod(EnableSession = true)]
        public static Tuple<bool, string> SaveParameterAssignment(AssignParameterPostInput postData)
        {
            var sessionId = HttpContext.Current.Session["UserID"].ToString();
            var assignedUnAssignedParameter = postData.AssignedParameterId > default(int) ? masterDataService.UpdateAssignedParameter(postData, sessionId) : masterDataService.SaveAssignedParameter(postData, sessionId);
            return assignedUnAssignedParameter;
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //string val1 = ddlLiquorType.Value;
            //string val2 = ddlSubLiquorType.Value;
            //string val3 = ddlSelectedParameters.Value;
        }
    }
}