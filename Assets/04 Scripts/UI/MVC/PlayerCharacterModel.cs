using System;
using System.Runtime.Serialization;
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

    public Data(T t) => v = t;
    public Data() { }

    public Action<T> onChange;

    public override string ToString()
    {
        return v.ToString();
    }
}

[CreateAssetMenu(fileName = "Character UI List", menuName = "Data/Character UI List")]
public class PlayerCharacterModel : ScriptableObject
{
    public static readonly int UNDEFINEDID = int.MinValue;

    public GameObject[] playerModels;
    public Data<int> selectID = new Data<int>(UNDEFINEDID);
    public GameObject boxPrefab;
}

public static class GameData
{
    private static Data<String> characterName = new Data<String>();
    public static Data<String> CharacterName
    {
        get { return characterName; }
        set { characterName = value; }
    }
}