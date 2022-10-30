using System;
using TMPro;
using UnityEngine;

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
    private int currentScore;
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

    private void Update()
    {
        //Timer();
    }

}
