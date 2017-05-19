static var questions = new Dictionary<String, Question>(){
  {
    "0",
    new Question(
      "Hallo. Sie haben ein Problem? Um Ihnen helfen zu können, muss ich wissen, welcher Anschluss gestört ist. Um welche Rufnummer oder Kundennummer geht es? Bitte schicken Sie mir eine der beiden Nummern.",
      new QuestionLink[]{
        new QuestionLink("Numer XYZ", "1")
      }
    )
  },
  {
    "1",
    new Question(
      "Wir haben festgestellt, dass Sie einen NGN Anschluss nutzen.\nWas funktioniert nicht? [Telefon, Internet, Komplett]",
      new QuestionLink[]{
        new QuestionLink("Telefon", "2"),
        new QuestionLink("Internet", "3"),
        new QuestionLink("Komplett", "3")
      }
    )
  },
  {
    "2",
    new Question(
      "Haben Sie ein schnurloses Telefon?",
      new QuestionLink[]{
        new QuestionLink("Ja", "4"),
        new QuestionLink("Nein", "5"),
      }
    )
  },
  {
    "3",
    new Question(
      "Diese Option wird noch nicht unterstützt.",
      new QuestionLink[]{
        new QuestionLink("OK", "1")
      }
    )
  },
  {
    "4",
    new Question(
      "Dient die Fritz Box als Basisstation für Ihre schnurlosen Telefone? (DECT Funktion)",
      new QuestionLink[]{
        new QuestionLink("Ja", "6"),
        new QuestionLink("Nein", "7"),
      }
    )
  },
  {
    "5",
    new Question(
      "Ist Ihr Telefon mit der Fritz Box, wie auf dem Bild zu sehen, angeschlossen?",
      new QuestionLink[]{
        new QuestionLink("Ja", "10"),
        new QuestionLink("Nein", "8"),
      }
    )
  },
  {
    "6",
    new Question(
      "Ist das Telefon mit der Fritz Box richtig verbunden?",
      new QuestionLink[]{
        new QuestionLink("Ja", "10"),
        new QuestionLink("Nein", "8"),
      }
    )
  },
  {
    "7",
    new Question(
      "Ist die Basisstation mit der Fritz Box, wir auf einem der zwei Bilder zu sehen, angeschlossen?",
      new QuestionLink[]{
        new QuestionLink("Phoneanschluss", "10"),
        new QuestionLink("S0 Port", "8"),
      }
    )
  },
  {
    "8",
    new Question(
      "Bitte passen Sie die Verkabelung/Einstellung anhand des gezeigten Bildes an.\nFunktioniert der Schluss wieder?",
      new QuestionLink[]{
        new QuestionLink("Ja", "11"),
        new QuestionLink("Nein", "10"),
      }
    )
  }
};
