using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public virtual int GetHp() { return _hp; }
    public virtual void SetHp(int hp) { _hp = hp; }
    public virtual int GetATK() { return _atk; }
    public virtual void SetATK(int atk) { _atk = atk; }

    public virtual void Die() { Destroy(this.gameObject); }
    public virtual void Die(float time) { Destroy(this.gameObject, time); }

    [SerializeField] protected int _hp;
    [SerializeField] protected int _atk;
}
