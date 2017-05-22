static var questions = new Dictionary<String, Question>(){
  {
    "0",
    new Question(
      "Hallo. Sie haben ein Problem? Um Ihnen helfen zu können, muss ich wissen, welcher Anschluss gestört ist. Um welche Rufnummer oder Kundennummer geht es? Bitte schicken Sie mir eine der beiden Nummern.",
      new QuestionLink[]{
        new QuestionLink("Beispielnummer", "1")
      },
      new String[]{
        "https://www.ewe.com/assets/images/ewe-logo.svg"
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
      },
      new String[]{
        "http://qfrog.de/chatbot/FRITZ%21Box_Analog_Schnurlos_900px.jpg"
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
      },
      new String[]{
        "http://qfrog.de/chatbot/FRITZ%21Box_Fon1_Kabelgebunden_900px.jpg"
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
      },
      new String[]{
        "https://upload.wikimedia.org/wikipedia/commons/0/01/FRITZFon_C3_2.jpg"
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
      },
      new String[]{
        "http://qfrog.de/chatbot/FRITZ%21Box_Fon1_Schnurlos_900px.jpg",
        "http://qfrog.de/chatbot/FRITZ%21Box_S0_ISDN-Schnurlos_900px.jpg"
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
  },
  {
    "10",
    new Question(
      "Lieber Kunde, nun haben wir alle benötigten Daten von Ihnen bekommen und werden Ihre Störung durch unsere Spezialisten bearbeiten lassen. Gleich bekommen Sie von uns eine Bearbeitungsnummer.",
      new QuestionLink[]{
        new QuestionLink("OK", "0")
      }
    )
  },
  {
    "11",
    new Question(
      "Lieber Kunde, schön, dass wir beide Ihren Anschluss entstören konnten. Wir wünschen Ihnen weiterhin viel Spaß an unseren Produkten. Falls Sie einen Verbesserungsvorschlag zu dem gerade durchgeführten Vorgehen haben, freuen wir uns über eine Rückmeldung von Ihnen.",
      new QuestionLink[]{
        new QuestionLink("OK", "0")
      }
    )
  }
};
