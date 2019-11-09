$(document).ready(() => {

    $('#show-or-hide-button-form').on('click', function myFunction(event) {

        let showHideButton = event.target;

        console.log(showHideButton);

        var divHolder = document.getElementById('application-form-div');

        if (divHolder.style.visibility === "hidden") {
            divHolder.style.visibility = "visible";
            showHideButton.value = 'Hide Form';
            showHideButton.classList.remove('btn-outline-success');
            showHideButton.classList.add('btn-success');
        } else {
            divHolder.style.visibility = "hidden";
            showHideButton.value = 'Show Form';
            showHideButton.classList.remove('btn-success');
            showHideButton.classList.add('btn-outline-success');
        }
    });
});