
$.get(RssRoot + '/concerts', function (datas) {
    $('item', datas).each(function (i) {
        var date = $(this).find('date').text();
        var id = $(this).find('id').text();
        $("#concerts").append('<li><a href="' + '/public/concert?id=' + id + '">' + date + '</a></li>');
    });
});
$.get(RssRoot + '/genus', function (datas) {
    $('item', datas).each(function (i) {
        var headline = $(this).find("headline").text();
        var thumbnail = $(this).find("thumbnail").text();
        var id = $(this).find("id").text();
        $("#instruments").append('<li><a href="' + '/public/genus?id=' + id + '">' + headline + '</a></li>');
    });
});

$.get(RssRoot + '/tracks', function (datas) {
    $('item', datas).each(function (i) {
        var name = $(this).find('name').text();
        var id = $(this).find('id').text();
        var composer_id = $(this).find('composer_id').text();
        var composer_name = $(this).find('composer_name').text();
        $('.tracks').append('<span>' + 'قطعه <a href="' + '/public/track?id=' + id + '"><b>' + name + '</b></a>' + ' به آهنگ سازی ' + '<a href="' + 'composer?id=' + composer_id + '"><b>' + composer_name + '</b></a>' + '</span>');
    });
    var num_stories = $('.tracks > span').length,
       news_interval = 2500,
       current_story = 0;
    $('.tracks > span:gt(0)').hide();
    window.setInterval(function () {
        var next_story = (current_story == (num_stories - 1)) ? 0 : current_story + 1;
        $('.tracks > span:eq(' + current_story + ')').hide();
        $('.tracks > span:eq(' + next_story + ')').show();
        current_story = next_story;
    }, news_interval);
});
