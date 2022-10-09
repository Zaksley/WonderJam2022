using UnityEngine;
using TMPro;

public class TerminalManager : MonoBehaviour
{
    // Theme for colors, default is white
    private System.Collections.Generic.Dictionary<string, string> colors = new System.Collections.Generic.Dictionary<string, string>()
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
    public bool isWriting = false;

    private void Update()
    {
        // always check if text must be written
        Write();

        // This is how you can use the terminal
        // TODO : size of text and terminal must be setup in the main scene
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isWriting)
            {
                this.Log($"H");
            }
            else
            {
                WriteAll();
            }
        }
    }

    /*
    * Setup writer to the specified textContainer
    */
    private void AddWriter(TextMeshProUGUI _textContainer, string _text)
    {
        this.textContainer = _textContainer;
        this.text = _text;
        this.isWriting = true;
        this.indexWritten = 0;
        this.timer = 0;
        this.oldSize = 0;
    }

    /*
    * Write letter by letter in the textContainer
    */
    private void Write()
    {
        if (isWriting)
        {
            // check if text is written
            if (indexWritten >= text.Length)
            {
                isWriting = false;
                return;
            }

            // write letter depending on writing speed
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
    * Write all letter textContainer
    */
    private void WriteAll()
    {
        if (isWriting)
        {
            textContainer.text = text;
            isWriting = false;
            UpdateTextHeight();
        }
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
        }
    }

    /*
    * Delete logs (gameObjects) that are not visible anymore
    * TODO : This can be optimised my detecting overflow on mask
    */
    private void DeleteOldLogs()
    {
        if (linesContainer.transform.childCount > 19)
        {
            Transform removedChild = linesContainer.transform.GetChild(18);

            // reduce lines container size
            float textSize = removedChild.GetComponentInChildren<TextMeshProUGUI>().preferredHeight;

            // Scale scroll rect
            Vector2 linesContainerSize = linesContainer.GetComponent<RectTransform>().sizeDelta;
            linesContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(linesContainerSize.x, linesContainerSize.y - textSize - 5); // 5 is spacing in vertical layer group

            // Delete log
            Destroy(removedChild.gameObject);
        }
    }

    /*
       * Add console prefix : hour + C:\User...
       * Hour is based on the real user hour
       */
    private string AddConsolePrefix(string text)
    {
        string hourInfo = System.DateTime.Now.ToString("[hh:mm]");
        return $"{Yellow(hourInfo)} C:\\Users\\Poutine> {text}";
    }

    /*
    * Add heavy string tags to color the text
    * Theme is defined by colors variable
    * NB : if text start with color, text is consered as 0
    * and this create visual bug with offcet
    */
    private string ColorString(string text, string color)
    {
        return "<color=" + colors[color] + ">" + text + "</color>";
    }

    /*
    * Public interface to Log and use colored text
    */
    public void Log(string text)
    {
        // Create a new line
        GameObject newLine = Instantiate(terminalLine, linesContainer.transform);

        // Set child index to respect the console line order, vertical layer is reversed
        // newLine.transform.SetSiblingIndex(linesContainer.transform.childCount - 1);
        newLine.transform.SetSiblingIndex(0);

        // Add writer to the textContainer
        AddWriter(newLine.GetComponentInChildren<TextMeshProUGUI>(), AddConsolePrefix(text));

        // Get height of the text and scale scroll rect and new line container depending on the content
        float textSize = newLine.GetComponentInChildren<TextMeshProUGUI>().preferredHeight;
        oldSize = textSize;
        // Scale scroll rect
        Vector2 linesContainerSize = linesContainer.GetComponent<RectTransform>().sizeDelta;
        linesContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(linesContainerSize.x, linesContainerSize.y + textSize + 5); // 5 is spacing in vertical layer group
        // Scale new line container                                                                                                                     // Scale text container
        Vector2 newLineSize = newLine.GetComponent<RectTransform>().sizeDelta;
        newLine.GetComponent<RectTransform>().sizeDelta = new Vector2(newLineSize.x, textSize);

        // Delete logs no more visible
        DeleteOldLogs();
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
