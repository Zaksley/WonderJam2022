using System.Collections.Generic;
using UnityEngine;

public class ObjectSelected : MonoBehaviour
{
    private bool _selectedState = false;
    [SerializeField] private List<GameObject> _childrenUI = new List<GameObject>(); 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _selectedState = !_selectedState; 
            EnableUI();
        }

        if (GameManager.State != GameManager.PlayerState.DEVELOPER && _selectedState)
        {
            _selectedState = false; 
            EnableUI();
        }
    }

    private void EnableUI()
    {
        for (int indexUI = 0; indexUI < _childrenUI.Count; indexUI++)
        {
            _childrenUI[indexUI].SetActive(_selectedState);
        }
    }
}
