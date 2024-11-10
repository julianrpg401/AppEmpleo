document.querySelectorAll("input").forEach(input => {
    // Agrega la clase "activo" cuando el input recibe el foco
    input.addEventListener("focus", () => {
        input.classList.add("register__input--active");
    });

    // Quita la clase "activo" cuando el input pierde el foco
    input.addEventListener("blur", () => {
        input.classList.remove("register__input--active");
    });
});