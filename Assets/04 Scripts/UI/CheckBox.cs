using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckBox : MonoBehaviour
{
    GameObject checkImage;
    public UnityEvent<CheckBox> CheckboxOn;
    public UnityEvent<CheckBox> CheckboxOff;

    private void Awake()
    {
        checkImage = transform.GetChild(0).gameObject;
    }

    public void OnClickCheckBox()
    {
        if (checkImage.activeSelf == true)
        {
            checkImage.SetActive(false);
            CheckboxOff?.Invoke(this);
        }
        else
        {
            checkImage.SetActive(true);
            CheckboxOn?.Invoke(this);
        }
    }

    public void On()
    {
        checkImage.SetActive(true);
    }

    public void Off()
    {
        checkImage.SetActive(false);
    }
}
