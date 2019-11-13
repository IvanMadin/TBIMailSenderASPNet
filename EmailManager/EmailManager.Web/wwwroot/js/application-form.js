$(document).ready(() => {

    $('#show-or-hide-button-form').on('click', function myFunction(event) {

        let showHideButton = event.target;

        console.log(showHideButton);

        var divHolder = document.getElementById('application-form-div');

        if (showHideButton.value === 'Show Form') {
            divHolder.style.visibility = "visible";
            showHideButton.value = 'Hide Form';
            showHideButton.classList.remove('btn-outline-warning');
            showHideButton.classList.add('btn-warning');
        } else {
            divHolder.style.visibility = "hidden";
            showHideButton.value = 'Show Form';
            showHideButton.classList.remove('btn-warning');
            showHideButton.classList.add('btn-outline-warning');
        }
    });
});

//divHolder.style.visibility === "hidden" that was inside the if condition thats why the first hit of the button was never shown. After the change it appears instantly.