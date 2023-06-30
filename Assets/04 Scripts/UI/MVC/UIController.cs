using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public PlayerCharacterModel playerCharacterModel;
    public CharacterUIView characterUIView;
    private ViewData viewData;

    //Unchecked all checkboxs except with parameter id.
    private void CheckboxUpdate(int id)
    {
        CheckBox checkbox = null;
        for (int i=0; i< viewData.checkboxArray.Length; i++)
        {
            if (viewData.checkboxArray[i].GetInstanceID() != id)
            {
                viewData.checkboxArray[i].Off();
            }
            else checkbox = viewData.checkboxArray[i];
        }

        //If not found checkbox object in Hireachy, variable reference array to index 0.
        if (id == PlayerCharacterModel.UNDEFINEDID)
        {
            checkbox = viewData.checkboxArray[0];
            viewData.checkboxArray[0].On();
        }

        GameObject playerModel = checkbox.transform.parent.GetChild(0).GetChild(0).gameObject;
        print(playerModel.name);
        GameData.CharacterName.Value = playerModel.name.Replace("(Clone)", "");
    }

    public void SetSelectInstanceID(CheckBox checkbox)
    {
        playerCharacterModel.selectID.Value = checkbox.GetInstanceID();
    }

    private void Start()
    {   // Create Character UI
        viewData = characterUIView.CreateView(playerCharacterModel);

        //Initialize Delegate Function
        playerCharacterModel.selectID.onChange += id => CheckboxUpdate(id);
        for (int i = 0; i < viewData.checkboxArray.Length; i++)
        {
            viewData.checkboxArray[i].CheckboxOn.AddListener(self => SetSelectInstanceID(self));
        }

        CheckboxUpdate(playerCharacterModel.selectID.Value);
    }

    public void ShowCharacterUI()
    {
        characterUIView.gameObject.SetActive(true);
    }

    public void HideCharacterUI()
    {
        characterUIView.gameObject.SetActive(false);
    }
}
