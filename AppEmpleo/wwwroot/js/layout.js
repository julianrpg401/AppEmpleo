const body = document.querySelector("body"),
    cabeza = body.querySelector(".cabeza"),
    toggle = body.querySelector(".toggle"),
    buscador = body.querySelector(".menu__buscador"),
    modoSwitch = body.querySelector(".modo__switch"),
    modoTexto = body.querySelector(".modo__texto");

toggle.addEventListener("click", () => {
    cabeza.classList.toggle("close");
});

modoSwitch.addEventListener("click", () => {
    body.classList.toggle("dark");
});