    $(document).ready(function () {
        $("#COLOR_CODE").change(function (e) {
            $("#BODY_CODE").empty();
            var obj1 = { color: $("#COLOR_CODE").val(),upper:$("#UPPER_CODE").val() };
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "GetBody",
                data: JSON.stringify(obj1),
                dataType: "json",
                success: function (data1) {
                    var appenddata1 = "";                  
                    $.each(data1, function () {
                        appenddata1 += "<option value = '" + this.Key + " '>" +this.Text + " </option>";
                    })
                    $("#BODY_CODE").append(appenddata1);
                }
            });
        });
        $("#COLOR_CODE").change();
    });
