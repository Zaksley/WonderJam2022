using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TerminalManager : MonoBehaviour
{

    // References to our prefab
    public GameObject text;
    public ScrollRect sr;
    public GameObject linesContainer;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
        AddLine("HELLO");

        }
    }

    private void AddLine(string newText)
    {
        // Scale scroll rect depending on the content
        Vector2 linesContainerSize = linesContainer.GetComponent<RectTransform>().sizeDelta;
        linesContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(linesContainerSize.x, linesContainerSize.y + 30.0f);

        // Create a new line
        GameObject newLine = Instantiate(text, linesContainer.transform);

        // Set child index to respect the console line order
        newLine.transform.SetSiblingIndex(linesContainer.transform.childCount -1);

        // Set the text
        newLine.GetComponentInChildren<TextMeshProUGUI>().text = newText;
    }

}
