using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL.MasterData;
using Usermngt.BL.ReceivingSection;
using Usermngt.BL.Service;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class CompactorMapForm : System.Web.UI.Page
    {
        #region private fields
        private readonly IReceivingSectionService _receivingSectionService = new ReceivingSectionProvider();
        private readonly IMasterDataService _masterDataService = new MasterDataProvider();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCompactor();
                LoadUser();

                if (Request.QueryString.AllKeys.Contains("QMode"))
                {
                    var key = Convert.ToInt32(Request.QueryString.Get("QId"));
                    LoadDetails(key);
                    ddlCompactorNames.Enabled = false;
                }
            }
        }

        private void LoadCompactor()
        {
            var compactors = _receivingSectionService.Compactors();
            ddlCompactorNames.DataSource = compactors;
            ddlCompactorNames.DataBind();
            ddlCompactorNames.Items.Insert(0, "Select");
        }

        private void LoadUser()
        {
            var userList = _masterDataService.UserList();
            ddlUserNames.DataSource = userList;            
            ddlUserNames.DataBind();
            ddlUserNames.Items.Insert(0, "Select");
            //ddlUserNames.SelectedIndex = 0;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var compactor = new Compactor()
            {
                compactor_id = Convert.ToInt32(ddlCompactorNames.SelectedValue),
                tech_id = Convert.ToInt32(ddlUserNames.SelectedValue)                
            };

            if (Request.QueryString.AllKeys.Contains("QMode"))
            {               
               compactor.compactor_id = Convert.ToInt32(Request.QueryString.Get("QId"));
            }

            try
            {
                var isCompactorSaved = Request.QueryString.AllKeys.Contains("QMode") ? _masterDataService.UpdateCompactor(compactor) : _masterDataService.SaveCompactor(compactor);
                if (!isCompactorSaved.Item1)
                {
                    var alert = ($@"
                                    <script type='text/javascript'>
                                        window.onload=function()
                                        {{
                                             alert('{ isCompactorSaved.Item2}');
                                        }};
                                    </script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", alert);
                    return;
                }
                Session["UserID"] = Session["UserID"].ToString();
                Response.Redirect("~/CompactorMapList.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadDetails(int key)
        {
           
            Compactor compactor = _masterDataService.LoadCompactorDetails(key);
            if (compactor == null)
            {
                throw new ArgumentNullException();
            }
            ddlCompactorNames.SelectedValue = compactor.compactor_id.ToString();
            ddlUserNames.SelectedValue = compactor.tech_id.ToString();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/CompactorMapList.aspx");
        }
    }
}