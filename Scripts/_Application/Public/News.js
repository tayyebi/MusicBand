$.get(RssRoot + '/news_details/' + QueryString_Id, function (data) {
    $('channel', data).each(function (i) {
        var headline = $(this).find("headline").text();
        $(document).prop('title', headline);
        var text = $(this).find("text").html();
        var thumbnail = $(this).find("thumbnail").text();
        $(".Stage").append('<h1 id="title">' + headline + '</h1>');
        $(".Stage").append('<img id="thumbnail" src="' + thumbnail + '" />');
        $(".Stage").append('<div id="text">' + text + '</div>');
    });
});