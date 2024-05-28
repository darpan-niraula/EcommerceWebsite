function updateStatus() {
    var newStatus = document.getElementById("status").value; // Get the selected status

    // Send AJAX request to update status
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/OrderDetail/UpdateStatus", true);
    xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            // Optionally, you can handle the response here
            console.log("Status updated successfully");
        }
    };
    xhr.send(JSON.stringify({ newStatus: newStatus }));
}