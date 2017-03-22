$.get(RssRoot + '/leader', function (data) {
    $('channel', data).each(function (i) {
        var fullname = $(this).find("fullname").text();
        $(document).prop('title', "استاد" + fullname);
        var date = $(this).find("birth").text();
        var text = $(this).find("text").html();
        var thumbnail = $(this).find("thumbnail").text();
        $(".Stage").append('<h1 id="title">استاد ' + fullname + '</h1>');
        $(".Stage").append('<img id="thumbnail" src="' + thumbnail + '" />');
        $(".Stage").append('<div id="text">' + text + '</div>');
    });
});