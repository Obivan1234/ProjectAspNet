
$(document).ready(function () {

    $(document).click(function (e) {

        //перевіряєм, якщо ми клікнули десь на сторінці і в нас розгорнутий список  мов то згорнути їх
        //але ми не перев чи  мови розгор чи згорнуті всеодно ховаєм
        var container = $(".CH_lang");
        
        if (!container.is(e.target) && container.has(e.target).length === 0) {
                $(".lang_selector").hide();
        }

    });

    //show or hide when click on globus area
    $(".language_s").click(function () {
        
        if ($(".lang_selector").is(":visible"))
            $(".lang_selector").hide();
        else {
            $(".lang_selector").show();
        }
    });

    //якщо вибрана якась мова з списку мов
    $(".lang_list_element").click(function () {

        var value = $(this).children('input').val();
        
        $.ajax({
            type: "POST",
            url: "/changelanguage",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({"langId": value}),
            dataType: "html",
            success: function (result) {

                var resultObj = $.parseJSON(result);
               
                if (resultObj.StatusCode === 200)
                    window.location.reload();
            }

        });
    });

});