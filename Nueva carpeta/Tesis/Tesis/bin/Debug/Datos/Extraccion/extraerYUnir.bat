cd Extraccion
..\..\tesis -extraer "Cobuild.original.legible" "Cobuild.extracted" "Cobuild.extracted.info"
..\..\tesis -2pasada "Cobuild.extracted" "Cobuild.tt" "Cobuild.2pasada"
..\..\tesis -unir "Cobuild.2pasada" "Cobuild.tt" "Cobuild.final"
..\..\tesis -comparar "Cobuild.tt" "Cobuild.extracted" "CobuildExtracted.mc" -p "Matriz de confusion entre Cobuild etiquetado con TnT (entrenado con WSJ) contra Cobuild extraido 1pasada" "TnT (entrenado con WSJ)" "Cobuild 1pasada" 
..\..\tesis -comparar "Cobuild.tt" "Cobuild.2pasada"   "Cobuild2Pasada.mc" -p "Matriz de confusion entre Cobuild etiquetado con TnT (entrenado con WSJ) contra Cobuild extraido 2pasada" "TnT (entrenado con WSJ)" "Cobuild 2pasada" 