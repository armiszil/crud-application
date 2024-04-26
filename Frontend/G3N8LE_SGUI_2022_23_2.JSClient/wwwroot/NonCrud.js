let shortestbook = [];
let longestbook = [];
let deciding = null;



async function ShortestBook() {
    document.getElementById('resultarea').innerHTML = "";
    document.getElementById('headresult').innerHTML = "";
    await fetch('http://localhost:26320/Noncrud/ShortestBook')
        .then(x => x.json())
        .then(y => {
            bestfan = y;
            deciding = "SB";
            display(deciding);
        });
}
async function LongestBook() {
    document.getElementById('resultarea').innerHTML = "";
    document.getElementById('headresult').innerHTML = "";
    await fetch('http://localhost:60617/Noncrud/LongestBook')
        .then(x => x.json())
        .then(y => {
            worstfan = y;
            deciding = "LB";
            display(deciding);
        });
}

function display() {
    if (deciding === "SB") {
        document.getElementById('headresult').innerHTML += "<tr><th>Book Id</th><th>Number of pages</th></tr> ";
        document.getElementById('resultarea').innerHTML = "";
        shortestbook.forEach(t => {
            document.getElementById('resultarea').innerHTML += "<tr><td>" + t.key + "</td><td>" + t.value + "</td></tr>";
        });
    }
    else if (deciding === "LB") {
        document.getElementById('headresult').innerHTML += "<tr><th>Book Id</th><th>Number of pages</th></tr> ";
        document.getElementById('resultarea').innerHTML = "";
        shortestbook.forEach(t => {
            document.getElementById('resultarea').innerHTML += "<tr><td>" + t.key + "</td><td>" + t.value + "</td></tr>";
        });


    }
}