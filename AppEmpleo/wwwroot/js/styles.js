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

function openOverlay(offer) {
  document.getElementById('detalleNombre').innerText = offer.nombre;
  document.getElementById('detalleDescripcion').innerText = offer.descripcion;
  document.getElementById('detalleFechaInicio').innerText = offer.fechainicio;
  document.getElementById('detalleFechaCierre').innerText = offer.fechacierre;
  document.getElementById('detalleModalidad').innerText = offer.modalidad;
  document.getElementById('detallePais').innerText = offer.pais;
  document.getElementById('detalleMoneda').innerText = offer.moneda;
  document.getElementById('detalleSalario').innerText = offer.salario;
  document.getElementById('detailsOverlay').classList.add('active');
}

function closeOverlay() {
  document.getElementById('detailsOverlay').classList.remove('active');
}

document.addEventListener('DOMContentLoaded', function () {
  document.querySelectorAll('.button__details').forEach(function (btn) {
    btn.addEventListener('click', function () {
      openOverlay({
        nombre: this.dataset.nombre,
        descripcion: this.dataset.descripcion,
        fechainicio: this.dataset.fechainicio,
        fechacierre: this.dataset.fechacierre,
        modalidad: this.dataset.modalidad,
        pais: this.dataset.pais,
        moneda: this.dataset.moneda,
        salario: this.dataset.salario,
        categoria: this.dataset.categoria
      });
    });
  });
});