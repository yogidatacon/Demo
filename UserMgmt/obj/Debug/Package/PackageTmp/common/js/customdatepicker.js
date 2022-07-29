$(".calendarGiftDate").datepicker({
    changeMonth: true,
    changeYear: true,
    yearRange: '1987:2050',
    nextText: '&rarr;',
    prevText: '&larr;',
    showOtherMonths: true,
    showButtonPanel: true,
    dateFormat: 'm/d/yy',
    dayNamesMin: ['S', 'M', 'T', 'W', 'T', 'F', 'S'],
    showOn: "button",
    buttonImage: "",
    buttonImageOnly: false,
    beforeShow: function (input) {
        localStorage.setItem("GiftDatePicker", true);
        $(input).attr('readonly', true);
    },
    onClose: function (dateText, inst) {
        localStorage.setItem("GiftDatePicker", false);
        PGM.Web.Application.GiftDateAndTermVM.GiftDateChanged();

        $(this).removeAttr('readonly', false);
        return false;
    },
    onChangeMonthYear: function (year, month, inst) {
        var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
        var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
        $(this).datepicker('setDate', new Date(year, month, inst.currentDay));
        $('.ui-datepicker-calendar').removeClass('ui-state-active');
        $('.ui-datepicker-calendar').find(inst.currentDay).addClass('ui-state-active');
    }
});