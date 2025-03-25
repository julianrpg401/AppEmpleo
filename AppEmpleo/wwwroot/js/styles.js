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

document.addEventListener("DOMContentLoaded", function () {
  const formOffer = document.getElementById("form");
  if (formOffer) {
    const plusIcon = document.querySelector(".form__plus--icon");
    plusIcon.addEventListener("click", function () {
      formOffer.classList.toggle("active");
    });
  }
});