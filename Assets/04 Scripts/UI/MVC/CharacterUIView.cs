using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewData
{
    public CheckBox[] checkboxArray;
}

public class CharacterUIView : MonoBehaviour
{
    [SerializeField] Transform parentWithHorizontalLayout;

    //UI data

    public ViewData CreateView(PlayerCharacterModel model)
    {
        var characterArray = model.playerModels;
        ViewData viewData = new ViewData();
        viewData.checkboxArray = new CheckBox[model.playerModels.Length];

        for (int i = 0; i < characterArray.Length; i++)
        {
            //박스 오브젝트 생성 및 Transform 재설
            var box = Instantiate(model.boxPrefab);
            box.transform.parent = parentWithHorizontalLayout;
            box.transform.localPosition = new Vector3(box.transform.position.x, box.transform.position.y, 2275);
            box.transform.localScale = new Vector3(75, 75, 75);

            //캐릭터 모델 생성 및 Transform 재설정
            var characterObject = Instantiate(characterArray[i]);
            characterObject.transform.parent = box.transform.GetChild(0);
            characterObject.transform.localScale = Vector3.one;
            characterObject.transform.rotation = Quaternion.Euler(-90, 0, 180);

            //Text 설정
            var textUI = box.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
            textUI.text = characterObject.name.Replace("(Clone)", "");

            viewData.checkboxArray[i] = box.transform.GetChild(1).GetComponent<CheckBox>();
        }

        return viewData;
    }
}
                