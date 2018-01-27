using System;

public enum GameEventEnum
{
    DEATH_BASIC,
    DEATH_BOSS,
    DESTORY_TRANSMITOR,
    DISCOVER_TRANSMITOR,
    DISCOVER_RADIO_TOWER,
    KILL_BOSS,
    KILL_BASIC,
    START
};

public class GameEvent {

    public GameEventEnum EventType { get; private set; }
    public DateTime Timestamp { get; private set; }
    public String Description { get; private set; }

    public GameEvent(GameEventEnum type, String description="")
    {
        EventType = type;
        Timestamp = DateTime.Now;
        Description = "";
    }
}

public static class GameEventExtensions
{
    public static bool IsLocation(this GameEventEnum ge)
    {
        switch (ge)
        {
            case GameEventEnum.DISCOVER_RADIO_TOWER:
            case GameEventEnum.DISCOVER_TRANSMITOR:
                return true;
        }

        return false;
    }

    public static bool IsFight(this GameEventEnum ge)
    {
        switch(ge)
        {
            case GameEventEnum.KILL_BASIC:
            case GameEventEnum.KILL_BOSS:
            case GameEventEnum.DEATH_BASIC:
            case GameEventEnum.DEATH_BOSS:
                return true;
        }

        return false;
    }

    public static bool IsDeath(this GameEventEnum ge)
    {
        switch(ge)
        {
            case GameEventEnum.DEATH_BOSS:
            case GameEventEnum.DEATH_BASIC:
                return true;
        }

        return false;
    }

    public static bool IsBoss(this GameEventEnum ge)
    {
        switch(ge)
        {
            case GameEventEnum.DEATH_BOSS:
            case GameEventEnum.KILL_BOSS:
                return true;
        }

        return false;
    }
}


