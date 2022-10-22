using System.Collections;
using UnityEngine;


public enum AttackStateType { ready, swing };

//������ �پ��ִ� ��ũ��Ʈ
//�浹�������� ������ �������� ����
public class WeaponController : MonoBehaviour
{
    public Player owner;
    private WaitForSeconds attackTime;
    private WaitForSeconds attackAnimationCooldown;
    [SerializeField] private AttackStateType attackStateType;
    [SerializeField] private ParticleSystem _particle;
    public ParticleSystem particle
    {
        get
        {
            if (_particle == null)
            {
                _particle = transform.GetChild(0).GetComponent<ParticleSystem>();
            }
            return _particle;
        }
    }


    private void SetAttackCooldown(float attackTime,float attackAnimationCooldown) 
    {
        this.attackTime = new WaitForSeconds(attackTime); 
        this.attackAnimationCooldown = new WaitForSeconds(attackAnimationCooldown); 
    }

    public void StartAttack()
    {
        StartCoroutine(SwingIEnumerator());
    }


    private IEnumerator SwingIEnumerator()
    {
        attackStateType = AttackStateType.swing;
        yield return attackTime;//�����ϴ� ����
                                //��������

        Attack();
        ParticlePlay();

        yield return attackAnimationCooldown;
        attackStateType = AttackStateType.ready;
        ParticleStop();
    }


    
    private void ParticlePlay()
    {
        particle.Play();
    }

    private void ParticleStop()
    {
        particle.Stop();
    }

    private Collider[] colls;
    [SerializeField] private float radius = 1.5f;
    private void Attack()
    {
        colls = Physics.OverlapSphere(owner.transform.position, radius);
        foreach(var coll in colls)
        {
            Character target = coll.GetComponent<Character>();
            if (target != null)
            {
                GameManager.Instance.AttackToTarget(owner, target);
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = owner.transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Vector3.zero, radius);
    }

    bool CheckAttackState(StateType currentStateType)
    {
        if (currentStateType == StateType.attack) return true;
        else return false;
    }

    public bool CheckCurrentAttacking() { return attackStateType != AttackStateType.ready; }

    private void Awake()
    {

    }

    private void Start()
    {
        owner = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        SetAttackCooldown(1.0f, 1.25f);
    }
}
