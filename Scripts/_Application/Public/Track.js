$.get(RssRoot + '/track_details/' + QueryString_Id, function (data) {
    $('channel', data).each(function (i) {
        var name = $(this).find("name").text();
        $(document).prop('title', "قطعه " + name);

        var text = $(this).find("text").html();
        var thumbnail = $(this).find("thumbnail").text();

        $("#Stage").append('<h1 id="name">' + name + '</h1>');
        $("#Stage").append('<img id="thumbnail" src="' + thumbnail + '" />');
        $("#Stage").append('<div id="text">' + text + '</div>');
    });
});