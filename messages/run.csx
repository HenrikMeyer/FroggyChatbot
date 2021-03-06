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
using System.Drawing;

//questions["0"] = new Question("Willst du Frage 2 hören?", new QuestionLink[]{new QuestionLink("Ja", "1")});
//questions["1"] = new Question("Willst du Frage 1 hören?", new QuestionLink[]{new QuestionLink("Ja", "0")});
//questions.Add("0", new Question("Willst du Frage 2 hören?", new QuestionLink[]{new QuestionLink("Ja", "1")}));
//questions.Add("1", new Question("Willst du Frage 1 hören?", new QuestionLink[]{new QuestionLink("Ja", "0")}));
//static String actualID = "0";
static var users = new Dictionary<String, String>(){};

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"1) WEBHOOK TRIGGERED");


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
            var client = new ConnectorClient(new Uri(activity.ServiceUrl));
            // one of these will have an interface and process it
            log.Info("2) All Users:");
            foreach(KeyValuePair<string, string> entry in users)
            {
              log.Info("-> "+entry.Key+", "+entry.Value);
            }
            log.Info("3) GENERATE ACTUAL QUESTION ID (0)");
            String actualID = "0";
            if(users.ContainsKey(activity.From.Id+"")){
              log.Info($"4) USER ALREADY KNOWN. SET ACTUAL QUESTUON TO {users[activity.From.Id]}");
              actualID = users[activity.From.Id+""];
            }
            log.Info("4) ACTUAL QUESTION ID: "+actualID);
            log.Info("5) ACTUAL USER ID: "+activity.From.Id);
            switch (activity.GetActivityType())
            {
                case ActivityTypes.Message:
                  log.Info($"6) ACTIVITY TYPE: MESSAGE (QUESTION-ID: {actualID})");
                  bool found=false;
                  int i = 0;
                  while(found==false && i<questions[actualID].links.Length){
                    if(questions[actualID].links[i].text==activity.Text){
                      found = true;
                      log.Info("7)USERS ADD ACTIVITY FROM ID (RAW): "+activity.From.Id);
                      if(users.ContainsKey(activity.From.Id+"")){
                        users[activity.From.Id+""]=questions[actualID].links[i].questionID;
                      }
                      else{
                        users.Add(activity.From.Id+"", questions[actualID].links[i].questionID);
                      }
                      actualID=questions[actualID].links[i].questionID;


                      var reply   = activity.CreateReply();
                      reply.Text  = questions[actualID].text;
                      List<CardAction> cardButtons = new List<CardAction>();
                      if(questions[actualID].showLinks){
                        for(int k=0; k<questions[actualID].links.Length; k++){
                          CardAction plButton = new CardAction()
                          {
                            Value = questions[actualID].links[k].text,
                            Type  = "postBack",
                            Title = questions[actualID].links[k].text
                          };
                          cardButtons.Add(plButton);
                        }
                      }
                      List<CardImage> cardImages = new List<CardImage>();
                      for(int k=0; k<questions[actualID].links.Length; k++){
                        if(questions[actualID].imageURLs!=null){
                          for(int u=0; u<questions[actualID].imageURLs.Length; u++){
                            cardImages.Add(new CardImage(url: questions[actualID].imageURLs[u]));
                          }
                        }
                      }
                      HeroCard plCard = new HeroCard()
                      {
                        //Subtitle = questions[actualID].text,
                        Images  = cardImages,
                        Buttons = cardButtons
                      };
                      Attachment plAttachment = plCard.ToAttachment();
                      if(cardButtons.Count>0||cardImages.Count>0){
                        reply.Attachments.Add(plAttachment);
                      }

                      await client.Conversations.ReplyToActivityAsync(reply);

                    }
                    i++;
                  }

                  if(found==false){

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
                    await client.Conversations.ReplyToActivityAsync(reply2);
                  }



                  break;

                case ActivityTypes.ConversationUpdate:

                log.Info($"6) ACTIVITY TYPE: ConversationUpdate (QUESTION-ID: {actualID})");

                /*
                  var reply3 = activity.CreateReply();
                  reply3.Text = "Hallo. Sie haben ein Problem? Um Ihnen helfen zu können, muss ich wissen, welcher Anschluss gestört ist. Um welche Rufnummer oder Kundennummer geht es? Bitte schicken Sie mir eine der beiden Nummern.";
                  await client.Conversations.ReplyToActivityAsync(reply3);
                  */
                  var reply3  = activity.CreateReply();
                  reply3.Text = questions[actualID].text;

                  List<CardAction> cardButtons1 = new List<CardAction>();
                  if(questions[actualID].showLinks){
                    for(int k=0; k<questions[actualID].links.Length; k++){
                      CardAction plButton = new CardAction()
                      {
                        Value = questions[actualID].links[k].text,
                        Type  = "postBack",
                        Title = questions[actualID].links[k].text
                      };
                      cardButtons1.Add(plButton);
                    }
                  }
                  List<CardImage> cardImages1 = new List<CardImage>();
                  for(int k=0; k<questions[actualID].links.Length; k++){
                    if(questions[actualID].imageURLs!=null){
                      for(int u=0; u<questions[actualID].imageURLs.Length; u++){
                        cardImages1.Add(new CardImage(url: questions[actualID].imageURLs[u]));
                      }
                    }
                  }
                  HeroCard plCard1 = new HeroCard()
                  {
                    //Subtitle = questions[actualID].text,
                    Images = cardImages1,
                    Buttons = cardButtons1
                  };
                  Attachment plAttachment1 = plCard1.ToAttachment();
                  if(cardButtons1.Count>0||cardImages1.Count>0){
                    reply3.Attachments.Add(plAttachment1);
                  }

                  await client.Conversations.ReplyToActivityAsync(reply3);

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

                  /*
                  var reply4 = activity.CreateReply();
                  reply4.Text = "Hallo. Sie haben ein Problem? Um Ihnen helfen zu können, muss ich wissen, welcher Anschluss gestört ist. Um welche Rufnummer oder Kundennummer geht es? Bitte schicken Sie mir eine der beiden Nummern.";
                  await client.Conversations.ReplyToActivityAsync(reply4);
                  break;
                  */
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
    public String         text;           //Question Text
    public QuestionLink[] links;  //Array of possible Answers with ID of connected next Question#
    public bool           showLinks;
    public String[]       imageURLs;

    public Question(String text, QuestionLink[] links, bool showLinks, String[] imageURLs)
    {
      this.text       = text;
      this.links      = links;
      this.showLinks  = showLinks;
      this.imageURLs  = imageURLs;
    }

    public Question(String text, QuestionLink[] links, bool showLinks)
    {
      this.text       = text;
      this.links      = links;
      this.showLinks  = showLinks;
      this.imageURLs  = null;
    }
}

public class QuestionLink
{
    public String text;       //Answer Text
    public String questionID; //ID of next Question

    public QuestionLink(String text, String questionID)
    {
      this.text       = text;
      this.questionID = questionID;
    }
}
