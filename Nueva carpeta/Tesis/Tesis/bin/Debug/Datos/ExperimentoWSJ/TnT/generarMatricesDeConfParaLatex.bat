..\..\..\tesis -comparar "..\FuentesDeEntrenamiento\NFI.tt" "..\..\Extraccion\Cobuild.tt" "Mediciones\NFI_VS_CobuildTnTwsj.l" -l "Matriz de confusión para etiquetas extraídas de COBUILD vs TnT" "COBUILD" "TnT"
..\..\..\tesis -comparar "..\FuentesDeEntrenamiento\NFI.tt" "..\..\Extraccion\Cobuild.tt" "Mediciones\NFI_VS_CobuildTnTwsj.mc" -p "Matriz de confusión para etiquetas extraídas de COBUILD vs TnT" "COBUILD" "TnT (entrenado con WSJ)"


..\..\..\tesis -comparar "..\WSJ\wsj.g" "Taggeado\wsj.tt" "Mediciones\wsj_VS_wsjTnTWSJ.l" -l "WSJ original contra WSJ etiquetado con TnT (entrenado con WSJ)" "WSJ" "TnT(WSJ)"
..\..\..\tesis -comparar "..\WSJ\wsj.g" "Taggeado\wsj+NFI.tt" "Mediciones\wsj_VS_wsjTnTWSJ+NFI.l" -l "WSJ original contra WSJ etiquetado con TnT (entrenado con WSJ + NFI)" "WSJ" "TnT(WSJ+NFI)"
..\..\..\tesis -comparar "Taggeado\wsj.tt" "Taggeado\wsj+NFI.tt" "Mediciones\wsjTnTwsj_VS_wsjTnTwsj+NFI.l" -l "WSJ etiquetado con TnT (entrenado con WSJ) vs WSJ etiquetado con TnT (entrenado con WSJ + NFI)" "TnT(WSJ)" "TnT(WSJ+NFI)"

..\..\..\tesis -comparar "..\WSJ\wsjM1.g" "Taggeado\wsjM1.tt" "Mediciones\wsjM1_VS_TnTwsjM2.l" -l "1 mitad WSJ original contra 1 mitad WSJ etiquetado con TnT (entrenado con 2 mitad de WSJ)" "WSJ" "TnT(2WSJ)"
..\..\..\tesis -comparar "..\WSJ\wsjM2.g" "Taggeado\wsjM2.tt" "Mediciones\wsjM2_VS_TnTwsjM1.l" -l "2 mitad WSJ original contra 2 mitad WSJ etiquetado con TnT (entrenado con 1 mitad de WSJ)" "WSJ" "TnT(1WSJ)"

..\..\..\tesis -comparar "..\WSJ\wsjM1.g" "Taggeado\wsjM1+NFI.tt" "Mediciones\wsjM1_VS_TnTwsjM2+NFI.l" -l "1 mitad WSJ original contra 1 mitad WSJ etiquetado con TnT (entrenado con 2 mitad de WSJ + NFI)" "WSJ" "TnT(2WSJ+NFI)"
..\..\..\tesis -comparar "..\WSJ\wsjM2.g" "Taggeado\wsjM2+NFI.tt" "Mediciones\wsjM2_VS_TnTwsjM1+NFI.l" -l "2 mitad WSJ original contra 2 mitad WSJ etiquetado con TnT (entrenado con 1 mitad de WSJ)" "WSJ" "TnT(1WSJ+NFI)"

..\..\..\tesis -comparar "Taggeado\wsjM1.tt" "Taggeado\wsjM1+NFI.tt" "Mediciones\wsjM1__TnTwsjM2_vs_TnTwsjM2+NFI.l" -l "1 mitad WSJ etiquetado por TnT (entrenado con 2 mitad WSJ) vs 1 mitad WSJ etiquetado con TnT (entrenado con 2 mitad de WSJ + NFI)" "TnT(2WSJ)" "TnT(2WSJ+NFI)"
..\..\..\tesis -comparar "Taggeado\wsjM2.tt" "Taggeado\wsjM2+NFI.tt" "Mediciones\wsjM2__TnTwsjM1_vs_TnTwsjM1+NFI.l" -l "2 mitad WSJ etiquetado por TnT (entrenado con 1 mitad WSJ) vs 2 mitad WSJ etiquetado con TnT (entrenado con 1 mitad de WSJ + NFI)" "TnT(1WSJ)" "TnT(1WSJ+NFI)"




