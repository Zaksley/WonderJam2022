using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TerminalManager : MonoBehaviour
{
    // Theme for colors, default is white
    System.Collections.Generic.Dictionary<string, string> colors = new System.Collections.Generic.Dictionary<string, string>()
    {
        {"rede", "#AF7176"},
        {"green", "#13A10E"},
        {"yellow", "#C19C00"},
        {"blue", "#0037DA"},
        {"purple", "#881798"},
        {"cyan", "#3A96DD"},
    };

    // References to our prefab
    public GameObject terminalLine;
    public GameObject linesContainer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // This is how you can use the terminal
            // TODO : size of text and terminal must be setup in the main scene
            this.Log($"{Red("ENZO")}\nHELLO WORLD");
            this.Log("TEST WITH A VERY LONG STRING \n TEST WITH A VERY LONG STRING TEST WITH A VERY LONG STRINGTEST WITH A VERY LONG STRINGTEST WITH A VERY LONG STRINGTEST WITH A VERY LONG STRING");
        }
    }

    /*
    * Add console prefix : hour + C:\User...
    * Hour is based on the real user your
    */
    private string AddConsolePrefix(string text)
    {
        string hourInfo = System.DateTime.Now.ToString("[hh:mm]");
        return $"{Yellow(hourInfo)} C:\\Users\\Poutine> {text}";
    }

    /*
    * Add heavy string tags to colorize the text
    * Theme is defined by colors variable
    */
    private string ColorString(string text, string color)
    {
        return "<color=" + color + ">" + text + "</color>";
    }

    /*
    * TODO
    * Delete logs (gameObjects) that are not visible anymore
    */
    private void DeleteOldLogs()
    {
    }

    /*
    * Public interface to Log and manage text color
    * TODO : use static keyword
    */

    public void Log(string text)
    {
        // Create a new line
        GameObject newLine = Instantiate(terminalLine, linesContainer.transform);

        // Set child index to respect the console line order
        newLine.transform.SetSiblingIndex(linesContainer.transform.childCount - 1);

        // Set the text
        newLine.GetComponentInChildren<TextMeshProUGUI>().text = AddConsolePrefix(text);

        // Get height of the text and scale scroll rect and text container depending on the content
        float textSize = newLine.GetComponentInChildren<TextMeshProUGUI>().preferredHeight;
        // Scale scroll rect
        Vector2 linesContainerSize = linesContainer.GetComponent<RectTransform>().sizeDelta;
        linesContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(linesContainerSize.x, linesContainerSize.y + textSize + 5); // 5 is spacing in vertical layer group
        // Scale text container
        Vector2 newLineSize = newLine.GetComponent<RectTransform>().sizeDelta;
        newLine.GetComponent<RectTransform>().sizeDelta = new Vector2(newLineSize.x, textSize);

        // TODO
        // DeleteOldLogs();
    }

    public string Red(string text)
    {
        return ColorString(text, "rede");
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
