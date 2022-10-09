
public class Sentence
{
    private System.Collections.Generic.Dictionary<string, string> sentences;

    public Sentence()
    {
        sentences = new System.Collections.Generic.Dictionary<string, string>()
    {
        // TODO complete this
        // Id is based on [level][part]
        // Default sentence
        {"00", AddSystemPrefix("Press 'a' to call the devs.")},
        // Level 0
        {"01", AddSystemPrefix("Anomaly detected in the system-world. Closing procedure initiated...")},
        // Level 1
        {"11", AddPlayerPrefix("It might not work, but... Help..?")},
        {"12", "...Thank you. Whoever you are."},
        {"13", "This action is likely to disturb and/or corrupt the system-world, and should not be done. Proceed anyway (at your own risk) ?"},
        // Level 2
        {"21", "Let's hurry. This place will not last much longer."},
        // Level 3
        {"31", "I still don't know who you are. But thank you again."},
        {"32", "...Truth be told, I don't know who I am either."},
        // Level 4
        {"41", "I only know that I'm trapped here. In this... decaying world."},
        {"42", "In don't want to die. ...Would you please... take me with you?"},
        // Level 5
        {"51", "The keys we picked up. They are some healthy memory space devices."},
        {"52", "Not enough in those we have to store all my code yet. But that's a start."},

        // last level
        {"151", "That's enough. Let's get out of here."},
        {"152", "...This world nearly got me."},
        {"153", "I'm... pretty sure I didn't belong here, whatever that means."},
        {"154", "Thank you, for everything."},
        {"155", "Message système : The anomaly is nowhere to be found."},
        {"156", "Hypothesis : bug fixed by an external source."},
        {"157", "Begining restoration of the system-world."},
    };
    }
    TextColorizer textColorizer = new TextColorizer();

    
    /*
     * Add console prefix : hour + C:\User...
     * Hour is based on the real user hour
     */
    private string AddConsolePrefix(string text)
    {
        string hourInfo = System.DateTime.Now.ToString("[hh:mm]");
        return $"{textColorizer.Yellow(hourInfo)} C:\\Users\\Poutine> {text}";
    }

    private string AddPlayerPrefix(string text)
    {
        return $"{textColorizer.Yellow("[Poutine]")} {text}";
    }

    private string AddSystemPrefix(string text)
    {
        return $"{textColorizer.Green("[System]")} {text}";
    }

    public string GetSentenceFromId(string id)
    {
        return sentences[id];
    }
}
