let authors = [];
let connection = null;
getdata();
setupSignalR();
let authorToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:26320/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("AuthorCreated", (user, message) => {
        getdata();
    });

    connection.on("AuthorDeleted", (user, message) => {
        getdata();
    });
    connection.on("AuthorUpdated", (user, message) => {
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
    await fetch('http://localhost:26320/authors')
        .then(x => x.json())
        .then(y => {
            authors = y;
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    authors.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" + t.branch + "</td><td>" + t.price + " $</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update Age</button>`
            + "</td></tr>";
    });
    document.getElementById('authorname').value = "";
    document.getElementById('authorage').value = "";
   
}
function showupdate(id) {
    document.getElementById('authorAgeToUpdate').value = authors.find(t => t['id'] == id)['price'];
    document.getElementById('updateformdiv').style.display = 'flex';
    authorIdToUpdate = id;


}
function remove(id) {
    fetch('http://localhost:26320/authors/' + id, {
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
    let Authorname = document.getElementById('authorname').value;
    let Authorbranch = document.getElementById('authorage').value;

    fetch('http://localhost:26320/authors', {
        method: 'POST',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { Branch: Authorbranch, Name: Authorname, Price: Authorcost })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}
function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let AuthorAgeToUpd = document.getElementById('authorAgeToUpdate').value;
    let AuthorAge = authors.find(t => t['id'] == authorIdToUpdate)['branch'];
    let Authorname = authors.find(t => t['id'] == authorIdToUpdate)['name'];
    fetch('http://localhost:26320/authors', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { Name: Authorname, Age: AuthorAge, Id: AuthorAgeToUpd })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}