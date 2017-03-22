$('.Stage').append('<div id="content"></div>');
$.get(RssRoot + '/genus_details/' + QueryString_Id, function (data) {
    $('channel', data).each(function (i) {
        var name = $(this).find("name").text();
        $(document).prop('title', name);
        var text = $(this).find("text").html();
        var thumbnail = $(this).find("thumbnail").text();
        $("#content").append('<h1 id="name">' + name + '</h1>');
        $("#content").append('<img id="thumbnail" src="' + thumbnail + '" />');
        $("#content").append('<div id="text">' + text + '</div>');
        $("#content").append('<div id="instrument"></div>')
        $.get(RssRoot + '/instruments?id=' + QueryString_Id, function (datas) {
            $('item', datas).each(function (i) {
                var name = $(this).find("name").text();
                var thumbnail = $(this).find("thumbnail").text();
                var id = $(this).find("id").text();
                var href ='instrument?id=' + id;
                $("#instrument").append('<a href="' + href + '"><img title="' + name + '" src="' + thumbnail + '" /></a>');
            });
        });
    });
});