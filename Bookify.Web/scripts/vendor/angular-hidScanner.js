/**
 * Author: Furqan Razzaq
 * License: MIT
 * Date: June 23, 2014
 */

// this directive is custom written, but built on top of an existing vendor file

(function (window, angular) {
    'use strict';

    angular.module('angular-hidScanner', []).

        factory('hidScanner', function ($window) {
            return {
                initialize: function (childScope) {
                    var chars = [];
                    var scanTimes = [];
                    var timer;

                    // launched whenever a key is pressed
                    angular.element($window).on('keypress', function (e) {

                        // clear the previous timer as the barcode scanner hasnt finished sending keystrokes yet
                        clearTimeout(timer);

                        scanTimes.push(Date.now());

                        // this line makes sure that input from a human is ignored
                        // it works by checking for the difference beetwen the last two keypresses
                        // if its larger than 300 milliseconds, it means that it was inputted by a human
                        if (scanTimes.length > 1 && Date.now() - scanTimes[scanTimes.length - 2] > 300)
                            chars = [];


                        // check if the inputted keystroke is a number
                        if (e.which >= 48 && e.which <= 57) {
                            chars.push(String.fromCharCode(e.which));
                        }

                        var c = chars;

                        timer = setTimeout(function () {

                            if (chars.length >= 10) {
                                var barcode = chars.join("");
                                chars = [];
                                // childScope is a parameter, usually $scope reffering back to the called context
                                childScope.processScannedBarcode(barcode);
                            }
                        }, 300);
                    });
                }
            };
        }).

    run();

})(window, window.angular);