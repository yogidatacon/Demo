/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
function valid(id, type) {
    var retval = true;
    var name = document.getElementById(id).value.trim();
    var emptrslt = mandatoryField(name);
    var invrslt = true;
    if (type == "name") {
        invrslt = nameValidate(name);
    }
    else if (type == "mail") {
        invrslt = emailValidation(name);
    }
    else if (type == "panno") {
        invrslt = pancardValidate(name);
    }
    else if (type == "mobileno") {
        invrslt = IsMobileNumber(name);
    }
    else if (type == "onlyNumber") {
        invrslt = onlyNumber(name);
    }
    else if (type == "Age") {
        invrslt = ageValidate(name);
    }
    if (emptrslt == false || invrslt == false) {
        retval = false;
    }
    return retval;
}

function checkField(elearray, divarray) {
    var j = 0;
    for (var i = 0; i < elearray.length; ++i) {

        if (elearray[i] == false) {
            $("#" + divarray[i]).show();
            j++;
        }
    }
    for (var k = 0; k < elearray.length; ++k) {
        if (elearray[k] == true) {
            $("#" + divarray[k]).hide();
        }
    }
    if (j > 0) {
        return false;
    } else {
        return true;
    }
}
function validGrid($this) {
    var retval = true;
    var name = $($this).val();
    var emptrslt = mandatoryField(name);
    if (emptrslt == false) {
        retval = false;
    }
    return retval;
}
function checkFieldGrid(elearray, divarray) {
    var j = 0;
    for (var i = 0; i < elearray.length; ++i) {

        if (elearray[i] == false) {
            $(divarray[i]).show();
            j++;
        }
    }
    for (var k = 0; k < elearray.length; ++k) {
        if (elearray[k] == true) {
            $(divarray[k]).hide();
        }
    }
    if (j > 0) {
        return false;
    } else {
        return true;
    }
}

function checkFields(elearray, divarray) {
    var j = 0;
    for (var i = 0; i < elearray.length; ++i) {

        if (elearray[i] == false) {
            $("#" + divarray[i]).show();
            j++;
        }
    }
    for (var k = 0; k < elearray.length; ++k) {
        if (elearray[k] == true) {
            $("#" + divarray[k]).hide();
        }
    }
    if (j > 0) {
        return false;
    }
    else
    {
        return true;
    }
}

//Pancard validation like AHSTJ4567H
function pancardValidate(pancardNumber) {
    var letterRegex = /^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$/;
    if (letterRegex.test(pancardNumber)) {
        return true;
    }
    return false;
}

//Name validation like User name/Employee name/Father name etc.
function nameValidate(name) {
    var letterRegex = /^[0-9a-zA-Z_-\s]+$/;
    if (letterRegex.test(name)) {
        return true;
    }
    return false;
}

//Email validation using regex like demo@yahoo.com
function emailValidation(email) {
    var atpos = email.indexOf("@");
    var dotpos = email.lastIndexOf(".");
    if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= email.length) {
        return false;
    }
    else {
        return true;
    }
}

// Allow only integer/number for a specific field based on onkeypress events
function isNumberKey(event)
{

    var charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57 || charCode == 8))
    {
        return false;
    }
    else
    {
        return true;
    }
}
// Allow only integer/number without dot for a specific field based on onkeypress events
function isNumberKeyWithoutDot(event)
{

    var charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57 || charCode == 8))
    {
        return false;
    }
    else
    {
        return true;
    }
}

//Compare dates fromdate and todate
function compareDate(fromDate, toDate) {
    if (toDate > fromDate) {
        return true;
    }
    else {
        return false;
    }
}

//Mandatory field validation
function mandatoryField(value) {
    if (value == "" || 0 === value.length) {
        return false;
    }
    else {
        return true;
    }
}

//Mobile number validation
function IsMobileNumber(txtMobId) {
    var mob = /^[1-9]{1}[0-9]{9}$/;
    if (mob.test(txtMobId) == false) {
        return false;
    }
    return true;
}
//Only number validation
function onlyNumber(txtMobId) {
    var mob = /^[0-9]*$/;
    if (mob.test(txtMobId) == false) {
        return false;
    }
    return true;
}

