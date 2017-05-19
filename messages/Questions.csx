static var questions = new Dictionary<String, Question>(){
  {
    "0",
    new Question(
      "Willst du Frage 2 hören?",
      new QuestionLink[]{
        new QuestionLink("Ja", "1"),
        new QuestionLink("Nein", "2")
      }
    )
  },
  {
    "1",
    new Question(
      "Willst du Frage 1 hören?",
      new QuestionLink[]{
        new QuestionLink("Ja", "0"),
        new QuestionLink("Nein", "2")
      }
    )
  },
  {
    "2",
    new Question(
      "Ok, ich wünsche Dir noch einen schönen Tag!",
      new QuestionLink[]{
        new QuestionLink("Danke", "0")
      }
    )
  }
};
