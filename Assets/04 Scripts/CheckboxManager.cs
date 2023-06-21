using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckboxManager : MonoBehaviour
{
    public CheckBox[] chks;
    public CheckBox selectedCheckbox;

    private void Start()
    {
        if (selectedCheckbox != null)
        {
            CheckboxUpdate(selectedCheckbox);
        }
        else
        {
            selectedCheckbox = chks[0];
            CheckboxUpdate(selectedCheckbox);
        }
    }

    public void CheckboxUpdate(CheckBox select)
    {
        foreach(var check in chks){
            if (check != select)
            {
                check.Off();
            }
        }

        GameObject playerModel = select.transform.parent.GetChild(0).GetChild(0).gameObject;
        print(playerModel.name);
        GameData.playableModel.Value = playerModel;

        selectedCheckbox = select;
    }


}
