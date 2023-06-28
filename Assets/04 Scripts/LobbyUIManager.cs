using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    public UIController characterUI;
    public GameObject opening;

    public void LoadScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void ShowCharacterUI()
    {
        characterUI.ShowCharacterUI();
        opening.SetActive(false);
    }

    public void HideCharacterUI()
    {
        characterUI.HideCharacterUI();
        opening.SetActive(true);
    }
}