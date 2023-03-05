function deleteItem(cart) {
    const cartObject = localStorage.getItem(cart);
    fetch('/Cart/Delete', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: cartObject,
    })
        .then(response => fetchCart())
        .then(response => console.log(JSON.stringify(response)))
}


async function fetchCart() {
    var ele = $("#cart > ul");
    var data = await fetch("/Cart/InCart");
    var jsonData = await data.json();
    var data = [`<a style="padding-left: 3rem;" href="/Cart">Go to cart</a>`];
    if (jsonData && jsonData.length > 0) {
        jsonData.map((cart) => {
            localStorage.setItem(cart.id, JSON.stringify(cart));
            data.push(` 
            <li id="cart-list-item">
                <img src="${cart.product.imgPath.replace("~/", "")}" alt="${cart.product.imgPath}" height="100%" width="auto">
                <div id="cart-item-label">
                    <a href="javascript:void(0)"><strong>${cart.product.name}</strong></a>
                    <br/>
                    <p>Quantity: ${cart.quantity} - Total: $${cart.product.price * cart.quantity}</p>
                    <div class="homeqt">
                        <button type="button" onclick="qty.minus('30')" class="form-control pull-left btn-number btnminus dis-30" disabled="disabled">
                            <span class="glyphicon glyphicon-minus"></span>
                        </button>
                        <input name="quantity" class="addhqty" type="text" value="1" size="2" min="1" max="999" id="input-quantity-30" class="form-control input-number pull-left" />
                        <button type="button" onclick="qty.plus('30')" class="form-control pull-left btn-number btnplus">
                            <span class="glyphicon glyphicon-plus"></span>
                        </button>
                    </div>
                </div>
                <a id="delete-cart-item" href="/Cart/Delete/${cart.id.toString()}"><ion-icon name="trash-outline"></ion-icon></a>
            </li>
        `)
        });
    } else {
        data.push(`
            <li>
                <p class="text-center">Your shopping cart is empty!</p>
            </li>
            `);
    }
    ele.html(data.join(""));
}