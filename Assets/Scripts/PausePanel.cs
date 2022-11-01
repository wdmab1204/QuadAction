using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    public Button continueBtn;
    public Button restartBtn;
    public Button exitBtn;

    public void Continue()
    {
        GameManager.Instance.AnimatingPausePanel(true);
    }

    public void Restart()
    {
        //버그가많다
        //UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.name);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void Start()
    {
        continueBtn.onClick.AddListener(Continue);
        restartBtn.onClick.AddListener(Restart);
        exitBtn.onClick.AddListener(Exit);
    }
}
