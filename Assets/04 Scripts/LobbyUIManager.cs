using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    public GameObject characterUI;
    public GameObject opening;

    public UIController characterUIController;

    public void LoadScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void ShowCharacterUI()
    {
        //characterUI.SetActive(true);
        characterUIController.ShowCharacterUI();
        opening.SetActive(false);
    }

    public void HideCharacterUI()
    {
        //characterUI.SetActive(false);
        characterUIController.HideCharacterUI();
        opening.SetActive(true);
    }
}