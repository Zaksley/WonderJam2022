using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectUI : MonoBehaviour
{
    private Canvas _canvas;

    [SerializeField]
    private GameObject CollisionToggleGameObject;

    [SerializeField]
    private GameObject GravityToggleGameObject;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        if(GlobalVariable.ObjectSelected != null)
        {
            _canvas.enabled=true;
            CollisionToggleGameObject.SetActive(GlobalVariable.ObjectSelected.GetComponent<ObjectProperties>().collision);
            GravityToggleGameObject.SetActive(GlobalVariable.ObjectSelected.GetComponent<ObjectProperties>().gravity);
        }
        else
        {
            _canvas.enabled = false;
        }
    }
}
