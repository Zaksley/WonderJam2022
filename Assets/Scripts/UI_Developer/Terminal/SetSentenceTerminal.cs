using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSentenceTerminal : MonoBehaviour
{
    [SerializeField]
    private string _sentenceId;

    private bool enabled = true;

    public TerminalManager terminalManagerObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enabled)
        {
            terminalManagerObject.LogSentence(_sentenceId);
            enabled = false;
        }

    }

}
