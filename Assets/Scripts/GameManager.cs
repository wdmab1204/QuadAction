using System.Collections;
using System.Collections.Generic;
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

    void Awake()
    {
        SingletonInit();
    }

    public void AttackToCha(Character attacker, Character target)
    {
        int target_hp = target.GetHp();
        target_hp -= attacker.GetATK();
        target.SetHp(target_hp);

        if (target.GetHp() <= 0)
        {
            target.Die();
        }
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
