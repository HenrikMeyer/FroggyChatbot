#r "Newtonsoft.Json"
#load "BasicLuisDialog.csx"

using System;
using System.Net;
using System.Threading;
using Newtonsoft.Json;

using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

using System.Collections.Generic;

static var questions = new Dictionary<String, Question>(){
  {"0", new Question("Willst du Frage 2 hören?", new QuestionLink[]{new QuestionLink("Ja", "1")})},
  {"1", new Question("Willst du Frage 1 hören?", new QuestionLink[]{new QuestionLink("Ja", "0")})}
};
//questions["0"] = new Question("Willst du Frage 2 hören?", new QuestionLink[]{new QuestionLink("Ja", "1")});
//questions["1"] = new Question("Willst du Frage 1 hören?", new QuestionLink[]{new QuestionLink("Ja", "0")});
//questions.Add("0", new Question("Willst du Frage 2 hören?", new QuestionLink[]{new QuestionLink("Ja", "1")}));
//questions.Add("1", new Question("Willst du Frage 1 hören?", new QuestionLink[]{new QuestionLink("Ja", "0")}));
static String actualID = "0";

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"Webhook was triggered!");
    // Initialize the azure bot
    using (BotService.Initialize())
    {
        // Deserialize the incoming activity
        string jsonContent = await req.Content.ReadAsStringAsync();
        var activity = JsonConvert.DeserializeObject<Activity>(jsonContent);

        // authenticate incoming request and add activity.ServiceUrl to MicrosoftAppCredentials.TrustedHostNames
        // if request is authenticated
        if (!await BotService.Authenticator.TryAuthenticateAsync(req, new [] {activity}, CancellationToken.None))
        {
            return BotAuthenticator.GenerateUnauthorizedResponse(req);
        }

        if (activity != null)
        {
            // one of these will have an interface and process it
            switch (activity.GetActivityType())
            {
                case ActivityTypes.Message:
                  //QuestionLink[] tempQuestionLinks = actualQuestion.links;
                  //QuestionLink questionLink = tempQuestionLinks[0];
                  //Question tempQuestion = questionLink.question;

                  //actualQuestion = actualQuestion.links[0].question;

                  foreach (KeyValuePair<string, Question> kvp in questions)
                  {
                    log.Info("Pair: "+kvp.Key+" "+kvp.Value.text);
                  }


                  log.Info("Actual ID: "+actualID);
                  log.Info("questions");
                  var client1 = new ConnectorClient(new Uri(activity.ServiceUrl));
                  var reply1 = activity.CreateReply();
                  reply1.Text = "Willkommen! "+questions[actualID].text;
                  await client1.Conversations.ReplyToActivityAsync(reply1);
                  actualID = questions[actualID].links[0].questionID;
                  break;

                case ActivityTypes.ConversationUpdate:
                /*
                    var client = new ConnectorClient(new Uri(activity.ServiceUrl));
                    IConversationUpdateActivity update = activity;
                    if (update.MembersAdded.Any())
                    {
                        var reply = activity.CreateReply();
                        var newMembers = update.MembersAdded?.Where(t => t.Id != activity.Recipient.Id);
                        foreach (var newMember in newMembers)
                        {
                            reply.Text = "Welcome";
                            if (!string.IsNullOrEmpty(newMember.Name))
                            {
                                reply.Text += $" {newMember.Name}";
                            }
                            reply.Text += "!";
                            await client.Conversations.ReplyToActivityAsync(reply);
                        }
                    }
                    */
                    break;
                case ActivityTypes.ContactRelationUpdate:
                case ActivityTypes.Typing:
                case ActivityTypes.DeleteUserData:
                case ActivityTypes.Ping:
                default:
                    log.Error($"Unknown activity type ignored: {activity.GetActivityType()}");
                    break;
            }
        }
        return req.CreateResponse(HttpStatusCode.Accepted);
    }
}

public class Question
{
    public String text;
    public QuestionLink[] links;

    public Question(String text, QuestionLink[] links)
    {
      this.text = text;
      this.links = links;
    }
}

public class QuestionLink
{
    public String text;
    public String questionID;

    public QuestionLink(String text, String questionID)
    {
      this.text = text;
      this.questionID = questionID;
    }
}
