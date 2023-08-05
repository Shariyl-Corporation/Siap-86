using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoice : MonoBehaviour
{

    private Hashtable questionResponses;
    private Hashtable talkResponses;

    struct QuestionResponse
    {
        public string response;
        public string knowledge;

        public QuestionResponse(string r, string k)
        {
            response = r;
            knowledge = k;
        }
    };

    void Start()
    {
        questionResponses = new Hashtable();
        talkResponses = new Hashtable();

        addQuestionResponse("What do you like doing with your free time?", "I love drawing, I just wish I was better at it.", "drawing");
        addQuestionResponse("What do you like doing with your free time?", "I spend a lot of time watching YouTube..", "YouTube");
        addQuestionResponse("What do you like doing with your free time?", "I don't really do anything at all.", "do nothing");

        addTalkResponse("drawing", "Being able to draw is a great creative outlet, I particularly enjoy drawing portraits. Perhaps I could draw you!");
        addTalkResponse("YouTube", "There's so much to learn on there. My favorite YouTuber is Acerola_t, you should check him out!");
        addTalkResponse("do nothing", "It's not very fun. I just haven't found anything I particularly enjoy, so I tend to spend a lot of my time staring at the wall in a depressive state as time marches forward.");

        addQuestionResponse("Watched any cool movies lately?", "My friend recommended me La La Land.", "La La Land");
        addQuestionResponse("Watched any cool movies lately?", "I just saw Whiplash.", "Whiplash");
        addQuestionResponse("Watched any cool movies lately?", "Yes! I recently watched The Green Knight.", "The Green Knight");
        addQuestionResponse("Watched any cool movies lately?", "I saw The Lighthouse a few days ago.", "The Lighthouse");


    }

    private void addQuestionResponse(string question, string response, string knowledge)
    {
        QuestionResponse qr = new QuestionResponse(response, knowledge);

        if (!questionResponses.ContainsKey(question))
        {
            questionResponses.Add(question, new List<QuestionResponse> { qr });
        }
        else
        {
            ((List<QuestionResponse>)questionResponses[question]).Add(qr);
        }
    }

    private void addTalkResponse(string knowledge, string response)
    {
        talkResponses.Add(knowledge, response);
    }

    public List<string> GetQuestionResponse(string inquery)
    {
        List<QuestionResponse> responses = (List<QuestionResponse>)questionResponses[inquery];

        QuestionResponse response = responses[Random.Range(0, responses.Count)];

        return new List<string> { response.response, response.knowledge };
    }

    public string GetTalkResponse(string inquery)
    {
        if (!talkResponses.Contains(inquery))
            return "Uh oh, looks like Acerola forgot to code a response to this, or he made a typo. It's quite hard to test all of this by yourself you know.";

        return (string)talkResponses[inquery];
    }
}
