using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableUIGameObject : MonoBehaviour
{
    public GameObject ObjectToSelect;
    public Button ButtonToSelect;

    private bool _mouseOverUI = false;
    private bool _clickOnChildren = false;
    private bool _mouseIsExit = false;
    public bool _isSelected = false;

    [SerializeField] private List<GameObject> _childrenUI = new List<GameObject>();

    private void Start()
    {
        UpdateUIObject(false);
    }

    void Update()
    {
        _mouseOverUI = MouseOverUI();

        // Manage the developer/plateformer mode of the game
        if (GameManager.State != GameManager.PlayerState.DEVELOPER && _isSelected)
        {
            UnselectAll();
            return;
        }

        if (Input.GetMouseButtonDown(0) &&_mouseIsExit)
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

            if(rayHit.transform.gameObject != ObjectToSelect.gameObject)
            {
                if (rayHit.transform != null)
                {
                    _clickOnChildren = ClickOnChildren(rayHit);

                    if (!_clickOnChildren)
                    {
                        UnselectAll();

                    }
                }
                else
                {
                    UnselectAll();
                }
            }
        } 
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

        //    if (rayHit.transform != null)
        //    {
        //        _clickOnChildren = ClickOnChildren(rayHit);

        //        if (!_clickOnChildren)
        //        {
        //            UnselectAll();

        //        }
        //    }
        //    else
        //    {
                
        //            UnselectAll();
                
        //    }
        //}
    }

    private bool MouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    // Check if we clicked on a children
    private bool ClickOnChildren(RaycastHit2D rayHit)
    {
        foreach (GameObject obj in _childrenUI)
        {
            if (rayHit.transform.gameObject == obj)
            {
                return true;
            }
        }

        return false;
    }

    private void EnableUIObject(bool stateObject)
    {
        for (int indexUI = 0; indexUI < _childrenUI.Count; indexUI++)
        {
            _childrenUI[indexUI].SetActive(stateObject);
        }
    }

    public void SelectGameObject()
    {
        //Debug.Log("click button");
        //if (GlobalVariable.ButtonSelected != null || !_isSelected)
        //{
        //    UnSelectButton();
        //}

        if (GlobalVariable.ObjectSelected != null)
        {
            UnSelectGameObject();

        }
        ObjectToSelect.GetComponent<cakeslice.Outline>().enabled = true;
        GlobalVariable.ObjectSelected = ObjectToSelect;
        UpdateUIObject(true);

    }

    public void ClickButton()
    {
            SelectGameObject();
            SelectButton();
            UpdateUIObject(true);
    }

    public void ClickGameObject()
    {
        SelectGameObject();
        SelectButton();
        UpdateUIObject(true);
    }

    public void UnSelectButton()
    {
        if (GlobalVariable.ButtonSelected != null)
        {
            var colors = GlobalVariable.ButtonSelected.colors;
            colors.normalColor = Color.white;
            GlobalVariable.ButtonSelected.colors = colors;
            GlobalVariable.ButtonSelected = null;
        }
    }

    private void UnselectAll()
    {
        Debug.Log("unselect all");
        UpdateUIObject(false);
        UnSelectButton();
        UnSelectGameObject();
    }

    private void UpdateUIObject(bool state)
    {
        _isSelected = state;
        EnableUIObject(_isSelected);
    }

    public void UnSelectGameObject()
    {
        GlobalVariable.ObjectSelected.GetComponent<cakeslice.Outline>().enabled = false;
        GlobalVariable.ObjectSelected = null;
    }

    public void SelectButton()
    {
        if (GlobalVariable.ButtonSelected != null)
        {
            UnSelectButton();
        }
        ButtonToSelect.Select();
        var colors = ButtonToSelect.colors;
        colors.normalColor = Color.red;
        ButtonToSelect.colors = colors;
        GlobalVariable.ButtonSelected = ButtonToSelect;
    }

    private void OnMouseDown()
    {
        ClickGameObject();
        //UpdateSelection();
        //SelectGameObject();
        //SelectButton();
    }
    private void OnMouseExit()
    {
        _mouseIsExit = true;
    }

    private void OnMouseEnter()
    {
        _mouseIsExit = false;
    }
}
