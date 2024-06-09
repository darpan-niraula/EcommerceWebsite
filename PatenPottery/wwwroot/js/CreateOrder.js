$(document).ready(function () {
    $('#editButton').click(function () {
        $('.editable').toggleClass('form-control form-control-static');
        // Toggle button text
        var buttonText = $(this).text() === "Edit Order Detail" ? "View Order Detail" : "Edit Order Detail";
        $(this).text(buttonText);
    });
});


function CreateOrder() {
    var form = $('#createOrderForm')[0];
    var data = new FormData(form);

    $.ajax({
        type: "POST",
        url: form.action, // Ensure the form action points to the correct endpoint
        data: data,
        contentType: false,
        processData: false,
        success: function (result) {
            if (result.success) {
                savesuccess(result.message);
            } else {
                savesuccess(result.message || "An error occurred.");
            }
        },
        error: function (err) {
            savesuccess("An error occurred while saving the order.");
        }
    });
}
