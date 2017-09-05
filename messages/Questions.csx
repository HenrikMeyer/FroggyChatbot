static var questions = new Dictionary<String, Question>(){
  {
    "0",
    new Question(
      "Ahoi!",
      new QuestionLink[]{
        new QuestionLink("Hi", "1"),
        new QuestionLink("Hallo", "1"),
        new QuestionLink("Guten Tag", "1"),
        new QuestionLink("Moin", "1")
      },
      false
    )
  },
  {
    "1",
    new Question(
      "Welche Frabe hat der Himmel?",
      new QuestionLink[]{
        new QuestionLink("Blau", "2"),
        new QuestionLink("Grün", "10"),
      },
      true
    )
  },
  {
    "2",
    new Question(
      "Stimmt. Ist das eine deiner Lieblingsfarben?",
      new QuestionLink[]{
        new QuestionLink("Ja", "3"),
        new QuestionLink("Nein", "4"),
      },
      true
    )
  },
  {
    "3",
    new Question(
      "Eine sehr schöne Farbe! Es war nett mit dir zu plaudern.",
      new QuestionLink[]{
        new QuestionLink("Zum Anfang", "0")
      },
      true
    )
  },
  {
    "4",
    new Question(
      "Was ist dann deine Lieblingsfarbe?",
      new QuestionLink[]{
        new QuestionLink("Rot", "3"),
        new QuestionLink("Gelb", "3"),
        new QuestionLink("Grün", "3")

      },
      true
    )
  },
  {
    "10",
    new Question(
      "Eigentlich ist der Himmel blau, aber du bist anscheinend sehr kreativ! Was ist deine Lieblingsfarbe?",
      new QuestionLink[]{
        new QuestionLink("Rot", "3"),
        new QuestionLink("Gelb", "3"),
        new QuestionLink("Grün", "3")
      },
      true
    )
  }
};
