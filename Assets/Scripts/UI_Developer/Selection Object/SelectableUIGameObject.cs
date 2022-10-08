using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using UnityEngine.UI;

public class SelectableUIGameObject : MonoBehaviour
{

    public GameObject ObjectToSelect;
    public Button ButtonToSelect;

    private bool _selectedState = false;
    
    [SerializeField] private List<GameObject> _childrenUI = new List<GameObject>();

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    _selectedState = !_selectedState;
        //    EnableUI();
        //}

        //if (GameManager.State != GameManager.PlayerState.DEVELOPER && _selectedState)
        //{
        //    _selectedState = false;
        //    EnableUI();
        //}
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
            DeselectButton(GlobalVariable.ButtonSelected);
        }
        if (GlobalVariable.ObjectSelected != null)
        {
            GlobalVariable.ObjectSelected.GetComponent<cakeslice.Outline>().enabled = false;
        }
        ObjectToSelect.GetComponent<cakeslice.Outline>().enabled = true;
        GlobalVariable.ObjectSelected = ObjectToSelect;
        EnableUIObject(true);
        
    }
    
    public void DeselectButton(Button ButtonToDeselect)
    {
        Debug.Log("deselect button");
        var colors = GlobalVariable.ButtonSelected.colors;
        colors.normalColor = Color.white;
        GlobalVariable.ButtonSelected.colors = colors;
        EnableUIObject(false);
    }

    public void DeselectGameObject()
    {

    }

    public void SelectButton()
    {

        if (GlobalVariable.ButtonSelected != null)
        {
            DeselectButton(GlobalVariable.ButtonSelected);
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

}
