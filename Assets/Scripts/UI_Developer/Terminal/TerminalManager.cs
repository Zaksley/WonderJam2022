using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TerminalManager : MonoBehaviour
{

    TextColorizer textColorizer = new TextColorizer();
    Sentence sentences = new Sentence();

    // Settings
    [SerializeField]
    private float timePerChar = 0.05f;
    [SerializeField]
    private bool tutorialDisplayed = true;
    [SerializeField]
    private bool tutorialAnimation = true;
    [SerializeField]
    private int maxLineDisplayed = 19;
    // Spacing in vertical layer group, used to keep line at the same height
    // THIS DONT CHANGE DE SPACING BETWEEN LINES
    [SerializeField]
    private int verticalSpacing = 5;

    // References to our prefab
    public GameObject terminalLine;
    public GameObject linesContainer;

    // Writing variables
    private TextMeshProUGUI textContainer;
    private string text;
    private List<string> pendingText = new List<string>();
    private bool isWriting = false;
    private int indexWritten;
    private float timer;
    private float oldHeight;

    private void Start()
    {
        if (tutorialDisplayed)
        {
            LogSentence("s11");
            if (!tutorialAnimation)
            {
                WriteAll();
            }
        }
    }

    private void Update()
    {
        // always check if text must be written
        Write();

        if (Input.GetKeyDown("e"))
        {
            WriteAll();
        }
    }

    /*
    * Setup writer on the specified textContainer (terminal line)
    */
    private void SetWriter(TextMeshProUGUI _textContainer, string _text)
    {
        this.textContainer = _textContainer;
        this.text = _text;
        this.isWriting = true;
        this.indexWritten = 0;
        this.timer = 0;
        this.oldHeight = 0;
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
                // check if there is more pending text
                if (pendingText.Count > 0)
                {
                    Log(pendingText[0]);
                    pendingText.RemoveAt(0);
                }
                return;
            }

            // write letter depending on writing speed
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer += timePerChar;
                textContainer.text += text[indexWritten];
                indexWritten++;
                UpdateTextHeight();
            }
        }
    }

    /*
    * Write all letters in one time and jump to next terminal line
    */
    private void WriteAll()
    {
        if (isWriting)
        {
            isWriting = false;
            textContainer.text = text;
            UpdateTextHeight();
            if (pendingText.Count > 0)
            {
                Log(pendingText[0]);
                pendingText.RemoveAt(0);
            }
        }
    }

    /*
    * Update text offcet when text change height while writing (multiline)
    */
    private void UpdateTextHeight()
    {
        // Get height of the text and scale scroll rect and text container depending on the content
        float textSize = textContainer.preferredHeight;

        if (textSize != oldHeight)
        {
            Transform lineContainer = textContainer.transform.parent;
            // Scale scroll rect
            Vector2 linesContainerSize = linesContainer.GetComponent<RectTransform>().sizeDelta;
            linesContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(linesContainerSize.x, linesContainerSize.y + textSize - oldHeight);
            // Scale line container
            Vector2 newLineSize = lineContainer.GetComponent<RectTransform>().sizeDelta;
            lineContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(newLineSize.x, newLineSize.y + textSize - oldHeight);
            oldHeight = textSize;
        }
    }

    /*
    * Delete logs (gameObjects) that are not visible anymore
    * TODO : This can be optimised by detecting overflow on mask
    */
    private void DeleteOldLogs()
    {
        if (linesContainer.transform.childCount > maxLineDisplayed)
        {
            Transform removedChild = linesContainer.transform.GetChild(maxLineDisplayed - 1);

            // Resize scroll rect
            float textSize = removedChild.GetComponentInChildren<TextMeshProUGUI>().preferredHeight;
            Vector2 linesContainerSize = linesContainer.GetComponent<RectTransform>().sizeDelta;
            linesContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(linesContainerSize.x, linesContainerSize.y - textSize - verticalSpacing);

            // Delete log
            Destroy(removedChild.gameObject);
        }
    }



    /*
    * Public interface to Log
    */

    /*
    * Log saved sentence
    */
    public void LogSentence(string id)
    {
        Log(sentences.GetSentenceFromId(id));
    }

    /*
    * Log custom text
    * If some text is ever writting, new text is adding to a queue
    */
    public void Log(string text)
    {
        if (isWriting)
        {
            pendingText.Add(text);
        }
        else
        {
            // Create a new line
            GameObject newLine = Instantiate(terminalLine, linesContainer.transform);

            // Set child index to respect the console line order, vertical layer is reversed
            newLine.transform.SetSiblingIndex(0);

            // Set writer to the textContainer
            SetWriter(newLine.GetComponentInChildren<TextMeshProUGUI>(), text);

            // Get height of the text and scale scroll rect and new line container depending on the content
            float textSize = newLine.GetComponentInChildren<TextMeshProUGUI>().preferredHeight;
            oldHeight = textSize;
            // Scale scroll rect
            Vector2 linesContainerSize = linesContainer.GetComponent<RectTransform>().sizeDelta;
            linesContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(linesContainerSize.x, linesContainerSize.y + textSize + verticalSpacing);
            // Scale new line container
            Vector2 newLineSize = newLine.GetComponent<RectTransform>().sizeDelta;
            newLine.GetComponent<RectTransform>().sizeDelta = new Vector2(newLineSize.x, textSize);

            // Delete logs no more visible
            DeleteOldLogs();
        }
    }
}