//file upload validation
function readURL(input) {
    $('#blah').show();
    var imageId = input.id;
    checkImageSize(input, imageId);
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#blah')
                    .attr('src', e.target.result)
                    .width(120)
                    .height(120);
        };

        reader.readAsDataURL(input.files[0]);
    }
}


function checkImageSize(obj, id1)
{
    var size = ($("#" + id1)[0].files[0].size) / (200 * 230);
    if (parseFloat(size) > 9)
    {
        alert("Exceeding File Size");
        $("#" + id1).val("");
    }
}

//Image upload validation

function readURL1(input) {
    $('#blah1').show();
    var imageId = input.id;
    checkImageSize1(input, imageId);
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#blah1')
                    .attr('src', e.target.result)
                    .width(120)
                    .height(120);
        };

        reader.readAsDataURL(input.files[0]);
    }
}


function checkImageSize1(obj, id1)
{
    var size = ($("#" + id1)[0].files[0].size) / (1024);
    if (parseFloat(size) > 5120)
    {
        alert("More than 5 mb file size is not allowed");
        $("#" + id1).val("");
    } else {
        Validate(obj, id1);
    }
}
function Validate(obj, id1)
{
    var image = document.getElementById(id1).value;
    if (image != '') {
        var checkimg = image.toLowerCase();
        if (!checkimg.match(/(\.jpg|\.png|\.JPG|\.PNG|\.jpeg|\.JPEG|\.doc|\.DOC|\.docx|\.DOCX)$/)) {
            alert("Please enter Image File Extensions .jpg,.png,.jpeg, .doc, .docx");
            $("#" + id1).focus();
            $("#" + id1).val("");
            return false;
        }
    }
    return true;
}

//Image & file upload validation

function readURL2(input) {
    //$('#blah1').show();
    var imageId = input.id;
    checkImageSizefile(input, imageId);
    if (input.files && input.files[0]) {
        var reader = new FileReader();

//        reader.onload = function (e) {
//            $('#blah1')
//                    .attr('src', e.target.result)
//                    .width(120)
//                    .height(120);
//        };

        reader.readAsDataURL(input.files[0]);
    }
}


function checkImageSizefile(obj, id1)
{
    var size = ($("#" + id1)[0].files[0].size) / (200 * 230);
    if (parseFloat(size) > 45)
    {
        alert("Exceeding File Size");
        $("#" + id1).val("");
    } else {
        Validatefile(obj, id1);
    }
}
function Validatefile(obj, id1)
{
    var image = document.getElementById(id1).value;
    if (image != '') {
        var checkimg = image.toLowerCase();
        if (!checkimg.match(/(\.jpg|\.png|\.JPG|\.PNG|\.jpeg|\.JPEG|\.xls|\.XLS|\.xlsx|\.XLSX|\.doc|\.DOC|\.docx|\.DOCX|\.pdf|\.PDF)$/)) {
            alert("Please select following File Extensions .jpg,.png,.jpeg,.xls,.xlsx,.doc,.docx,.pdf");
            $("#" + id1).focus();
            $("#" + id1).val("");
            return false;
        }
    }
    return true;
}

//Image & file upload validation

function readURL3(input) {
    //$('#blah1').show();
    var imageId = input.id;
    checkAttachmentSizefile(input, imageId);
    if (input.files && input.files[0]) {
        var reader = new FileReader();

//        reader.onload = function (e) {
//            $('#blah1')
//                    .attr('src', e.target.result)
//                    .width(120)
//                    .height(120);
//        };

        reader.readAsDataURL(input.files[0]);
    }
}


