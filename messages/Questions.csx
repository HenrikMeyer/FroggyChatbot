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
      true
    )
  },
  {
    "1",
    new Question(
      "Welche Frabe hat der Himmel?",
      new QuestionLink[]{
        new QuestionLink("Blau", "0"),
        new QuestionLink("blau", "0")
      },
      true
    )
  },
  {
    "2",
    new Question(
      "Nett dich kennen zu lernen.",
      new QuestionLink[]{
        new QuestionLink("OK", "0")
      },
      true
    )
  }
};
