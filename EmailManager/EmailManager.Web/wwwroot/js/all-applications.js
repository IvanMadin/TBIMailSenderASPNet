let openOrCloseEmailButtons = Array.from(document.querySelectorAll('.open-close-email'));

openOrCloseEmailButtons.forEach(button => {
    button.addEventListener('click', showOrHideEmail);
});


function showOrHideEmail(event) {
    let openOrCloseButton = event.target;
    let tdElement = openOrCloseButton.parentNode;
    let trElement = tdElement.parentNode;

    let emailContainer = trElement.lastElementChild;

    if (openOrCloseButton.value === 'Preview body') {
        emailContainer.style.display = 'block';
        openOrCloseButton.value = 'Hide body';
    } else if (openOrCloseButton.value === 'Hide body') {
        emailContainer.style.display = 'none';
        openOrCloseButton.value = 'Preview body';
    }
}