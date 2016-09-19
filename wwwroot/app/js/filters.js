"use strict";

angularMovieApp.filter('stars', function () {

    var STARS = {
        1: '\u2605',
        2: '\u2605\u2605',
        3: '\u2605\u2605\u2605',
        4: '\u2605\u2605\u2605\u2605',
        5: '\u2605\u2605\u2605\u2605\u2605'
    };

    return function(startCount) {
        return STARS[startCount];
    };
});


angularMovieApp.filter('poster', function () {
    return function(data) {
        if(!data){
            return "img/no-poster.jpg";
        } else {
            return 'data:image/jpeg;base64,' + data;
        }
    };
});