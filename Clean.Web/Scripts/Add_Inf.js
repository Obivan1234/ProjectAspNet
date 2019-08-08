
(function ($) {

    $(document).ready(function () {

        $(document).on('click', function (e) {

            //перевіряєм, якщо ми клікнули десь на сторінці і в нас розгорнутий список  мов то згорнути їх
            //але ми не перев чи  мови розгор чи згорнуті всеодно ховаєм
            var container = $(".CH_lang");

            if (!container.is(e.target) && container.has(e.target).length === 0) {
                $(".lang_selector").hide();
            }

            //when some photo was clicked
            var clickedPhotoContainer = $(".cl-item");

            if (clickedPhotoContainer.is(e.target))
                $(".cl-item").hide();


            //when add photo was clicked
            //сутть така що при кліку на додати фото сайт огортується  сірим  беграундом і по верх нього форма 
            //, так от якщо клікнути на сіру форму то знач не на форму і закрити форму
            var clickedAddPhotoContainer = $(".cl-add-inf");

            if (clickedAddPhotoContainer.is(e.target))
                $(".cl-add-inf").hide();

        });


        $(".addInfo-form > #quit").on('click', function () {
            $(".cl-add-inf").hide();
        });

        $('.user-photo').hover(function () {
            $(".user-photo-hover", this).show();
        }, function () {
            $(".user-photo-hover", this).hide();
        });

        $(".user-photo-hover").click(function () {
            $(".cl-add-inf").css({ "display": "flex" });
        });



        $(".addInfo-form").submit(function (e) {

            $('.wait-load').css({ "display": "flex" });

            e.preventDefault();

            let sendMyData_1 = {};

            let myForm_1 = new FormData(this);

            let descryptionType = myForm_1.get('typeOfInf');

            getBase64FromFile_1(myForm_1.get('imageOwn_1'))
                .then(
                    async response => {

                        //бо так буде data:image/png;base64,iVBORw0KGgoAAAANSUhEU такий формат який при конвертуванні в C# видасть помилку
                        sendMyData_1.img64 = response.split(',')[1];
                        sendMyData_1.mimeType = response.substring("data:".length, response.indexOf(";base64"));

                        let descriptionInfo;

                        descriptionInfo = myForm_1.get('ownInf_1');
                        sendMyData_1.descriptionInfo = descriptionInfo;


                        return sendMyData_1;
                    }
                ).then(receivedData => {

                    $.ajax({
                        type: "POST",
                        url: "Common/AddNewInf",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify(receivedData),
                        dataType: "json",
                        success: function (result) {


                            let imgSrc = "data:" + sendMyData_1.mimeType + ";base64," + sendMyData_1.img64;

                            $(".user-photo > img").attr("src", imgSrc);

                            $(".user-about").html(
                                '<span>' + receivedData.descriptionInfo + '</span>'
                            );

                            //here we  set default values to form

                            $('.wait-load').css({ "display": "none" });
                            $(".cl-add-inf").hide();
                            $('#upload-file-info').text('Файл не вибрано');
                            $('#typeOfInf').val('Own').prop('selected', true);
                            $(".inf-wiki").hide();
                            $(".inf-own").show();
                            $(".inf-wiki > input[name='wikiUrl']").prop('required', true);
                            $(".inf-wiki > input[name='wikiUrl']").val('');
                            $(".inf-own > textarea[name='ownInf']").prop('required', false);
                        }

                    });
                });

        });



        function getBase64FromFile_1(file) {

            return new Promise((resolve, reject) => {
                var reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = (e) => {
                    resolve(e.target.result);
                };
            });

        }
    });

})(jQuery);


