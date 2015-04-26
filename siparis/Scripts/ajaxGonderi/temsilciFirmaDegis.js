$(document).ready(function () {
    $("#Company").change(function (event) {
        var code = $(this).val();        
        $.ajax({
            url: "/Order/setChartCompany",
            data: {id:code},            
            type: "POST",           
            datatype: 'json',
            success: function (data) {                
                if (data == "True") {
                    $().toastmessage('showSuccessToast', "Firma Değişti");
                }
                else {
                    $().toastmessage('showErrorToast', "İşlem Başarısız");
                }

            }
        });
    });
});