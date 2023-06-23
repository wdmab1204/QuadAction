using System;
using UnityEngine;

public class Data<T>
{
    private T v;
    public T Value
    {
        get
        {
            return this.v;
        }
        set
        {
            this.v = value;
            this.onChange?.Invoke(value);
        }
    }
    public Action<T> onChange;

    public override string ToString()
    {
        return v.ToString();
    }
}

//MVC : Model
public static class GameData
{
    private static Data<String> characterName = new Data<String>();
    public static Data<String> CharacterName
    {
        get { return characterName; }
        set { characterName = value; }
    }
}

[CreateAssetMenu(fileName = "Character UI List", menuName = "Data/Character UI List")]
public class PlayerCharacterModel : ScriptableObject
{
    [SerializeField] private GameObject[] playerModels;

    public Data<GameObject>[] MakeDataFromPlayerModels()
    {
        Data<GameObject>[] dataList = new Data<GameObject>[playerModels.Length];

        for(int i=0; i<dataList.Length; i++)
        {
            dataList[i] = new Data<GameObject>();
            dataList[i].Value = playerModels[i];
        }

        return dataList;
    }
}