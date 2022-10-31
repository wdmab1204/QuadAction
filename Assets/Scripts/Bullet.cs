using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Monster>();

        if (enemy != null)
            GameManager.Instance.AttackToTarget(Player.Instance, enemy);
    }
}