using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainCanvas;

    [SerializeField]
    private GameObject _creditCanavas;
    public void PlayGame()
    {
        SceneManager.LoadScene("Level0.5");
    }
    
    public void Credit()
    {
        _mainCanvas.SetActive(false);
        _creditCanavas.SetActive(true);
    }

    public void Return()
    {
        _mainCanvas.SetActive(true);
        _creditCanavas.SetActive(false);
    }
}
