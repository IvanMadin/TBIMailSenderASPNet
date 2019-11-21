var secondClick = false;
var duration = 1000;

$('#someElement').on('click', 'a', function (event) {
    var that = $(this);

    if (!secondClick) {
        event.stopPropagation();
        setTimeout(function () {
            secondClick = true;
            that.click();
        }, duration);

        someAsynchronousFunction();
    } else {
        secondClick = false;
    }
});

    //Delaying and blocking and preventing second click