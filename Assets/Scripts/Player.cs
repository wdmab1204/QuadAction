using UnityEngine;

public enum StateType { none, idle, move, attack };

public class Player : Character
{

    #region Singleton
    private static Player instance = null;

    protected void SingletonInit()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public static Player Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion

    [SerializeField] private Rigidbody rig;
    public StateType stateType;

    #region Movement
    public int speed;
    public float rotationSpeed = 180f;

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // -1 ~ 1

        Vector3 velocity = new Vector3(h, 0, v);
        
        velocity.Normalize();
        rig.velocity = velocity * speed;

        if (velocity != Vector3.zero)
        {
            stateType = StateType.move;

            Quaternion toRotation = Quaternion.LookRotation(velocity, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);


            Run();
        }
        else
        {
            stateType = StateType.idle;

            Idle();
        }



       
    }
    #endregion

    #region Attack

    private WeaponController weaponController;

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetTrigger("Attack");
            stateType = StateType.attack;
            weaponController.StartAttack(0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetTrigger("Kick");
            stateType = StateType.attack;
            weaponController.StartAttack(1);
        }
    }
    #region damaged

    [SerializeField] private Collider col;
    private void OnTriggerEnter(Collider collision)
    {
        var enemy = collision.GetComponent<Monster>();
        if (enemy != null)
        {
            OnDamaged(collision.transform.position);
            GameManager.Instance.AttackToTarget(enemy, this);
        }
    }

    void OnDamaged(Vector3 targetPos)
    {
        col.enabled = false; // 公利

        int dirc_x = transform.position.x - targetPos.x > 0 ? 1 : -1;
        int dirc_z = transform.position.z - targetPos.z > 0 ? 1 : -1;
        rig.AddForce(new Vector3(dirc_x, transform.position.y, dirc_z) * 10.0f, ForceMode.Impulse);

        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        col.enabled = true; //公利 秦力
    }

    #endregion
    #endregion


    #region beDamaged
    [SerializeField] private ParticleSystem _particle;
    public ParticleSystem particle
    {
        get
        {
            if (_particle == null)
            {
                _particle = transform.GetChild(0).GetComponent<ParticleSystem>();
            }
            return _particle;
        }
    }

    public override void beDamaged()
    {
        var random = Random.Range(0f, 360f);
        _particle.transform.rotation = Quaternion.Euler(0, random, 0);
        _particle.Play();
    }
    #endregion

    #region Animation
    [SerializeField] private Animator anim;

    private void Idle() { anim.SetBool("Move", false); }
    private void Run() { anim.SetBool("Move", true); }
    #endregion


    private void Awake()
    {
        SingletonInit();
        weaponController = FindObjectOfType<WeaponController>();
        col = GetComponent<Collider>();
        rig = GetComponent<Rigidbody>();
    }

    Action1 action;
    Action2 action2;
    [SerializeField] private GameObject ball;

    [Header("Action 1")]
    [SerializeField] private int objCount = 1;
    [SerializeField] private float objSpeed = 1.0f;
    [SerializeField] private float circleR = 1.0f;

    [Header("Action 2")]
    [SerializeField] private int objCount2 = 1;
    [SerializeField] private float theta = 30.0f;
    [SerializeField] private float objSpeed2 = 1.0f;
    [SerializeField] private float distance = 1.0f;
    void Start()
    {
        action = new Action1();
        action.Init(ball, this.transform, objCount, objSpeed, circleR);
        StartCoroutine(action.UpdateAction());

        action2 = new Action2();
        action2.Init(ball, this.transform, objCount2, theta, objSpeed2, distance);
        StartCoroutine(action2.UpdateAction());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // if action2啊 角青吝牢啊?
    }
}
