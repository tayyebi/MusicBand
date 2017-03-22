$.get(RssRoot + '/concert_details/' + QueryString_Id, function (data) {
    $('channel', data).each(function (i) {
        var date = $(this).find("date").text();
        $(document).prop('title', "کنسرت " + date);
        var address = $(this).find("address").text();
        var text = $(this).find("definition").html();
        var thumbnail = $(this).find("thumbnail").text();
        $("#Stage").append('<h1 id="date">' + date + '</h1>');
        $("#Stage").append('<h2 id="address">مکان: ' + address + '</h2>');
        $("#Stage").append('<img id="thumbnail" src="' + thumbnail + '" />');
        $("#Stage").append('<div id="text">' + text + '</div>');
    });
});