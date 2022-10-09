using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEnd : MonoBehaviour
{
    [SerializeField]
    private TerminalManager terminalManager;
    private TextColorizer textColorizer = new TextColorizer();

    [SerializeField]
    private string _endSentence;

    [SerializeField] private Texture2D _spriteCursor; 

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(_spriteCursor, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true; 
        terminalManager.Log($"{textColorizer.Cyan("[System] The anomaly is nowhere to be found.")}");
        terminalManager.Log($"{textColorizer.Cyan("[System] Hypothesis : bug fixed by an external source.")}");
        terminalManager.Log($"{textColorizer.Cyan("[System] Begining restoration of the system-world.")}");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
