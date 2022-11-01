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
            DontDestroyOnLoad(this.gameObject);
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
            //game over
        }
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

    
    public int AttackToTarget(Character attacker, Character target)
    {
        Debug.Log("I hurt..!" + target.name);

        int target_hp = target.GetHp();
        target_hp -= attacker.GetATK();
        target.SetHp(target_hp);
        target.beDamaged();
        playerHpSlider.UpdateValue();

        if (target.GetHp() <= 0) target.Die();

        return target_hp;
    }

    Sequence s;
    public void SetGotItemText(string text)
    {
        gotItemText.SetText(text);

        s = DOTween.Sequence();
        s.SetAutoKill(false);
        s.Append(gotItemText.rectTransform.DOAnchorPosY(60.0f, 3.0f).SetEase(Ease.OutExpo));
        s.Join(gotItemText.DOFade(1.0f, 2.0f));
        s.Append(gotItemText.DOFade(0.0f, 0.2f));

        s.Restart();
        //gotItemText.rectTransform.DOAnchorPosY(60.0f, 3.0f).SetEase(Ease.OutExpo);
        //gotItemText.DOFade(1.0f, 2.0f);
    }

    private void SummonMob(GameObject mob)
    {
        Instantiate(mob);
    }

    private bool CheckTimeOver()
    {
        return true;
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
    }

    private void Update()
    {
        Timer();
    }

}
