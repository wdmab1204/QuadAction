using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    #region Movement
    public int speed;
    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        // -1 ~ 1

        Vector3 velocity = new Vector3(-inputX, 0, -inputZ);
        
        velocity = velocity.normalized;
        velocity *= speed;
        rig.velocity = velocity;

        transform.LookAt(new Vector3(transform.position.x+velocity.x, transform.position.y, transform.position.z + velocity.z));
        
        if (inputX == 0 && inputZ == 0) Idle();
        else Run();

       
    }
    #endregion

    #region Attack

    private Character attackTarget;

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetTrigger("Attack");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        attackTarget = collision.gameObject.GetComponent<Character>();
    }

    private void OnCollisionExit(Collision collision)
    {
        attackTarget = null;
    }

    #endregion

    #region Animation
    [SerializeField] private Animator anim;

    private void Idle() { anim.SetBool("Move", false); }
    private void Run() { anim.SetBool("Move", true); }
    #endregion


    private void Awake()
    {
        //anim = GetComponent<Animator>();
        //rig = GetComponent<Rigidbody>();
        SingletonInit();
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
