using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    [SerializeField] private Animator anim;
    #region Follow Player
    float moveSpeed = 3f;
    float contactDistance = 1f;

    private Rigidbody rb;
    private Transform target;
    private Collider col;

    void FollowTarget()
    {
        if (dying) return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        transform.LookAt(target);
    }

    #endregion
    // Start is called before the first frame update

    private bool dying;
    public float dieDelay;
    public override void SetHp(int hp)
    {
        base.SetHp(hp);
        if (_hp <= 0)
        {
            dying = true;
            anim.SetTrigger("Die");
            Destroy(this.gameObject,dieDelay);
            col.enabled = false;
            rb.useGravity = false;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider>();

    }
    private void Start()
    {
        target = Player.Instance.transform;
    }
    private void Update()
    {
        //FollowTarget();
    }
}
