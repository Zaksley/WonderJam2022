using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cakeslice;

public class DisableOutline : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<cakeslice.Outline>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
