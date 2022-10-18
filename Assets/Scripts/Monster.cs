using System.Collections;
using UnityEngine;

public class Monster : Character
{
    [SerializeField] private Animator anim;
    #region Follow Player
    [SerializeField] private float moveSpeed = 3f;

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

    #region Die
    private bool dying;
    public float dyingTime;
    public override void SetHp(int hp)
    {
        base.SetHp(hp);
        if (_hp <= 0) Die();
    }

    private int score = 5;
    public override void Die()
    {
        dying = true;
        anim.SetTrigger("Die");
        Destroy(this.gameObject, dyingTime);
        col.enabled = false;
        rb.useGravity = false;

        GameManager.Instance.SetScore(score);
    }
    #endregion

    #region Attack
    public float attackingTime;
    public AnimationClip attackclip;
    private void Attack()
    {
        //�÷��̾ �����ϰ�
        int playerHp = GameManager.Instance.AttackToTarget(this, Player.Instance);
        //�÷��̾� ü�� slider�� ����
        GameManager.Instance.SetSliderValue(playerHp);
    }
    #endregion

    private void MonsterAI()
    {
        //handler�Ẽ��
        
        //if �÷��̾�� �ָ� �ִٸ�
        //    handler = followtarget
            
        //else
        //    handler = attack

        //���ÿ� �ΰ��� �������� ���ϵ���
        //���� bool ���������� ����������
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
        MonsterAI();
    }
}
