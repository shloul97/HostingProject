$(document).ready(function () {

    var action;
    if (localStorage.getItem("login-action") === null) {
        action = 'login-form';
    }
    else {
        action = localStorage.getItem("login-action");
    }


    $("#" + action).fadeIn(200);

    if (action == "login-form") {
        $('#reg-form-collapse').removeClass("active");
        $('#login-form-collapse').addClass("active");
    }
    else {
        $('#login-form-collapse').removeClass("active");
        $('#reg-form-collapse').addClass("active");
    }
    $(document).on('click', '#reg-form-collapse', function () {
        $('#login-form').fadeOut(200, function () {
            $('#reg-form').fadeIn(200);
        });
        localStorage.setItem("login-action", "reg-form");
        $('#login-form-collapse').removeClass("active");
        $('#reg-form-collapse').addClass("active");
        

        //alert("Hello");
    });

    $(document).on('click', '#login-form-collapse', function () {
        $('#reg-form').fadeOut(200, function () {
            $('#login-form').fadeIn(200);
        });
        $('#reg-form-collapse').removeClass("active");
        $('#login-form-collapse').addClass("active");

        localStorage.setItem("login-action", "login-form");
        
        //alert("Hello");
    });

    $('#login-form').submit(function (e) {
        e.preventDefault();

        var formData = $(this).serialize();

        $.ajax({
            type: "POST",
            url: '/Account/Login',
            data: formData,
            success: function (d) {
                if (d.success) {
                    window.location.href = d.redirectUrl;
                } else {
                    $('#ErrorMsgLogin').html(d.message);
                }
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
            }
        });
    });

    $('#reg-form').submit(function (e) {
        e.preventDefault();

        var formData = $(this).serialize();

        $.ajax({
            type: "POST",
            url: '/Account/Register',
            data: formData,
            success: function (d) {
                if (d.success) {
                    window.location.href = d.redirectUrl;
                } else {
                    $('#ErrorMsgReg').html(d.message);
                }
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
            }
        });
    });

});