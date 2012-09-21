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
</div>"),
    
    PlantillaGeneral: jQuery.template("recurso_plantilla",
'<div id="recursos-desplazable">																										\
    <div id="recursos-desplazable-items">                                                                                               \
        <div id="recursos-pantalla1" class="slider-panel">                                                                              \
            <div id="recursos-principal-crear-btn">                                                                                     \
                <input id="crear-recurso" class="boton" name="Crear un nuevo recurso" value="Crear un nuevo recurso" />                 \
            </div>                                                                                                                      \
            <input id="recursos-busqueda" type="text" placeholder="Buscar" />                                                           \
            <div class="clear"></div>                                                                                                   \
            <label id="recursos-por-nombre" title="Ordenar por nombre">Recuros actuales</label>                                         \
            <div id="recursos-lista" class="contenedor-2"></div>                                                                        \
            <div class="informacion">Con su actual suscripción puede agregar hasta 6 recursos más.</div>                                \
        </div>                                                                                                                          \
        <div id="recursos-pantalla2" class="slider-panel">                                                                              \
            <div id="recursos-confirmar-borrado" class="hidden">                                                                        \
                <h2>Confirmar elminación</h2>                                                                                           \
                <p>                                                                                                                     \
                    Eliminando este recurso se removerá toda la información relativa persistida<br />                                   \
                    Continúa con la elminación de todos modos?<br />                                                                    \
                </p>                                                                                                                    \
                <div>                                                                                                                   \
                    <input id="eliminacion-del-recurso-confirmada" class="boton" type="button" value="Si" />                            \
                    <input id="elminacion-del-recurso-cancelada" class="boton cancelar" type="button" value="Cancelar" />               \
                </div>                                                                                                                  \
            </div>                                                                                                                      \
            <div id="recursos-contenedor">                                                                                              \
                    <div id="recurso-datos-tab" class="ui-tabs-panel"> \
                        <input type="hidden" id="recurso-id" value="0" /> \
                        <div class="content_user_left">                                                                                     \
                            <label>Nombre</label>                                                                                           \
                            <div>                                                                                                           \
                                <input id="recurso-nombre" type="text" />                                                                   \
                            </div>                                                                                                          \
                        </div>                                                                                                              \
                        <div class="content_user_left">                                                                                     \
                            <label>Especialidad</label>                                                                                           \
                            <div>                                                                                                           \
                                <input id="recurso-especialidad" type="text" />                                                                   \
                            </div>                                                                                                          \
                        </div>                                                                                                              \
                        <div class="content_user_left">                                                                                     \
                            <label>Email</label>                                                                                           \
                            <div>                                                                                                           \
                                <input id="recurso-email" type="text" />                                                                   \
                            </div>                                                                                                          \
                        </div> \
                        <div class="content_user_left">                                                                                     \
                            <label>Foto</label>                                                                                           \
                            <div>                                                                                                           \
                                <input id="recurso-foto" type="text" />                                                                   \
                            </div>                                                                                                          \
                        </div> \
                    </div> \
                    <div id="recurso-disponibilidad-tab" class="ui-tabs-panel"> \
                        <div class="content_user_left">                                                                                         \
                            <table id="disponibilidad"> \
                                <thead> \
                                    <tr> \
                                        <th>Lunes</th> \
                                        <th>Martes</th> \
                                        <th>Miércoles</th> \
                                        <th>Jueves</th> \
                                        <th>Viernes</th> \
                                        <th>Sábado</th> \
                                        <th>Domingo</th> \
                                    </tr> \
                                </thead> \
                                <tbody> \
                                    <tr> \
                                        <td><input type="text" value="10-10.30"></td> \
                                        <td><input type="text" value="10-10.30"></td> \
                                        <td><input type="text" value="10-10.30"></td> \
                                        <td><input type="text" value="10-10.30"></td> \
                                        <td><input type="text" value="10-10.30"></td> \
                                        <td><input type="text" value="10-10.30"></td> \
                                        <td><input type="text" value="10-10.30"></td> \
                                    </tr> \
                                    <tr> \
                                        <td><input type="text" value="11-11.30"></td> \
                                        <td><input type="text" value="11-11.30"></td> \
                                        <td><input type="text" value="11-11.30"></td> \
                                        <td><input type="text" value="11-11.30"></td> \
                                        <td><input type="text" value="11-11.30"></td> \
                                        <td><input type="text" value="11-11.30"></td> \
                                        <td><input type="text" value="11-11.30"></td> \
                                    </tr> \
                                    <tr> \
                                        <td><input type="text" value=""></td> \
                                        <td><input type="text" value=""></td> \
                                        <td><input type="text" value=""></td> \
                                        <td><input type="text" value=""></td> \
                                        <td><input type="text" value=""></td> \
                                        <td><input type="text" value=""></td> \
                                        <td><input type="text" value=""></td> \
                                    </tr> \
                                </tbody> \
                            </table>\
                        </div>                                                                                                                  \
                    </div> \
                    <div id="recurso-excepciones-tab" class="ui-tabs-panel"> \
                    <label>Fecha:</label> <input type="date" id="recurso-excepcion-fecha">\
    <label>Desde: </label><input type="time" id="recurso-excepcion-hora-desde">\
        <label>Hasta:</label><input type="time" id="recurso-excepcion-hora-hasta">\
    <input type="checkbox" id="recurso-excepcion-todo-el-dia">Todo el día</input>\
    </div> \
                <div class="clear">                                                                                                     \
                </div>                                                                                                                  \
                <br />                                                                                                                  \
                <br />                                                                                                                  \
                <br />                                                                                                                  \
                <div>                                                                                                                   \
                    <input id="recursos-formulario-grabar" class="boton" type="button" value="Grabar" />                        \
                    <input id="recursos-formulario-cancelar" class="boton cancelar" type="button" value="Cancelar" />                   \
                </div>                                                                                                                  \
                <div class="invalid-error-text">                                                                                    \
                    <span id="error"></span>                                                                                    \
                </div>                                                                                                                  \
            </div>                                                                                                                      \
        </div>                                                                                                                          \
    </div>                                                                                                                              \
</div>')
};



