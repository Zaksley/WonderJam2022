using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectUI : MonoBehaviour
{
    private Canvas _canvas;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        _canvas.enabled = (GlobalVariable.ObjectSelected != null);
    }
}
