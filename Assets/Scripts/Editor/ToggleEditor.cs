using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleEditor : MonoBehaviour
{
    public bool CollisionTogglePossible=false;
    public bool GravityTogglePossible=false;

    [SerializeField]
    private GameObject CollisionToggle;

    [SerializeField]
    private GameObject GravityToggle;

    [SerializeField]
    private Canvas _canvas;

    private void Update()
    {
        _canvas.enabled = (GlobalVariable.ObjectSelected != null);
    }
    // Start is called before the first frame update
    void Awake()
    {
        //GravityTogglePossible = GetComponent<ObjectProperties>().gravity;
        //CollisionTogglePossible = GetComponent<ObjectProperties>().collision;

        //if (!CollisionTogglePossible)
        //{
        //    Destroy(CollisionToggle);
        //}

        //if (!GravityTogglePossible)
        //{
        //    Destroy(GravityToggle);
        //}
    }

    
}
