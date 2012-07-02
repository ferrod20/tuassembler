java -mx5000m -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsj.g" -model "Entrenado/wsj" -outputFile "Taggeado/wsj.tt"
java -mx5000m -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsj.g" -model "Entrenado/wsj+NFI" -outputFile "Taggeado/wsj+NFI.tt"

java -mx5000m -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompC1.g" -model "Entrenado/wsjC1" -outputFile "Taggeado/wsjCompC1.tt"
java -mx5000m -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompC2.g" -model "Entrenado/wsjC2" -outputFile "Taggeado/wsjCompC2.tt"
java -mx5000m -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompC3.g" -model "Entrenado/wsjC3" -outputFile "Taggeado/wsjCompC3.tt"
java -mx5000m -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompC4.g" -model "Entrenado/wsjC4" -outputFile "Taggeado/wsjCompC4.tt"
java -mx5000m -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjM1.g" -model "Entrenado/wsjM2" -outputFile "Taggeado/wsjM2.tt"
java -mx5000m -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjM2.g" -model "Entrenado/wsjM1" -outputFile "Taggeado/wsjM1.tt"
java -mx5000m -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompD.g" -model "Entrenado/wsjD" -outputFile "Taggeado/wsjCompD.tt"

java -mx5000m -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompC1.g" -model "Entrenado/wsjC1+NFI" -outputFile "Taggeado/wsjCompC1+NFI.tt"
java -mx5000m -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompC2.g" -model "Entrenado/wsjC2+NFI" -outputFile "Taggeado/wsjCompC2+NFI.tt"
java -mx5000m -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompC3.g" -model "Entrenado/wsjC3+NFI" -outputFile "Taggeado/wsjCompC3+NFI.tt"
java -mx5000m -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompC4.g" -model "Entrenado/wsjC4+NFI" -outputFile "Taggeado/wsjCompC4+NFI.tt"
java -mx5000m -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjM1.g" -model "Entrenado/wsjM2+NFI" -outputFile "Taggeado/wsjM2+NFI.tt"
java -mx5000m -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjM2.g" -model "Entrenado/wsjM1+NFI" -outputFile "Taggeado/wsjM1+NFI.tt"
java -mx5000m -cp "tagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop tagger.prop -textFile "format=TSV,wordColumn=0,tagColumn=1,../WSJ/wsjCompD.g" -model "Entrenado/wsjD+NFI" -outputFile "Taggeado/wsjCompD+NFI.tt"


