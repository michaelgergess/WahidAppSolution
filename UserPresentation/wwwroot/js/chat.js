    var hubCon = new signalR.HubConnectionBuilder().withUrl("/Chat").build();
    hubCon.start().then(function () {
        console.log("Connected to Chat Hub");
        }).catch(function (err) {
            return console.error(err.toString());
        });

    hubCon.on("ReceiveNewMessage", function (m, n) {
            var listItem = `<li class="message"><span class="sender">${n}:</span> ${m}</li>`;
    $("#Conv").append(listItem);
    // Scroll to bottom to show latest message
    $("#Conv").scrollTop($("#Conv")[0].scrollHeight);
        });

    // Load chat messages when page loads
    $(document).ready(function () {
        LoadChatMessages();
        });

function LoadChatMessages() {
    $.ajax({
        type: "GET",
        url: "/Account/GetAllMessages",
        success: function (data) {
            console.log("Received data:", data);

            $("#Conv").empty(); // Clear existing messages

            if (Array.isArray(data)) {
                data.forEach(function (message) {
                    var listItem = `<li class="message"><span class="sender">${message.userName}:</span> ${message.message}</li>`;
                    $("#Conv").append(listItem);
                });
            } else {
                // Assuming single ChatDTO object
                var listItem = `<li class="message"><span class="sender">${data.userName}:</span> ${data.message}</li>`;
                $("#Conv").append(listItem);
            }

            // Scroll to bottom after loading messages
            $("#Conv").scrollTop($("#Conv")[0].scrollHeight);
        },
        error: function (xhr, status, error) {
            console.error("Error loading messages:", error);
            // Handle error here, if needed
        }
    });
}


   
