﻿/**
  * Rhinoslider 1.05
  * http://rhinoslider.com/
  *
  * Copyright 2015: Sebastian Pontow, Rene Maas (http://renemaas.de/)
  * Dual licensed under the MIT or GPL Version 2 licenses.
  * http://rhinoslider.com/license/
  */

(function ($, window, undefined) {
    $.extend($.easing, {
        def: 'out', out: function (none, currentTime, startValue, endValue, totalTime) { return -endValue * (currentTime /= totalTime) * (currentTime - 2) + startValue; }, kick: function (none, currentTime, startValue, endValue, totalTime) {
            if ((currentTime /= totalTime / 2) < 1) { return endValue / 2 * Math.pow(2, 10 * (currentTime - 1)) + startValue; }
            return endValue / 2 * (-Math.pow(2, -10 * --currentTime) + 2) + startValue;
        }, shuffle: function (none, currentTime, startValue, endValue, totalTime) {
            if ((currentTime /= totalTime / 2) < 1) { return endValue / 2 * currentTime * currentTime * currentTime * currentTime * currentTime + startValue; }
            return endValue / 2 * ((currentTime -= 2) * currentTime * currentTime * currentTime * currentTime + 2) + startValue;
        }
    }); var rhinoSlider = function (element, opts) {
        var
        settings = $.extend({}, $.fn.rhinoslider.defaults, opts), $slider = $(element), effects = $.fn.rhinoslider.effects, preparations = $.fn.rhinoslider.preparations, vars = { isPlaying: false, intervalAutoPlay: false, active: '', next: '', container: '', items: '', buttons: [], prefix: 'rhino-', playedArray: [], playedCounter: 0, original: element }; settings.callBeforeInit(); var
        setUpSettings = function (settings) { settings.controlsPrevNext = String(settings.controlsPrevNext) == 'true' ? true : false; settings.controlsKeyboard = String(settings.controlsKeyboard) == 'true' ? true : false; settings.controlsMousewheel = String(settings.controlsMousewheel) == 'true' ? true : false; settings.controlsPlayPause = String(settings.controlsPlayPause) == 'true' ? true : false; settings.pauseOnHover = String(settings.pauseOnHover) == 'true' ? true : false; settings.animateActive = String(settings.animateActive) == 'true' ? true : false; settings.autoPlay = String(settings.autoPlay) == 'true' ? true : false; settings.cycled = String(settings.cycled) == 'true' ? true : false; settings.showTime = parseInt(settings.showTime, 10); settings.effectTime = parseInt(settings.effectTime, 10); settings.controlFadeTime = parseInt(settings.controlFadeTime, 10); settings.captionsFadeTime = parseInt(settings.captionsFadeTime, 10); tmpShiftValue = settings.shiftValue; tmpParts = settings.parts; settings.shiftValue = []; settings.parts = []; return settings; }, init = function ($slider, settings, vars) {
            settings = setUpSettings(settings); $slider.wrap('<div class="' + vars.prefix + 'container">'); vars.container = $slider.parent('.' + vars.prefix + 'container'); vars.isPlaying = settings.autoPlay; var buttons = ''; if (settings.controlsPrevNext) { vars.container.addClass(vars.prefix + 'controls-prev-next'); buttons = '<a class="' + vars.prefix + 'prev ' + vars.prefix + 'btn">' + settings.prevText + '</a><a class="' + vars.prefix + 'next ' + vars.prefix + 'btn">' + settings.nextText + '</a>'; vars.container.append(buttons); vars.buttons.prev = vars.container.find('.' + vars.prefix + 'prev'); vars.buttons.next = vars.container.find('.' + vars.prefix + 'next'); vars.buttons.prev.click(function () { prev($slider, settings); if (settings.autoPlay) { pause(); } }); vars.buttons.next.click(function () { next($slider, settings); if (settings.autoPlay) { pause(); } }); }
            if (settings.controlsPlayPause) { vars.container.addClass(vars.prefix + 'controls-play-pause'); buttons = settings.autoPlay ? '<a class="' + vars.prefix + 'toggle ' + vars.prefix + 'pause ' + vars.prefix + 'btn">' + settings.pauseText + '</a>' : '<a class="' + vars.prefix + 'toggle ' + vars.prefix + 'play ' + vars.prefix + 'btn">' + settings.playText + '</a>'; vars.container.append(buttons); vars.buttons.play = vars.container.find('.' + vars.prefix + 'toggle'); vars.buttons.play.click(function () { if (vars.isPlaying === false) { play(); } else { pause(); } }); }
            vars.container.find('.' + vars.prefix + 'btn').css({ position: 'absolute', display: 'block', cursor: 'pointer' }); if (settings.showControls !== 'always') { var allControls = vars.container.find('.' + vars.prefix + 'btn'); allControls.stop(true, true).fadeOut(0); if (settings.showControls === 'hover') { vars.container.mouseenter(function () { allControls.stop(true, true).fadeIn(settings.controlFadeTime); }).mouseleave(function () { allControls.delay(200).fadeOut(settings.controlFadeTime); }); } }
            if (settings.showControls !== 'never') { vars.container.addClass(vars.prefix + 'show-controls'); }
            vars.items = $slider.children(); vars.items.addClass(vars.prefix + 'item'); vars.items.first().addClass(vars.prefix + 'active'); var sliderStyles = settings.styles.split(','), style; $.each(sliderStyles, function (i, cssAttribute) { style = $.trim(cssAttribute); vars.container.css(style, $slider.css(style)); $slider.css(style, ' '); switch (style) { case 'width': case 'height': $slider.css(style, '100%'); break; } }); if (vars.container.css('position') == 'static') { vars.container.css('position', 'relative'); }
            $slider.css({ top: 'auto', left: 'auto', position: 'relative' }); vars.items.css({ margin: 0, width: $slider.css('width'), height: $slider.css('height'), position: 'absolute', top: 0, left: 0, zIndex: 0, opacity: 0, overflow: 'hidden' }); vars.items.each(function (i) { $(this).attr('id', vars.prefix + 'item' + i); }); if (settings.showBullets !== 'never') {
                vars.container.addClass(vars.prefix + 'show-bullets'); var navi = '<ol class="' + vars.prefix + 'bullets">'; vars.items.each(function (i) { var $item = $(this); var id = vars.prefix + 'item' + i; navi = navi + '<li><a id="' + id + '-bullet" class="' + vars.prefix + 'bullet">' + parseInt(i + 1, 10) + '</a></li>'; }); navi = navi + '</ol>'; vars.container.append(navi); vars.navigation = vars.container.find('.' + vars.prefix + 'bullets'); vars.buttons.bullets = vars.navigation.find('.' + vars.prefix + 'bullet'); vars.buttons.bullets.first().addClass(vars.prefix + 'active-bullet ' + vars.prefix + 'first-bullet'); vars.buttons.bullets.last().addClass(vars.prefix + 'last-bullet'); vars.buttons.bullets.click(function () {
                    var itemID = $(this).attr('id').replace('-bullet', ''); var $next = vars.container.find('#' + itemID); var curID = parseInt(vars.navigation.find('.' + vars.prefix + 'active-bullet').attr('id').replace('-bullet', '').replace(vars.prefix + 'item', ''), 10); var nextID = parseInt(itemID.replace(vars.prefix + 'item', ''), 10); if (curID < nextID) { next($slider, settings, $next); } else if (curID > nextID) { prev($slider, settings, $next); } else { return false; }
                    if (settings.autoPlay) { pause(); }
                });
            }
            if (settings.showBullets === 'hover') { vars.navigation.hide(); vars.container.mouseenter(function () { vars.navigation.stop(true, true).fadeIn(settings.controlFadeTime); }).mouseleave(function () { vars.navigation.delay(200).fadeOut(settings.controlFadeTime); }); }
            if (settings.showCaptions !== 'never') { vars.container.addClass(vars.prefix + 'show-captions'); vars.items.each(function () { var $item = $(this); if ($item.children('.' + vars.prefix + 'caption').length == 0) { if ($item.children('img').length > 0) { var title = $.trim($item.children('img:first').attr('title')); if (undefined != title || '' == title) { $item.append('<div class="' + vars.prefix + 'caption">' + title + '</div>'); $item.children('.' + vars.prefix + 'caption:empty').remove(); } } } }); if (settings.showCaptions === 'hover') { $('.' + vars.prefix + 'caption').hide(); vars.container.mouseenter(function () { vars.active.find('.' + vars.prefix + 'caption').stop(true, true).fadeTo(settings.captionFadeTime, settings.captionsOpacity); }).mouseleave(function () { vars.active.find('.' + vars.prefix + 'caption').delay(200).fadeOut(settings.captionFadeTime); }); } else if (settings.showCaptions === 'always') { $('.' + vars.prefix + 'caption').fadeTo(0, settings.captionsOpacity); } }
            vars.items.each(function () { $(this).children('img').removeAttr('title'); }); if (settings.autoPlay) { vars.intervalAutoPlay = setInterval(function () { next($slider, settings); }, settings.showTime); } else { vars.intervalAutoPlay = false; }
            if (settings.pauseOnHover) { vars.container.addClass(vars.prefix + 'pause-on-hover'); $slider.mouseenter(function () { if (vars.isPlaying) { clearInterval(vars.intervalAutoPlay); if (settings.controlsPlayPause) { vars.buttons.play.text(settings.playText).removeClass(vars.prefix + 'pause').addClass(vars.prefix + 'play'); } } }).mouseleave(function () { if (vars.isPlaying) { vars.intervalAutoPlay = setInterval(function () { next($slider, settings); }, settings.showTime); if (settings.controlsPlayPause) { vars.buttons.play.text(settings.pauseText).removeClass(vars.prefix + 'play').addClass(vars.prefix + 'pause'); } } }); }
            if (settings.controlsKeyboard) {
                vars.container.addClass(vars.prefix + 'controls-keyboard'); $(document).keyup(function (e) {
                    switch (e.keyCode) {
                        case 37: pause(); prev($slider, settings); break; case 39: pause(); next($slider, settings); break; case 80: if (vars.isPlaying === false) { play(); } else { pause(); }
                            break;
                    }
                });
            }
            if (settings.controlsMousewheel) {
                vars.container.addClass(vars.prefix + 'controls-mousewheel'); if (!$.isFunction($.fn.mousewheel)) { alert('$.fn.mousewheel is not a function. Please check that you have the mousewheel-plugin installed properly.'); } else {
                    $slider.mousewheel(function (e, delta) {
                        e.preventDefault(); if (vars.container.hasClass('inProgress')) { return false; }
                        var dir = delta > 0 ? 'up' : 'down'; if (dir === 'up') { pause(); prev($slider, settings); } else { pause(); next($slider, settings); }
                    });
                }
            }
            vars.active = $slider.find('.' + vars.prefix + 'active'); vars.active.css({ zIndex: 1, opacity: 1 }); if (!settings.cycled) {
                vars.items.each(function () {
                    var $item = $(this); if ($item.is(':first-child')) { $item.addClass(vars.prefix + 'firstItem'); }
                    if ($item.is(':last-child')) { $item.addClass(vars.prefix + 'lastItem'); }
                }); if (vars.active.is(':first-child') && settings.controlsPrevNext) { vars.buttons.prev.addClass('disabled'); }
                if (vars.active.is(':last-child')) {
                    if (settings.controlsPrevNext) { vars.buttons.next.addClass('disabled'); pause(); }
                    if (settings.autoPlay) { vars.buttons.play.addClass('disabled'); }
                }
            }
            if (preparations[settings.effect] == undefined) { console.log('Effect for ' + settings.effect + ' not found.'); } else { preparations[settings.effect]($slider, settings, vars); }
            $slider.data('slider:vars', vars); settings.callBackInit();
        }, isFirst = function ($item) { return $item.is(':first-child'); }, isLast = function ($item) { return $item.is(':last-child'); }, pause = function () {
            var vars = $slider.data('slider:vars'); clearInterval(vars.intervalAutoPlay); vars.isPlaying = false; if (settings.controlsPlayPause) { vars.buttons.play.text(settings.playText).removeClass(vars.prefix + 'pause').addClass(vars.prefix + 'play'); }
            settings.callBackPause();
        }, play = function () {
            var vars = $slider.data('slider:vars'); vars.intervalAutoPlay = setInterval(function () { next($slider, settings); }, settings.showTime); vars.isPlaying = true; if (settings.controlsPlayPause) { vars.buttons.play.text(settings.pauseText).removeClass(vars.prefix + 'play').addClass(vars.prefix + 'pause'); }
            settings.callBackPlay();
        }, prev = function ($slider, settings, $next) {
            var vars = $slider.data('slider:vars'); if (!settings.cycled && isFirst(vars.active)) { return false; }
            settings.callBeforePrev(); if (vars.container.hasClass('inProgress')) { return false; }
            vars.container.addClass('inProgress'); if (!$next) { if (settings.randomOrder) { var nextID = getRandom(vars); vars.next = vars.container.find('#' + nextID); } else { vars.next = vars.items.first().hasClass(vars.prefix + 'active') ? vars.items.last() : vars.active.prev(); } } else { vars.next = $next; }
            if (vars.next.hasClass(vars.prefix + 'active')) { return false; }
            if (settings.showCaptions !== 'never') { $('.' + vars.prefix + 'caption').stop(true, true).fadeOut(settings.captionsFadeTime); }
            if (settings.showBullets !== 'never' && settings.changeBullets == 'before') { vars.navigation.find('.' + vars.prefix + 'active-bullet').removeClass(vars.prefix + 'active-bullet'); vars.navigation.find('#' + vars.next.attr('id') + '-bullet').addClass(vars.prefix + 'active-bullet'); }
            setTimeout(function () {
                var params = []; params.settings = settings; params.animateActive = settings.animateActive; params.direction = settings.slidePrevDirection; if (effects[settings.effect] == undefined) { console.log('Preparations for ' + settings.effect + ' not found.'); } else { effects[settings.effect]($slider, params, resetElements); }
                setTimeout(function () {
                    if (settings.showBullets !== 'never' && settings.changeBullets == 'after') { vars.navigation.find('.' + vars.prefix + 'active-bullet').removeClass(vars.prefix + 'active-bullet'); vars.navigation.find('#' + vars.next.attr('id') + '-bullet').addClass(vars.prefix + 'active-bullet'); }
                    settings.callBackPrev();
                }, settings.effectTime);
            }, settings.captionsFadeTime); if (settings.showBullets !== 'never' && settings.changeBullets == 'after') { vars.navigation.find('.' + vars.prefix + 'active-bullet').removeClass(vars.prefix + 'active-bullet'); vars.navigation.find('#' + vars.next.attr('id') + '-bullet').addClass(vars.prefix + 'active-bullet'); }
        }, next = function ($slider, settings, $next) {
            var vars = $slider.data('slider:vars'); if (!settings.cycled && isLast(vars.active)) { return false; }
            settings.callBeforeNext(); if (vars.container.hasClass('inProgress')) { return false; }
            vars.container.addClass('inProgress'); if (!$next) { if (settings.randomOrder) { var nextID = getRandom(vars); vars.next = vars.container.find('#' + nextID); } else { vars.next = vars.items.last().hasClass(vars.prefix + 'active') ? vars.items.first() : vars.active.next(); } } else { vars.next = $next; }
            if (vars.next.hasClass(vars.prefix + 'active')) { return false; }
            if (settings.showCaptions !== 'never') { $('.' + vars.prefix + 'caption').stop(true, true).fadeOut(settings.captionsFadeTime); }
            if (settings.showBullets !== 'never' && settings.changeBullets == 'before') { vars.navigation.find('.' + vars.prefix + 'active-bullet').removeClass(vars.prefix + 'active-bullet'); vars.navigation.find('#' + vars.next.attr('id') + '-bullet').addClass(vars.prefix + 'active-bullet'); }
            setTimeout(function () {
                var params = []; params.settings = settings; params.animateActive = settings.animateActive; params.direction = settings.slideNextDirection; if (effects[settings.effect] == undefined) { console.log('Preparations for ' + settings.effect + ' not found.'); } else { effects[settings.effect]($slider, params, resetElements); }
                setTimeout(function () {
                    if (settings.showBullets !== 'never' && settings.changeBullets == 'after') { vars.navigation.find('.' + vars.prefix + 'active-bullet').removeClass(vars.prefix + 'active-bullet'); vars.navigation.find('#' + vars.next.attr('id') + '-bullet').addClass(vars.prefix + 'active-bullet'); }
                    settings.callBackNext();
                }, settings.effectTime);
            }, settings.captionsFadeTime);
        }, getRandom = function (vars) {
            var curID = vars.active.attr('id'); var itemCount = vars.items.length; var nextID = vars.prefix + 'item' + parseInt((Math.random() * itemCount), 10); var nextKey = nextID.replace(vars.prefix + 'item', ''); if (vars.playedCounter >= itemCount) { vars.playedCounter = 0; vars.playedArray = []; }
            if (curID == nextID || vars.playedArray[nextKey] === true) { return getRandom(vars); } else { vars.playedArray[nextKey] = true; vars.playedCounter++; return nextID; }
        }, resetElements = function ($slider, settings) {
            var vars = $slider.data('slider:vars'); vars.next.addClass(vars.prefix + 'active').css({ zIndex: 1, top: 0, left: 0, width: '100%', height: '100%', margin: 0, opacity: 1 }); vars.active.css({ zIndex: 0, top: 0, left: 0, margin: 0, opacity: 0 }).removeClass(vars.prefix + 'active'); settings.additionalResets(); if (!settings.cycled) {
                if (settings.controlsPrevNext) {
                    if (isFirst(vars.next)) { vars.buttons.prev.addClass('disabled'); } else { vars.buttons.prev.removeClass('disabled'); }
                    if (isLast(vars.next)) { vars.buttons.next.addClass('disabled'); pause(); } else { vars.buttons.next.removeClass('disabled'); }
                }
                if (settings.controlsPlayPause) { if (isLast(vars.next)) { vars.buttons.play.addClass('disabled'); pause(); } else { vars.buttons.play.removeClass('disabled'); } }
            }
            if (settings.showBullets !== 'never') { vars.navigation.find('.' + vars.prefix + 'active-bullet').removeClass(vars.prefix + 'active-bullet'); vars.navigation.find('#' + vars.next.attr('id') + '-bullet').addClass(vars.prefix + 'active-bullet'); }
            vars.active = vars.next; if (settings.showCaptions !== 'never') { vars.active.find('.' + vars.prefix + 'caption').stop(true, true).fadeTo(settings.captionsFadeTime, settings.captionsOpacity); }
            vars.container.removeClass('inProgress');
        }; this.pause = function () { pause(); }; this.play = function () { play(); }; this.prev = function ($next) { prev($slider, settings, $next); }; this.next = function ($next) { next($slider, settings, $next); }; this.uninit = function () { pause(); vars.container.before($(element).data('slider:original')); $slider.data('slider:vars', null); vars.container.remove(); $(element).data('rhinoslider', null); }; init($slider, settings, vars);
    }; $.fn.rhinoslider = function (opts) {
        return this.each(function () {
            var element = $(this); if (element.data('rhinoslider')) { return element.data('rhinoslider'); }
            element.data('slider:original', element.clone()); var rhinoslider = new rhinoSlider(this, opts); element.data('rhinoslider', rhinoslider);
        });
    }; $.fn.rhinoslider.defaults = { effect: 'slide', easing: 'swing', randomOrder: false, controlsMousewheel: true, controlsKeyboard: true, controlsPrevNext: true, controlsPlayPause: true, pauseOnHover: true, animateActive: true, autoPlay: false, cycled: true, showTime: 3000, effectTime: 1000, controlFadeTime: 650, captionsFadeTime: 250, captionsOpacity: 0.7, partDelay: 100, shiftValue: '150', parts: '5,3', showCaptions: 'never', showBullets: 'hover', changeBullets: 'after', showControls: 'hover', slidePrevDirection: 'toLeft', slideNextDirection: 'toRight', prevText: 'prev', nextText: 'next', playText: 'play', pauseText: 'pause', styles: 'position,top,right,bottom,left,margin-top,margin-right,margin-bottom,margin-left,width,height', callBeforeInit: function () { return false; }, callBackInit: function () { return false; }, callBeforeNext: function () { return false; }, callBeforePrev: function () { return false; }, callBackNext: function () { return false; }, callBackPrev: function () { return false; }, callBackPlay: function () { return false; }, callBackPause: function () { return false; }, additionalResets: function () { return false; } }; $.fn.rhinoslider.effects = {
        fade: function ($slider, params, callback) {
            var vars = $slider.data('slider:vars'); var settings = params.settings; if (settings.animateActive) { vars.active.animate({ opacity: 0 }, settings.effectTime); }
            vars.next.css({ zIndex: 2 }).animate({ opacity: 1 }, settings.effectTime, settings.easing, function () { callback($slider, settings); });
        }, slide: function ($slider, params, callback) {
            var vars = $slider.data('slider:vars'); var settings = params.settings; var direction = params.direction; var values = []; values.width = vars.container.width(); values.height = vars.container.height(); values.easing = settings.showTime === 0 ? 'linear' : settings.easing; values.nextEasing = settings.showTime === 0 ? 'linear' : settings.easing; $slider.css('overflow', 'hidden'); switch (direction) { case 'toTop': values.top = -values.height; values.left = 0; values.nextTop = -values.top; values.nextLeft = 0; break; case 'toBottom': values.top = values.height; values.left = 0; values.nextTop = -values.top; values.nextLeft = 0; break; case 'toRight': values.top = 0; values.left = values.width; values.nextTop = 0; values.nextLeft = -values.left; break; case 'toLeft': values.top = 0; values.left = -values.width; values.nextTop = 0; values.nextLeft = -values.left; break; }
            vars.next.css({ zIndex: 2, opacity: 1 }); if (settings.animateActive) { vars.active.css({ top: 0, left: 0 }).animate({ top: values.top, left: values.left, opacity: 1 }, settings.effectTime, values.easing); }
            vars.next.css({ top: values.nextTop, left: values.nextLeft }).animate({ top: 0, left: 0, opacity: 1 }, settings.effectTime, values.nextEasing, function () { callback($slider, settings); });
        }, kick: function ($slider, params, callback) {
            var vars = $slider.data('slider:vars'); var settings = params.settings; var direction = params.direction; var values = []; values.delay = settings.effectTime / 2; values.activeEffectTime = settings.effectTime / 2; settings.shiftValue.x = settings.shiftValue.x < 0 ? settings.shiftValue.x * -1 : settings.shiftValue.x; switch (direction) { case 'toTop': values.top = -settings.shiftValue.x; values.left = 0; values.nextTop = settings.shiftValue.x; values.nextLeft = 0; break; case 'toBottom': values.top = settings.shiftValue.x; values.left = 0; values.nextTop = -settings.shiftValue.x; values.nextLeft = 0; break; case 'toRight': values.top = 0; values.left = settings.shiftValue.x; values.nextTop = 0; values.nextLeft = -settings.shiftValue.x; break; case 'toLeft': values.top = 0; values.left = -settings.shiftValue.x; values.nextTop = 0; values.nextLeft = settings.shiftValue.x; break; }
            vars.next.css({ zIndex: 2, opacity: 0 }); vars.active.css({ top: 0, left: 0 }); if (settings.animateActive) { vars.active.delay(values.delay).animate({ top: values.top, left: values.left, opacity: 0 }, values.activeEffectTime, 'out'); }
            vars.next.css({ top: values.nextTop, left: values.nextLeft }).animate({ top: 0, left: 0, opacity: 1 }, settings.effectTime, 'kick', function () { callback($slider, settings); });
        }, transfer: function ($slider, params, callback) {
            var settings = params.settings; var direction = params.direction; var vars = $slider.data('slider:vars'); var values = []; values.width = $slider.width(); values.height = $slider.height(); switch (direction) { case 'toTop': values.top = -settings.shiftValue.y; values.left = values.width / 2; values.nextTop = values.height + settings.shiftValue.y; values.nextLeft = values.width / 2; break; case 'toBottom': values.top = values.height + settings.shiftValue.y; values.left = values.width / 2; values.nextTop = -settings.shiftValue.y; values.nextLeft = values.width / 2; break; case 'toRight': values.top = values.height / 2; values.left = values.width + settings.shiftValue.x; values.nextTop = values.height / 2; values.nextLeft = -settings.shiftValue.x; break; case 'toLeft': values.top = values.height / 2; values.left = -settings.shiftValue.x; values.nextTop = values.height / 2; values.nextLeft = values.width + settings.shiftValue.x; break; }
            vars.next.children().wrapAll('<div id="' + vars.prefix + 'nextContainer" class="' + vars.prefix + 'tmpContainer"></div>'); vars.active.children().wrapAll('<div id="' + vars.prefix + 'activeContainer" class="' + vars.prefix + 'tmpContainer"></div>'); var
            $nextContainer = vars.next.find('#' + vars.prefix + 'nextContainer'), $activeContainer = vars.active.find('#' + vars.prefix + 'activeContainer'), $tmpContainer = vars.container.find('.' + vars.prefix + 'tmpContainer'); $activeContainer.css({ width: values.width, height: values.height, position: 'absolute', top: '50%', left: '50%', margin: '-' + parseInt(values.height * 0.5, 10) + 'px 0 0 -' + parseInt(values.width * 0.5, 10) + 'px' }); $nextContainer.css({ width: values.width, height: values.height, position: 'absolute', top: '50%', left: '50%', margin: '-' + parseInt(values.height * 0.5, 10) + 'px 0 0 -' + parseInt(values.width * 0.5, 10) + 'px' }); if (settings.animateActive) { vars.active.css({ width: '100%', height: '100%', top: 0, left: 0 }).animate({ width: 0, height: 0, top: values.top, left: values.left, opacity: 0 }, settings.effectTime); }
            vars.next.css({ opacity: 0, zIndex: 2, width: 0, height: 0, top: values.nextTop, left: values.nextLeft }).animate({ width: '100%', height: '100%', top: 0, left: 0, opacity: 1 }, settings.effectTime, settings.easing, function () { $tmpContainer.children().unwrap(); callback($slider, settings); });
        }, transfer: function ($slider, params, callback) {
            var settings = params.settings; var direction = params.direction; var vars = $slider.data('slider:vars'); var values = []; values.width = $slider.width(); values.height = $slider.height(); switch (direction) { case 'toTop': values.top = -settings.shiftValue.y; values.left = values.width / 2; values.nextTop = values.height + settings.shiftValue.y; values.nextLeft = values.width / 2; break; case 'toBottom': values.top = values.height + settings.shiftValue.y; values.left = values.width / 2; values.nextTop = -settings.shiftValue.y; values.nextLeft = values.width / 2; break; case 'toRight': values.top = values.height / 2; values.left = values.width + settings.shiftValue.x; values.nextTop = values.height / 2; values.nextLeft = -settings.shiftValue.x; break; case 'toLeft': values.top = values.height / 2; values.left = -settings.shiftValue.x; values.nextTop = values.height / 2; values.nextLeft = values.width + settings.shiftValue.x; break; }
            vars.next.children().wrapAll('<div id="' + vars.prefix + 'nextContainer" class="' + vars.prefix + 'tmpContainer"></div>'); vars.active.children().wrapAll('<div id="' + vars.prefix + 'activeContainer" class="' + vars.prefix + 'tmpContainer"></div>'); var
            $nextContainer = vars.next.find('#' + vars.prefix + 'nextContainer'), $activeContainer = vars.active.find('#' + vars.prefix + 'activeContainer'), $tmpContainer = vars.container.find('.' + vars.prefix + 'tmpContainer'); $activeContainer.css({ width: values.width, height: values.height, position: 'absolute', top: '50%', left: '50%', margin: '-' + parseInt(values.height * 0.5, 10) + 'px 0 0 -' + parseInt(values.width * 0.5, 10) + 'px' }); $nextContainer.css({ width: values.width, height: values.height, position: 'absolute', top: '50%', left: '50%', margin: '-' + parseInt(values.height * 0.5, 10) + 'px 0 0 -' + parseInt(values.width * 0.5, 10) + 'px' }); if (settings.animateActive) { vars.active.css({ width: '100%', height: '100%', top: 0, left: 0 }).animate({ width: 0, height: 0, top: values.top, left: values.left, opacity: 0 }, settings.effectTime); }
            vars.next.css({ opacity: 0, zIndex: 2, width: 0, height: 0, top: values.nextTop, left: values.nextLeft }).animate({ width: '100%', height: '100%', top: 0, left: 0, opacity: 1 }, settings.effectTime, settings.easing, function () { $tmpContainer.children().unwrap(); callback($slider, settings); });
        }, shuffle: function ($slider, params, callback) {
            var
            vars = $slider.data('slider:vars'), settings = params.settings, values = [], preShuffle = function ($slider, settings, $li) {
                var vars = $slider.data('slider:vars'); $li.html('<div class="' + vars.prefix + 'partContainer">' + $li.html() + '</div>'); var part = $li.html(); var width = $slider.width(); var height = $slider.height(); for (i = 1; i < (settings.parts.x * settings.parts.y) ; i++) { $li.html($li.html() + part); }
                var $parts = $li.children('.' + vars.prefix + 'partContainer'); var partValues = []; partValues.width = $li.width() / settings.parts.x; partValues.height = $li.height() / settings.parts.y; $parts.each(function (i) { var $this = $(this); partValues.top = ((i - (i % settings.parts.x)) / settings.parts.x) * partValues.height; partValues.left = (i % settings.parts.x) * partValues.width; partValues.marginTop = -partValues.top; partValues.marginLeft = -partValues.left; $this.css({ top: partValues.top, left: partValues.left, width: partValues.width, height: partValues.height, position: 'absolute', overflow: 'hidden' }).html('<div class="' + vars.prefix + 'part">' + $this.html() + '</div>'); $this.children('.' + vars.prefix + 'part').css({ marginTop: partValues.marginTop, marginLeft: partValues.marginLeft, width: width, height: height, background: $li.css('background-image') + ' ' + $li.parent().css('background-color') }); }); return $parts;
            }, calcParts = function (parts, c) {
                if (parts.x * parts.y > 36) {
                    if (c) {
                        if (parts.x > 1) { parts.x--; } else { parts.y--; }
                        c = false;
                    } else {
                        if (parts.y > 1) { parts.y--; } else { parts.x--; }
                        c = true;
                    }
                    return calcParts(parts, c);
                }
                return parts;
            }, shuffle = function ($slider, settings) {
                settings.parts.x = settings.parts.x < 1 ? 1 : settings.parts.x; settings.parts.y = settings.parts.y < 1 ? 1 : settings.parts.y; settings.parts = calcParts(settings.parts, true); settings.shiftValue.x = settings.shiftValue.x < 0 ? settings.shiftValue.x * -1 : settings.shiftValue.x; settings.shiftValue.y = settings.shiftValue.y < 0 ? settings.shiftValue.y * -1 : settings.shiftValue.y; var vars = $slider.data('slider:vars'); var activeContent = vars.active.html(); var nextContent = vars.next.html(); var width = $slider.width(); var height = $slider.height(); var $activeParts = preShuffle($slider, settings, vars.active); var $nextParts = preShuffle($slider, settings, vars.next); var activeBackgroundImage = vars.active.css('background-image'); var activeBackgroundColor = vars.active.css('background-color'); var nextBackgroundImage = vars.next.css('background-image'); var nextBackgroundColor = vars.next.css('background-color'); vars.active.css({ backgroundImage: 'none', backgroundColor: 'none', opacity: 1 }); vars.next.css({ backgroundImage: 'none', backgroundColor: 'none', opacity: 1, zIndex: 2 }); var partValues = []; partValues.width = vars.next.width() / settings.parts.x; partValues.height = vars.next.height() / settings.parts.y; if (settings.animateActive) { $activeParts.each(function (i) { $this = $(this); var newLeft, newTop; newLeft = (Math.random() * (settings.shiftValue.x * 2) - settings.shiftValue.x); newTop = (Math.random() * (settings.shiftValue.y * 2) - settings.shiftValue.y); $this.animate({ opacity: 0, top: '+=' + newTop, left: '+=' + newLeft }, settings.effectTime, settings.easing); }); }
                $nextParts.each(function (i) { $this = $(this); partValues.top = ((i - (i % settings.parts.x)) / settings.parts.x) * partValues.height; partValues.left = (i % settings.parts.x) * partValues.width; var newLeft, newTop; newLeft = partValues.left + (Math.random() * (settings.shiftValue.x * 2) - settings.shiftValue.x); newTop = partValues.top + (Math.random() * (settings.shiftValue.y * 2) - settings.shiftValue.y); $this.css({ top: newTop, left: newLeft, opacity: 0 }).animate({ top: partValues.top, left: partValues.left, opacity: 1 }, settings.effectTime, settings.easing, function () { if (i == $activeParts.length - 1) { vars.active.html(activeContent); vars.next.html(nextContent); vars.active.css({ backgroundImage: activeBackgroundImage, backgroundColor: activeBackgroundColor, opacity: 0 }); vars.next.css({ backgroundImage: nextBackgroundImage, backgroundColor: nextBackgroundColor, opacity: 1 }); callback($slider, settings); } }); });
            }
            shuffle($slider, settings);
        }, explode: function ($slider, params, callback) {
            var
            vars = $slider.data('slider:vars'), settings = params.settings, values = [], preShuffle = function ($slider, settings, $li) {
                var vars = $slider.data('slider:vars'); $li.html('<div class="' + vars.prefix + 'partContainer">' + $li.html() + '</div>'); var part = $li.html(); var width = $slider.width(); var height = $slider.height(); for (i = 1; i < (settings.parts.x * settings.parts.y) ; i++) { $li.html($li.html() + part); }
                var $parts = $li.children('.' + vars.prefix + 'partContainer'); var partValues = []; partValues.width = $li.width() / settings.parts.x; partValues.height = $li.height() / settings.parts.y; $parts.each(function (i) { var $this = $(this); partValues.top = ((i - (i % settings.parts.x)) / settings.parts.x) * partValues.height; partValues.left = (i % settings.parts.x) * partValues.width; partValues.marginTop = -partValues.top; partValues.marginLeft = -partValues.left; $this.css({ top: partValues.top, left: partValues.left, width: partValues.width, height: partValues.height, position: 'absolute', overflow: 'hidden' }).html('<div class="' + vars.prefix + 'part">' + $this.html() + '</div>'); $this.children('.' + vars.prefix + 'part').css({ marginTop: partValues.marginTop, marginLeft: partValues.marginLeft, width: width, height: height, background: $li.css('background-image') + ' ' + $li.parent().css('background-color') }); }); return $parts;
            }, calcParts = function (parts, c) {
                if (parts.x * parts.y > 36) {
                    if (c) {
                        if (parts.x > 1) { parts.x--; } else { parts.y--; }
                        c = false;
                    } else {
                        if (parts.y > 1) { parts.y--; } else { parts.x--; }
                        c = true;
                    }
                    return calcParts(parts, c);
                }
                return parts;
            }, explode = function ($slider, settings) {
                settings.parts.x = settings.parts.x < 1 ? 1 : settings.parts.x; settings.parts.y = settings.parts.y < 1 ? 1 : settings.parts.y; settings.parts = calcParts(settings.parts, true); settings.shiftValue.x = settings.shiftValue.x < 0 ? settings.shiftValue.x * -1 : settings.shiftValue.x; settings.shiftValue.y = settings.shiftValue.y < 0 ? settings.shiftValue.y * -1 : settings.shiftValue.y; var vars = $slider.data('slider:vars'); var activeContent = vars.active.html(); var nextContent = vars.next.html(); var width = $slider.width(); var height = $slider.height(); var $activeParts = preShuffle($slider, settings, vars.active); var $nextParts = preShuffle($slider, settings, vars.next); var activeBackgroundImage = vars.active.css('background-image'); var activeBackgroundColor = vars.active.css('background-color'); var nextBackgroundImage = vars.next.css('background-image'); var nextBackgroundColor = vars.next.css('background-color'); vars.active.css({ backgroundImage: 'none', backgroundColor: 'none', opacity: 1 }); vars.next.css({ backgroundImage: 'none', backgroundColor: 'none', opacity: 1, zIndex: 2 }); var partValues = []; partValues.width = vars.next.width() / settings.parts.x; partValues.height = vars.next.height() / settings.parts.y; if (settings.animateActive) { $activeParts.each(function (i) { $this = $(this); var newLeft, newTop; var position = []; position.top = $this.position().top; position.bottom = $this.parent().height() - $this.position().top - $this.height(); position.left = $this.position().left; position.right = $this.parent().width() - $this.position().left - $this.width(); var rndX = parseInt(Math.random() * settings.shiftValue.x, 10); var rndY = parseInt(Math.random() * settings.shiftValue.y, 10); newLeft = position.right <= position.left ? (position.right == position.left ? rndX / 2 : rndX) : -rndX; newTop = position.bottom <= position.top ? (position.top == (position.bottom - 1) ? rndY / 2 : rndY) : -rndY; $this.animate({ top: '+=' + newTop, left: '+=' + newLeft, opacity: 0 }, settings.effectTime, settings.easing); }); }
                $nextParts.each(function (i) { $this = $(this); partValues.top = ((i - (i % settings.parts.x)) / settings.parts.x) * partValues.height; partValues.left = (i % settings.parts.x) * partValues.width; var newLeft, newTop, position = []; position.top = $this.position().top; position.bottom = $this.parent().height() - $this.position().top - $this.height(); position.left = $this.position().left; position.right = $this.parent().width() - $this.position().left - $this.width(); var rndX = parseInt(Math.random() * settings.shiftValue.x, 10); var rndY = parseInt(Math.random() * settings.shiftValue.y, 10); newLeft = position.right <= position.left ? (position.right == position.left ? rndX / 2 : rndX) : -rndX; newTop = position.bottom <= position.top ? (position.top == (position.bottom - 1) ? rndY / 2 : rndY) : -rndY; newLeft = partValues.left + newLeft; newTop = partValues.top + newTop; $this.css({ top: newTop, left: newLeft, opacity: 0 }).animate({ top: partValues.top, left: partValues.left, opacity: 1 }, settings.effectTime, settings.easing, function () { if (i == $activeParts.length - 1) { vars.active.html(activeContent); vars.next.html(nextContent); vars.active.css({ backgroundImage: activeBackgroundImage, backgroundColor: activeBackgroundColor, opacity: 0 }); vars.next.css({ backgroundImage: nextBackgroundImage, backgroundColor: nextBackgroundColor, opacity: 1 }); callback($slider, settings); } }); });
            }
            explode($slider, settings);
        }, turnOver: function ($slider, params, callback) {
            var
            vars = $slider.data('slider:vars'), settings = params.settings, direction = params.direction, values = []; values.width = vars.container.width(); values.height = vars.container.height(); switch (direction) { case 'toTop': values.top = -values.height; values.left = 0; break; case 'toBottom': values.top = values.height; values.left = 0; break; case 'toRight': values.top = 0; values.left = values.width; break; case 'toLeft': values.top = 0; values.left = -values.width; break; }
            values.timeOut = settings.animateActive ? settings.effectTime : 0; values.effectTime = settings.animateActive ? settings.effectTime / 2 : settings.effectTime; vars.next.css({ zIndex: 2, opacity: 1 }); vars.next.css({ top: values.top, left: values.left }); if (settings.animateActive) { vars.active.css({ top: 0, left: 0 }).animate({ top: values.top, left: values.left, opacity: 1 }, values.effectTime, settings.easing); }
            setTimeout(function () { vars.next.animate({ top: 0, left: 0, opacity: 1 }, values.effectTime, settings.easing, function () { vars.active.css('opacity', 0); callback($slider, settings); }); }, values.timeOut);
        }, chewyBars: function ($slider, params, callback) {
            var
            vars = $slider.data('slider:vars'), settings = params.settings, direction = params.direction, values = [], preSlide = function ($slider, settings, $li) {
                var vars = $slider.data('slider:vars'); $li.html('<div class="' + vars.prefix + 'partContainer">' + $li.html() + '</div>'); var
                part = $li.html(), width = $slider.width(), height = $slider.height(); for (i = 1; i < settings.parts; i++) { $li.html($li.html() + part); }
                var
                $parts = $li.children('.' + vars.prefix + 'partContainer'), partValues = []; switch (direction) { case 'toLeft': partValues.width = $li.width() / settings.parts; partValues.height = height; break; case 'toTop': partValues.width = width; partValues.height = $li.height() / settings.parts; break; }
                $parts.each(function (i) {
                    var $this = $(this), liWidth = $li.width(), liHeight = $li.height(); partValues.left = 'auto'; partValues.marginLeft = 'auto'; partValues.top = 'auto'; partValues.marginTop = 'auto'; partValues.right = 'auto'; partValues.bottom = 'auto'; switch (direction) { case 'toLeft': partValues.width = liWidth / settings.parts; partValues.height = height; partValues.left = (i % settings.parts) * partValues.width; partValues.marginLeft = -partValues.left; partValues.top = 0; partValues.marginTop = 0; break; case 'toRight': partValues.width = liWidth / settings.parts; partValues.height = height; partValues.right = (i % settings.parts) * partValues.width; partValues.marginLeft = -(liWidth - partValues.right - partValues.width); partValues.top = 0; partValues.marginTop = 0; break; case 'toTop': partValues.width = width; partValues.height = liHeight / settings.parts; partValues.left = 0; partValues.marginLeft = 0; partValues.top = (i % settings.parts) * partValues.height; partValues.marginTop = -partValues.top; break; case 'toBottom': partValues.width = width; partValues.height = liHeight / settings.parts; partValues.left = 0; partValues.marginLeft = 0; partValues.bottom = (i % settings.parts) * partValues.height; partValues.marginTop = -(liHeight - partValues.bottom - partValues.height); break; }
                    $this.css({ top: partValues.top, left: partValues.left, bottom: partValues.bottom, right: partValues.right, width: partValues.width, height: partValues.height, position: 'absolute', overflow: 'hidden' }).html('<div class="' + vars.prefix + 'part">' + $this.html() + '</div>'); $this.children('.' + vars.prefix + 'part').css({ marginLeft: partValues.marginLeft, marginTop: partValues.marginTop, width: width, height: height, background: $li.css('background-image') + ' ' + $li.parent().css('background-color') });
                }); return $parts;
            }, slideBars = function ($slider, settings) {
                settings.parts = settings.parts < 1 ? 1 : settings.parts; settings.shiftValue.x = settings.shiftValue.x < 0 ? settings.shiftValue.x * -1 : settings.shiftValue.x; settings.shiftValue.y = settings.shiftValue.y < 0 ? settings.shiftValue.y * -1 : settings.shiftValue.y; var vars = $slider.data('slider:vars'); var
                partDuration, partDelay = settings.partDelay, activeContent = vars.active.html(), nextContent = vars.next.html(), width = $slider.width(), height = $slider.height(), $activeParts = preSlide($slider, settings, vars.active), $nextParts = preSlide($slider, settings, vars.next), activeBackgroundImage = vars.active.css('background-image'), activeBackgroundColor = vars.active.css('background-color'), nextBackgroundImage = vars.next.css('background-image'), nextBackgroundColor = vars.next.css('background-color'), delay = 0; partDuration = settings.effectTime - (2 * ((settings.parts - 1) * partDelay)); vars.active.css({ backgroundImage: 'none', backgroundColor: 'none', opacity: 1 }); vars.next.css({ backgroundImage: 'none', backgroundColor: 'none', opacity: 1, zIndex: 2 }); var values = [], aniMap = { opacity: 0 }, cssMapNext = { opacity: 0 }; switch (direction) { case 'toTop': aniMap.left = -settings.shiftValue.x; aniMap.top = -settings.shiftValue.y; cssMapNext.left = settings.shiftValue.x; cssMapNext.top = height + settings.shiftValue.y; values.width = width; values.height = vars.next.height() / settings.parts; break; case 'toRight': values.width = vars.next.width() / settings.parts; values.height = height; aniMap.top = -settings.shiftValue.y; aniMap.right = -settings.shiftValue.x; cssMapNext.top = settings.shiftValue.y; cssMapNext.right = width + settings.shiftValue.x; break; case 'toBottom': values.width = width; values.height = vars.next.height() / settings.parts; aniMap.left = -settings.shiftValue.x; aniMap.bottom = -settings.shiftValue.y; cssMapNext.left = settings.shiftValue.x; cssMapNext.bottom = height + settings.shiftValue.y; break; case 'toLeft': values.width = vars.next.width() / settings.parts; values.height = height; aniMap.top = -settings.shiftValue.y; aniMap.left = -settings.shiftValue.x; cssMapNext.top = settings.shiftValue.y; cssMapNext.left = width + settings.shiftValue.x; break; }
                if (settings.animateActive) { $activeParts.each(function (i) { $this = $(this); $this.delay(partDelay * i).animate(aniMap, partDuration, settings.easing); }); delay = settings.parts * partDelay; }
                $nextParts.each(function (i) {
                    var $this = $(this), newValues = [], aniMap = { opacity: 1 }; switch (direction) { case 'toTop': aniMap.left = 0; aniMap.top = values.height * i; break; case 'toRight': aniMap.top = 0; aniMap.right = values.width * i; break; case 'toBottom': aniMap.left = 0; aniMap.bottom = values.height * i; break; case 'toLeft': aniMap.top = 0; aniMap.left = values.width * i; break; }
                    $this.delay(delay).css(cssMapNext).delay(i * partDelay).animate(aniMap, partDuration, settings.easing, function () { if (i == settings.parts - 1) { vars.active.html(activeContent); vars.next.html(nextContent); vars.active.css({ backgroundImage: activeBackgroundImage, backgroundColor: activeBackgroundColor, opacity: 0 }); vars.next.css({ backgroundImage: nextBackgroundImage, backgroundColor: nextBackgroundColor, opacity: 1 }); callback($slider, settings); } });
                });
            }
            slideBars($slider, settings);
        }
    }; $.fn.rhinoslider.preparations = {
        fade: function ($slider, settings, vars) { }, slide: function ($slider, settings, vars) { vars.items.css('overflow', 'hidden'); $slider.css('overflow', 'hidden'); }, kick: function ($slider, settings, vars) { vars.items.css('overflow', 'hidden'); settings.shiftValue.x = parseInt(tmpShiftValue, 10); settings.shiftValue.y = parseInt(tmpShiftValue, 10); settings.parts.x = parseInt(tmpParts, 10); settings.parts.y = parseInt(tmpParts, 10); }, transfer: function ($slider, settings, vars) {
            var shiftValue = String(tmpShiftValue); if (shiftValue.indexOf(',') >= 0) { var tmp = shiftValue.split(','); settings.shiftValue.x = parseInt(tmp[0], 10); settings.shiftValue.y = parseInt(tmp[1], 10); } else { settings.shiftValue.x = parseInt(tmpShiftValue, 10); settings.shiftValue.y = parseInt(tmpShiftValue, 10); }
            vars.items.css('overflow', 'hidden');
        }, shuffle: function ($slider, settings, vars) {
            var shiftValue = String(tmpShiftValue); if (shiftValue.indexOf(',') >= 0) { var tmp = shiftValue.split(','); settings.shiftValue.x = tmp[0]; settings.shiftValue.y = tmp[1]; } else { settings.shiftValue.x = parseInt(tmpShiftValue, 10); settings.shiftValue.y = parseInt(tmpShiftValue, 10); }
            var parts = String(tmpParts); if (parts.indexOf(',') >= 0) { var tmp = parts.split(','); settings.parts.x = tmp[0]; settings.parts.y = tmp[1]; } else { settings.parts.x = parseInt(tmpParts, 10); settings.parts.y = parseInt(tmpParts, 10); }
            vars.items.css('overflow', 'visible');
        }, explode: function ($slider, settings, vars) {
            var shiftValue = String(tmpShiftValue); if (shiftValue.indexOf(',') >= 0) { var tmp = shiftValue.split(','); settings.shiftValue.x = tmp[0]; settings.shiftValue.y = tmp[1]; } else { settings.shiftValue.x = parseInt(tmpShiftValue, 10); settings.shiftValue.y = parseInt(tmpShiftValue, 10); }
            var parts = String(tmpParts); if (parts.indexOf(',') >= 0) { var tmp = parts.split(','); settings.parts.x = tmp[0]; settings.parts.y = tmp[1]; } else { settings.parts.x = parseInt(tmpParts, 10); settings.parts.y = parseInt(tmpParts, 10); }
            vars.items.css('overflow', 'visible');
        }, turnOver: function ($slider, settings, vars) { vars.items.css('overflow', 'hidden'); $slider.css('overflow', 'hidden'); }, chewyBars: function ($slider, settings, vars) {
            var shiftValue = String(tmpShiftValue); if (shiftValue.indexOf(',') >= 0) { var tmp = shiftValue.split(','); settings.shiftValue.x = parseInt(tmp[0], 10); settings.shiftValue.y = parseInt(tmp[1], 10); } else { settings.shiftValue.x = parseInt(tmpShiftValue, 10); settings.shiftValue.y = parseInt(tmpShiftValue, 10); }
            var parts = String(tmpParts); if (parts.indexOf(',') >= 0) { var tmp = parts.split(','); settings.parts = parseInt(tmp[0], 10) * parseInt(tmp[1], 10); } else { settings.parts = parseInt(tmpParts, 10); }
            vars.items.css('overflow', 'visible');
        }
    };
})(jQuery, window);

