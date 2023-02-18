let txt = "https://localhost:7235/api/shorten?fullurl="
let shortbox = document.getElementById("shorturlbox")
let btn = document.getElementById("btnGenerate")
btn.addEventListener("click", generate)
let btnClear = document.getElementById("btnClear")
btnClear.addEventListener("click", function (event) {
    event.preventDefault()
    shortbox.value = null
    document.getElementById("fullurlbox").value = null

})
let btnCopy = document.getElementById("btnCopy")
btnCopy.addEventListener("click", Copy)


async function generate()
{
    
    let fullboxtxt = document.getElementById("fullurlbox").value
    if (fullboxtxt) {

        let resStr = txt + fullboxtxt
        let res = await fetch(resStr, {
            method: "POST",
            headers: { "Content-type": "application/text;" }
        })
            .then(response => response.text())
            .then(data => shortbox.value = data);
    }
    else {
        alert("Invalid Input!")
    }
}

function Copy() {
    if (shortbox.value)
    navigator.clipboard.writeText(shortbox.value)
}




