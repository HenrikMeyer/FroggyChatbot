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
      "Nett dich kennen zu lernen.",
      new QuestionLink[]{
        new QuestionLink("OK", "0")
      },
      true
    )
  }
};
