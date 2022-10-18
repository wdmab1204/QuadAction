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
    private Vector3 rotation;

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
            weaponController.StartAttack();
        }
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
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }
}
