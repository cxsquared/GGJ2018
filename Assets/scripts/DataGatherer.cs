using System.Collections.Generic;
using System.Text;
using UnityEngine;

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
    }

    public void AddEvent(GameEvent gameEvent)
    {
        GameEventsTimeline.Add(gameEvent);
    }

    public string BuildTweet(int transmittorsMissed)
    {
        float str = (float)(transmittorsMissed / 5);
        return new TweetTemplates(GameEventsTimeline).GenerateTweet().GarbleString(str, transmittorsMissed);
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
