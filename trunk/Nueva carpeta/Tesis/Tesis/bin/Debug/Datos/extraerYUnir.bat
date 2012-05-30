cd Extraccion
..\..\tesis -extraer "Cobuild.original.legible" "Cobuild.extracted" "Cobuild.extracted.info"
..\..\tesis -2pasada "Cobuild.extracted" "Cobuild.tagged" "Cobuild.2pasada"
..\..\tesis -unir "Cobuild.2pasada" "Cobuild.tagged" "Cobuild.final"
..\..\tesis -comparar "Cobuild.extracted" "Cobuild.tagged"  "CobuildExtracted.mconf" -p "Matriz de confusion entre Cobuild extraido 1pasada contra Cobuild etiquetado con TnT (entrenado con WSJ)" "Cobuild 1pasada" "TnT (entrenado con WSJ)"
..\..\tesis -comparar "Cobuild.2pasada"   "Cobuild.tagged"  "Cobuild2Pasada.mconf" -p "Matriz de confusion entre Cobuild extraido 2pasada contra Cobuild etiquetado con TnT (entrenado con WSJ)" "Cobuild 2pasada" "TnT (entrenado con WSJ)"