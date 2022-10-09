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

    [SerializeField]
    private float speedTransparence;

    private float alphaImage=1;
    private Color color;
    bool dissapear = false;
    
    private void Start()
    {
        color = GetComponent<Color>();
    }
    private void Update()
    {
        StartCoroutine(HideDevScreen());

        //alphaImage = alphaImage - speedTransparence;
       // devScreen.color = new Color(devScreen.color.r, devScreen.color.g, devScreen.color.b, alphaImage); ;

    }

    IEnumerator HideDevScreen()
    {
        yield return new WaitForSeconds(2f);
        devScreen.gameObject.SetActive(false);
        yield break;
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
