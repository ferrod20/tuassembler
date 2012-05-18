var RecursoPlantilla  = {
    FilaPlantilla: jQuery.template("recurso_fila_plantilla",
"<div class='recurso-fila' id='fila_${id}'>                 \
    <div class='nombre-contenedor'>                                                                                               \
        <div id='nombre'>${nombre}</div>                                                                              \
    </div>                                                                                                                \
    <div class='iconos-contenedor'>                                                                                             \
    <span class='eliminar' title='Eliminar'></span>          \
    <span class='editar' title='Editar'></span>                                                                           \
    <span class='tilde' title='Recurso activo'></span>    \
    <div class='estado-habilitado'></div>                      \
    </div>                                                                                                                  \
</div>")
};
