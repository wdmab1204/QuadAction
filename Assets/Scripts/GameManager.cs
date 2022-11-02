using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = null;

    protected void SingletonInit()
    {
        if (null == instance)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
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
    [SerializeField] private HpSlider playerHpSlider;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text gotItemText;
    [SerializeField] private RectTransform pausePanel;
    #endregion

    #region GameRule

    #region Timer
    [SerializeField] private float maxTime;
    private float currentTime;
    private void Timer()
    {

        currentTime -= Time.deltaTime;
        timerText.text = (float)Math.Round(currentTime, 2) + "";
        if (currentTime <= 0.0f)
        {
            //GameOver();
        }
    }
    #endregion

    #region GameOver
    [SerializeField] private RectTransform blackScreen;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject produceCamera;
    [SerializeField] private TextMeshProUGUI scoreResultText;
    Sequence ss;
    public void GameOver()
    {
        blackScreen.gameObject.SetActive(true);
        ss = DOTween.Sequence();
        ss.SetUpdate(true);
        ss.Append(blackScreen.DOAnchorPosX(0.0f, 0.7f));
        ss.AppendCallback(
            ()=>
            {
                mainCamera.SetActive(false);
                produceCamera.SetActive(true);
                timerText.enabled = false;
                scoreText.enabled = false;
                playerHpSlider.gameObject.SetActive(false);
                scoreResultText.enabled = true;
                scoreResultText.text = currentScore + "";
                Time.timeScale = 0.0f;
            });
        ss.Append(blackScreen.DOAnchorPosX(-Screen.width, 0.4f));

        
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
    public void SetGotItemText(string text)
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
        timerText.rectTransform.DOAnchorPosX(-250.0f, 1.0f).From(true);
        scoreText.rectTransform.DOAnchorPosX(250.0f, 1.0f).From(true);

        blackScreen.anchoredPosition = new Vector2(Screen.width, blackScreen.anchoredPosition.y);
        scoreResultText.enabled = false;

        Invoke("GameOver", 2.0f);

    }

    private void Update()
    {
        Timer();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AnimatingPausePanel(isOpened);
            isOpened = !isOpened;
        }
    }

}
