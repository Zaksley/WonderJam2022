using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using UnityEngine.UI;

public class SelectableUIGameObject : MonoBehaviour
{

    public GameObject ObjectToSelect;
    public Button ButtonToSelect;

    private bool _mouseIsExit = false;
    private bool _isSelected = false; 

    [SerializeField] private List<GameObject> _childrenUI = new List<GameObject>();

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && _mouseIsExit)
        {
            Debug.Log("onmousedown");
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            Debug.Log(rayHit.transform.gameObject.name);
            if (rayHit.transform.gameObject != GlobalVariable.ObjectSelected.gameObject)
            {
                UnSelectGameObject();
                UnSelectButton();
                UpdateUIObject(false);
            }
        }

        if (GameManager.State != GameManager.PlayerState.DEVELOPER && _isSelected)
        {
            UpdateUIObject(false);
            UnSelectGameObject();
            UnSelectButton();
        }

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

    public void UnSelectButton()
    {
        var colors = GlobalVariable.ButtonSelected.colors;
        colors.normalColor = Color.white;
        GlobalVariable.ButtonSelected.colors = colors;
        GlobalVariable.ButtonSelected = null;

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
        EnableUIObject(false);
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
    }
    private void OnMouseExit()
    {
        Debug.Log("exit");
        _mouseIsExit = true;

    }

    private void OnMouseEnter()
    {
        _mouseIsExit = false;
    }

}
