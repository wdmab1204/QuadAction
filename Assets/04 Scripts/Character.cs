using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public virtual int GetHp() { return hp; }
    public virtual void SetHp(int hp) { this.hp = (hp < max_hp) ? hp : max_hp; }
    public virtual int GetATK() { return atk; }
    public virtual void SetATK(int atk) { this.atk = atk; }
    public virtual void SetSpeed(float speed) { this.speed = speed; }
    public virtual float GetSpeed() { return speed; }
    public abstract void Hit(int damageAmount);
    public virtual void Die() { Destroy(this.gameObject); }
    public virtual void Die(float time) { Destroy(this.gameObject, time); }

    [Header("Character Status")]
    [SerializeField] protected int max_hp;
    [SerializeField] protected int hp;
    [SerializeField] protected int atk;
    [SerializeField] protected float speed;

    public System.Action<int> OnMaxHpChange;
    public System.Action<int> OnHpChange;
    public System.Action<int> OnAtkChange;
    public System.Action<float> OnSpeedChange;

    public Data<int> MaxHp;
    public Data<int> Hp;
    public Data<int> Atk;
    public Data<float> Speed;

    public void Initialized()
    {
        MaxHp = new Data<int>(max_hp);
        Hp = new Data<int>(hp);
        Atk = new Data<int>(atk);
        Speed = new Data<float>(speed);
    }

    protected virtual void Awake()
    {
        hp = max_hp;
    }
}
