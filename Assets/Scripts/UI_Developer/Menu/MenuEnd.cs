using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEnd : MonoBehaviour
{
    [SerializeField]
    private TerminalManager terminalManager;

    [SerializeField]
    private string _endSentence;


    // Start is called before the first frame update
    void Start()
    {
        terminalManager.Log("The anomaly is nowhere to be found.");
        terminalManager.Log("Hypothesis : bug fixed by an external source.");
        terminalManager.Log("Begining restoration of the system-world.");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
