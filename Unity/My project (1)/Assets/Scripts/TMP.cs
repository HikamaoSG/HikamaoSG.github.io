using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TMP : MonoBehaviour
{
    [SerializeField] private bool TextBoxSelected;
    [SerializeField] private GameObject inputfield;

    public void OnSelected()
    {
        if (TextBoxSelected == true)
            inputfield.SetActive(false);

    }

    public void OnDeselected()
    {
        if (TextBoxSelected == false)
            inputfield.SetActive(true);

    }
}
