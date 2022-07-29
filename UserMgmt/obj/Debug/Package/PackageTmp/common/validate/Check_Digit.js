function check_digit(e, obj, intsize, deczize) {
    var keycode;
    var charCode = (e.which) ? e.which : e.keyCode
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
    {
//        alert("Please Enter Numeric value");
        if (e.keyCode == '38') {
            // up arrow
            return true;
        }
        else if (e.keyCode == '40') {
            // down arrow
            return true;
        }
        else if (e.keyCode == '37') {
            // left arrow
            return true;
        }
        else if (e.keyCode == '39') {
            // right arrow
            return true;
        }
        return false;
    }
    if (window.event)
        keycode = window.event.keyCode;
    else if (e) {
        keycode = e.which;

    } else {
        return true;
    }
    var fieldval = (obj.value),
            dots = fieldval.split(".").length;
    if (keycode === 46) {
        return dots <= 1;
    }
    if (keycode == 8 || keycode == 9 || keycode == 46 || keycode == 13 || keycode != 46 || keycode != 8) {
        // back space, tab, delete, enter
        return true;
    }
    if ((keycode >= 32 && keycode <= 45) || keycode === 47 || (keycode >= 58 && keycode <= 127)) {
        return false;
    }
    if (fieldval === "0" && keycode === 48) {
        return false;
    }
    if (fieldval.indexOf(".") !== -1) {
        if (keycode === 46) {
            return false;
        }

        var splitfield = fieldval.split(".");
        if (splitfield[1].length >= deczize && keycode !== 8 && keycode !== 0)
            return false;
    } else if (fieldval.length >= intsize && keycode !== 46) {
        return false;
    } else {
        return true;
    }
}