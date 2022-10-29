using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackStateType { ready, swing };

//웨폰에 붙어있는 스크립트
//충돌판정으로 적에게 데미지를 입힘
public class WeaponController : MonoBehaviour
{
    public Player owner;
    [SerializeField] private Skill[] skills;
    private Skill selectedSkill;
    private WaitForSeconds attackTime;
    private WaitForSeconds attackAnimationCooldown;
    private ParticleSystem _particle;
    
    public ParticleSystem particle
    {
        get
        {
            if (_particle == null)
            {
                _particle = transform.Find(selectedSkill.particleName).GetComponent<ParticleSystem>();
                if (_particle == null)
                {
                    Debug.LogWarning("Cannot find particle name", _particle);
                }
            }
            return _particle;
        }
    }


    private void InitAttackCooldown() 
    {
        this.attackTime = new WaitForSeconds(selectedSkill.attackTime); 
        this.attackAnimationCooldown = new WaitForSeconds(selectedSkill.attackAnimationCooldown);
    }

    public void StartAttack(int skillIndex)
    {
        selectedSkill = skills[skillIndex];
        InitAttackCooldown();
        StartCoroutine(SwingIEnumerator());
    }


    private IEnumerator SwingIEnumerator()
    {
        yield return attackTime;//공격하는 순간
                                //공격판정

        Attack();
        ParticlePlay();

        yield return attackAnimationCooldown;
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
    private void Attack()
    {
        colls = Physics.OverlapSphere(owner.transform.position, selectedSkill.range);
        foreach(var coll in colls)
        {
            Character target = coll.GetComponent<Character>();
            if (target != null)
            {
                GameManager.Instance.AttackToTarget(owner, target);
            }
        }
        
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.matrix = owner.transform.localToWorldMatrix;
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(Vector3.zero, selectedSkill.range);
    //}

    bool CheckAttackState(StateType currentStateType)
    {
        if (currentStateType == StateType.attack) return true;
        else return false;
    }


    private void Awake()
    {

    }

    private void Start()
    {
        owner = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        skills = Resources.LoadAll<Skill>("Skills");
        selectedSkill = skills[0];
    }
}
