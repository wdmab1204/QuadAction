using System.Collections;
using UnityEngine;

enum MonsterBehaviourType { FollowTarget, Attack };

[RequireComponent(typeof(Collider))]
public class Monster : Character
{
    [Header("Global")]
    [SerializeField] private Animator anim;
    [SerializeField] private MonsterBehaviourType behaviourType;


    #region Follow Player
    [Header("Following variable")]
    [SerializeField] private float moveSpeed = 3f;
    private Rigidbody rb;
    private Character target;
    private Collider col;

    void FollowTarget()
    {
        if (dying) return;

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        transform.LookAt(target.transform);
    }

    #endregion
    // Start is called before the first frame update

    #region Die
    [Header("Dying variable")]
    private bool dying;
    public float dyingTime;
    public GameObject treasureBoxPrefab;

    public override void SetHp(int hp)
    {
        base.SetHp(hp);
        if (GetHp() <= 0) Die();
    }

    private int score = 5;
    public override void Die()
    {
        dying = true;
        GameManager.Instance.SetScore(score);
        anim.SetTrigger("Die");
        Destroy(this.gameObject, dyingTime);
        col.enabled = false;
        //rb.useGravity = false;

        
    }
    #endregion

    #region Attack
    [Header("Attacking variable")]
    public float timingAttack;
    public float attackCoolTime;
    public float attackDistance;
    private IEnumerator Attack()
    {
        Debug.Assert(timingAttack < attackCoolTime);

        yield return new WaitForSeconds(timingAttack); //����Ÿ�̹� �ð�
        //�÷��̾ ����
        int playerHp = GameManager.Instance.AttackToTarget(GetATK(), target);

        yield return new WaitForSeconds(attackCoolTime - timingAttack);//���� ���ð�
    }
    #endregion

    private IEnumerator MonsterAI()
    {
        Vector3 vec2Target;
        float distance;

        while (true)
        {
            //distance ���ϴ� ����
            vec2Target = target.transform.position - this.transform.position;
            vec2Target.y = 0;
            distance = vec2Target.magnitude;

            if (distance < attackDistance) //target�� ���ݻ����Ÿ��ȿ� ���Դ°�?
            {
                //anim.SetBool("canAttack", true);
                yield return StartCoroutine(Attack());
            }
            else //�����Ÿ����̶�� �÷��̾� ���󰡱�
            {
                //anim.SetBool("canAttack", false);
                FollowTarget();
            }

            yield return null;
        }
        

        
    }


    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
        col = GetComponent<Collider>();
        dying = false;
    }
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        StartCoroutine(MonsterAI());
    }
}
