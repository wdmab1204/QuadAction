using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackStateType { ready, swing };

//������ �پ��ִ� ��ũ��Ʈ
//�浹�������� ������ �������� ����
public class WeaponController : MonoBehaviour
{
    private Character target;
    private Collider collider;
    public Player owner;
    private WaitForSeconds attackTime;
    private WaitForSeconds attackAnimationCooldown;
    [SerializeField] private AttackStateType attackStateType;
    public ParticleSystem swordParticle;

    private void SetAttackCooldown(float attackTime,float attackAnimationCooldown) 
    {
        this.attackTime = new WaitForSeconds(attackTime); 
        this.attackAnimationCooldown = new WaitForSeconds(attackAnimationCooldown); 
    }


    public void SetColliderEnabled(bool colliderEnabled)
    {
        collider.enabled = colliderEnabled;
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
        swordParticle.Play();

        yield return attackAnimationCooldown;
        attackStateType = AttackStateType.ready;
        swordParticle.Stop();
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
                GameManager.Instance.AttackToCha(owner, target);
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
        collider = GetComponent<Collider>();
    }

    private void Start()
    {
        owner = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        SetAttackCooldown(1.0f, 1.25f);
    }
}
