using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TerminalManager : MonoBehaviour
{

    TextColorizer textColorizer = new TextColorizer();
    Sentence sentences = new Sentence();


    // References to our prefab
    public GameObject terminalLine;
    public GameObject linesContainer;

    // Writing variables
    private TextMeshProUGUI textContainer;
    private List<string> pendingText = new List<string>();
    private string text;
    private int indexWritten;
    public float TIME_PER_CHAR = 0.05f;
    private float timer;
    private float oldSize;
    public bool isWriting = false;

    int a = 0;
    private List<string> aa = new List<string>();

    private void Start()
    {
        aa.Add("aaaaaaaaaaaaaa");
        aa.Add("bbbbbbbbbbbbb");
        aa.Add("ccccccccccccc");
        aa.Add("dddddddddddd");
    }
    private void Update()
    {
        // always check if text must be written
        Write();

        // This is how you can use the terminal
        // TODO : size of text and terminal must be setup in the main scene
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // this.Log($"{textColorizer.Red("ENZO")}HELLO WORLD");
            this.Log(aa[a]);
            a++;
            if (a == 4) {
                a = 0;
            }
        }

        if (Input.GetKeyDown("e")) {
            WriteAll();
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
    * Public interface to Log
    */

    public void LogSentence(string id)
    {
        Log(sentences.GetSentenceFromId(id));
    }

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
            // newLine.transform.SetSiblingIndex(linesContainer.transform.childCount - 1);
            newLine.transform.SetSiblingIndex(0);

            // Add writer to the textContainer
            AddWriter(newLine.GetComponentInChildren<TextMeshProUGUI>(), text);

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
    }
}
