$(document).ready(function () {

    $(".collapsinga").click(function () {
        var PDiv = $(this).parent().prev();

        if ($(this).hasClass("collapsed")) {
            PDiv.animate({ height: '150px' }, 300);
            $(this).removeClass("collapsed");
            $(this).html("Show More...");
        }
        else {
            console.log("Hello");
            

            PDiv.animate({ height: '100%' }, 1);

            // Record the height of the div
            var newHeight = PDiv.height();
            console.log(newHeight);

            PDiv.animate({ height: 150 }, 1);
            PDiv.animate({ height: newHeight }, 300);

            $(this).html("Show less...");
            $(this).addClass("collapsed");

        }


        
    });

    $('#summary-btn').click(function (e) {
        e.preventDefault();

        $("#payment-div").slideDown(200);
        $(this).addClass("disabled");
    });

    $('input[name=payment-select]').change(function () {
        $('#payment-btn').removeClass("disabled");
    });
    $('#payment-btn').click(function (e) {
        e.preventDefault();

        $("#payment-div").slideUp(300, function () {
            $('#user-div').slideDown(300)
        });
    });

    


    $('#userInfo-form').submit(function(e){
        e.preventDefault();

        var formData = $(this).serialize();

        $.ajax({
            type: "POST",
            url: '/Cart/AddUserInfo',
            data: formData,
            success: function (d) {
                
                if (d.success) {
                    $("#user-div").slideUp(300, function () {
                        $("#checkout-div").slideDown(300);
                    });
                    
                } else {
                    console.log(d.message);
                }
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
            }
        });
    });

});