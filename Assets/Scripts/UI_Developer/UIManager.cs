using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //[SerializeField] AudioSource AudioGeneral;
    //[SerializeField] GameObject SoundOn;
    //[SerializeField] GameObject SoundOff;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Quit();
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    
    
    //public void CutSound()
    //{
    //    AudioGeneral.;
    //}

    //public void PlayerSound()
    //{
    //    AudioGeneral.Play();
    //}
}
