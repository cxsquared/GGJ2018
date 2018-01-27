using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TweetTemplates {

    private List<GameEvent> GameEvents { get; set; }

    private List<string> specialBossNames = new List<string>(new string[] { "Byte Destoryer", "large Byter", "glitched Big Byter" });

    private List<string> specialBasicNames = new List<string>(new string[] { "small byter", "basic byter", "tiny byter", "byter baby", "baby byter" });

    private List<string> deathEnemyDescription = new List<string>(new string[] { "cause a massive wound", "was a hard fought battle", "was a loosing battle", "proved a deadly match", "slaughtered my hopes" });
    private List<string> killEnemyDescription = new List<string>(new string[] { "fell to my sword", "put up a good fight", "felt my wrath", "was too easy" });
    private List<string> travelEnemyDescription = new List<string>(new string[] { "on the way to", "while traveling to", "on the path towards", "while on the trail of the", "on my quest towards the" });

    private List<string> basicDeathDescription  = new List<string>(new string[] { "I was slain again.", "This death won't stop me.", "Death is but a minor set back.", "I will rise to fight another day." });

    private List<string> genericDescription = new List<string>(new string[] { "The radio signal must be sent", "This message means I have succeded." });

    private List<string> lowKills = new List<string>(new string[] { "I didn't see many enemies on my way to the tower.", "I chose not to harm the unkowning monsters on my journey.", "The monsters were not my targets but sacrifices I didn't want to make."});


    public TweetTemplates(List<GameEvent> gameEvents)
    {
        GameEvents = gameEvents;
    }

    public string GenerateTweet()
    { 
        if (Random.value > .85)
        {
            if (CountNumberOfKills() < 5)
            {
                return lowKills.GetRandom();
            }
        }
        else
        {
            var firstEvent = GetEventFromFirstHalf();
            var seccondEvent = GetEventFromSecondHalf();
             
            if (firstEvent.EventType.IsFight() && seccondEvent.EventType.IsLocation())
            {
                return BattleEvent(firstEvent, seccondEvent);
            }
        }

        return BasicEvent(GameEvents.GetRandom());
    }

    private string BasicEvent(GameEvent gameEvent) {
        if (gameEvent.EventType.IsDeath())
        {
            return basicDeathDescription.GetRandom(); 
        }

        return basicDeathDescription.GetRandom();
    }


    private string BattleEvent(GameEvent battle, GameEvent location)
    {
        var builder = new StringBuilder();

        if (Random.value > .5)
        {
            var name = battle.EventType.IsBoss() 
                        ? specialBossNames.GetRandom() 
                        : specialBasicNames.GetRandom();
            builder.Append("The ")
                   .Append(name)
                   .Append(" ");
        }
        else
        {
            builder.Append("The byter ");
        }

        if (battle.EventType.IsDeath())
        {
            builder.Append(deathEnemyDescription.GetRandom())
                    .Append(" ");
        }
        else
        {
            builder.Append(killEnemyDescription.GetRandom())
                   .Append(" ");
        }

        builder.Append(travelEnemyDescription.GetRandom())
            .Append(" ")
            .Append(location.Description);


        return builder.ToString();
    }

    private GameEvent GetEventFromFirstHalf()
    {
        return GameEvents[(int)Mathf.Floor(Random.Range(0, GameEvents.Count / 2))];
    }

    private GameEvent GetEventFromSecondHalf()
    {
        return GameEvents[(int)Mathf.Floor(Random.Range(GameEvents.Count / 2, GameEvents.Count))];
    }

    private int CountNumberOfKills()
    {
        var kills = 0;
        foreach(var gameEvent in GameEvents)
        {
            if (gameEvent.EventType.IsFight() && !gameEvent.EventType.IsDeath())
            {
                kills++;
            }
        }

        return kills;
    }
}
