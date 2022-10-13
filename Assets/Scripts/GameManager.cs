using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private HpSlider playerHpSlider;

    void Awake()
    {
        SingletonInit();
        playerHpSlider = FindObjectOfType<HpSlider>();
    }

    public int AttackToTarget(Character attacker, Character target)
    {
        Debug.Log("I hurt..!" + target.name);

        int target_hp = target.GetHp();
        target_hp -= attacker.GetATK();
        target.SetHp(target_hp);

        return target_hp;
    }

    public void SetSliderValue(float value)
    {
        playerHpSlider.SetValue((int)value);
    }

    void SummonMob(GameObject mob)
    {
        Instantiate(mob);
    }

    bool CheckTimeOver()
    {
        return true;
    }

    
    
}
