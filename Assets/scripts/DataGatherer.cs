using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Twity.DataModels.Core;

public class DataGatherer : MonoBehaviour {

    // Singleton Stuff
    private static DataGatherer _instance;
    public static DataGatherer Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
    }

    // Class
    private List<GameEvent> GameEventsTimeline { get; set; }

    private DataGatherer()
    {
        GameEventsTimeline = new List<GameEvent>();
        Twity.Oauth.consumerKey = "OYpntKrtpj1vPRz1dF0YECRxq";
        Twity.Oauth.consumerSecret = "WtGAilvToR3zSCpKKngokhRCVyJ5DgknOEZCrvTokwFNcZtwib";
        Twity.Oauth.accessToken = "957398892479426560-7hgsPNScpw08WHsZ9A2wsTbaWihO9QR";
        Twity.Oauth.accessTokenSecret = "OvO2efxvEkYYSe1jR4UBEnIrcQbBB0j8BrDJl5Q9RrvM6";
    }

    private void Start()
    {
        GameEventsTimeline.Add(new GameEvent(GameEventEnum.KILL_BOSS));
        GameEventsTimeline.Add(new GameEvent(GameEventEnum.DISCOVER_RADIO_TOWER, "the radio tower"));
        GameEventsTimeline.Add(new GameEvent(GameEventEnum.KILL_BOSS));
        GameEventsTimeline.Add(new GameEvent(GameEventEnum.DISCOVER_RADIO_TOWER, "the signal tower"));
        GameEventsTimeline.Add(new GameEvent(GameEventEnum.DEATH_BASIC));
        GameEventsTimeline.Add(new GameEvent(GameEventEnum.DEATH_BASIC));
        GameEventsTimeline.Add(new GameEvent(GameEventEnum.DISCOVER_RADIO_TOWER, "the signal tower"));
        GameEventsTimeline.Add(new GameEvent(GameEventEnum.DISCOVER_RADIO_TOWER, "the radio tower"));
        GameEventsTimeline.Add(new GameEvent(GameEventEnum.DEATH_BASIC));
        GameEventsTimeline.Add(new GameEvent(GameEventEnum.DEATH_BASIC));
        GameEventsTimeline.Add(new GameEvent(GameEventEnum.KILL_BOSS));
        SendTweet(0);
    }

    public void AddEvent(GameEvent gameEvent)
    {
        GameEventsTimeline.Add(gameEvent);
    }

    public string SendTweet(int transmittorsMissed)
    {
        float str = transmittorsMissed > 0 ? transmittorsMissed / 12f : 0;
        var tweet = new TweetTemplates(GameEventsTimeline).GenerateTweet().GarbleString(str, transmittorsMissed);

        Debug.Log("Sending tweet " + tweet);

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters["status"] = tweet; 
        StartCoroutine(Twity.Client.Post("statuses/update", parameters, Callback));


        return tweet;
    }

    void Callback(bool success, string response)
    {
        if (success)
        {
            Tweet tweet = JsonUtility.FromJson<Tweet>(response);
            Debug.Log("Tweet sent " + tweet.text);
        }
        else
        {
            Debug.Log(response);
        }
    }

    override public string ToString()
    {
        var builder = new StringBuilder();

        foreach (var gameEvent in GameEventsTimeline)
        {
            builder.Append(gameEvent.EventType.ToString())
                   .Append(" at ")
                   .Append(gameEvent.Timestamp.ToString())
                   .Append(": ")
                   .Append(gameEvent.Description)
                   .Append(", ");
        }

        // Remove final ,
        builder.Remove(builder.Length - 1, 1);

        return builder.ToString();
    }
}
