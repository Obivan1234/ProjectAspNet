
if (window.jQuery) {

    $(document).ready(function () {

        //закономірність така що якщо ми викорстовуємо REST API то такої помилки немає
        //Access to XMLHttpRequest at from origin has been blocked by CORS policy: No 'Access-Control-Allow-
        //але якщо щось накшталь https://www.wikidata.org/w/api.php?action=wbgetentities&ids=Q471379&format=json&languages=en то помилка

        var wiki_data = {};

        function getTitleFromUrl(url) {

            let itemId;

            let dataArr = url.split('/');

            let title = dataArr[dataArr.length - 1];

            let tempArr = dataArr.filter(i => i.indexOf('wikipedia.org') > -1)[0].split('.');

            let lang = tempArr[0];

            return title;
        }

        function getItemIdFromUrl(url) {

            let title = getTitleFromUrl(url);

            //JSONP https://ru.wikipedia.org/wiki/JSONP
            return new Promise(function (resolve, reject) {

                $.ajax({
                    url: "https://en.wikipedia.org/w/api.php?action=query&format=json&prop=pageprops&titles=" + title,
                    dataType: "jsonp",
                    success: function (data, textStatus, jqXHR) {

                        itemId = Object.values(data['query']['pages'])[0]['pageprops']['wikibase_item'];

                        resolve(itemId);
                    },
                    error: function (errorMessage) {
                        reject('error');
                    }
                });

            });
        }

        function getEntityByItemId(id) {

            return new Promise(function (resolve, reject) {

                $.ajax({
                    url: "https://www.wikidata.org/w/api.php?action=wbgetentities&format=json&languages=en&ids=" + id,
                    dataType: "jsonp",
                    success: function (data, textStatus, jqXHR) {
                        resolve(Object.values(data['entities'])[0]);
                    },
                    error: function (errorMessage) {
                        reject('error');
                    }
                });

            });

        }

        function getTitleByEntity(entity) {
            let title = '';

            if (entity['labels']['en'])
                title += entity['labels']['en'].value;
            else
                title += Object.values(entity['labels']['en'])[0].value;

            return title;
        }

        function getYearsByEntity(entity) {
            let year = '';

            let yearProp = entity.claims['P571'];

            for (let prop in yearProp) {

                if (yearProp[prop]['qualifiers']) {
                    for (let prop2 in yearProp[prop]['qualifiers']) {
                        let t_y = Object.values(yearProp[prop]['qualifiers'][prop2])[0]['datavalue']['value'].time;
                        if (t_y !== undefined) {
                            t_y = t_y.split('-');
                            year += t_y[0].replace('+', '') + ' ';
                        }
                    }
                }
                
                if (yearProp[prop]['mainsnak']['datavalue']['value']  && !year) {

                    let t_y = yearProp[prop]['mainsnak']['datavalue']['value'].time;
                    if (t_y !== undefined) {
                        t_y = t_y.split('-');
                        year += t_y[0].replace('+', '') + ' ';
                    }
                }

            }

            return year;
        }

        function getCreatorByEntity(entity) {

            let creator = '';

            let creatorProp = entity.claims['P170'];

            return new Promise(async (resolve, reject) => {

                for (let prop in creatorProp) {

                    if (creatorProp[prop]['mainsnak']['datavalue']['value'].id) {
                        let t_c = await getEntityByItemId(creatorProp[prop]['mainsnak']['datavalue']['value'].id);

                        if (t_c['labels']['en'])
                            creator += t_c['labels']['en'].value + ' ';
                        else
                            creator += Object.values(t_c['labels']['en'])[0].value + ' ';
                    }
                }
                resolve(creator);
            });
        }

        function getLocationByEntity(entity) {
            let location = '';

            let locationProp = entity.claims['P276'];

            return new Promise(async (resolve, reject) => {

                for (let prop in locationProp) {

                    if (locationProp[prop]['mainsnak']['datavalue']['value'].id) {
                        let t_l = await getEntityByItemId(locationProp[prop]['mainsnak']['datavalue']['value'].id);

                        if (t_l['labels']['en'])
                            location += t_l['labels']['en'].value + ' ';
                        else
                            location += Object.values(t_l['labels']['en'])[0].value + ' ';
                    }
                }
                resolve(location);
            });
        }

        function getHeightByEntity(entity) {
            let height = '';

            let heightProp = entity.claims['P2048'];

            height = Object.values(heightProp)[0]['mainsnak']['datavalue']['value'].amount;
            height = height.replace('+', '');

            let metric = '';

            return new Promise(async (resolve, reject) => {
                let temp = Object.values(heightProp)[0]['mainsnak']['datavalue']['value'].unit;
                temp = temp.split('/');

                let unitId = temp[temp.length - 1];

                let t_m = await getEntityByItemId(unitId);

                if (t_m['labels']['en'])
                    height += ' ' + t_m['labels']['en'].value;
                else
                    height += ' ' + Object.values(t_m['labels']['en'])[0].value;
                
                resolve(height);
            });
        }

        function getWidthByEntyty(entity) {
            let width = '';

            let widthProp = entity.claims['P2049'];

            width = Object.values(widthProp)[0]['mainsnak']['datavalue']['value'].amount;
            width = width.replace('+', '');

            let metric = '';

            return new Promise(async (resolve, reject) => {
                let temp  = Object.values(widthProp)[0]['mainsnak']['datavalue']['value'].unit;
                temp = temp.split('/');

                let unitId = temp[temp.length - 1];

                let t_m = await getEntityByItemId(unitId);

                if (t_m['labels']['en'])
                    width += ' '+t_m['labels']['en'].value;
                else
                    width += ' '+Object.values(t_m['labels']['en'])[0].value;
                
                resolve(width);
            });
        }

        function getShortDescriptionFromUrl(url) {

            let title = getTitleFromUrl(url);
            
            return new Promise(function (resolve, reject) {

                $.ajax({
                    url: "https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro&explaintext&exchars=500&redirects=1&titles=" + title,
                    dataType: "jsonp",
                    success: function (data, textStatus, jqXHR) {
                        let text = Object.values(data['query']['pages'])[0]['extract'];
                        resolve(text);
                    },
                    error: function (errorMessage) {
                        reject('error');
                    }
                });

            });

        }


        wiki_data.getItemIdFromUrl = getItemIdFromUrl;
        wiki_data.getEntityByItemId = getEntityByItemId;
        wiki_data.getShortDescriptionFromUrl = getShortDescriptionFromUrl;
        wiki_data.getTitleByEntity = getTitleByEntity;
        wiki_data.getYearsByEntity = getYearsByEntity;
        wiki_data.getCreatorByEntity = getCreatorByEntity;
        wiki_data.getLocationByEntity = getLocationByEntity;
        wiki_data.getHeightByEntity = getHeightByEntity;
        wiki_data.getWidthByEntyty = getWidthByEntyty;


        window._wiki_data = wiki_data;
    });

} else {
    console.log("for wiki-data.js need jQuery plugin");
}
