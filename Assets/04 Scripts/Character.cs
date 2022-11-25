using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public virtual int GetHp() { return hp; }
    public virtual void SetHp(int hp) { this.hp = (hp < max_hp) ? hp : max_hp; }
    public virtual int GetATK() { return atk; }
    public virtual void SetATK(int atk) { this.atk = atk; }
    public virtual void SetSpeed(float speed) { this.speed = speed; }
    public virtual float GetSpeed() { return speed; }
    public virtual void beDamaged() { }
    public virtual void Die() { Destroy(this.gameObject); }
    public virtual void Die(float time) { Destroy(this.gameObject, time); }

    [Header("Character Status")]
    [SerializeField] protected int max_hp;
    [SerializeField] protected int hp;
    [SerializeField] protected int atk;
    [SerializeField] protected float speed;

    protected virtual void Awake()
    {
        hp = max_hp;
    }
}