function checkAttachmentSizefile(obj, id1)
{
    var size = ($("#" + id1)[0].files[0].size) / (200 * 230);
    if (parseFloat(size) > 45)
    {
        alert("Exceeding File Size");
        $("#" + id1).val("");
    } else {
    	ValidateAttachment(obj, id1);
    }
}
function ValidateAttachment(obj, id1)
{
    var image = document.getElementById(id1).value;
    if (image != '') {
        var checkimg = image.toLowerCase();
        if (!checkimg.match(/(\.jpg|\.png|\.JPG|\.PNG|\.jpeg|\.JPEG|\.doc|\.DOC|\.docx|\.DOCX|\.pdf|\.PDF)$/)) {
            alert("Please select following File Extensions .jpg, .png, .jpeg, .doc, .docx, .pdf");
            $("#" + id1).focus();
            $("#" + id1).val("");
            return false;
        }
    }
    return true;
}
// word, xlx, pdf files
function readURL4(input) {
    //$('#blah1').show();
    var imageId = input.id;
    checkAttachmentSizefile1(input, imageId);
    if (input.files && input.files[0]) {
        var reader = new FileReader();

//        reader.onload = function (e) {
//            $('#blah1')
//                    .attr('src', e.target.result)
//                    .width(120)
//                    .height(120);
//        };

        reader.readAsDataURL(input.files[0]);
    }
}


function checkAttachmentSizefile1(obj, id1)
{
    var size = ($("#" + id1)[0].files[0].size) / (200 * 230);
    if (parseFloat(size) > 45)
    {
        alert("Exceeding File Size");
        $("#" + id1).val("");
    } else {
    	ValidateAttachment1(obj, id1);
    }
}
function ValidateAttachment1(obj, id1)
{
    var image = document.getElementById(id1).value;
    if (image != '') {
        var checkimg = image.toLowerCase();
        if (!checkimg.match(/(\.doc|\.DOC|\.docx|\.DOCX|\.xls|\.XLS|\.xlsx|\.XLSX|\.pdf|\.PDF)$/)) {
            alert("Please select following File Extensions .xls, .xlsx, .doc, .docx, .pdf");
            $("#" + id1).focus();
            $("#" + id1).val("");
            return false;
        }
    }
    return true;
}

//special characters are not allowed
function isNumberKey4(evt) {
    debugger;
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if (charCode > 32 && (charCode < 48 || charCode > 57) && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122))
    {
//        alert("Special Characters are not allowed");
        return false;
    }

}

//Allow only alphabets and numbers with backspace and space key for a specific field based on onkeypress events
function isNumberKey3(event) {
    var key = window.event ? event.keyCode : event.which;

    if (event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 32) {
        return true;
    } else if ((event.keyCode == 32) && (key < 65 || key > 90) && (key < 97 || key > 122)) {
//        alert("Please Enter Only Alphabets");
        return false;
    } else
        return true;
}

//Age Calculation
function ageCalculate() {
    var dob = document.getElementById('dob').value;
    var dob1 = dob.split("/");
    var y1 = dob1[2];
    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth() + 1;
    var y = date.getFullYear();
    var age = y - y1;
    document.getElementById('age').value = age;
    return true;
}

//Age validation
function ageValidate(age) {

    if (age < 21) {
        return false;
    } else {
        document.getElementById('age').value = age;
        return true;
    }
}

//Double(Float) type validation
function check_digit(e, obj, intsize, deczize) {
    var keycode;
    if (window.event)
        keycode = window.event.keyCode;
    else if (e) {
        keycode = e.which;
    }
    else {
        return true;
    }

    var fieldval = (obj.value),
            dots = fieldval.split(".").length;
    if (keycode == 46) {
        return dots <= 1;
    }
    if (keycode == 8 || keycode == 9 || keycode == 46 || keycode == 13) {
        // back space, tab, delete, enter 
        return true;
    }
    if ((keycode >= 32 && keycode <= 45) || keycode == 47 || (keycode >= 58 && keycode <= 127)) {
        return false;
    }
    if (fieldval == "0" && keycode == 48) {
        return false;
    }
    if (fieldval.indexOf(".") != -1) {
        if (keycode == 46) {
            return false;
        }
        var splitfield = fieldval.split(".");
        if (splitfield[1].length >= deczize && keycode != 8 && keycode != 0)
            return false;
    } else if (fieldval.length >= intsize && keycode != 46) {
        return false;
    } else {
        return true;
    }
}

//Old Password and new Password comparision
function comparePassword(pass, repass) {
    if (pass === repass) {
        return true;
    }
    else {
        return false;
    }
}

