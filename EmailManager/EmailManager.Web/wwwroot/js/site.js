// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let openOrCloseEmailButtons = Array.from(document.querySelectorAll('.open-close-email'));

openOrCloseEmailButtons.forEach(button => {
    button.addEventListener('click', showOrHideEmail);
});


function showOrHideEmail(event) {
    let openOrCloseButton = event.target;
    let tdElement = openOrCloseButton.parentNode;
    let trElement = tdElement.parentNode;

    let emailContainer = trElement.lastElementChild;

    if (openOrCloseButton.value === 'Open') {
        emailContainer.style.display = 'block';
        openOrCloseButton.value = 'Close';
    } else if (openOrCloseButton.value === 'Close') {
        emailContainer.style.display = 'none';
        openOrCloseButton.value = 'Open';
    }
}