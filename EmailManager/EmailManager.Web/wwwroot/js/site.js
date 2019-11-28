
$('#checkegn').keyup(() => {
    let egn = $('#checkegn').val();
    
    if (egn.length === 10) {
        $.ajax({
            type: 'Get',
            url: "/Home/CheckEGN",
            data:
            {
                'egn': egn
            },
            success: function (response) {
                if (!response) {
                    $('#isrealegn').text("This EGN is invalid! Are you sure?");
                }
                else {
                    $('#isrealegn').text("");
                }
            }
        })
    }
    
})