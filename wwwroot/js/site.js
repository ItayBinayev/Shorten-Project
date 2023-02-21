// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const options = {
    method: 'GET',
    headers: {
        'X-RapidAPI-Key': '689c4fe7f1msh9486a2c7271cbb0p17e3fbjsn4416960ba2f1',
        'X-RapidAPI-Host': 'coingecko.p.rapidapi.com'
        }

}

fetch('https://coingecko.p.rapidapi.com/simple/price?vs_currencies=usd&ids=bitcoin&iclude_24hr_change=true', options)
    .then(response => response.json())
    .then(response => {
        let btcpr = document.getElementById("btcprice")
        let price = 0
        let change24 = 0
        price = response.bitcoin.usd;
        change24 = response.bitcoin.usd_24g_chnage;
        btcpr.innerHTML = price + '$ ';
        let i = document.createElement("i")
        btcpr.appendChild(i)
        if (change24 >= 0) {
            btcpr.classList.add("text-success")
            i.classList.remove("fa-arrow-down")
            i.classList.add("fa-solid","fa-arrow-up")
        }
        else {
            btcpr.classList.add("text-danger")
            i.classList.remove("fa-arrow-up")
            i.classList.add("fa-solid", "fa-arrow-down")
        }

    }
)
