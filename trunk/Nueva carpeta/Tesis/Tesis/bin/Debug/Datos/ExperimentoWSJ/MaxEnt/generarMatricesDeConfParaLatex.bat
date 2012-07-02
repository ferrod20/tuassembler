..\..\..\tesis -comparar "..\FuentesDeEntrenamiento\NFI.txt" "..\..\Extraccion\Cobuild.tagged" "Mediciones\NFI_VS_CobuildTnTwsj.latexMConf" -l "Matriz de confusión para etiquetas extraídas de COBUILD vs TnT" "COBUILD" "TnT"
..\..\..\tesis -comparar "..\FuentesDeEntrenamiento\NFI.txt" "..\..\Extraccion\Cobuild.tagged" "Mediciones\NFI_VS_CobuildTnTwsj.mConf" -p "Matriz de confusión para etiquetas extraídas de COBUILD vs TnT" "COBUILD" "TnT (entrenado con WSJ)"


..\..\..\tesis -comparar "..\WSJ\wsj.gold" "Taggeado\wsj.tagged" "Mediciones\wsj_VS_wsjTnTWSJ.latexMConf" -l "WSJ original contra WSJ etiquetado con TnT (entrenado con WSJ)" "WSJ" "TnT(WSJ)"
..\..\..\tesis -comparar "..\WSJ\wsj.gold" "Taggeado\wsj+NFI.tagged" "Mediciones\wsj_VS_wsjTnTWSJ+NFI.latexMConf" -l "WSJ original contra WSJ etiquetado con TnT (entrenado con WSJ + NFI)" "WSJ" "TnT(WSJ+NFI)"
..\..\..\tesis -comparar "Taggeado\wsj.tagged" "Taggeado\wsj+NFI.tagged" "Mediciones\wsjTnTwsj_VS_wsjTnTwsj+NFI.latexMConf" -l "WSJ etiquetado con TnT (entrenado con WSJ) vs WSJ etiquetado con TnT (entrenado con WSJ + NFI)" "TnT(WSJ)" "TnT(WSJ+NFI)"

..\..\..\tesis -comparar "..\WSJ\wsjM1.gold" "Taggeado\wsjM1.tagged" "Mediciones\wsjM1_VS_TnTwsjM2.latexMConf" -l "1 mitad WSJ original contra 1 mitad WSJ etiquetado con TnT (entrenado con 2 mitad de WSJ)" "WSJ" "TnT(2WSJ)"
..\..\..\tesis -comparar "..\WSJ\wsjM2.gold" "Taggeado\wsjM2.tagged" "Mediciones\wsjM2_VS_TnTwsjM1.latexMConf" -l "2 mitad WSJ original contra 2 mitad WSJ etiquetado con TnT (entrenado con 1 mitad de WSJ)" "WSJ" "TnT(1WSJ)"

..\..\..\tesis -comparar "..\WSJ\wsjM1.gold" "Taggeado\wsjM1+NFI.tagged" "Mediciones\wsjM1_VS_TnTwsjM2+NFI.latexMConf" -l "1 mitad WSJ original contra 1 mitad WSJ etiquetado con TnT (entrenado con 2 mitad de WSJ + NFI)" "WSJ" "TnT(2WSJ+NFI)"
..\..\..\tesis -comparar "..\WSJ\wsjM2.gold" "Taggeado\wsjM2+NFI.tagged" "Mediciones\wsjM2_VS_TnTwsjM1+NFI.latexMConf" -l "2 mitad WSJ original contra 2 mitad WSJ etiquetado con TnT (entrenado con 1 mitad de WSJ)" "WSJ" "TnT(1WSJ+NFI)"

..\..\..\tesis -comparar "Taggeado\wsjM1.tagged" "Taggeado\wsjM1+NFI.tagged" "Mediciones\wsjM1__TnTwsjM2_vs_TnTwsjM2+NFI.latexMConf" -l "1 mitad WSJ etiquetado por TnT (entrenado con 2 mitad WSJ) vs 1 mitad WSJ etiquetado con TnT (entrenado con 2 mitad de WSJ + NFI)" "TnT(2WSJ)" "TnT(2WSJ+NFI)"
..\..\..\tesis -comparar "Taggeado\wsjM2.tagged" "Taggeado\wsjM2+NFI.tagged" "Mediciones\wsjM2__TnTwsjM1_vs_TnTwsjM1+NFI.latexMConf" -l "2 mitad WSJ etiquetado por TnT (entrenado con 1 mitad WSJ) vs 2 mitad WSJ etiquetado con TnT (entrenado con 1 mitad de WSJ + NFI)" "TnT(1WSJ)" "TnT(1WSJ+NFI)"




