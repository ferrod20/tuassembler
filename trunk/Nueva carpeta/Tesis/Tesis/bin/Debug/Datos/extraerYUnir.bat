cd Extraccion
..\..\tesis -extraer "Cobuild.original.legible" "Cobuild.extracted" "Cobuild.extracted.info"
..\..\tesis -2pasada "Cobuild.extracted" "Cobuild.tagged" "Cobuild.2pasada"
..\..\tesis -unir "Cobuild.2pasada" "Cobuild.tagged" "Cobuild.final"
..\..\tesis -comparar "Cobuild.tagged" "Cobuild.extracted" "CobuildExtracted.mconf" -p "Matriz de confusion entre Cobuild etiquetado con TnT (entrenado con WSJ) contra Cobuild extraido 1pasada" "TnT (entrenado con WSJ)" "Cobuild 1pasada" 
..\..\tesis -comparar "Cobuild.tagged" "Cobuild.2pasada"   "Cobuild2Pasada.mconf" -p "Matriz de confusion entre Cobuild etiquetado con TnT (entrenado con WSJ) contra Cobuild extraido 2pasada" "TnT (entrenado con WSJ)" "Cobuild 2pasada" 