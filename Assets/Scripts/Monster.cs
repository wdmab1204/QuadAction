using System.Collections;
using UnityEngine;

enum MonsterBehaviourType { FollowTarget, Attack };

public class Monster : Character
{
    [Header("Global")]
    [SerializeField] private Animator anim;
    [SerializeField] private MonsterBehaviourType behaviourType;


    #region Follow Player
    [Header("Following variable")]
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
    [Header("Dying variable")]
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
    [Header("Attacking variable")]
    public float timingAttack;
    public float attackCoolTime;
    private IEnumerator Attack()
    {
        Debug.Assert(timingAttack > attackCoolTime);

        yield return new WaitForSeconds(timingAttack); //����Ÿ�̹� �ð�
        //�÷��̾ �����ϰ�
        int playerHp = GameManager.Instance.AttackToTarget(this, Player.Instance);
        //�÷��̾� ü�� slider�� ����
        GameManager.Instance.SetSliderValue(playerHp);

        yield return new WaitForSeconds(attackCoolTime - timingAttack);//���� ���ð�
    }
    #endregion

    private IEnumerator MonsterAI()
    {
        Vector3 vec2Target;
        float distance;

        while (true)
        {
            vec2Target = target.position - this.transform.position;
            vec2Target.y = 0; //xz plane������ �����̱⶧����
            distance = vec2Target.magnitude;

            if (distance < 2.0f) //target�� ���ݻ����Ÿ��ȿ� ���Դ°�?
            {
                //StartCoroutine(Attack());

                Debug.Assert(timingAttack > attackCoolTime);

                yield return new WaitForSeconds(timingAttack); //����Ÿ�̹� �ð�
                                                               //�÷��̾ �����ϰ�
                int playerHp = GameManager.Instance.AttackToTarget(this, Player.Instance);
                //�÷��̾� ü�� slider�� ����
                GameManager.Instance.SetSliderValue(playerHp);

                yield return new WaitForSeconds(attackCoolTime - timingAttack);//���� ���ð�
            }

            yield return null;
        }
        

        
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
        StartCoroutine(MonsterAI());
    }
}
