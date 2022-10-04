using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    #region Follow Player
    float moveSpeed = 3f;
    float contactDistance = 1f;

    private Rigidbody rb;
    Transform target;

    void FollowTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        transform.LookAt(target);
    }

    #endregion
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        target = Player.Instance.transform;
    }
    private void Update()
    {
        FollowTarget();
    }
}
