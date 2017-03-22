$.get(RssRoot + '/composer_details/' + QueryString_Id, function (data) {
    $('channel', data).each(function (i) {
        var fullname = $(this).find("fullname").text();
        $(document).prop('title', fullname);

        var text = $(this).find("text").html();
        var thumbnail = $(this).find("thumbnail").text();

        $("#Stage").append('<h1 id="fullname">' + fullname + '</h1>');
        $("#Stage").append('<img id="thumbnail" src="' + thumbnail + '" />');
        $("#Stage").append('<div id="text">' + text + '</div>');
    });
});