/*! Copyright (c) 2011 Brandon Aaron (http://brandonaaron.net)
 * Licensed under the MIT License (LICENSE.txt).
 *
 * Thanks to: http://adomas.org/javascript-mouse-wheel/ for some pointers.
 * Thanks to: Mathias Bank(http://www.mathias-bank.de) for a scope bug fix.
 * Thanks to: Seamus Leahy for adding deltaX and deltaY
 *
 * Version: 3.0.6
 * 
 * Requires: 1.2.2+
 */
(function (a) { function d(b) { var c = b || window.event, d = [].slice.call(arguments, 1), e = 0, f = !0, g = 0, h = 0; return b = a.event.fix(c), b.type = "mousewheel", c.wheelDelta && (e = c.wheelDelta / 120), c.detail && (e = -c.detail / 3), h = e, c.axis !== undefined && c.axis === c.HORIZONTAL_AXIS && (h = 0, g = -1 * e), c.wheelDeltaY !== undefined && (h = c.wheelDeltaY / 120), c.wheelDeltaX !== undefined && (g = -1 * c.wheelDeltaX / 120), d.unshift(b, e, g, h), (a.event.dispatch || a.event.handle).apply(this, d) } var b = ["DOMMouseScroll", "mousewheel"]; if (a.event.fixHooks) for (var c = b.length; c;) a.event.fixHooks[b[--c]] = a.event.mouseHooks; a.event.special.mousewheel = { setup: function () { if (this.addEventListener) for (var a = b.length; a;) this.addEventListener(b[--a], d, !1); else this.onmousewheel = d }, teardown: function () { if (this.removeEventListener) for (var a = b.length; a;) this.removeEventListener(b[--a], d, !1); else this.onmousewheel = null } }, a.fn.extend({ mousewheel: function (a) { return a ? this.bind("mousewheel", a) : this.trigger("mousewheel") }, unmousewheel: function (a) { return this.unbind("mousewheel", a) } }) })(jQuery);



