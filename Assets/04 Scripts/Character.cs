using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public abstract void Hit(int damageAmount, Vector3 force);
    public virtual void Die() { Destroy(this.gameObject); OnDie("sometag", gameObject); }

    [Header("Character Status")]
    [SerializeField] protected int maxhp;
    [SerializeField] protected int hp;
    [SerializeField] protected int atk;
    [SerializeField] protected float speed;

    public Data<int> MaxHp;
    public Data<int> Hp;
    public Data<int> Atk;
    public Data<float> Speed;

    public System.Action<string,GameObject> OnDie;

    public virtual void Initialize()
    {
        MaxHp = new Data<int>(maxhp);
        Hp = new Data<int>(hp);
        Atk = new Data<int>(atk);
        Speed = new Data<float>(speed);
        hp = maxhp;
    }
}
