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
        Debug.Log("Follow..");
    }

    #endregion
    // Start is called before the first frame update

    private bool dying;
    public float dieDelay;
    public override void SetHp(int hp)
    {
        base.SetHp(hp);
        if (_hp <= 0) Die();
    }

    private int score = 5;
    private void Die()
    {
        dying = true;
        anim.SetTrigger("Die");
        Destroy(this.gameObject, dieDelay);
        col.enabled = false;
        rb.useGravity = false;

        GameManager.Instance.SetScore(score);
    }

    void Attack()
    {
        //플레이어를 공격하고
        int playerHp = GameManager.Instance.AttackToTarget(this, Player.Instance);

        //플레이어 체력 slider에 적용
        GameManager.Instance.SetSliderValue(playerHp);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider>();
        dying = false;

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
