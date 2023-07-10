using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //public virtual int GetHp() { return hp; }
    //public virtual void SetHp(int hp) { this.hp = (hp < maxhp) ? hp : maxhp; }
    //public virtual int GetATK() { return atk; }
    //public virtual void SetATK(int atk) { this.atk = atk; }
    //public virtual void SetSpeed(float speed) { this.speed = speed; }
    //public virtual float GetSpeed() { return speed; }
    public abstract void Hit(int damageAmount, Vector3 force);
    public virtual void Die() { Destroy(this.gameObject); }
    public virtual void Die(float time) { Destroy(this.gameObject, time); }

    [Header("Character Status")]
    [SerializeField] protected int maxhp;
    [SerializeField] protected int hp;
    [SerializeField] protected int atk;
    [SerializeField] protected float speed;

    public Data<int> MaxHp;
    public Data<int> Hp;
    public Data<int> Atk;
    public Data<float> Speed;

    public virtual void Initialize()
    {
        MaxHp = new Data<int>(maxhp);
        Hp = new Data<int>(hp);
        Atk = new Data<int>(atk);
        Speed = new Data<float>(speed);
        hp = maxhp;
    }
}
