(function ($) {
    $.TRSearch = {
        // Public methods
        // Object Properts homeId, Key, OutPutID, OutPutName, Method 
        TRSearch: function (objSearch) {
            var obj = jQuery.parseJSON(objSearch);
            //            if (obj.SearchLength > 4) {
            $('.TRSearch').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: obj.Method,
                        dataType: "JSON",
                        data: objSearch,
                        async: false,
                        success: function (data) {
                            if (data.d != '0') {
                                var objData = jQuery.parseJSON(data.d);
                                response($.map(objData, function (item) {
                                    return {
                                        label: item.FirstName,
                                        val: item.ID
                                    }
                                }));
                            }
                            else {
                                window.location = 'frmLogin.aspx';
                            }
                        },
                        error: function (result) {
                            jAlert('Failed', 'TR Search');
                        }
                    });
                },
                select: function (event, ui) {
                    if (obj.OutPutID != '') {
                        $('#' + obj.OutPutID).val(ui.item.val);
                        $('#' + obj.OutPutName).val(ui.item.label);
                    }
                    if (event.keyCode == 9 || event.keyCode == 13) {
                        $('#' + obj.OutPutID).val(ui.item.val);
                        $('#' + obj.OutPutName).val(ui.item.label);
                    }
                }
            });
            // } 
        }
    }
    // Shortuct functions
    TRSearch = function (objSearch) {
        $.TRSearch.TRSearch(objSearch);
    }
})(jQuery);