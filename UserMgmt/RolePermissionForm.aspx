<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="RolePermissionForm.aspx.cs" Inherits="UserMgmt.RolePermissionForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">

    <div role="main">
        <br />
        <div class="">
            <div class="row top_tiles">
                <div class="">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div class="x_panel">
                            <html>
                            <head>
                                <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
                                <title>User Management</title>

                                <link href="cssres/Accodian_Menu.css" rel="stylesheet" />



                                <script type="text/javascript">
                                    $(document).ready(function () {


                                    });

                                    function CheckBoxCheckAll(element, chkid) {

                                        var id = "#" + element;
                                        var checkboxId = "#" + chkid;
                                        if ($(checkboxId).is(":checked")) {
                                            $(id).find('input[type=checkbox]').each(function () {
                                                // some staff
                                                this.checked = true;
                                            });
                                        }
                                        else {
                                            $(id).find('input[type=checkbox]').each(function () {
                                                // some staff
                                                this.checked = false;
                                            });
                                        }
                                    }


                                    function SubModuleCheckAll(element, parent, chkid) {
                                        debugger;
                                        var id = "." + element;
                                        var checkboxId = "#" + chkid;
                                        if ($(checkboxId).is(":checked")) {
                                            $(id).find('input[type=checkbox]').each(function () {
                                                // some staff
                                                this.checked = true;
                                            });
                                            //if(!$('.class_1').parent().hasClass('ChildAccordionHeaderSelected'))
                                            //{                                       
                                            //    $('.class_1').parent().removeClass('ChildAccordionHeader').addClass('ChildAccordionHeaderSelected');
                                            //}
                                        }
                                        else {

                                            $(id).find('input[type=checkbox]').each(function () {
                                                // some staff
                                                this.checked = false;
                                            });
                                        }
                                    }
                                    function ModuleCheckAll(element, chkid) {
                                        debugger;;
                                        var id = "." + element;
                                        var checkboxId = "#" + chkid;
                                        if ($(checkboxId).is(":checked")) {
                                            $(id).find('input[type=checkbox]').each(function () {
                                                // some staff
                                                this.checked = true;
                                            });
                                            //if(!$('.class_1').parent().hasClass('ChildAccordionHeaderSelected'))
                                            //{                                       
                                            //    $('.class_1').parent().removeClass('ChildAccordionHeader').addClass('ChildAccordionHeaderSelected');
                                            //}
                                        }
                                        else {

                                            $(id).find('input[type=checkbox]').each(function () {
                                                // some staff
                                                this.checked = false;
                                            });
                                        }
                                    }
                                    function BindRolePermissionData() {
                                        debugger;
                                        $.ajax({
                                            type: "POST",
                                            url: "RolePermissionForm.aspx/GetRolePermissionData",
                                            data: '{role_namecode: "test" }',
                                            contentType: 'application/json; charset=utf-8',
                                            cache: false,
                                            async: false,
                                            dataType: 'json',
                                            success: function (result) {
                                                debugger;
                                                var obj = JSON.parse(result.d);
                                                if (obj != null) {
                                                    for (var i = 0; i < obj.length; i++) {

                                                        var maduleid = "chk_" + obj[i].mns_no;
                                                        var submaduleid = "chk_subModule_" + obj[i].mns_no + "_" + obj[i].submodule_name;
                                                        var tabid = "tab_" + obj[i].mns_no + "_" + obj[i].submodule_name + "_" + obj[i].tab_name;

                                                        if (obj[i].addpermition == "Y") {
                                                            var chkid = obj[i].mns_no + "_" + obj[i].submodule_name + "_" + obj[i].tab_name + "_Add";
                                                            $('#' + chkid).prop('checked', true);
                                                            $('#' + maduleid).prop('checked', true);
                                                            $('#' + submaduleid).prop('checked', true);
                                                            $('#' + tabid).prop('checked', true);
                                                        }
                                                        if (obj[i].editpermition == "Y") {
                                                            var chkid = obj[i].mns_no + "_" + obj[i].submodule_name + "_" + obj[i].tab_name + "_Edit";
                                                            $('#' + chkid).prop('checked', true);

                                                            $('#' + maduleid).prop('checked', true);
                                                            $('#' + submaduleid).prop('checked', true);
                                                            $('#' + tabid).prop('checked', true);
                                                        }
                                                        if (obj[i].deletepermition == "Y") {
                                                            var chkid = obj[i].mns_no + "_" + obj[i].submodule_name + "_" + obj[i].tab_name + "_Delete";
                                                            $('#' + chkid).prop('checked', true);

                                                            $('#' + maduleid).prop('checked', true);
                                                            $('#' + submaduleid).prop('checked', true);
                                                            $('#' + tabid).prop('checked', true);
                                                        }
                                                        if (obj[i].reviewpermition == "Y") {
                                                            var chkid = obj[i].mns_no + "_" + obj[i].submodule_name + "_" + obj[i].tab_name + "_Review";
                                                            $('#' + chkid).prop('checked', true);

                                                            $('#' + maduleid).prop('checked', true);
                                                            $('#' + submaduleid).prop('checked', true);
                                                            $('#' + tabid).prop('checked', true);
                                                        }
                                                        if (obj[i].approvepermition == "Y") {
                                                            var chkid = obj[i].mns_no + "_" + obj[i].submodule_name + "_" + obj[i].tab_name + "_Approve";
                                                            $('#' + chkid).prop('checked', true);

                                                            $('#' + maduleid).prop('checked', true);
                                                            $('#' + submaduleid).prop('checked', true);
                                                            $('#' + tabid).prop('checked', true);
                                                        }

                                                    }

                                                }



                                            },
                                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                                alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                                            },
                                        });

                                    }
                                </script>


                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                        debugger;

                                        if ($('#ddRolename').val() == 'Select') {
                                            alert("Select  Role Name");
                                            $('#ddRolename').focus();
                                            e.stopImmediatePropagation();
                                            return false;

                                        }
                                        return true;
                                    }
                                    function getFormData($form) {
                                        var unindexed_array = $form.serializeArray();
                                        var indexed_array = {};

                                        $.map(unindexed_array, function (n, i) {
                                            indexed_array[n['name']] = n['value'];
                                        });

                                        return indexed_array;
                                    }

                                    function serializeForm(form) {
                                        var kvpairs = [];
                                        for (var i = 0; i < form.elements.length; i++) {
                                            var e = form.elements[i];
                                            if (!e.name || !e.value) continue; // Shortcut, may not be suitable for values = 0 (considered as false)
                                            switch (e.type) {
                                                case 'text':
                                                case 'textarea':
                                                case 'password':
                                                case 'hidden':
                                                    kvpairs.push(encodeURIComponent(e.name) + "=" + encodeURIComponent(e.value));
                                                    break;
                                                case 'radio':
                                                case 'checkbox':
                                                    if (e.checked) kvpairs.push(encodeURIComponent(e.name) + "=" + encodeURIComponent(e.value));
                                                    break;
                                                    /*  To be implemented if needed:
                                                    case 'select-one':
                                                        ... document.forms[x].elements[y].options[0].selected ...
                                                        break;
                                                    case 'select-multiple':
                                                        for (z = 0; z < document.forms[x].elements[y].options.length; z++) {
                                                            ... document.forms[x].elements[y].options[z].selected ...
                                                        } */
                                                    break;
                                            }
                                        }
                                        return kvpairs.join("&");
                                    }


                                    function SaveRolePermissionsForm() {

                                        debugger;;
                                        if (validationMsg()) {
                                            // var datamodel = serializeForm($('#mainRolePermission :input').serialize())
                                            //check properly to every control name should be unique
                                            //name and id should be same for all the controls
                                            //$('#divId :input').serialize();
                                            //var rolePermissionsObj = $('#mainRolePermission :input').serialize().split("=");
                                            var testfield = "";
                                            var selected = [];
                                            $('#mainRolePermission input:checked').each(function () {
                                                selected.push($(this).attr('name'));
                                            });

                                            // selected.push($("#BodyContent_ddRolename option:selected").val());
                                            //selected.push($("#BodyContent_ddorgnnames option:selected").val());


                                            var rolePermissionsObj = $('#mainRolePermission :input').serializeArray();
                                            var indexed_array = {};
                                            var datastring = [];
                                            $.map(rolePermissionsObj, function (n, i) {

                                                indexed_array[n['name']] = n['value'];
                                                datastring.push(n['name'], n['value']);

                                            });
                                            // var rolePermissionsObj = getFormData($form);

                                            <%-- var rolePermissionsObj =
                                                {
                                                    module_name:document.getElementById('<%=ddRolename.ClientID%>').sel,
                                                    subModule_name: $('input[name=chk_subModule_1]').val(),
                                                    tab_name: $('input[name= tab_1]').val()
                                                };--%>

                                            var jsondata = JSON.stringify(selected);


                                            $.ajax({
                                                type: "POST",
                                                url: "RolePermissionForm.aspx/SaveRolePermission",
                                                data: '{roleobj: ' + jsondata + ',rolecode:' + $("#BodyContent_ddRolename option:selected").val() + ',orgId:' + $("#BodyContent_ddorgnnames option:selected").val() + ',rolepermissionID:' + $("#BodyContent_txtid").val() + '}',
                                                //dataType: 'json',
                                                datatype: "application/json",
                                                contentType: "application/json; charset=utf-8",
                                                cache: false,
                                                async: false,
                                                success: function (result) {

                                                    alert(result.d);
                                                    if (result.d == "SuccessFully Submitted")
                                                        document.getElementById('<%=ShowRecords.ClientID%>').click();
                                                },
                                                error: function (XMLHttpRequest, textStatus, errorThrown) {
                                                    alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                                                },
                                            });



                                        }
                                    }
                                </script>

                            </head>
                            <body>

                                <div id="mainRolePermission">
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton ID="RolePermissions" OnClick="RolePermissions_Click" runat="server"><span style="color:#fff;font-size:14px;">Role Permissions</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="UserPermissions" OnClick="UserPermissions_Click" runat="server"><span style="color:#fff;font-size:14px;">User Permissions</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>Role Permission</h2>

                                            <div class="clearfix"></div>
                                        </div>


                                        <div class="x_content">
                                            <br />

                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label style="font-size: small"><a style="color: red">*</a> Organisation Name</label><br />
                                                <asp:DropDownList ID="ddorgnnames" Height="30px" Width="350px" runat="server" data-toggle="tooltip" data-placement="right" title="Organisation Name"></asp:DropDownList>
                                                <asp:HiddenField ID="txtid" runat="server" />
                                            </div>
                                            <asp:HiddenField ID="org_id" runat="server" />
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label style="font-size: small"><a style="color: red">*</a> Role</label><br />
                                                <asp:DropDownList ID="ddRolename"  Height="30px" Width="350px" runat="server" data-toggle="tooltip" data-placement="right" CssClass="form-control" title="Role Name"></asp:DropDownList>

                                            </div>

                                            <p>&nbsp;</p>
                                            <p>&nbsp;</p>
                                            <p>&nbsp;</p>
                                            <div id="MenuCotenet" runat="server">
                                                <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
                                                </asp:ScriptManager>
                                                <asp:Panel ID="MyContent" runat="server">
                                                </asp:Panel>
                                            </div>
                                            <p>&nbsp;</p>
                                            <p>&nbsp;</p>
                                            <p>&nbsp;</p>

                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                 <input type="button" title="Save" class="btn btn-primary" id="btnSaveIndentForm" value="Submit" onclick="SaveRolePermissionsForm();">
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </body>
                            </html>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>


