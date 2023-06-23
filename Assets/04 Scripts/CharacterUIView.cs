using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUIView : MonoBehaviour
{
    public PlayerCharacterModel model;
    public GameObject boxPrefab;

    CheckBox[] checkboxArray;
    bool isLoaded = false;
    [SerializeField] CheckBox selectedCheckbox;
    private void OnEnable()
    {
        if (isLoaded) return;

        var characterArray = model.MakeDataFromPlayerModels();
        checkboxArray = new CheckBox[characterArray.Length];

        for(int i=0; i<characterArray.Length; i++)
        {
            //박스 오브젝트 생성 및 Transform 재설
            var box = Instantiate(boxPrefab);                   
            box.transform.parent = this.transform;
            box.transform.localPosition = new Vector3(box.transform.position.x, box.transform.position.y, 2275);
            box.transform.localScale = new Vector3(75, 75, 75);
                    
            //캐릭터 모델 생성 및 Transform 재설정
            var characterObject = Instantiate(characterArray[i].Value);
            characterObject.transform.parent = box.transform.GetChild(0);
            characterObject.transform.localScale = Vector3.one;
            characterObject.transform.rotation = Quaternion.Euler(-90, 0, 180);
                 
            //Text 설정
            var textUI = box.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
            textUI.text = characterObject.name.Replace("(Clone)", "");

            checkboxArray[i] = box.transform.GetChild(1).GetComponent<CheckBox>();
            CheckBox myself = checkboxArray[i];
            checkboxArray[i].CheckboxOn.AddListener(() => CheckboxUpdate(myself));
        }

        CheckboxUpdate(checkboxArray[0]);
        isLoaded = true;
    }

    public void CheckboxUpdate(CheckBox select)
    {
        foreach (var check in checkboxArray)
        {
            if (check != select)
            {
                check.Off();
            }
        }

        GameObject playerModel = select.transform.parent.GetChild(0).GetChild(0).gameObject;
        print(playerModel.name);
        GameData.CharacterName.Value = playerModel.name.Replace("(Clone)", "");

        selectedCheckbox = select;
    }
}
                