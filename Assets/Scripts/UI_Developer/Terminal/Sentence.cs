using UnityEngine;

public class Sentence
{
    private System.Collections.Generic.Dictionary<string, string> sentences;

    public Sentence()
    {
        sentences = new System.Collections.Generic.Dictionary<string, string>()
    {
        // Id is based on (type)[level-order][part]
        // Level 0
        {"01", SystemWarning("Anomaly detected in the system-world. Closing procedure initiated...")},
        {"02", Player("...Uh ?")},
        // Level 1
        {"10", Player("...The platform has collapsed. I'm stuck.")},
        {"11", Player("It might not work, but... Help..?")},
        {"12", Player("Is anyone here? Can you do anything?")},
        {"s11", SystemInfo("Developper tools enabled. Press [A] to switch betweens UIs.")},
        {"s12", SystemWarning("USER WARNING.")},
        {"s13", SystemWarning("Editing this interface is likely to disturb and/or corrupt the system-world, and should not be done. Proceed anyway (at your own risk) ?")},
        {"12", Player("...Thank you. Whoever you are.")},
        
        // Level 3
        {"21", Player("Let's hurry. This place will not last much longer.")},
        // Level 5
        {"31", Player("I still don't know who you are. But thank you again.")},
        {"32", Player("...Truth be told, I don't know who I am either.")},
        // Level 7
        {"41", Player("I only know that I'm trapped here. In this... decaying world.")},
        {"42", Player("In don't want to die. ...Would you please... take me with you?")},
        // Level 9
        {"51", Player("The keys we picked up. They are some healthy memory space devices.")},
        {"52", Player("Not enough in those we have to store all my code yet. But that's a start.")},

        // Level 11
        {"61", Player("Keep going. Those are the last healthy things remaining in this world.")},

        // Level 13
        {"71", Player("Almost done.")},

        // last level (14)
        {"151", Player("That's enough. Let's get out of here.")},
        {"152", Player("...This world nearly got me.")},
        {"153", Player("I'm... pretty sure I didn't belong here, whatever that means.")},
        {"154", Player("Thank you, for everything.")},
        {"155", SystemWarning("The anomaly is nowhere to be found.")},
        {"156", SystemWarning("Hypothesis : bug fixed by an external source.")},
        {"157", SystemWarning("Begining restoration of the system-world Poutine.sw.")},

        // Other
        {"objMov1", SystemWarning("Forbidden object update.")},
        {"objMov2", SystemWarning("Unauthorized action.")},
        {"objMov3", SystemWarning("You can't do that.")},
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

    private string Player(string text) // AddPlayerPrefix
    {
        return $"{textColorizer.Yellow("[REM]")} {text}";
    }

    private string SystemWarning(string text) //AddSystemPrefix
    {
        return $"{textColorizer.Red($"[System] {text}")}";
    }

    private string SystemInfo(string text) //AddSystemPrefix
    {
        return $"{textColorizer.Cyan($"[System] {text}")}";
    }

    public string GetSentenceFromId(string id)
    {
        return sentences[id];
    }

    public string GetRandomBugSentence()
    {
        int value = Random.Range(0, 3);
        switch(value)
        {
            case 0: return sentences["objMov1"];
            case 1: return sentences["objMov2"];
            case 2: return sentences["objMov3"];
            default: return sentences["objMov1"];
        }
    }
}
