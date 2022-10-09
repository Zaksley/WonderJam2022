using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField]
    private Image devScreen;

    [SerializeField]
    private GameObject _mainCanvas;

    [SerializeField]
    private GameObject _creditCanavas;

    private void Update()
    {
        
    }

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
