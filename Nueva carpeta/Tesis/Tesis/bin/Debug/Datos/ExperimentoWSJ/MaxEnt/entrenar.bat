java -mx3800m -cp "stanford-postagger.jar;" edu.stanford.nlp.tagger.maxent.MaxentTagger -prop entrenar.prop -trainFile "format=TSV,wordColumn=0,tagColumn=1,%1" -model %2
