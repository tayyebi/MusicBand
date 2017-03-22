$.get(RssRoot + '/Stringers', function (datas) {
    $('item', datas).each(function (i) {
        var fullname = $(this).find("fullname").text();
        var birth = $(this).find("birth").text();
        var thumbnail = $(this).find("thumbnail").text();
        var id = $(this).find("id").text();
        var href = '/Public/stringer/' + id;
        $("#Stage").append('<a class="item" href="' + href + '"><img id="thumbnail" alt="' + fullname + '" src="' + thumbnail + '" /><h2 id="fullname">' + fullname + '</h2><h3 id="birth">' + birth + '</h3></a>');
    });
});