//validate Password
function checkPassword(password)
{
    var passPattern = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,15}$/;
    if (passPattern.test(password))
    {
        return true;
    }
    else
    {
        return false;
    }
}

//Calcualte LP litres id1(Strength),id2(Bulk ltr),id3(L P litre)
//In JSP Jquery-Eg:
//function calculatelpltr() {
//    calclplitre('strength', 'bulkLtr', 'lpltr');
//}
function calclplitre(id1, id2, id3) {
    var strength = document.getElementById(id1).value;
    var bulkltr = document.getElementById(id2).value;
    if (strength === null || strength === "") {
        document.getElementById(id1).value = 0;
//        document.getElementById(id2).value = "";
    } else {
        strength = (100 + parseFloat(strength)) / 100;
        var lp = parseFloat(bulkltr) * strength;
        lp = parseFloat(Math.round(lp * 1000) / 1000).toFixed(3);
        if (!isNaN(lp)) {
            document.getElementById(id3).value = parseFloat(lp);
        }
        else {
            document.getElementById(id3).value = 0;
        }
    }
}

//function dateFyear(id1, id2, id3) {
//    var date = document.getElementById(id1).value;
//    var fyear = document.getElementById(id2).value;
//    var fyearspl = fyear.split("-");
//    var datespl = date.split("-");
//    var datecomp = new Date(datespl[1] + '/' + datespl[0] + '/' + datespl[2]);
//    if (fyearspl != '' && datespl != '') {
//        if (fyearspl[0] == datespl[2] || datespl[2] == fyearspl[1]) {
////        document.getElementById(id3).style.display = 'none';
//            return true;
//        } else {
//            alert("Please enter the date between Financial year...!");
//            document.getElementById(id1).value = "";
////        document.getElementById(id3).style.display = 'block';
//            return false;
//        }
//    }
//}

function dateFyear(id1, id2, id3) {
    var date = document.getElementById(id1).value;
    var fyear = document.getElementById(id2).value;

    var fyearspl = fyear.split("-");
    var datespl = date.split("/");

    var dateFrom = "01/Apr/" + fyearspl[0];
    var dateTo = "31/Mar/" + fyearspl[1];
    var dateNow = moment.utc(date, 'DD/MM/YYYY').local().format('DD/MMM/YYYY');

    if (fyearspl != '' && datespl != '') {
        if (Date.parse(dateFrom) <= Date.parse(dateNow) && Date.parse(dateTo) >= Date.parse(dateNow)) {
//        document.getElementById(id3).style.display = 'none';
            return true;
        } else {
            alert("Please enter the date between Financial year...!");
            document.getElementById(id1).value = "";
//        document.getElementById(id3).style.display = 'block';
            return false;
        }
    }
}

function CheckDecimal(inputtxt)
{
    var decimal = /^[-+]?[0-9]+\.[0-9]+$/;
    if (decimal.test(inputtxt.value))
    {
        return true;
    }
    else
    {
        return false;
    }
}
function validateFloatKeyPress(el, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var number = el.value.split('.');
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    //just one dot (thanks ddlab)
    if (number.length > 1 && charCode == 46) {
        return false;
    }
    //get the carat position
    var caratPos = getSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
        if (charCode == 8) {
            return true;
        } else {
            return false;
        }
    }
    return true;
}
function getSelectionStart(o) {
    if (o.createTextRange) {
        var r = document.selection.createRange().duplicate()
        r.moveEnd('character', o.value.length)
        if (r.text == '')
            return o.value.length
        return o.value.lastIndexOf(r.text)
    } else
        return o.selectionStart
}

function quintalKgConversion(id1, id2, changeType) {

    var ob1 = document.getElementById(id1).value;
    if (ob1 == null || ob1 == '')
    {
        ob1 = parseFloat(0);
    }
    if (changeType === "Q2K") {
        var kg = parseFloat(ob1) * 100;
        document.getElementById(id2).value = kg.toFixed(2);
    }
    else if (changeType === "K2Q") {
        var Qlts = parseFloat(ob1) / 100;
        document.getElementById(id2).value = Qlts.toFixed(2);
    }
}

