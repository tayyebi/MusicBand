$('.Stage').append('<div id="content"></div>');
$.get(RssRoot + '/instrument_details/' + QueryString_Id, function (data) {
    $('channel', data).each(function (i) {
        var name = $(this).find("name").text();
        $(document).prop('title', name);
        var text = $(this).find("text").html();
        var thumbnail = $(this).find("thumbnail").text();
        var genus_name = $(this).find('genus_name').text();
        var genus_href = Root + '/genus.html?id=' + $(this).find('genus_id').text();
        $("#content").append('<h1 id="name">' + name + '</h1>');
        $("#content").append('<a id="genus" href="' + genus_href + '">' + genus_name + '</a>');
        $("#content").append('<img id="thumbnail" src="' + thumbnail + '" />');
        $("#content").append('<div id="text">' + text + '</div>');
        $('#content').append('<div id="stringer"></div>');
        $.get(RssRoot + '/Stringers?id=' + QueryString_Id, function (datas) {
            $('item', datas).each(function (i) {
                var name = $(this).find("name").text();
                var thumbnail = $(this).find("thumbnail").text();
                var id = $(this).find("id").text();
                var href = 'stringer/' + id;
                $("#stringer").append('<a href="' + href + '"><img title="' + name + '" src="' + thumbnail + '" /></a>');
            });
        });
    });
});