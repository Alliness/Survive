using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class BuildToolTipController : MonoBehaviour 
{
    public Text toolTipTitle;
    public Text toolTipDescription;
    private Object[] _requiredResoruces;


    public void Display(String title, String description) // todo add resourcesList
    {
        toolTipTitle.text = title;
        toolTipDescription.text = description;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}