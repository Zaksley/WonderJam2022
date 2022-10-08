using System;
using UnityEngine;

public class ObjectsInCameraScreen : MonoBehaviour
{
    private Vector2 _screenBounds;
    private float _objectWidth; 
    private float _objectHeight; 
    
    void Start()
    {
        _screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        _objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2f; 
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, _screenBounds.x * -1 + _objectWidth, _screenBounds.x - _objectWidth); 
        viewPos.y = Mathf.Clamp(viewPos.y, _screenBounds.y * -1 + _objectHeight, _screenBounds.y - _objectHeight);
        transform.position = viewPos; 
    }
}
