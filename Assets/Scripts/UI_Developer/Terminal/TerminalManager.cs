using UnityEngine;
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
    public GameObject terminalLine;
    public GameObject linesContainer;

    // Writing variables
    private TextMeshProUGUI textContainer;
    private string text;
    private int indexWritten;
    public float TIME_PER_CHAR = 0.05f;
    private float timer;
    private float oldSize;
    public bool isReady = true;
    private void Update()
    {
        Write();

        if (Input.GetKeyDown(KeyCode.Return) && isReady)
        {
            // This is how you can use the terminal
            // TODO : size of text and terminal must be setup in the main scene
            this.Log($"{Red("ENZO")}\nHELLO WORLD");
        }
    }

    public void AddWriter(TextMeshProUGUI _textContainer, string _text)
    {
        this.textContainer = _textContainer;
        this.text = _text;
        this.isReady = false;
        this.indexWritten = 0;
        this.timer = 0;
        this.oldSize = 0;
    }


    private void Write()
    {
        if (textContainer != null && !isReady)
        {
            if (indexWritten >= text.Length)
            {
                isReady = true;
            }
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer += TIME_PER_CHAR;
                textContainer.text += text[indexWritten];
                indexWritten++;
                UpdateTextHeight();
            }
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
        return "<color=" + colors[color] + ">" + text + "</color>";
    }

    /*
    * Update text offcet when text change height while writing (multiline)
    */
    private void UpdateTextHeight()
    {
        // Get height of the text and scale scroll rect and text container depending on the content
        float textSize = textContainer.preferredHeight;

        if (textSize != oldSize)
        {
            Transform lineContainer = textContainer.transform.parent;
            // Scale scroll rect
            Vector2 linesContainerSize = linesContainer.GetComponent<RectTransform>().sizeDelta;
            linesContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(linesContainerSize.x, linesContainerSize.y + textSize - oldSize); // 5 is spacing in vertical layer group
                                                                                                                                                   // Scale text container
            Vector2 newLineSize = lineContainer.GetComponent<RectTransform>().sizeDelta;
            lineContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(newLineSize.x, newLineSize.y + textSize - oldSize);
            oldSize = textSize;

            // TODO
            // DeleteOldLogs();
        }
    }
    /*
    * TODO
    * Delete logs (gameObjects) that are not visible anymore
    */
    // private void DeleteOldLogs()
    // {
    //     // for childs
    //     // if trop loin
    //     // delete child
    //     // reduce container size
    //     foreach (Transform line in linesContainer.transform)
    //     {
    //      line.preferredHeight
    //     }
    // }

    /*
    * Public interface to Log and manage text color
    * TODO : use static keyword
    */

    public void Log(string text)
    {
        // Create a new line
        GameObject newLine = Instantiate(terminalLine, linesContainer.transform);

        // Set child index to respect the console line order
        // newLine.transform.SetSiblingIndex(linesContainer.transform.childCount - 1);
        newLine.transform.SetSiblingIndex(0);

        // Set the text
        // newLine.GetComponentInChildren<TextMeshProUGUI>().text = AddConsolePrefix(text);
        AddWriter(newLine.GetComponentInChildren<TextMeshProUGUI>(), AddConsolePrefix(text));

        // Get height of the text and scale scroll rect and text container depending on the content
        float textSize = newLine.GetComponentInChildren<TextMeshProUGUI>().preferredHeight;
        oldSize = textSize;
        // Scale scroll rect
        Vector2 linesContainerSize = linesContainer.GetComponent<RectTransform>().sizeDelta;
        linesContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(linesContainerSize.x, linesContainerSize.y + textSize + 5); // 5 is spacing in vertical layer group
        // Scale text container
        Vector2 newLineSize = newLine.GetComponent<RectTransform>().sizeDelta;
        newLine.GetComponent<RectTransform>().sizeDelta = new Vector2(newLineSize.x, textSize);
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
