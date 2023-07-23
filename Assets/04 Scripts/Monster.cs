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
    private Rigidbody rb;
    private Character target;

    void FollowTarget()
    {
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        if (target != null)
        {
            rb.velocity = (target.transform.position - transform.position).normalized * speed;
            transform.LookAt(target.transform);
        }
    }

    #endregion
    // Start is called before the first frame update

    #region Die
    [Header("Dying variable")]
    public float dyingTime;
    public GameObject dieParticle;
    public GameObject treasureBoxPrefab;

    private int score = 5;
    private int percent = 15;
    public override void Die()
    {
        GameManager.Instance.SetScore(score);
        anim.SetTrigger("Die");

        ParticleSystem particle = Instantiate(dieParticle).GetComponent<ParticleSystem>();
        particle.transform.position = this.transform.position;
        particle.Play();

        
        bool[] arr = new bool[100];
        for (int i = 0; i < percent; i++) arr[i] = true;
        int randI = Random.Range(0, 100);

        if (arr[randI] == true)
        {
            GameObject obj = Instantiate(treasureBoxPrefab);
            obj.transform.position = this.transform.position;
        }
    }
    #endregion

    public override void Hit(int damage, Vector3 force)
    {
        Hp.Value -= damage;
        StartCoroutine(ForceCoroutine(force : transform.position - target.transform.position.normalized));
    }

    IEnumerator ForceCoroutine(Vector3 force)
    {
        float time = 0;
        while (time <= 0.5f)
        {
            time += Time.fixedDeltaTime;
            rb.AddForce(force);
            yield return new WaitForFixedUpdate();
        }
    }

    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Initialize();
    }
    private void Start()
    {
        var playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
            target = playerObject.GetComponent<Character>();
        else
            Debug.LogWarning("Can not found Player Object : " + name);
    }

    private void Update()
    {
        FollowTarget();
    }
}