/*
 * jQuery Easing v1.3 - http://gsgd.co.uk/sandbox/jquery/easing/
 *
 * Uses the built in easing capabilities added In jQuery 1.1
 * to offer multiple easing options
 *
 * TERMS OF USE - jQuery Easing
 * 
 * Open source under the BSD License. 
 * 
 * Copyright © 2008 George McGinley Smith
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 * 
 * Redistributions of source code must retain the above copyright notice, this list of 
 * conditions and the following disclaimer.
 * Redistributions in binary form must reproduce the above copyright notice, this list 
 * of conditions and the following disclaimer in the documentation and/or other materials 
 * provided with the distribution.
 * 
 * Neither the name of the author nor the names of contributors may be used to endorse 
 * or promote products derived from this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
 *  COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 *  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE
 *  GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED 
 * AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 *  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED 
 * OF THE POSSIBILITY OF SUCH DAMAGE. 
 *
*/

// t: current time, b: begInnIng value, c: change In value, d: duration
jQuery.easing['jswing'] = jQuery.easing['swing'];

jQuery.extend(jQuery.easing,
{
    def: 'easeOutQuad',
    swing: function (x, t, b, c, d) {
        //alert(jQuery.easing.default);
        return jQuery.easing[jQuery.easing.def](x, t, b, c, d);
    },
    easeInQuad: function (x, t, b, c, d) {
        return c * (t /= d) * t + b;
    },
    easeOutQuad: function (x, t, b, c, d) {
        return -c * (t /= d) * (t - 2) + b;
    },
    easeInOutQuad: function (x, t, b, c, d) {
        if ((t /= d / 2) < 1) return c / 2 * t * t + b;
        return -c / 2 * ((--t) * (t - 2) - 1) + b;
    },
    easeInCubic: function (x, t, b, c, d) {
        return c * (t /= d) * t * t + b;
    },
    easeOutCubic: function (x, t, b, c, d) {
        return c * ((t = t / d - 1) * t * t + 1) + b;
    },
    easeInOutCubic: function (x, t, b, c, d) {
        if ((t /= d / 2) < 1) return c / 2 * t * t * t + b;
        return c / 2 * ((t -= 2) * t * t + 2) + b;
    },
    easeInQuart: function (x, t, b, c, d) {
        return c * (t /= d) * t * t * t + b;
    },
    easeOutQuart: function (x, t, b, c, d) {
        return -c * ((t = t / d - 1) * t * t * t - 1) + b;
    },
    easeInOutQuart: function (x, t, b, c, d) {
        if ((t /= d / 2) < 1) return c / 2 * t * t * t * t + b;
        return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
    },
    easeInQuint: function (x, t, b, c, d) {
        return c * (t /= d) * t * t * t * t + b;
    },
    easeOutQuint: function (x, t, b, c, d) {
        return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
    },
    easeInOutQuint: function (x, t, b, c, d) {
        if ((t /= d / 2) < 1) return c / 2 * t * t * t * t * t + b;
        return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
    },
    easeInSine: function (x, t, b, c, d) {
        return -c * Math.cos(t / d * (Math.PI / 2)) + c + b;
    },
    easeOutSine: function (x, t, b, c, d) {
        return c * Math.sin(t / d * (Math.PI / 2)) + b;
    },
    easeInOutSine: function (x, t, b, c, d) {
        return -c / 2 * (Math.cos(Math.PI * t / d) - 1) + b;
    },
    easeInExpo: function (x, t, b, c, d) {
        return (t == 0) ? b : c * Math.pow(2, 10 * (t / d - 1)) + b;
    },
    easeOutExpo: function (x, t, b, c, d) {
        return (t == d) ? b + c : c * (-Math.pow(2, -10 * t / d) + 1) + b;
    },

    easeInOutExpo: function (x, t, b, c, d) {
        if (t == 0) return b;
        if (t == d) return b + c;
        if ((t /= d / 2) < 1) return c / 2 * Math.pow(2, 10 * (t - 1)) + b;
        return c / 2 * (-Math.pow(2, -10 * --t) + 2) + b;
    },
    easeInCirc: function (x, t, b, c, d) {
        return -c * (Math.sqrt(1 - (t /= d) * t) - 1) + b;
    },
    easeOutCirc: function (x, t, b, c, d) {
        return c * Math.sqrt(1 - (t = t / d - 1) * t) + b;
    },
    easeInOutCirc: function (x, t, b, c, d) {
        if ((t /= d / 2) < 1) return -c / 2 * (Math.sqrt(1 - t * t) - 1) + b;
        return c / 2 * (Math.sqrt(1 - (t -= 2) * t) + 1) + b;
    },
    easeInElastic: function (x, t, b, c, d) {
        var s = 1.70158; var p = 0; var a = c;
        if (t == 0) return b; if ((t /= d) == 1) return b + c; if (!p) p = d * .3;
        if (a < Math.abs(c)) { a = c; var s = p / 4; }
        else var s = p / (2 * Math.PI) * Math.asin(c / a);
        return -(a * Math.pow(2, 10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p)) + b;
    },
    easeOutElastic: function (x, t, b, c, d) {
        var s = 1.70158; var p = 0; var a = c;
        if (t == 0) return b; if ((t /= d) == 1) return b + c; if (!p) p = d * .3;
        if (a < Math.abs(c)) { a = c; var s = p / 4; }
        else var s = p / (2 * Math.PI) * Math.asin(c / a);
        return a * Math.pow(2, -10 * t) * Math.sin((t * d - s) * (2 * Math.PI) / p) + c + b;
    },
    easeInOutElastic: function (x, t, b, c, d) {
        var s = 1.70158; var p = 0; var a = c;
        if (t == 0) return b; if ((t /= d / 2) == 2) return b + c; if (!p) p = d * (.3 * 1.5);
        if (a < Math.abs(c)) { a = c; var s = p / 4; }
        else var s = p / (2 * Math.PI) * Math.asin(c / a);
        if (t < 1) return -.5 * (a * Math.pow(2, 10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p)) + b;
        return a * Math.pow(2, -10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p) * .5 + c + b;
    },
    easeInBack: function (x, t, b, c, d, s) {
        if (s == undefined) s = 1.70158;
        return c * (t /= d) * t * ((s + 1) * t - s) + b;
    },
    easeOutBack: function (x, t, b, c, d, s) {
        if (s == undefined) s = 1.70158;
        return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
    },
    easeInOutBack: function (x, t, b, c, d, s) {
        if (s == undefined) s = 1.70158;
        if ((t /= d / 2) < 1) return c / 2 * (t * t * (((s *= (1.525)) + 1) * t - s)) + b;
        return c / 2 * ((t -= 2) * t * (((s *= (1.525)) + 1) * t + s) + 2) + b;
    },
    easeInBounce: function (x, t, b, c, d) {
        return c - jQuery.easing.easeOutBounce(x, d - t, 0, c, d) + b;
    },
    easeOutBounce: function (x, t, b, c, d) {
        if ((t /= d) < (1 / 2.75)) {
            return c * (7.5625 * t * t) + b;
        } else if (t < (2 / 2.75)) {
            return c * (7.5625 * (t -= (1.5 / 2.75)) * t + .75) + b;
        } else if (t < (2.5 / 2.75)) {
            return c * (7.5625 * (t -= (2.25 / 2.75)) * t + .9375) + b;
        } else {
            return c * (7.5625 * (t -= (2.625 / 2.75)) * t + .984375) + b;
        }
    },
    easeInOutBounce: function (x, t, b, c, d) {
        if (t < d / 2) return jQuery.easing.easeInBounce(x, t * 2, 0, c, d) * .5 + b;
        return jQuery.easing.easeOutBounce(x, t * 2 - d, 0, c, d) * .5 + c * .5 + b;
    }
});

/*
 *
 * TERMS OF USE - EASING EQUATIONS
 * 
 * Open source under the BSD License. 
 * 
 * Copyright © 2001 Robert Penner
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 * 
 * Redistributions of source code must retain the above copyright notice, this list of 
 * conditions and the following disclaimer.
 * Redistributions in binary form must reproduce the above copyright notice, this list 
 * of conditions and the following disclaimer in the documentation and/or other materials 
 * provided with the distribution.
 * 
 * Neither the name of the author nor the names of contributors may be used to endorse 
 * or promote products derived from this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
 *  COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 *  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE
 *  GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED 
 * AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 *  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED 
 * OF THE POSSIBILITY OF SUCH DAMAGE. 
 *
 */

$.get(RssRoot + '/gallery/5',function(datas){
	$('item',datas).each(function(i) {
	    $("#slider").append('<li><img src="' + $(this).find("src").text() + '" /></li>');
	});
	$('#slider').rhinoslider();
	$('#slider').show()
});