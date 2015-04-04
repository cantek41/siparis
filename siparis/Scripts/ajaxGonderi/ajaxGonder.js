function sepeteEkle(id)
{
    $.ajax({
        url: "/Order/AddCart",
        type: 'POST',
        data: {stokID:id},
        datatype: 'json',
        success: function (data) {           
            if (data == "True") {
              
                    $().toastmessage('showSuccessToast', "Sepete Eklendi");
                  
            }


            else {
                
                $().toastmessage('showErrorToast', "İşlem Başarısız");
            }

        },
        error: function (data) {
            alert("Server Hatası"+data.toString());
        }
    });
}