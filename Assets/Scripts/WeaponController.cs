using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//웨폰에 붙어있는 스크립트
//충돌판정으로 적에게 데미지를 입힘
public class WeaponController : MonoBehaviour
{
    private Character target;
    private Collider collider;
    public Player owner;


    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void Start()
    {
        owner = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CheckAttackState(owner.stateType)) return;

        target = other.gameObject.GetComponent<Character>();
        if (target != null)
            GameManager.Instance.AttackToCha(owner, target);
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    bool CheckAttackState(StateType currentStateType)
    {
        if (currentStateType == StateType.attack) return true;
        else return false;
    }
}
