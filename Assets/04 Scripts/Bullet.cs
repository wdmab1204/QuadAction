using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Monster>();

        if (enemy != null)
            GameManager.Instance.AttackToTarget(damage, enemy, Vector3.zero);
    }
}