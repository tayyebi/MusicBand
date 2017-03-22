$.get(RssRoot + '/news_top/10', function (datas) {
    $('item', datas).each(function (i) {
        var headline = $(this).find("headline").text();
        var date = $(this).find("date").text();
        var id = $(this).find("id").html();
        var abstract = $(this).find("abstract").text();
        var thumbnail = $(this).find("thumbnail").text();
        var href = 'news?id=' + id;
        $("#Stage").append('<div id="post"><a id="title" href="' + href + '"><h2>' + headline + '</h2></a><img id="thumbnail" src="' + thumbnail + '" /><span id="abstract">' + abstract + '</span></div>');
    });
});