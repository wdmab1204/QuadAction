using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    public GameObject characterUI;
    public GameObject opening;

    public void LoadScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void ShowCharacterUI()
    {
        characterUI.SetActive(true);
        opening.SetActive(false);
    }

    public void HideCharacterUI()
    {
        characterUI.SetActive(false);
        opening.SetActive(true);
    }
}