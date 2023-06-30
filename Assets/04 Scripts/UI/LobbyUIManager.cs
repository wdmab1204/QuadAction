using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LobbyUIManager : MonoBehaviour
{
    public UIController characterUI;
    public GameObject opening;
    public GameObject titleImage;

    private void Start()
    {
        //animate title image
        titleImage.transform.DOScale(0.8f, 1.0f).From().SetEase(Ease.OutQuad);
    }

    //Start button onclick event
    public void LoadScene()
    {
        SceneManager.LoadScene("Main");
    }

    //Character UI button onclick event
    public void ShowCharacterUI()
    {
        characterUI.ShowCharacterUI();
        opening.SetActive(false);
    }

    //X button from Character UI onclick event
    public void HideCharacterUI()
    {
        characterUI.HideCharacterUI();
        opening.SetActive(true);
    }
}