function onlyAlphabetsNums(e, t) {
    try {
        if (window.event) {
            var charCode = window.event.keyCode;
        }
        else if (e) {
            var charCode = e.which;
        }
        else {
            return true;
        }
        if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || !(charCode < 48 || charCode > 57) || (charCode == 8) || (charCode == 32))
            return true;
        else
            return false;
    }
    catch (err) {
        alert(err.Description);
    }
}

function uniqueNessscm(id, className, columnName, err) {

    var uniqName = document.getElementById(id).value.trim();
    $.ajax
            ({
                type: "POST",
                url: '../SCM Masters/chkunique1.htm',
                data: {
                    uniqName: uniqName,
                    className: className,
                    columnName: columnName
                },
                success: function (data) {
                    if ($.trim(data) === 'Already Exists.Enter another') {
//                        document.getElementById(err).style.display = 'block';
                        alert('Record already exists,Please enter another..');
                        document.getElementById(id).value = "";
                        return false;
                    } else {
//                        document.getElementById(err).style.display = 'none';
                        return true;
                    }
                }
            });
}

function uniqueNess(id, className, columnName, err) {
    var uniqName = document.getElementById(id).value.trim();
    $.ajax
            ({
                type: "POST",
                url: '../login/unique.htm',
                data: {
                    uniqName: uniqName,
                    className: className,
                    columnName: columnName
                },
                success: function (data) {
                    if ($.trim(data) === 'Already Exists.Enter another') {
//                        document.getElementById(err).style.display = 'block';
                        alert('Record already exists,Please enter another..');
                        document.getElementById(id).value = "";
                        return false;
                    } else {
//                        document.getElementById(err).style.display = 'none';
                        return true;
                    }
                }
            });
}

function dateconvert(input) {

    var date = input.split("-");
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    for (var j = 0; j < months.length; j++) {
        if (date[1] == months[j]) {
            date[1] = months.indexOf(months[j]) + 1;
        }
    }
    if (date[1] < 10) {
        date[1] = '0' + date[1];
    }
    var formattedDate = date[2] + "-" + date[1] + "-" + date[0];
    return formattedDate;
}

function securityValid(id, className, columnName, err) {
    var uniqName = document.getElementById(id).value;
    var msg = "";

    if (id === "email")
    {
        msg = "Email-ID";
    }
    if (id === "mobile")
    {
        msg = "Mobile number";
    }
    $.ajax
            ({
                type: "POST",
                url: '../login/securityValid.htm',
                data: {
                    uniqName: uniqName,
                    className: className,
                    columnName: columnName
                },
                success: function (data) {
                    if ($.trim(data) === 'Already Exists.Enter another') {
//                        document.getElementById(err).style.display = 'block';                        
//                        return true;
                        return true;
                    } else {
//                        document.getElementById(err).style.display = 'none';
                        alert(msg + " doesn't exist..!");
                        document.getElementById(id).value = "";
//                        return false;
                        return  false;
                    }
                }
            });

}

function mandatorydate(id,evt)
{
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 43 || charCode > 43))
    {
        alert("Enter Numeric value");
         document.getElementById(id).value = "";
        return false;
    } else {

        var dt = document.getElementById(id).value;
        var finDt = "";
        if (dt.match(/^\d{2}$/) !== null) {
            if (dt > 31) {
                alert("Invalid Date Entered");
                document.getElementById(id).value = "";
            } else {
                finDt = dt + '/';
                document.getElementById(id).value = finDt;
            }
        } else if (dt.match(/^\d{2}\/\d{2}$/) !== null) {
            var dtSp = dt.split("/");
            if (dtSp[1] > 12) {
                alert("Invalid Month Entered");
                document.getElementById(id).value = "";
            } else {
                finDt = dt + '/';
                document.getElementById(id).value = finDt;
            }
        }
//                
    }
}

function attachMand(id, id1) {
    var attachname = document.getElementById(id1).value;
    var val = document.getElementById(id).value;
    if (attachname === '' || attachname === null) {
        if (val === '' || val === null) {
            alert("Attach a file")
            document.getElementById(id1).value = "";
            return false;
        } else {
            return true;
        }

    }
}

