<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignParameterForm.aspx.cs" Inherits="UserMgmt.AssignParameterForm" MasterPageFile="~/Admin.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div role="main">
        <br>
        <div class="">
            <div class="row top_tiles">
                <div class="">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div class="x_panel">


                            <script src="js/knockout-latest.min.js"></script>
                            <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
                            <title>Assign Parameter Form</title>

                            <div>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecords_Click">
                                        <i class="fa fa-list ">SHOW RECORD LIST</i>
                                    </asp:LinkButton></a>
                                </a>
                                <div class="row">
                                    <div class="x_title">
                                        <h2>Assign Parameter Form</h2>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="x_content" id="dvAddAssignParameter">
                                        <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                            <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span> Liquor Type</label><br>
                                            <select id="ddlLiquorType" class="form-control"
                                                data-bind="options: LiquorTypeList, optionsText: 'type_of_liquor_name', optionsValue: 'type_of_liquor_id', optionsCaption: 'Select', value: SelectedLiquorType, disable: QueryLiquorId && Number(QueryLiquorId) > 0">
                                            </select>

                                        </div>
                                        <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                            <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Sub Liquor Type</label><br>
                                            <select id="ddlSubLiquorType" class="form-control"
                                                data-bind="options: SubLiquorTypeList, optionsText: 'liquor_sub_name', optionsValue: 'liquor_sub_type_id', optionsCaption: 'Select', value: SelectedSubLiquorType, disable: QuerySubLiquorId && Number(QuerySubLiquorId) > 0">
                                            </select>
                                        </div>
                                        <div>
                                            <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                        </div>
                                        <p>&nbsp;</p>

                                        <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                           <%-- <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span> Assigned Parameter</label><br>
                                            <select id="ddlSelectedParameters" class="form-control"
                                                data-bind="options: AssignedParameters, optionsText: 'Value', optionsValue: 'Id', selectedOptions: SelectedAssignedParameters" multiple="">
                                            </select>--%>

                                            <label style="font-size: small; font-weight: bold;">Un Assigned Parameter</label><br>
                                            <select id="ddlAvailableParameters" class="form-control"
                                                data-bind="options: AvailableParameters, optionsText: 'Value', optionsValue: 'Id', selectedOptions: SelectedAvailableParameters" multiple="">
                                            </select>

                                        </div>

                                        <div class="col-md-1 col-sm-6 col-xs-12 form-inline">
                                            <%-- <a href="javascript:moveForwordUser();" class="btn btn-primary">
                                                <i class="fa fa-angle-right"></i>
                                            </a>
                                            <br />
                                            <a href="javascript:moveBackwordUser();" class="btn btn-primary">
                                                <i class="fa fa-angle-left"></i>
                                            </a>
                                            <br />
                                            <a href="javascript:moveBackwordAllUser();" class="btn btn-primary">
                                                <i class="fa fa-angle-double-left"></i>
                                            </a>
                                            <br />
                                            <a href="javascript:moveForwordAllUser();" class="btn btn-primary">
                                                <i class="fa fa-angle-double-right"></i>
                                            </a>--%>


                                            <%--Bhavin--%>
                                            <button type="button" style="width: 50%" data-bind="click: doubleleft">
                                                <i class="fa fa-angle-double-left"></i>
                                            </button>
                                            <button type="button" style="width: 50%" data-bind="click: right">
                                                <i class="fa fa-angle-right"></i>
                                            </button>
                                            <button type="button" style="width: 50%" data-bind="click: left">
                                                <i class="fa fa-angle-left"></i>
                                            </button>
                                            <button type="button" style="width: 50%" data-bind="click: doubleright">
                                                <i class="fa fa-angle-double-right"></i>
                                            </button>
                                            <%--Bhavin--%>
                                        </div>

                                        <div class="col-md-4 col-sm-6 col-xs-12 col-md-offset-2 form-inline">
                                            <%--<label style="font-size: small; font-weight: bold;">Un Assigned Parameter</label><br>
                                            <select id="ddlAvailableParameters" class="form-control"
                                                data-bind="options: AvailableParameters, optionsText: 'Value', optionsValue: 'Id', selectedOptions: SelectedAvailableParameters" multiple="">
                                            </select>--%>

                                             <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span> Assigned Parameter</label><br>
                                            <select id="ddlSelectedParameters" class="form-control"
                                                data-bind="options: AssignedParameters, optionsText: 'Value', optionsValue: 'Id', selectedOptions: SelectedAssignedParameters" multiple="">
                                            </select>

                                        </div>

                                        <div>
                                            <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                        </div>
                                        <p>&nbsp;</p>

                                        <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                            <%--<asp:Button ID="btnSubmit" runat="server" class="btn btn-primary" OnClick="btnSubmit_Click" /> --%>
                                            <button type="button" class="btn btn-primary" data-bind="click: Submit">Submit</button>
                                            <button type="button" class="btn btn-danger" data-bind="click: Cancel">Cancel</button>
                                        </div>

                                    </div>
                                    <input name="hdnBaseUrl" type="hidden" id="hdnBaseUrl" runat="server">
                                    <input name="hdnLiquourId" type="hidden" id="hdnLiquourId" runat="server">
                                    <input name="hdnSubLiqourId" type="hidden" id="hdnSubLiqourId" runat="server">
                                    <input name="hdnAssignedParameterId" type="hidden" id="hdnAssignedParameterId" runat="server">
                                </div>
                            </div>
                            <script>
                                function moveForwordUser() {
                                    moveItems('#ddlSelectedParameters', '#ddlAvailableParameters');
                                }

                                function moveBackwordUser() {
                                    moveItems('#ddlAvailableParameters', '#ddlSelectedParameters');
                                }
                                function moveForwordAllUser() {
                                    debugger;
                                    moveAllItems('#ddlSelectedParameters', '#ddlAvailableParameters');
                                }

                                function moveBackwordAllUser() {
                                    moveAllItems('#ddlAvailableParameters', '#ddlSelectedParameters');
                                }


                                function moveItems(origin, dest) {
                                    $(origin).find(':selected').appendTo(dest);
                                    $("#ddlSelectedParameters option").prop("selected", "selected");
                                }

                                function moveAllItems(origin, dest) {
                                    $(origin).children().appendTo(dest);
                                    $("#ddlSelectedParameters option").prop("selected", "selected");
                                }

                                var viewModel = function () {

                                    // alert("Bhavin - viewModel - Pg Load");
                                    debugger;
                                    var self = this;
                                    self.LiquorTypeList = ko.observableArray([]);
                                    self.SubLiquorTypeList = ko.observableArray([]);
                                    self.SelectedLiquorType = ko.observable("");
                                    self.SelectedSubLiquorType = ko.observable("");
                                    self.SelectedLiquorType.subscribe(function (newLiquorType) {
                                        self.SubLiquorTypeList.removeAll();
                                        if (newLiquorType) {
                                            self.loadSubLiquorTypes(newLiquorType);
                                        }
                                    });
                                    self.SelectedSubLiquorType.subscribe(function (newSubLiquorType) {
                                        self.AssignedParameters.removeAll();
                                        self.AvailableParameters.removeAll();
                                        if (newSubLiquorType) {
                                            self.loadAssignedAndUnAssignedParameters(newSubLiquorType);
                                        }
                                    });
                                    self.AssignedParameters = ko.observableArray();
                                    self.AvailableParameters = ko.observableArray();
                                    // alert(" Bhavin - AvailableParameters: " + self.AvailableParameters.length);
                                    self.SelectedAssignedParameters = ko.observableArray();
                                    self.SelectedAvailableParameters = ko.observableArray();
                                    self.QueryLiquorId = $('#<%=hdnLiquourId.ClientID%>').val();
                                    self.QuerySubLiquorId = $('#<%=hdnSubLiqourId.ClientID%>').val();
                                    self.AssignedParameterId = $("#<%=hdnAssignedParameterId.ClientID%>").val();
                                };

                                viewModel.prototype.loadLiquorTypes = function () {
                                    var self = this;
                                    $.ajax({
                                        type: "POST",
                                        url: "AssignParameterForm.aspx/GetLiquorTypes",
                                        data: {},
                                        datatype: "application/json",
                                        contentType: "application/json; charset=utf-8",
                                        cache: false,
                                        async: false,
                                        success: function (liquorTypes) {
                                            if (liquorTypes.d) {
                                                $.grep(liquorTypes.d, function (item) {
                                                    self.LiquorTypeList.push(item);
                                                });
                                                if (self.QueryLiquorId && Number(self.QueryLiquorId) > 0) {
                                                    self.SelectedLiquorType(self.QueryLiquorId);
                                                    //self.loadSubLiquorTypes(self.QuerySubLiquorId);
                                                }

                                            }
                                        }
                                    });
                                };

                                viewModel.prototype.loadSubLiquorTypes = function (newLiquorType) {
                                    var self = this;
                                    var liquorTypeData = {
                                        selectedLiquor: newLiquorType
                                    }
                                    var jsondata = JSON.stringify(liquorTypeData);
                                    $.ajax({
                                        type: "POST",
                                        url: "AssignParameterForm.aspx/GetSubLiquorTypes",
                                        data: jsondata,
                                        datatype: "application/json",
                                        contentType: "application/json; charset=utf-8",
                                        cache: false,
                                        async: false,
                                        success: function (msg) {
                                            if (msg.d) {
                                                $.grep(msg.d, function (item) {
                                                    self.SubLiquorTypeList.push(item);
                                                });
                                                if (self.QueryLiquorId && Number(self.QuerySubLiquorId) > 0) {
                                                    self.SelectedSubLiquorType(self.QuerySubLiquorId);
                                                    //self.loadAssignedAndUnAssignedParameters(self.QuerySubLiquorId);
                                                }
                                            }
                                        }
                                    });
                                };

                                viewModel.prototype.loadAssignedAndUnAssignedParameters = function (sub_liquor_type_id) {
                                    // alert("loadAssignedAndUnAssignedParameters");
                                    var self = this;
                                    var assignParameterData = {
                                        liquor: self.SelectedLiquorType(),
                                        subLiquor: sub_liquor_type_id
                                    }
                                    var jsondata = JSON.stringify(assignParameterData);
                                    $.ajax({
                                        type: "POST",
                                        url: "AssignParameterForm.aspx/GetAssignUnAssignParameter",
                                        data: jsondata,
                                        datatype: "application/json",
                                        contentType: "application/json; charset=utf-8",
                                        cache: false,
                                        async: false,
                                        success: function (msg) {
                                            if (msg.d) {
                                                if (msg.d.AssignedParameters) {
                                                    $.grep(msg.d.AssignedParameters, function (item) {

                                                        self.AssignedParameters.push(item);
                                                    });
                                                    // alert(self.AssignedParameters.item.length);
                                                }
                                                if (msg.d.UnAssignedParameters) {
                                                    $.grep(msg.d.UnAssignedParameters, function (item) {
                                                        // alert("Bhavin >> item: " + item.Id);
                                                        self.AvailableParameters.push(item);
                                                    });
                                                }
                                                //Bhavin_1-for loop
                                                //$.grep(self.AvailableParameters(), function (item) {
                                                //    //alert(self.AvailableParameters.Id);
                                                //    alert(item.Id);
                                                //});
                                            }
                                        }
                                    });
                                };

                                //Bhavin
                                viewModel.prototype.doubleright = function () {
                                    debugger;
                                    
                                    var self = this;
                                    //Bhavin_2 for loop
                                    //alert("Push Started");
                                    //alert("Count:" + self.AvailableParameters().length);
                                    $.grep(self.AvailableParameters(), function (item) {
                                        // alert(item.Id);
                                        self.AssignedParameters.push(item);

                                        //alert(item.Id);
                                        //self.AvailableParameters.remove(item);
                                    });
                                    //alert("push completed")
                                    ////Thread.Sleep(5000);
                                    //alert("Remove started")
                                    //alert("Count:" + self.AvailableParameters().length);

                                    for (var i = 0; i < self.AvailableParameters().length; i++) {

                                        $.grep(self.AvailableParameters(), function (item) {
                                            //alert("Count:" + self.AvailableParameters().length);                                   
                                            //alert(item.Id);
                                            // self.AssignedParameters.push(item);
                                            self.AvailableParameters.remove(item);
                                        });

                                    }


                                    //alert("Count:" + self.AvailableParameters().length);
                                    $.grep(self.AvailableParameters(), function (item) {
                                        //alert("Count:" + self.AvailableParameters().length);                                   
                                        //alert(item.Id);
                                        // self.AssignedParameters.push(item);
                                        self.AvailableParameters.remove(item);
                                    });

                                    $.grep(self.AvailableParameters(), function (item) {
                                        //alert("Count:" + self.AvailableParameters().length);                                   
                                        //alert(item.Id);
                                        // self.AssignedParameters.push(item);
                                        self.AvailableParameters.remove(item);
                                    });

                                   

                                };



                                


                             


                                //Bhavin
                                viewModel.prototype.doubleleft = function () {
                                    // alert("left");
                                    // debugger;
                                   

                                    var self = this;


                                    $.grep(self.AssignedParameters(), function (item) {
                                        // alert(item.Id);
                                        self.AvailableParameters.push(item);

                                    });


                                    // alert("Count:" + self.AssignedParameters().length);

                                    for (var i = 0; i < self.AssignedParameters().length; i++) {

                                        $.grep(self.AssignedParameters(), function (item) {
                                            //alert("Count:" + self.AvailableParameters().length);                                   
                                            //alert(item.Id);
                                            // self.AssignedParameters.push(item);
                                            self.AssignedParameters.remove(item);
                                        });

                                    }


                                    $.grep(self.AssignedParameters(), function (item) {
                                        //alert("Count:" + self.AvailableParameters().length);                                   
                                        //alert(item.Id);
                                        // self.AssignedParameters.push(item);
                                        self.AssignedParameters.remove(item);
                                    });

                                    $.grep(self.AssignedParameters(), function (item) {
                                        //alert("Count:" + self.AvailableParameters().length);                                   
                                        //alert(item.Id);
                                        // self.AssignedParameters.push(item);
                                        self.AssignedParameters.remove(item);
                                    });

                                    
                                };

                                viewModel.prototype.right = function () {

                                    // alert("left");
                                    debugger;
                                    var self = this;
                                    if (self.SelectedAvailableParameters().length == 0) {
                                        return alert("Please select at least one Un-Assigned Parameter to Move to Assigned")
                                    }
                                    $.grep(self.SelectedAvailableParameters(), function (item) {
                                        // alert("self.AvailableParameters(): " + self.AvailableParameters().length);
                                        var availableItem = ko.utils.arrayFirst(self.AvailableParameters(), function (searchItem) {
                                            // alert("item:" + item + "   -- searchItem: " + searchItem.Id);
                                            return searchItem.Id == item;
                                        });
                                        //  alert("Going to add and remove: " + availableItem.Id);
                                        if (availableItem) {
                                            self.AssignedParameters.push(availableItem);
                                            self.AvailableParameters.remove(availableItem);
                                        }
                                    });
                                  
                                };

                                viewModel.prototype.left = function () {
                                   
                                    // alert("right");
                                    debugger;
                                    var self = this;
                                    if (self.SelectedAssignedParameters().length == 0) {
                                        return alert("Please select at least one Assigned Parameter to Move to Un Assigned")
                                    }
                                    $.grep(self.SelectedAssignedParameters(), function (item) {
                                        var availableItem = ko.utils.arrayFirst(self.AssignedParameters(), function (searchItem) {
                                            return searchItem.Id == item;
                                        });
                                        if (availableItem) {
                                            self.AvailableParameters.push(availableItem);
                                            self.AssignedParameters.remove(availableItem);
                                        }
                                    });


                                };

                                viewModel.prototype.Submit = function () {

                                    debugger;
                                    //alert($('#ddlSelectedParameters').val());
                                    var self = this;
                                    var validateSubmit = function () {
                                        if (!self.SelectedLiquorType()) {
                                            return alert("Please Select Liquor Type");
                                        }
                                        if (!self.SelectedSubLiquorType()) {
                                            return alert("Please Select Sub Liquor Type");
                                        }
                                        if (!self.AssignedParameters() || self.AssignedParameters() == 0) {
                                            return alert("Please Include at least one in Assigned Parameter");
                                        }
                                        return true;
                                    }

                                    if (validateSubmit()) {
                                        var saveInputs = {
                                            postData: {

                                                LiquorTypeId: self.SelectedLiquorType(),
                                                SubLiquorTypeId: self.SelectedSubLiquorType(),
                                                AssignedParameter: self.AssignedParameters(),
                                                // AssignedParameter: $('#ddlSelectedParameters').val(),
                                                AssignedParameterId: self.AssignedParameterId && Number(self.AssignedParameterId) ? self.AssignedParameterId : 0
                                            }
                                        }
                                        $.ajax({
                                            type: "POST",
                                            url: "AssignParameterForm.aspx/SaveParameterAssignment",
                                            // url: "AssignParameterForm.aspx/SaveParameterAssignment?ddlSelectedParameters="+$('#ddlSelectedParameters').val(),
                                            data: JSON.stringify(saveInputs),
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (saveInfo) {
                                                if (saveInfo.d) {
                                                    alert(saveInfo.d.Item2);
                                                    if (saveInfo.d.Item1) {
                                                        self.Cancel();
                                                    }
                                                }
                                            }
                                        });
                                    }
                                };

                                viewModel.prototype.Cancel = function () {
                                    window.location = $('#<%=hdnBaseUrl.ClientID%>').val() + "AssignParameterList.aspx";
                                };

                                $(function () {

                                    var self = new viewModel();
                                    ko.options.deferUpdates = true;
                                    ko.applyBindings(self, document.getElementById("dvAddAssignParameter"));
                                    self.loadLiquorTypes();
                                });
                            </script>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
