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
    [SerializeField] private TMP_Text gotItemText;
    [SerializeField] private RectTransform blackScreen;

    [Header("Produce Scene")]
    [SerializeField] private TextMeshProUGUI scoreResultText;

    [Header("Start Scene")]
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject titleImage;
    [SerializeField] private GameObject dummy;

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
        SwitchingCamera(CameraType.Opening_Camera);
        StartCoroutine(Timer());
    }
    #endregion

    #region Main
    private Vector3 timerTextOriginalPosition;
    private Vector3 scoreTextOriginalPosition;
    private void StartMain()
    {
        Time.timeScale = 1.0f;
        SwitchingCamera(CameraType.Main_Camera);

        //scoreText, timerText 원위치로 시키고
        timerText.rectTransform.anchoredPosition = timerTextOriginalPosition;
        scoreText.rectTransform.anchoredPosition = scoreTextOriginalPosition;
        blackScreen.anchoredPosition = new Vector2(Screen.width, blackScreen.anchoredPosition.y);

        //scoreText, timerText, hpsliderbar 애니메이션 실행
        timerText.rectTransform.DOAnchorPosX(-250.0f, 1.0f).From(true);
        scoreText.rectTransform.DOAnchorPosX(250.0f, 1.0f).From(true);
        playerHpSlider.UpdateValue();
    

        //몬스터 스폰 기능

        //타이머

        //업데이트문에서 처리해도 될지도?
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
    private enum CameraType { None, Main_Camera, Opening_Camera, Produce_Camera };
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
            gotItemText.gameObject.SetActive(true);
            blackScreen.gameObject.SetActive(true);

            scoreResultText.gameObject.SetActive(false);

            startButton.gameObject.SetActive(false);
            titleImage.gameObject.SetActive(false);
            blackScreen.position = new Vector2(Screen.width, blackScreen.position.y);
            dummy.gameObject.SetActive(false);
        }
        else if (cameraType == CameraType.Produce_Camera)
        {
            mainCamera.SetActive(false);
            produceCamera.SetActive(true);
            openingCamera.SetActive(false);

            playerHpSlider.gameObject.SetActive(false);
            timerText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
            gotItemText.gameObject.SetActive(false);
            //blackScreen.gameObject.SetActive(false);

            scoreResultText.gameObject.SetActive(true);

            startButton.gameObject.SetActive(false);
            titleImage.gameObject.SetActive(false);
            dummy.gameObject.SetActive(false);

        }
        else if (cameraType == CameraType.Opening_Camera)
        {
            mainCamera.SetActive(false);
            produceCamera.SetActive(false);
            openingCamera.SetActive(true);

            playerHpSlider.gameObject.SetActive(false);
            timerText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
            gotItemText.gameObject.SetActive(false);
            blackScreen.gameObject.SetActive(false);

            scoreResultText.gameObject.SetActive(false);

            startButton.gameObject.SetActive(true);
            titleImage.gameObject.SetActive(true);
            dummy.gameObject.SetActive(true);

        }

        this.cameraType = cameraType;
    }
    #endregion

    #endregion


    public int AttackToTarget(int damage, Character target)
    {
        int target_hp = target.GetHp();
        target_hp -= damage;
        target.SetHp(target_hp);
        target.beDamaged();
        playerHpSlider.UpdateValue();

        if (target.GetHp() <= 0) target.Die();

        return target_hp;
    }

    public int HealToTarget(int heal, Character target)
    {
        int target_hp = target.GetHp();
        target_hp += heal;
        target.SetHp(target_hp);
        playerHpSlider.UpdateValue();

        return target_hp;
    }

    Sequence s;
    public void SendItemMessage(string text)
    {
        float distance = 60.0f;

        gotItemText.SetText(text);
        s = DOTween.Sequence();
        s.SetAutoKill(false);
        s.Append(gotItemText.rectTransform.DOAnchorPosY(distance, 3.0f).SetEase(Ease.OutExpo));
        s.Join(gotItemText.DOFade(1.0f, 2.0f));
        s.Append(gotItemText.DOFade(0.0f, 0.2f));
        s.OnComplete(() => {
            gotItemText.rectTransform.anchoredPosition += Vector2.down * distance;
        });

    }

    bool isOpened = false;
    Tweener tweenner;
    public void AnimatingPausePanel(bool onoff)
    {
        if (onoff)
        {
            //패널이 닫히는 애니메이션
            tweenner.Kill();
            tweenner = pausePanel.transform.DOScale(new Vector3(0, 0, 0), 0.25f).SetEase(Ease.InOutExpo);//.OnComplete(() => pausePanel.gameObject.SetActive(false));

            Time.timeScale = 1.0f;

        }
        else
        {
            
            //패널이 열리는 애니메이션
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

        startButton.onClick.AddListener(StartMain);

        StartOpening();
    }

    private void Update()
    {
        if (cameraType == CameraType.Main_Camera)
        {
            //몬스터 스폰 켜기
            Timer();
        }
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AnimatingPausePanel(isOpened);
            isOpened = !isOpened;
        }
    }

}
