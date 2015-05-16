/*ürün detay sayfasındaki textarea alanının yüksekliğini ayarlar*/
function textAreaAdjust(obje) {

    obje.style.height = "1px";
    obje.style.height = (25 + obje.scrollHeight) + "px";
}



$(document).ready(function () {
    //renk değeri 9(Diğer) ise renk divini gizliyor
    var renk = $("#COLOR_CODE").val();
    var fiyat = $("#UNIT_PRICE").val();
 
    if (fiyat=="") {
        $("#UNIT_PRICE").val('Ürünün fiyat bilgisi olamadığından sepete ekleyemezsiniz.');
        $("#UNIT_PRICE").removeClass("product-info-price");
        $("#UNIT_PRICE").addClass("no-product-info-price");
       
    }

    if (renk == 9) $("#divColor").hide();
    else $("#divColor").show();
    //alert(($("#BODY_CODE option").length));
    if (($("#COLOR_CODE option").length) <= 1) $("#COLOR_CODE").prop("disabled", true);
    else $("#COLOR_CODE").prop("disabled", false);

   
    $("#COLOR_CODE").change(function (e) {
        $("#BODY_CODE").empty();
        var obje = { color: $("#COLOR_CODE").val(), upper: $("#UPPER_CODE").val() };
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "GetBody",
            data: JSON.stringify(obje),
            dataType: "json",
            success: function (data) {
                var appenddata1 = "";
                $.each(data, function () {
                    appenddata1 += "<option value = '" + this.Key + " '>" + this.Text + " </option>";
                })
                $("#BODY_CODE").append(appenddata1);

                $("#BODY_CODE").change();
                if (($("#BODY_CODE option").length) <= 1) $("#BODY_CODE").prop("disabled", true);
                else $("#BODY_CODE").prop("disabled", false);
            }
        });
       
       
    });
   
    $("#BODY_CODE").change(function (e) {
        //alert(($("#BODY_CODE option").length));
        var obje = { color: $("#COLOR_CODE").val(), upper: $("#UPPER_CODE").val(), body: $("#BODY_CODE").val(), DES_TR: $("#DES_TR") };
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
                $("#DES_TR").val(data.DES_TR);
                $("#DES_TR").click();

            }
        });
        //beden değeri 1(Diğer) ise beden divini gizliyor
        var beden = $("#BODY_CODE").val();
        if (beden == 1) $("#divBody").hide();
        else $("#divBody").show();
    });
    $("#COLOR_CODE").change();

    $("#QUANTITY").change(function (e) {
        var quantity= $("#QUANTITY").val();
        var stok = $("#STOK").val();
        if (quantity>stok) {
            $().toastmessage('showStickyWarningToast', "Miktar hatalı!!");
            $("#QUANTITY").val(stok);
        }


    });
});


