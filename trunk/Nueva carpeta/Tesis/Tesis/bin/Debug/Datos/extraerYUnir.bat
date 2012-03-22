cd Extraccion
..\..\tesis -extraer "Cobuild.original.legible" "Cobuild.extracted" "Cobuild.extracted.info"
..\..\tesis -unir "Cobuild.extracted" "Cobuild.tagged" "Cobuild.final"
..\..\tesis -comparar "Cobuild.tagged" "Cobuild.extracted" "Cobuild.mconf"