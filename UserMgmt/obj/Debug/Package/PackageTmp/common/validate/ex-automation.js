/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

var csmlForms = {
    addcsml_form26_data: function () {
        // Get the table object to use for adding a row at the end of the table
        var $agentTable = $('#csmlForm26');
        var $row1 = $(csmlRowData);
        $('.csml_data_grid:last', $agentTable).after($row1);
        var $itemRow = $(this).closest('tr');
        $itemRow.find().focus();
    },
    delcsml_form26_data: function (row) {
        var rowCount = $('#csmlForm26 tr:visible').length;
        if (rowCount !== 2) {
            $(row).parents('.csml_data_grid').remove();
//            $("tr.csml_data_grid").each(function () {
//                var $itemRow = $(this).closest('tr');
//                var rowCount = ($(this).closest("tr")[0].rowIndex);               
//            });
            return true;
        } else {
            alert('You cannot delete this row');
            return false;
        }

    }
};
var csmlRowData = [
    '<tr class="csml_data_grid">' +
            '<td align="center" onclick=""><button type="button" style="button" id="deleteCsmlData"><i class="fa fa-trash-o"></i></button></td>' +
            '<td><input class="form-control" autocomplete="off"  type="text" id="agentName" name="agentName" data-toggle="tooltip" data-placement="right" title="Name" maxlength="30"  style="width: 100%"><p id="agentNameError" style="color: red; font-weight: bold; display: none; text-align: left;">Please enter Name</p></td>' +
            '<td><input class="form-control" autocomplete="off"  type="text" id="agentFname" name="agentFname" data-toggle="tooltip" data-placement="right" title="Fathers Name" maxlength="30" style="width: 100%"><p id="agentFnameError" style="color: red; font-weight: bold; display: none; text-align: left;">Please enter Fathers Name</p></td>' +
            '<td><input class="form-control" autocomplete="off"  type="text" id="agentAge" name="agentAge" data-toggle="tooltip" data-placement="right" title="Age" maxlength="2" onkeypress="return isNumberKeyWithoutDot(event);" style="width: 100%"><p id="agentAgeError" style="color: red; font-weight: bold; display: none; text-align: left;">Please enter Age</p></td>' +
            '<td><input class="form-control" autocomplete="off"  type="text" id="agentVillage" name="agentVillage" data-toggle="tooltip" data-placement="right" title="Native village, Thana & District" maxlength="30" style="width: 100%"><p id="agentVillageError" style="color: red; font-weight: bold; display: none; text-align: left;">Please enter the mentioned details</p></td>' +
            '</tr>'

].join('');


