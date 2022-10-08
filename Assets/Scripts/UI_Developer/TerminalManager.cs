using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TerminalManager : MonoBehaviour
{
    // Theme for colors, default is white
    System.Collections.Generic.Dictionary<string, string> colors = new System.Collections.Generic.Dictionary<string, string>()
    {
        {"red", "#AF7176"},
        {"green", "#13A10E"},
        {"yellow", "#C19C00"},
        {"blue", "#0037DA"},
        {"purple", "#881798"},
        {"cyan", "#3A96DD"},
    };

    // References to our prefab
    public GameObject textPrefab;
    public ScrollRect sr;
    public GameObject linesContainer;

    int a = 0;
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            this.Log("HELLO");
        }
    }

    private string FormatConsoleText(string text)
    {
        a = a + 1;
        string hourInfo = System.DateTime.Now.ToString("[hh:mm]");
        return $"{Yellow(hourInfo)} C:\\Users\\Poutine> {text} {a}";
    }

    /*
    * Public interface to Log and manage text color
    */

    public void Log(string text)
    {
        // Scale scroll rect depending on the content
        Vector2 linesContainerSize = linesContainer.GetComponent<RectTransform>().sizeDelta;
        linesContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(linesContainerSize.x, linesContainerSize.y + 35.0f);

        // Create a new line
        GameObject newLine = Instantiate(textPrefab, linesContainer.transform);

        // Set child index to respect the console line order
        newLine.transform.SetSiblingIndex(linesContainer.transform.childCount - 1);

        // Set the text
        newLine.GetComponentInChildren<TextMeshProUGUI>().text = FormatConsoleText(text);

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

    private string ColorString(string text, string color)
    {
        return "<color=" + color + ">" + text + "</color>";
    }
}
