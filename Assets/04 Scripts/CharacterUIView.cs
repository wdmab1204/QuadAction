using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUIView : MonoBehaviour
{
    public PlayableCharacterModel model;
    public GameObject boxPrefab;
    bool isLoaded = false;
    private void OnEnable()
    {
        if (isLoaded) return;



        isLoaded = true;
    }
}
