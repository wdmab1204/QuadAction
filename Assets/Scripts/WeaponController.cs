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
    private WaitForSeconds attackCooldown;
    [SerializeField] private AttackStateType attackStateType;

    private void SetAttackCooldown(float cooldown) { attackCooldown = new WaitForSeconds(cooldown); }

    private void OnTriggerEnter(Collider other)
    {
        target = other.gameObject.GetComponent<Character>();
        if (target != null)
        {
            GameManager.Instance.AttackToCha(owner, target);
            StartCoroutine(SwingIEnumerator());
            SetColliderEnabled(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    public void SetColliderEnabled(bool colliderEnabled)
    {
        collider.enabled = colliderEnabled;
    }


    public IEnumerator SwingIEnumerator()
    {
        attackStateType = AttackStateType.swing;
        yield return attackCooldown;
        attackStateType = AttackStateType.ready;
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
        SetAttackCooldown(1.25f);
        SetColliderEnabled(false);
    }
}
