let reviews = [];
let connection = null;
getdata();
setupSignalR();
let reviewDescToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:26320/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ReviewCreated", (user, message) => {
        getdata();
    });

    connection.on("ReviewDeleted", (user, message) => {
        getdata();
    });
    connection.on("ReviewUpdated", (user, message) => {
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
    await fetch('http://localhost:26320/reviews')
        .then(x => x.json())
        .then(y => {
            reviews = y;
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    reviews.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" + t.city + "</td><td>" + t.email + "</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update City</button>`
            + "</td></tr>";
    });
    document.getElementById('studentname').value = "";
    document.getElementById('studentcity').value = "";
    document.getElementById('studentemail').value = "";
}
function showupdate(id) {
    document.getElementById('studentcityToUpdate').value = reviews.find(t => t['id'] == id)['city'];
    document.getElementById('updateformdiv').style.display = 'flex';
    studentIdToUpdate = id;
}
function remove(id) {
    fetch('http://localhost:26320/reviews/' + id, {
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
    let Reviewname = document.getElementById('reviewname').value;
    let ReviewDesc = document.getElementById('reviewdesc').value;
    let ReviewRate = document.getElementById('reviewrating').value;

    fetch('http://localhost:26320/reviews', {
        method: 'POST',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { Name: Reviewname, Desc: ReviewDesc, Rating: ReviewRate })
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
    let ReviewDescToUpdate = document.getElementById('reviewDescToUpdate').value;
    let Reviewrating = reviews.find(t => t['id'] == studentIdToUpdate)['rating'];
    let Reviewname = reviews.find(t => t['id'] == studentIdToUpdate)['name'];
    fetch('http://localhost:26320/reviews', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { Name: Reviewname, Rating: Reviewrating, ReviewDesc: reviewDescToUpdate, Id: studentIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
