//"use strict";
document.getElementById("sendButton").disabled = true;
var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();
var currScript = document.currentScript;
var path;
var isResetClicked = false;



document.getElementById("messagesList").innerHTML = sessionStorage.getItem("messages");

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveMessage", function (user, message) {
   
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.innerHTML = `User ${user} :  ${message} `;


    var messageList = document.getElementById("messagesList").innerHTML;
    sessionStorage.setItem("messages", messageList);
});
connection.on("ReceiveMessageAndPath", function (user, message, path) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.innerHTML = `User ${user} :  ${message} ` + `<img src="${path}" />`;


    var messageList = document.getElementById("messagesList").innerHTML;
    sessionStorage.setItem("messages", messageList);

});

//Message
document.getElementById("sendButton").addEventListener("click", function (event) {
    let user = document.getElementById("userInput").value;
    let message = document.getElementById("messageInput").value;

    if (currScript != null) {
        console.log(isResetClicked)
        if (isResetClicked) {
            path = null;
        }
        else path = document.getElementById("photopath").value;
    }
    if (path != null) {
        path = path.slice(1);
        connection.invoke("SendMessageAndPath", user, message, path).catch(function (err) {
            return console.error(err.toString());
        });
    }
    else {
        connection.invoke("SendMessage", user, message).catch(function (err) {
            return console.error(err.toString());
        });
    }


    event.preventDefault();
});
function ResetPath() {
    document.getElementById("photopath").value = "This is reset";
    isResetClicked = true;
}

function upload() {
    isResetClicked = false;

   

    //const data = new FormData(event.target);
    //#TODO propably to remove
    //const value = data.get("login");
    //const value2 = data.get("password");

    //const response = await fetch("https://localhost:44356/GetPhoto", {
    //    headers: {
    //        "Content-Type": "application/json",
    //        Accept: "application/json",
    //    },
    //    method: "Post",
    //    // mode: "cors",
    //    // credentials: "same-origin",
    //    credentials: "include",
    //    referrerPolicy: "origin",
    //    body: JSON.stringify(user),
    //});
}

async function GetPath() {

    isResetClicked = false;
    var response = await fetch("https://localhost:44356/getpath");
    var path = await response.text();
    console.log(path);
    var photopath = document.getElementById("photopath");
    document.getElementById("photopath").value = path;
    photopath.outerHTML = '<input type="text" name="photopath" id="photopath" value=' + path + ' disabled />';
}