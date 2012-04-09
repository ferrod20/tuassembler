cd Extraccion
..\..\tesis -extraer "Cobuild.original.legible" "Cobuild.extracted" "Cobuild.extracted.info"
..\..\tesis -2pasada "Cobuild.extracted" "Cobuild.tagged" "Cobuild.2pasada"
..\..\tesis -unir "Cobuild.2pasada" "Cobuild.tagged" "Cobuild.final"
..\..\tesis -comparar "Cobuild.tagged" "Cobuild.extracted" "CobuildExtracted.mconf"
..\..\tesis -comparar "Cobuild.tagged" "Cobuild.2pasada" "Cobuild2Pasada.mconf"