using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������ �پ��ִ� ��ũ��Ʈ
//�浹�������� ������ �������� ����
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
