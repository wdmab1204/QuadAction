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
    private Rigidbody rb;
    private Character target;

    void FollowTarget()
    {
        if (dying) return;

        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        rb.velocity = (target.transform.position-transform.position).normalized * speed;
        transform.LookAt(target.transform);
    }

    #endregion
    // Start is called before the first frame update

    #region Die
    [Header("Dying variable")]
    private bool dying;
    public float dyingTime;
    public GameObject dieParticle;
    public GameObject treasureBoxPrefab;

    public override void SetHp(int hp)
    {
        base.SetHp(hp);
    }

    private int score = 5;
    private int percent = 15;
    public override void Die()
    {
        dying = true;
        GameManager.Instance.SetScore(score); //ui에 score 반영
        anim.SetTrigger("Die"); //Die 애니메이션 실행
        Destroy(this.gameObject, dyingTime); //dyingTime만큼의 시간 이후 자신의 오브젝트 삭제


        //Particle 생성
        ParticleSystem particle = Instantiate<GameObject>(dieParticle).GetComponent<ParticleSystem>();
        particle.transform.position = this.transform.position;
        particle.Play();

        
        //보물상자 확률생성
        bool[] arr = new bool[100];
        for (int i = 0; i < percent; i++) arr[i] = true;
        int randI = Random.Range(0, 100);

        if (arr[randI] == true)
        {
            GameObject obj = Instantiate<GameObject>(treasureBoxPrefab);
            obj.transform.position = this.transform.position;
        }
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

        yield return new WaitForSeconds(timingAttack); //공격타이밍 시간
        //플레이어를 공격
        int playerHp = GameManager.Instance.AttackToTarget(GetATK(), target);

        yield return new WaitForSeconds(attackCoolTime - timingAttack);//재사용 대기시간
    }
    #endregion

    public override void beDamaged()
    {
        //var v = transform.position - target.transform.position.normalized;
        //rb.AddForce(v ,ForceMode.Impulse);
        StartCoroutine(force());
    }

    IEnumerator force()
    {
        float time = 0;
        while (time <= 0.5f)
        {
            time += Time.fixedDeltaTime;
            rb.AddForce(transform.position - target.transform.position.normalized);
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator MonsterAI()
    {
        Vector3 vec2Target;
        float distance;

        while (true)
        {
            //distance 구하는 과정
            vec2Target = target.transform.position - this.transform.position;
            vec2Target.y = 0;
            distance = vec2Target.magnitude;

            if (distance < attackDistance) //target이 공격사정거리안에 들어왔는가?
            {
                //anim.SetBool("canAttack", true);
                yield return StartCoroutine(Attack());
            }
            else //사정거리밖이라면 플레이어 따라가기
            {
                //anim.SetBool("canAttack", false);
                FollowTarget();
            }

            yield return null;
        }
        

        
    }


    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
        dying = false;
    }
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        StartCoroutine(MonsterAI());
    }
}
