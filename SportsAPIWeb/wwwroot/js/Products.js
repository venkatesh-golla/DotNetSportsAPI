var productsUrl = "https://localhost:44369/Products"

var listOfProducts = document.getElementById("products-list")
if (listOfProducts) {
    fetch(productsUrl)
        .then(response => response.json())
        .then(data => showProductsList(data))
        .catch(ex => {
            alert("something went wrong")
            console.log(ex)
        })
}

function showProductsList(products) {
    products.forEach(product => {
        var li = document.createElement("li")
        var text = `${product.name} ($${product.price})`
        li.appendChild(document.createTextNode(text))
        listOfProducts.appendChild(li)
    })
}