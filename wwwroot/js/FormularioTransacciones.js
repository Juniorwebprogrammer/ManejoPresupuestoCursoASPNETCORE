function initFormularioTransacciones(urlObtenerCategorias) {
    $("#TipoOperacionId").change(async function () {
        const valorSeleccionado = parseInt($(this).val());

        try {
            const respuesta = await fetch(urlObtenerCategorias, {
                method: 'POST',
                body: JSON.stringify(valorSeleccionado),
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (!respuesta.ok) {
                console.error("Error en la respuesta del servidor");
                return;
            }

            const json = await respuesta.json();

            const opciones = json.map(c => {
                const id = c.value || c.Value;
                const texto = c.text || c.Text;
                return `<option value="${id}">${texto}</option>`;
            });

            $("#CategoriaId").html(opciones.join(''));
        } catch (error) {
            console.error("Error en la petición de red:", error);
        }
    });

    $("#TipoOperacionId").trigger('change');
}