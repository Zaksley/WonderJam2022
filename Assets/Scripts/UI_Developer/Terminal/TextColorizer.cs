public class TextColorizer
{
    public TextColorizer()
    {

    }

    // Theme for colors, default is white
    public System.Collections.Generic.Dictionary<string, string> colors = new System.Collections.Generic.Dictionary<string, string>()
    {
        {"red", "#AF7176"},
        {"green", "#13A10E"},
        {"yellow", "#C19C00"},
        {"blue", "#0037DA"},
        {"purple", "#881798"},
        {"cyan", "#3A96DD"},
    };

    /*
     * Add heavy string tags to color the text
     * Theme is defined by colors variable
     * NB : if text start with color, text is consered as 0
     * and this create visual bug with offcet
     */
    private string ColorString(string text, string color)
    {
        return " <color=" + colors[color] + ">" + text + "</color>";
    }

    /*
    * Public interface to use colors
    */
    public string CustomColor(string text, string hexColorCode)
    {
        return $" <color={hexColorCode}>{text}</color>";
    }

    public string Red(string text)
    {
        return ColorString(text, "red");
    }

    public string Green(string text)
    {
        return ColorString(text, "green");
    }

    public string Yellow(string text)
    {
        return ColorString(text, "yellow");
    }

    public string Blue(string text)
    {
        return ColorString(text, "blue");
    }

    public string Purple(string text)
    {
        return ColorString(text, "purple");
    }

    public string Cyan(string text)
    {
        return ColorString(text, "cyan");
    }

}
