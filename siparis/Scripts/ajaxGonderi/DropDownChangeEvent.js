$(document).ready(function () {
    alert(($("#BODY_CODE option").length));
    if (($("#COLOR_CODE option").length) <= 1) $("#COLOR_CODE").prop("disabled", true);
    if (($("#BODY_CODE option").length) <= 1) $("#BODY_CODE").prop("disabled", true);
        $("#COLOR_CODE").change(function (e) {
            $("#BODY_CODE").empty();
            var obje = { color: $("#COLOR_CODE").val(),upper:$("#UPPER_CODE").val() };
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "GetBody",
                data: JSON.stringify(obje),
                dataType: "json",
                success: function (data) {
                    var appenddata1 = "";                  
                    $.each(data, function () {
                        appenddata1 += "<option value = '" + this.Key + " '>" +this.Text + " </option>";
                    })
                    $("#BODY_CODE").append(appenddata1);
                    
                    $("#BODY_CODE").change();
                }
            });
        });
       
        $("#BODY_CODE").change(function (e) {            
            var obje = { color: $("#COLOR_CODE").val(), upper: $("#UPPER_CODE").val(), body: $("#BODY_CODE").val() };
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "GetProduct",
                data: JSON.stringify(obje),
                dataType: "json",
                success: function (data) {
                   
                    //alert(data.ID);
                    $("#ID").val(data.ID);
                    $("#STOK").val(data.UNIT);
                    $("#CODE").val(data.CODE);
                    $("#UNIT_PRICE").val(data.UNIT_PRICE + " " + data.CUR_TYPE);
                   
                   
                }
            });
        });
        $("#COLOR_CODE").change();
       
    });
