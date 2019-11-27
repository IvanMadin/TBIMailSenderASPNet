"use strict";

let connection = new signalR.HubConnectionBuilder().withUrl("/test").build();


connection.on("Locked", function(id) {
    console.log("Button changed to Locked");
    console.log('Locked');
    let btn = document.getElementById(id);
    btn.setAttribute("disabled", true);
})

connection.on("Unlocked", function (id) {
    console.log("Button changed to Unlocked");
    let btn = document.getElementById(id);
    btn.removeAttribute("disabled");
})


connection.start().then(function () {
    console.log("ConnectionStarted");
}).catch(function(err) {
    return console.error(err.toString());
})