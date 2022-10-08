using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using UnityEngine.UI;

public class SelectableUIGameObject : MonoBehaviour
{

    public GameObject ObjectToSelect;
    public Button ButtonToSelect;

 
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
        
    }
    
    public void DeselectButton(Button ButtonToDeselect)
    {
        Debug.Log("deselect button");
        var colors = GlobalVariable.ButtonSelected.colors;
        colors.normalColor = Color.white;
        GlobalVariable.ButtonSelected.colors = colors;
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
