using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//웨폰에 붙어있는 스크립트
//충돌판정으로 적에게 데미지를 입힘
public class WeaponController : MonoBehaviour
{
    private Character target;
    public Character owner;

    private void OnTriggerEnter(Collider other)
    {
        target = other.gameObject.GetComponent<Character>();

        GameManager.Instance.AttackToCha(owner, target);
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
