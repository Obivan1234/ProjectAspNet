
$(document).ready(function () {

    let options = {
        itemPerPage: 4,
        minIdOfItems: $('input[name="minItemsId"]').val()
    };

    let isAllContantLoaded = false;

    // $(document).on and $(document).scroll are same as $(window).on and $(window).scroll 

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
        var clickedAddPhotoContainer = $(".cl-add");

        if (clickedAddPhotoContainer.is(e.target))
            $(".cl-add").hide();

    });

    $(document).scroll(function () {

        updateContentWhenScrollToBottom();

    });

    //show or hide when click on globus area
    $(".language_s").on('click', function () {

        if ($(".lang_selector").is(":visible"))
            $(".lang_selector").hide();
        else {
            $(".lang_selector").show();
        }
    });

    //якщо вибрана якась мова з списку мов
    $(".lang_list_element").on('click', function () {

        var value = $(this).children('input').val();

        $.ajax({
            type: "POST",
            url: "/changelanguage",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "langId": value }),
            dataType: "html",
            success: function (result) {

                var resultObj = $.parseJSON(result);

                if (resultObj.StatusCode === 200)
                    window.location.reload();
            }

        });
    });

    
    $(document).on('mouseenter', '.content-item', function() {
        $(".content-item-hover", this).show();
    });

    $(document).on('mouseleave', '.content-item', function () {
        $(".content-item-hover", this).hide();
    });


    $(".cl-item-btn").on('click', function () {
        $(".cl-item").css({ "display":"none"});
    });

    $(document).on('click', '.content-item', function () {

        $('.cl-item-photo').empty();
        $('.cl-item-photo').append(
            '<img  src="' + $('.content-item-image > img', this).attr('src')+'"/>'
        );

        
        $('.cl-item-photo-inf').empty();
        $('.cl-item-photo-inf').append(
            $('.information', this).html()
        );
        

        $(".cl-item").css({ "display": "flex" });
    });


    $(".user-btn-add").on('click', function () {
        $(".cl-add").css({ "display": "flex" });
    });

    

    $("#typeOfInf").change(function () {

        let selectedVal = $(this).find(":selected").val();

        if (selectedVal === 'Own') {
            $(".inf-wiki").hide();
            $(".inf-own").show();
            $(".inf-wiki > input[name='wikiUrl']").prop('required', false);
            $(".inf-own > textarea[name='ownInf']").prop('required', true);
        }
        else if (selectedVal === 'Wiki') {
            $(".inf-wiki").show();
            $(".inf-own").hide();
            $(".inf-wiki > input[name='wikiUrl']").prop('required', true);
            $(".inf-own > textarea[name='ownInf']").prop('required', false);
        }
        else if (selectedVal === 'None') {
            $(".inf-wiki").hide();
            $(".inf-own").hide();
            $(".inf-wiki > input[name='wikiUrl']").prop('required', false);
            $(".inf-own > textarea[name='ownInf']").prop('required', false);
        }

    });

    $(".addImage-form > #quit").on('click',function () {
        $(".cl-add").hide();
    });

    $(".addImage-form").submit(function (e) {
        
        $('.wait-load').css({ "display": "flex" });

        e.preventDefault();

        let sendData = {};

        let myForm = new FormData(this);
        
        let descryptionType = myForm.get('typeOfInf');

        getBase64FromFile(myForm.get('imageOwn'))
            .then(
                async response => {

                    //бо так буде data:image/png;base64,iVBORw0KGgoAAAANSUhEU такий формат який при конвертуванні в C# видасть помилку
                    sendData.img64 = response.split(',')[1];
                    sendData.mimeType = response.substring("data:".length, response.indexOf(";base64"));
                    
                    let description;

                    if (descryptionType === 'None') {

                        sendData.description = '<div class="item-title">Без опису</div>';

                    } else if (descryptionType === 'Own') {

                        description = myForm.get('ownInf');
                        sendData.description = '<div class="item-title">Опис</div>' +
                            '<hr/>' +
                            '<div class="desc">' +
                            '<p>' + description +'</p>' +
                            '</div>';

                    } else if (descryptionType === 'Wiki') {

                        let wikiData = {};

                        let wikiUrl = myForm.get('wikiUrl');

                        await _wiki_data.getItemIdFromUrl(wikiUrl)
                            .then(
                                response => { return response; },
                                error => console.log(error)
                            )
                            .then(
                                async data => { wikiData.Props = await _wiki_data.getEntityByItemId(data); }
                            ).then(
                                () => { return _wiki_data.getShortDescriptionFromUrl(wikiUrl); }
                            ).then(
                                async data => {
                                    
                                    sendData.wikiUrl = wikiUrl;

                                    let title = _wiki_data.getTitleByEntity(wikiData.Props);

                                    let year = _wiki_data.getYearsByEntity(wikiData.Props);

                                    let creator = await _wiki_data.getCreatorByEntity(wikiData.Props);

                                    let location = await _wiki_data.getLocationByEntity(wikiData.Props);

                                    let height = await _wiki_data.getHeightByEntity(wikiData.Props);

                                    let width = await _wiki_data.getWidthByEntyty(wikiData.Props);

                                    sendData.description = '<div class="item-title">Опис: ' + title + '</div>' +
                                        '<hr/>' +
                                        '<div class="desc">' +
                                        '<span><b>Creator:  </b>' + creator + '</span> <br />' +
                                        '<span><b>Year:  </b>' + year + '</span> <br />' +
                                        '<span><b>Location:  </b>' + location + '</span> <br />' +
                                        '<span><b>Height:  </b>' + height + '</span> <br />' +
                                        '<span><b>Width:  </b>' + width + '</span> <br />' +
                                        '<br /> <br />' +
                                        '<p>' +
                                         data + '<a target="_blank" href="' + wikiUrl + '">More on wikipedia</a>' +
                                        '</p>' +
                                        '</div>';

                                }
                            );
                    }

                    return sendData;
                }
            ).then(receivedData => {

                $.ajax({
                    type: "POST",
                    url: "Common/AddNewItem",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(receivedData),
                    dataType: "json",
                    success: function (result) {


                        let imgSrc = "data:" + sendData.mimeType + ";base64," + sendData.img64;

                        $(".content-container").prepend(
                            '<div class="content-item">' +
                            '<div class="content-item-image">' +
                            '<img src="' + imgSrc + '" />' +
                            '</div>' +
                            '<div class="content-item-hover"></div>' +
                            '<div class="information" hidden>' +
                            receivedData.description +
                            '</div>' +
                            '</div>'
                        );

                        //here we  set default values to form

                        $('.wait-load').css({ "display": "none" });
                        $(".cl-add").hide();
                        $('#upload-file-info').text('Файл не вибрано');
                        $('#typeOfInf').val('Wiki').prop('selected', true);
                        $(".inf-wiki").show();
                        $(".inf-own").hide();
                        $(".inf-wiki > input[name='wikiUrl']").prop('required', true);
                        $(".inf-wiki > input[name='wikiUrl']").val('');
                        $(".inf-own > textarea[name='ownInf']").prop('required', false);
                    }

                });
            });
        
    });


    function getBase64FromFile(file) {

        return new Promise((resolve, reject) => {
            var reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = (e) => {
                resolve(e.target.result);
            };
        });

    }
    
    function updateContentWhenScrollToBottom() {

        if ($(window).scrollTop() === $(document).height() - $(window).height()) {

            if (isAllContantLoaded)
                return;

            $('.bottom-load').show();

            //$(window).scrollTop($(document).height());

            $.ajax({
                url: "Common/AllItemPageProducts",
                type: 'post',
                data: options,
                success: function (data) {

                    if (data.StatusCode === 200) {

                        $(".content-container").append(data.Content);

                        isAllContantLoaded = data.AllContantLoaded;

                        if (data.MinItemsId > 0)
                            options.minIdOfItems = data.MinItemsId;

                        $('.bottom-load').hide();
                    }
                }

            }).fail(function (jqXHR, status, errorThrown) {
                console.log(jqXHR.responseText);
                console.log(status);
                console.log(errorThrown);
            });

        }

    }

});

