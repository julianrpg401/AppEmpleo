document.querySelectorAll("input").forEach(input => {
    // Agrega la clase "activo" cuando el input recibe el foco
    input.addEventListener("focus", () => {
        input.classList.add("form__input--active");
    });

    // Quita la clase "activo" cuando el input pierde el foco
    input.addEventListener("blur", () => {
        input.classList.remove("form__input--active");
    });
});