..\..\..\tesis -comparar "..\WSJ\wsj.g" "Taggeado\wsj.tt" "Mediciones\wsj_VS_wsjTnTWSJ.mc" -p "WSJ original contra WSJ etiquetado con TnT (entrenado con WSJ)" "WSJ" "TnT(WSJ)"
..\..\..\tesis -comparar "..\WSJ\wsj.g" "Taggeado\wsj+NFI.tt" "Mediciones\wsj_VS_wsjTnTWSJ+NFI.mc" -p "WSJ original contra WSJ etiquetado con TnT (entrenado con WSJ + NFI)" "WSJ" "TnT(WSJ+NFI)"
..\..\..\tesis -comparar "Taggeado\wsj.tt" "Taggeado\wsj+NFI.tt" "Mediciones\wsjTnTwsj_VS_wsjTnTwsj+NFI.mc" -p "WSJ etiquetado con TnT (entrenado con WSJ) vs WSJ etiquetado con TnT (entrenado con WSJ + NFI)" "TnT(WSJ)" "TnT(WSJ+NFI)"

..\..\..\tesis -comparar "..\WSJ\wsjM1.g" "Taggeado\wsjM1.tt" "Mediciones\wsjM1_VS_TnTwsjM2.mc" -p "1 mitad WSJ original contra 1 mitad WSJ etiquetado con TnT (entrenado con 2 mitad de WSJ)" "WSJ" "TnT(2WSJ)"
..\..\..\tesis -comparar "..\WSJ\wsjM2.g" "Taggeado\wsjM2.tt" "Mediciones\wsjM2_VS_TnTwsjM1.mc" -p "2 mitad WSJ original contra 2 mitad WSJ etiquetado con TnT (entrenado con 1 mitad de WSJ)" "WSJ" "TnT(1WSJ)"

..\..\..\tesis -comparar "..\WSJ\wsjM1.g" "Taggeado\wsjM1+NFI.tt" "Mediciones\wsjM1_VS_TnTwsjM2+NFI.mc" -p "1 mitad WSJ original contra 1 mitad WSJ etiquetado con TnT (entrenado con 2 mitad de WSJ + NFI)" "WSJ" "TnT(2WSJ+NFI)"
..\..\..\tesis -comparar "..\WSJ\wsjM2.g" "Taggeado\wsjM2+NFI.tt" "Mediciones\wsjM2_VS_TnTwsjM1+NFI.mc" -p "2 mitad WSJ original contra 2 mitad WSJ etiquetado con TnT (entrenado con 1 mitad de WSJ)" "WSJ" "TnT(1WSJ+NFI)"

..\..\..\tesis -comparar "Taggeado\wsjM1.tt" "Taggeado\wsjM1+NFI.tt" "Mediciones\wsjM1__TnTwsjM2_vs_TnTwsjM2+NFI.mc" -p "1 mitad WSJ etiquetado por TnT (entrenado con 2 mitad WSJ) vs 1 mitad WSJ etiquetado con TnT (entrenado con 2 mitad de WSJ + NFI)" "TnT(2WSJ)" "TnT(2WSJ+NFI)"
..\..\..\tesis -comparar "Taggeado\wsjM2.tt" "Taggeado\wsjM2+NFI.tt" "Mediciones\wsjM2__TnTwsjM1_vs_TnTwsjM1+NFI.mc" -p "2 mitad WSJ etiquetado por TnT (entrenado con 1 mitad WSJ) vs 2 mitad WSJ etiquetado con TnT (entrenado con 1 mitad de WSJ + NFI)" "TnT(1WSJ)" "TnT(1WSJ+NFI)"

