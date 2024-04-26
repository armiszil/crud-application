let books = [];
let connection = null;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:26320/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("BookCreated", (user, message) => {
        getdata();
    });

    connection.on("BookDeleted", (user, message) => {
        getdata();
    });
    connection.on("BookUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}
async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};
async function getdata() {
    await fetch('http://localhost:26320/books')
        .then(x => x.json())
        .then(y => {
            books = y;
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    books.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" + t.grading + "</td><td>" + t.price + " $</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            + "</td></tr>";

    });
    document.getElementById('bookname').value = "";
    document.getElementById('booklength').value = "";
    document.getElementById('bookauthor').value = "";
}

function remove(id) {
    fetch('http://localhost:26320/books/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function create() {
    let Bookname = document.getElementById('bookname').value;
    let BookLength = document.getElementById('booklength').value;
    let BookAuthor = document.getElementById('bookauthor').value;

    fetch('http://localhost:26320/books', {
        method: 'POST',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { Name: Bookname, Length: BookLength, Author: BookAuthor })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
