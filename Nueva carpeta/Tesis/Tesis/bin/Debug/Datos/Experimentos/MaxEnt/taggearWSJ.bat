java -mx10000m -d64 -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsj.g" -model "Entrenado/wsj" -outputFile "WSJ\Taggeado\wsj.tt"
java -mx10000m -d64 -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsj.g" -model "Entrenado/wsj+NFI" -outputFile "WSJ\Taggeado\wsj+NFI.tt"

java -mx10000m -d64 -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompC1.g" -model "Entrenado/wsjC1" -outputFile "WSJ\Taggeado\wsjCompC1.tt"
java -mx10000m -d64 -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompC2.g" -model "Entrenado/wsjC2" -outputFile "WSJ\Taggeado\wsjCompC2.tt"
java -mx10000m -d64 -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompC3.g" -model "Entrenado/wsjC3" -outputFile "WSJ\Taggeado\wsjCompC3.tt"
java -mx10000m -d64 -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompC4.g" -model "Entrenado/wsjC4" -outputFile "WSJ\Taggeado\wsjCompC4.tt"
java -mx10000m -d64 -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjM1.g" -model "Entrenado/wsjM2" -outputFile "WSJ\Taggeado\wsjM1.tt"
java -mx10000m -d64 -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjM2.g" -model "Entrenado/wsjM1" -outputFile "WSJ\Taggeado\wsjM2.tt"
java -mx10000m -d64 -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompD.g" -model "Entrenado/wsjD" -outputFile "WSJ\Taggeado\wsjCompD.tt"

java -mx10000m -d64 -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompC1.g" -model "Entrenado/wsjC1+NFI" -outputFile "WSJ\Taggeado\wsjCompC1+NFI.tt"
java -mx10000m -d64 -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompC2.g" -model "Entrenado/wsjC2+NFI" -outputFile "WSJ\Taggeado\wsjCompC2+NFI.tt"
java -mx10000m -d64 -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompC3.g" -model "Entrenado/wsjC3+NFI" -outputFile "WSJ\Taggeado\wsjCompC3+NFI.tt"
java -mx10000m -d64 -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompC4.g" -model "Entrenado/wsjC4+NFI" -outputFile "WSJ\Taggeado\wsjCompC4+NFI.tt"
java -mx10000m -d64 -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjM1.g" -model "Entrenado/wsjM2+NFI" -outputFile "WSJ\Taggeado\wsjM1+NFI.tt"
java -mx10000m -d64 -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjM2.g" -model "Entrenado/wsjM1+NFI" -outputFile "WSJ\Taggeado\wsjM2+NFI.tt"
java -mx10000m -d64 -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompD.g" -model "Entrenado/wsjD+NFI" -outputFile "WSJ\Taggeado\wsjCompD+NFI.tt"






