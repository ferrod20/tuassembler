..\tesis -comparar "a.txt" "b.txt" "pr.latexMConf" -l "Matriz de confusioooon para etiquetas " "A" "B"
..\tesis -comparar "Entrenamiento\NFI.txt" "Extraccion\Cobuild.tagged" "Mediciones\NFIcontraCobuildTaggedConTnT.latexMConf" -l "Matriz de confusión para etiquetas extraídas de COBUILD vs TnT" "COBUILD" "TnT"
..\tesis -comparar "Entrenamiento\NFI.txt" "Extraccion\Cobuild.tagged" "Mediciones\NFIcontraCobuildTaggedConTnT.mConf" -p "Matriz de confusión para etiquetas extraídas de COBUILD vs TnT" "COBUILD" "TnT (entrenado con WSJ)"
..\tesis -comparar "Extraccion\Cobuild.extracted" "Extraccion\Cobuild.tagged"  "Mediciones\CobuildExtracted.mconf" -p "Matriz de confusion entre Cobuild extraido 1pasada contra Cobuild etiquetado con TnT (entrenado con WSJ)" "Cobuild 1pasada" "TnT (entrenado con WSJ)"

