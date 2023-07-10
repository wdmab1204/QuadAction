using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = null;

    protected void SingletonInit()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion

    #region UI
    [Header("UI")]

    [Header("Main Scene")]
    [SerializeField] private HpSlider playerHpSlider;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text systemText;
    [SerializeField] private RectTransform blackScreen;

    [Header("Produce Scene")]
    [SerializeField] private TextMeshProUGUI scoreResultText;

    [Header("Pause")]
    [SerializeField] private RectTransform pausePanel;
    #endregion

    #region GameRule
    [Header("Game Rule")]

    #region Timer
    [SerializeField] private float maxTime;
    private float currentTime;
    private IEnumerator Timer()
    {
        while (currentTime>0)
        {
            currentTime -= Time.deltaTime;
            timerText.text = (float)Math.Round(currentTime, 2) + "";
            yield return null;
        }

        GameOver();
    }

    #endregion

    #region GameOver
    public void GameOver()
    {
        StartProduce();
    }

    #endregion

    #region Score
    private int currentScore = 0;
    public void SetScore(int score)
    {
        currentScore += score;
        scoreText.text = currentScore + "";
    }
    #endregion

    #region Opening
    private void StartOpening()
    {
        Time.timeScale = 1.0f;
        SwitchingCamera(CameraType.Main_Camera);
        StartCoroutine(Timer());
    }
    #endregion

    #region Main
    private Vector3 timerTextOriginalPosition;
    private Vector3 scoreTextOriginalPosition;
    private void StartMain()
    {
        Time.timeScale = 1.0f;

        //scoreText, timerText ����ġ�� ��Ű��
        timerText.rectTransform.anchoredPosition = timerTextOriginalPosition;
        scoreText.rectTransform.anchoredPosition = scoreTextOriginalPosition;
        blackScreen.anchoredPosition = new Vector2(Screen.width, blackScreen.anchoredPosition.y);

        //scoreText, timerText, hpsliderbar �ִϸ��̼� ����
        timerText.rectTransform.DOAnchorPosX(-250.0f, 1.0f).From(true);
        scoreText.rectTransform.DOAnchorPosX(250.0f, 1.0f).From(true);
        
    }
    #endregion

    #region Produce
    Sequence ss;
    private void StartProduce()
    {
        Time.timeScale = 0.0f;
        blackScreen.gameObject.SetActive(true);
        ss = DOTween.Sequence();
        ss.SetUpdate(true);
        ss.Append(blackScreen.DOAnchorPosX(0.0f, 0.7f));
        ss.AppendCallback(
            () =>
            {
                SwitchingCamera(CameraType.Produce_Camera);
                scoreResultText.text = currentScore + "";
            });
        ss.Append(blackScreen.DOAnchorPosX(-Screen.width, 0.7f));
    }
    #endregion

    #region Camera Manage
    [Header("Camera")]
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject produceCamera;
    [SerializeField] private GameObject openingCamera;
    private enum CameraType { None, Main_Camera, Produce_Camera };
    [SerializeField] private CameraType cameraType;
    private void SwitchingCamera(CameraType cameraType)
    {
        if(cameraType == CameraType.Main_Camera)
        {
            mainCamera.SetActive(true);
            produceCamera.SetActive(false);
            openingCamera.SetActive(false);

            playerHpSlider.gameObject.SetActive(true);
            timerText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(true);
            systemText.gameObject.SetActive(true);
            blackScreen.gameObject.SetActive(true);

            scoreResultText.gameObject.SetActive(false);

            StartMain();
        }
        else if (cameraType == CameraType.Produce_Camera)
        {
            mainCamera.SetActive(false);
            produceCamera.SetActive(true);
            openingCamera.SetActive(false);

            playerHpSlider.gameObject.SetActive(false);
            timerText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
            systemText.gameObject.SetActive(false);
            //blackScreen.gameObject.SetActive(false);

            scoreResultText.gameObject.SetActive(true);
        }

        this.cameraType = cameraType;
    }
    #endregion

    #endregion

    #region Player
    public Player player;
    public PlayerCharacterModel model;
    #endregion

    public void AttackToTarget(int damage, Character target, Vector3 force)
    {
        target.Hit(damage, force);

        if (target.Hp.Value <= 0) target.Die();
    }

    public void HealToTarget(int heal, Character target)
    {
        if (target.Hp.Value + heal <= target.MaxHp.Value)
            target.Hp.Value += heal;
    }

    Sequence s;
    public void SendSystemMessage(string text)
    {
        float distance = 60.0f;

        systemText.SetText(text);
        s = DOTween.Sequence();
        s.SetAutoKill(false);
        s.Append(systemText.rectTransform.DOAnchorPosY(distance, 3.0f).SetEase(Ease.OutExpo));
        s.Join(systemText.DOFade(1.0f, 2.0f));
        s.Append(systemText.DOFade(0.0f, 0.2f));
        s.OnComplete(() => {
            systemText.rectTransform.anchoredPosition += Vector2.down * distance;
        });

    }

    bool isOpened = false;
    Tweener tweenner;
    public void AnimatingPausePanel(bool onoff)
    {
        if (onoff)
        {
            //�г��� ������ �ִϸ��̼�
            tweenner.Kill();
            tweenner = pausePanel.transform.DOScale(new Vector3(0, 0, 0), 0.25f).SetEase(Ease.InOutExpo);//.OnComplete(() => pausePanel.gameObject.SetActive(false));

            Time.timeScale = 1.0f;

        }
        else
        {
            
            //�г��� ������ �ִϸ��̼�
            tweenner.Kill();
            tweenner = pausePanel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.InExpo).SetEase(Ease.OutBounce).SetUpdate(true);

            Time.timeScale = 0.0f;
        }
    }

    private void SummonMob(GameObject mob)
    {
        Instantiate(mob);
    }

    private void Awake()
    {
        SingletonInit();
        playerHpSlider = FindObjectOfType<HpSlider>();
        currentTime = maxTime;
        currentScore = 0;
    }

    private void Start()
    {
        //scoreResultText.enabled = false;

        timerTextOriginalPosition = timerText.rectTransform.anchoredPosition;
        scoreTextOriginalPosition = scoreText.rectTransform.anchoredPosition;

        //var playerObject = Instantiate(GameData.PlayerModel.Value);
        //playerObject.transform.parent = player.transform;

        string characterName = "Luka";
        if (!string.IsNullOrEmpty(GameData.CharacterName.Value)) characterName = GameData.CharacterName.Value;
        else { Debug.LogError("Character Name Can not found in GameData class"); }

        var characterPrefab = Resources.Load<GameObject>("PlayerModels/" + characterName);
        var playerModel = Instantiate(characterPrefab);
        playerModel.transform.parent = player.transform;

        player.Initialize();
        playerHpSlider.Initialize();


        StartOpening();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AnimatingPausePanel(isOpened);
            isOpened = !isOpened;
        }
    }

}
