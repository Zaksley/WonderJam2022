using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _menuInGame;
    public static bool StateMenuInGame = false;

    private void Start()
    {
        _menuInGame.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetMenu();
        }
    }
    
    public void SetMenu()
    {
        StateMenuInGame = !StateMenuInGame;
        _menuInGame.SetActive(StateMenuInGame);
        
        
        if (StateMenuInGame)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;

            // Disable cursor after being in the menu
            if (GameManager.State == GameManager.PlayerState.PLATEFORMER)
            {
                Cursor.visible = false;
            }
        }
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("Menu"); 
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
