#r "Newtonsoft.Json"
#load "BasicLuisDialog.csx"
#load "Questions.csx"

using System;
using System.Net;
using System.Threading;
using Newtonsoft.Json;

using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

using System.Collections.Generic;


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

                  bool found=false;
                  int i = 0;
                  while(found==false && i<questions[actualID].links.Length){
                    if(questions[actualID].links[i].text==activity.Text){
                      found = true;
                      actualID = questions[actualID].links[i].questionID;

                      var client1 = new ConnectorClient(new Uri(activity.ServiceUrl));
                      var reply1 = activity.CreateReply();
                      //reply1.Text = questions[actualID].text;
                      /*await client1.Conversations.ReplyToActivityAsync(reply1);*/

                      List<CardAction> cardButtons = new List<CardAction>();
                      for(int k=0; k<questions[actualID].links.Length; k++){
                        CardAction plButton = new CardAction()
                        {
                          Value = questions[actualID].links[k].text,
                          Type = "postBack",
                          Title = questions[actualID].links[k].text
                        };
                        cardButtons.Add(plButton);
                      }

                      HeroCard plCard = new HeroCard()
                      {
                        Title = questions[actualID].text,
                        Subtitle = "Wikipedia Page",
                        //Images = cardImages,
                        Buttons = cardButtons
                      };
                      Attachment plAttachment = plCard.ToAttachment();
                      reply1.Attachments.Add(plAttachment);

                      await client1.Conversations.ReplyToActivityAsync(reply1);

                    }
                    i++;
                  }

                  if(found==false){
                    var client2 = new ConnectorClient(new Uri(activity.ServiceUrl));
                    var reply2 = activity.CreateReply();
                    reply2.Text = "Tut mir leid, das habe ich nicht verstanden. Ich muss noch einiges lernen. Bitte Antworte bis dahin mit ";
                    for(int x=0; x<questions[actualID].links.Length; x++){
                      reply2.Text+="'";
                      reply2.Text+=questions[actualID].links[x].text;
                      reply2.Text+="'";
                      if(x<questions[actualID].links.Length-1){
                        if(x==questions[actualID].links.Length-2){
                          reply2.Text+=" oder ";
                        }
                        else{
                          reply2.Text+=", ";
                        }
                      }
                    }
                    reply2.Text+=".";
                    await client2.Conversations.ReplyToActivityAsync(reply2);
                  }



                  break;

                case ActivityTypes.ConversationUpdate:

                  var client3 = new ConnectorClient(new Uri(activity.ServiceUrl));
                  var reply3 = activity.CreateReply();
                  reply3.Text = questions[actualID].text;
                  await client3.Conversations.ReplyToActivityAsync(reply3);

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
    public String text;           //Question Text
    public QuestionLink[] links;  //Array of possible Answers with ID of connected next Question

    public Question(String text, QuestionLink[] links)
    {
      this.text = text;
      this.links = links;
    }
}

public class QuestionLink
{
    public String text;       //Answer Text
    public String questionID; //ID of next Question

    public QuestionLink(String text, String questionID)
    {
      this.text = text;
      this.questionID = questionID;
    }
}
