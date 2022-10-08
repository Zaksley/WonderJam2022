using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectableUIGameObject : MonoBehaviour
{
    public GameObject ObjectToSelect;
    public Button ButtonToSelect;

    private bool _clickOnChildren = false;
    private bool _mouseIsExit = false;
    private bool _isSelected = false;

    [SerializeField] private List<GameObject> _childrenUI = new List<GameObject>();

    private void Start()
    {
        UpdateUIObject(false);
    }

    void Update()
    {
        // Manage the developer/plateformer mode of the game
        if (GameManager.State != GameManager.PlayerState.DEVELOPER && _isSelected)
        {
            UnselectAll();
            return; 
        }
        
        if (Input.GetMouseButtonDown(0) && _mouseIsExit && _isSelected)
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
          
            if (rayHit.transform == null)
            {
                Debug.Log("ray update");
                UnselectAll();
                return; 
            }

            _clickOnChildren = ClickOnChildren(rayHit);
            if (!_clickOnChildren)
            {
                UnselectAll();
                return; 
            }
            
            // if (rayHit.transform.gameObject != GlobalVariable.ObjectSelected.gameObject)
            // {
            //     Debug.Log("QUEL EST CE CAS WSH");
            //     UnselectAll();
            //     return; 
            // }

                // if (rayHit.transform != null)
            // {
            //     if (rayHit.transform.gameObject != GlobalVariable.ObjectSelected.gameObject && !_clickOnChildren)
            //     {
            //         UnselectAll(); 
            //     }
            //     else
            //     {
            //         _clickOnChildren = false;
            //     }
            // }
        }
    }

    // Check if we clicked on a children
    private bool ClickOnChildren(RaycastHit2D rayHit)
    {
        foreach(GameObject obj in _childrenUI)
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
        Debug.Log("button update");
        if (GlobalVariable.ButtonSelected != null)
        {
            UnSelectButton();
        }
        if (GlobalVariable.ObjectSelected != null)
        {
            GlobalVariable.ObjectSelected.GetComponent<cakeslice.Outline>().enabled = false;
        }
        ObjectToSelect.GetComponent<cakeslice.Outline>().enabled = true;
        GlobalVariable.ObjectSelected = ObjectToSelect;
        UpdateUIObject(true);
    }

    public void UpdateSelection()
    {
        if (_isSelected)
        {
            UnselectAll();
        }
        else
        {
            Debug.Log("Selection par ici");
            SelectGameObject();
            SelectButton();
            UpdateUIObject(true);
        }
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
        var colors = ButtonToSelect.colors;
        colors.normalColor = Color.red;
        ButtonToSelect.colors = colors;
        GlobalVariable.ButtonSelected = ButtonToSelect;
    }

    private void OnMouseDown()
    {
        Debug.Log("click button");
        UpdateSelection(); 
        // SelectGameObject();
        // SelectButton();
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
