function sepeteEkle(id)
{
    $.ajax({
        url: "/Order/AddCart",
        type: 'POST',
        data: {stokID:id},
        datatype: 'json',
        success: function (data) {           
            if (data == "True") {
                //alert("Sepete Eklendi");
             //   $.toaster({ priority: 'success', title: 'Title', message: 'Your message here' });
                    $().toastmessage('showSuccessToast', "Ürün Sepete Eklendi");
                   // $('.error').fadeIn(400).delay(3000).fadeOut(400);

                //}
               // $.simplyToast('success', 'This is a success message!');
            }


            else {
                //alert("İşlem Başarısız");
                $().toastmessage('showErrorToast', "İşlem Başarısız");
            }

        },
        error: function (data) {
            alert("Server Hatası"+data.toString());
        }
    });
}