..\..\..\tesis -comparar "..\WSJ\wsj.gold" "Taggeado\wsj.tagged" "Mediciones\wsj_VS_wsjTnTWSJ.mConf" -p "WSJ original contra WSJ etiquetado con TnT (entrenado con WSJ)" "WSJ" "TnT(WSJ)"
..\..\..\tesis -comparar "..\WSJ\wsj.gold" "Taggeado\wsj+NFI.tagged" "Mediciones\wsj_VS_wsjTnTWSJ+NFI.mConf" -p "WSJ original contra WSJ etiquetado con TnT (entrenado con WSJ + NFI)" "WSJ" "TnT(WSJ+NFI)"
..\..\..\tesis -comparar "Taggeado\wsj.tagged" "Taggeado\wsj+NFI.tagged" "Mediciones\wsjTnTwsj_VS_wsjTnTwsj+NFI.mConf" -p "WSJ etiquetado con TnT (entrenado con WSJ) vs WSJ etiquetado con TnT (entrenado con WSJ + NFI)" "TnT(WSJ)" "TnT(WSJ+NFI)"

..\..\..\tesis -comparar "..\WSJ\wsjM1.gold" "Taggeado\wsjM1.tagged" "Mediciones\wsjM1_VS_TnTwsjM2.mConf" -p "1 mitad WSJ original contra 1 mitad WSJ etiquetado con TnT (entrenado con 2 mitad de WSJ)" "WSJ" "TnT(2WSJ)"
..\..\..\tesis -comparar "..\WSJ\wsjM2.gold" "Taggeado\wsjM2.tagged" "Mediciones\wsjM2_VS_TnTwsjM1.mConf" -p "2 mitad WSJ original contra 2 mitad WSJ etiquetado con TnT (entrenado con 1 mitad de WSJ)" "WSJ" "TnT(1WSJ)"

..\..\..\tesis -comparar "..\WSJ\wsjM1.gold" "Taggeado\wsjM1+NFI.tagged" "Mediciones\wsjM1_VS_TnTwsjM2+NFI.mConf" -p "1 mitad WSJ original contra 1 mitad WSJ etiquetado con TnT (entrenado con 2 mitad de WSJ + NFI)" "WSJ" "TnT(2WSJ+NFI)"
..\..\..\tesis -comparar "..\WSJ\wsjM2.gold" "Taggeado\wsjM2+NFI.tagged" "Mediciones\wsjM2_VS_TnTwsjM1+NFI.mConf" -p "2 mitad WSJ original contra 2 mitad WSJ etiquetado con TnT (entrenado con 1 mitad de WSJ)" "WSJ" "TnT(1WSJ+NFI)"

..\..\..\tesis -comparar "Taggeado\wsjM1.tagged" "Taggeado\wsjM1+NFI.tagged" "Mediciones\wsjM1__TnTwsjM2_vs_TnTwsjM2+NFI.mConf" -p "1 mitad WSJ etiquetado por TnT (entrenado con 2 mitad WSJ) vs 1 mitad WSJ etiquetado con TnT (entrenado con 2 mitad de WSJ + NFI)" "TnT(2WSJ)" "TnT(2WSJ+NFI)"
..\..\..\tesis -comparar "Taggeado\wsjM2.tagged" "Taggeado\wsjM2+NFI.tagged" "Mediciones\wsjM2__TnTwsjM1_vs_TnTwsjM1+NFI.mConf" -p "2 mitad WSJ etiquetado por TnT (entrenado con 1 mitad WSJ) vs 2 mitad WSJ etiquetado con TnT (entrenado con 1 mitad de WSJ + NFI)" "TnT(1WSJ)" "TnT(1WSJ+NFI)"

