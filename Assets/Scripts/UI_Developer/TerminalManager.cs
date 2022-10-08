using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TerminalManager : MonoBehaviour
{
    // References to our prefab
    public GameObject textPrefab;
    public ScrollRect sr;
    public GameObject linesContainer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Log("HELLO");
        }
    }

    public void Log(string text)
    {
        // Scale scroll rect depending on the content
        Vector2 linesContainerSize = linesContainer.GetComponent<RectTransform>().sizeDelta;
        linesContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(linesContainerSize.x, linesContainerSize.y + 30.0f);

        // Create a new line
        GameObject newLine = Instantiate(textPrefab, linesContainer.transform);

        // Set child index to respect the console line order
        newLine.transform.SetSiblingIndex(linesContainer.transform.childCount - 1);

        // Set the text
        newLine.GetComponentInChildren<TextMeshProUGUI>().text = FormatConsoleText(text);

    }

    private string FormatConsoleText(string text)
    {
        System.DateTime currentTime = System.DateTime.Now;
        return "<color=#FF0000>" + currentTime.Hour.ToString() + "</color>" ;
    }
}
