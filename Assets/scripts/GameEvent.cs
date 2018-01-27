using System;

public enum GameEventEnum
{
    DEATH,
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
