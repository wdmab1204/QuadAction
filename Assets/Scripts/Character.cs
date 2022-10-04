using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int GetHp() { return _hp; }
    public void SetHp(int hp) { _hp = hp; }
    public int GetATK() { return _atk; }
    public void SetATK(int atk) { _atk = atk; }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    private int _hp;
    private int _atk;
}
