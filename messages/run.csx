#r "Newtonsoft.Json"
#load "BasicLuisDialog.csx"

using System;
using System.Net;
using System.Threading;
using Newtonsoft.Json;

using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

static Question actualQuestion1 = new Question("Möchtest du Frage 2 hören?", 0, new QuestionLink[]{});
static Question actualQuestion2 = new Question("Möchtest du Frage 1 hören?", 0, new QuestionLink[]{});
actualQuestion1.links = new QuestionLink[]{new QuestionLink("Ja", actualQuestion2)};
actualQuestion2.links = new QuestionLink[]{new QuestionLink("Ja", actualQuestion1)};

static Question actualQuestion = actualQuestion1;

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
                  log.Info(actualQuestion.text);
                  var client1 = new ConnectorClient(new Uri(activity.ServiceUrl));
                  var reply1 = activity.CreateReply();
                  reply1.Text = "Willkommen! "+actualQuestion.text+actualQuestion1.links.Length;
                  await client1.Conversations.ReplyToActivityAsync(reply1);
                  //actualQuestion = actualQuestion.links[0].question;
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
    public int id;
    public QuestionLink[] links;

    public Question(String text, int id, QuestionLink[] links)
    {
      this.text = text;
      this.id = id;
      this.links = links;
    }
}

public class QuestionLink
{
    public String text;
    public Question question;

    public QuestionLink(String text, Question question)
    {
      this.text = text;
      this.question = question;
    }
}

public class Linker{

  public Question[] questions;

}
