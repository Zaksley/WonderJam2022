using UnityEngine;

public class MoveObjectByArrow : MonoBehaviour
{
    public enum Direction
    {
        HORIZONTAL,
        VERTICAL
    };
    
    private Vector3 _offset;
    public Direction ArrowDirection;
    private Transform _parentTransform;

    void Start()
    {
        _parentTransform = transform.parent; 
    }
    
    private void OnMouseDrag()
    {
        if (ArrowDirection == Direction.HORIZONTAL)
        {
            _parentTransform.position = new Vector3(MouseWorldPosition().x + _offset.x, _parentTransform.position.y, _parentTransform.position.z);
        }
        else if (ArrowDirection == Direction.VERTICAL)
        {
            _parentTransform.position = new Vector3(_parentTransform.position.x, MouseWorldPosition().y + _offset.y, _parentTransform.position.z);
        }
    }

    private void OnMouseDown()
    {
        _offset = _parentTransform.position - MouseWorldPosition(); 
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(_parentTransform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos); 
    }

}
