using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

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
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        rb.velocity = (target.transform.position-transform.position).normalized * speed;
        transform.LookAt(target.transform);
    }

    #endregion
    // Start is called before the first frame update

    #region Die
    [Header("Dying variable")]
    public float dyingTime;
    public GameObject dieParticle;
    public GameObject treasureBoxPrefab;

    public override void SetHp(int hp)
    {
        base.SetHp(hp);
    }

    private int score = 5;
    private int percent = 15;
    System.Action<Monster> destroyEvent;
    public override void Die()
    {
        GameManager.Instance.SetScore(score); //ui�� score �ݿ�
        anim.SetTrigger("Die"); //Die �ִϸ��̼� ����
        //base.Die();
        destroyEvent?.Invoke(this);


        //Particle ����
        ParticleSystem particle = Instantiate<GameObject>(dieParticle).GetComponent<ParticleSystem>();
        particle.transform.position = this.transform.position;
        particle.Play();

        
        //�������� Ȯ������
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

    public override void Hit()
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

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    private void Update()
    {
        FollowTarget();
    }
}
