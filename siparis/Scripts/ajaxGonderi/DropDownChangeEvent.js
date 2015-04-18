    $(document).ready(function () {
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
                   
                        $("#ID").val(data);
                   
                   
                }
            });
        });
        $("#COLOR_CODE").change();
        
    });
