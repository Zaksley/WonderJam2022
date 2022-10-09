using UnityEngine;

public class MoveObjectByArrow : MonoBehaviour
{
    public enum Direction
    {
        HORIZONTAL,
        VERTICAL
    };
    
    private Vector3 _offset;
    public Sprite GlitchedSprite;

    public Direction ArrowDirection;
    private Transform _parentTransform;
    private bool _glitched;

    void Start()
    {
        _parentTransform = transform.parent; 
        _glitched = false;
    }
    
    private void OnMouseDrag()
    {
        if(!_glitched)
        {
            SwitchToGlitchedSprite();
            _glitched = true;
        }
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

    private void SwitchToGlitchedSprite()
    {
        Debug.Log("Glitched now");
        transform.parent.GetComponent<SpriteRenderer>().sprite = GlitchedSprite;
    }

}
