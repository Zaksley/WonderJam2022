using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
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
        EnableUIObject(false);
       
    }
    void Update()
    {
        _clickOnChildren = false;
        if (GameManager.State != GameManager.PlayerState.DEVELOPER)
        {
          
            UnSelectGameObject();
            UnSelectButton();
            return;
        }
        if (Input.GetMouseButtonDown(0) && _mouseIsExit)
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (rayHit.transform != null)
            {
                if(GlobalVariable.ObjectSelected)
                {
                    if (rayHit.transform.gameObject != GlobalVariable.ObjectSelected.gameObject)
                    {
                        foreach (GameObject obj in GlobalVariable.listChildrenGameObject)
                        {
                            if (rayHit.transform.gameObject == obj)
                            {
                                _clickOnChildren = true;

                            }
                        }
                        Debug.Log(_clickOnChildren);
                        if (!_clickOnChildren)
                        {
                            UnSelectGameObject();
                            UnSelectButton();
                        }
                    }
                }
                else
                {
                    UnSelectGameObject();
                    UnSelectButton();
                }
                
            }
            else
            {
                UnSelectGameObject();
                UnSelectButton();
            }

        }

        
    }

    private void UnselectChildren()
    {
        for (int indexUI = 0; indexUI < GlobalVariable.listChildrenGameObject.Count; indexUI++)
        {
            GlobalVariable.listChildrenGameObject[indexUI].SetActive(false);
        }

    }
   

    private void EnableUIObject(bool stateObject)
    {
        GlobalVariable.listChildrenGameObject = new List<GameObject>(); 
        for (int indexUI = 0; indexUI < _childrenUI.Count; indexUI++)
        {
            _childrenUI[indexUI].SetActive(stateObject);
            GlobalVariable.listChildrenGameObject.Add(_childrenUI[indexUI]);

        }
        
    }


    public void SelectGameObject()
    {

        if (GlobalVariable.ButtonSelected != null)
        {
            Debug.Log("button selected");
            UnSelectButton();
        }
        if (GlobalVariable.ObjectSelected != null)
        {
            UnSelectGameObject();
        }

        ObjectToSelect.GetComponent<cakeslice.Outline>().enabled = true;
        GlobalVariable.ObjectSelected = ObjectToSelect;
        EnableUIObject(true);

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

    private void UpdateUIObject(bool state)
    {
        _isSelected = state;
        EnableUIObject(_isSelected);
    }

    public void UnSelectGameObject()
    {
        if (GlobalVariable.ObjectSelected != null)
        {
            GlobalVariable.ObjectSelected.GetComponent<cakeslice.Outline>().enabled = false;
            GlobalVariable.ObjectSelected = null;
            UnselectChildren();
        }
        
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
        
        SelectGameObject();
        SelectButton();
        EnableUIObject(true);
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

