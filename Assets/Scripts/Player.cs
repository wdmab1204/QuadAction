using System.Collections;
using System.Collections.Generic;
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
    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        // -1 ~ 1

        Vector3 velocity = new Vector3(-inputX, 0, -inputZ);
        
        velocity = velocity.normalized;
        velocity *= speed;
        rig.velocity = velocity;

        if (stateType != StateType.attack)
        {
            if (velocity != Vector3.zero) stateType = StateType.move;
            else stateType = StateType.idle;
        }
        

        transform.LookAt(new Vector3(transform.position.x+velocity.x, transform.position.y, transform.position.z + velocity.z));
        
        if (inputX == 0 && inputZ == 0) Idle();
        else Run();

       
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
            weaponController.SetColliderEnabled(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnCollisionExit(Collision collision)
    {

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
        weaponController = GameObject.FindObjectOfType<WeaponController>();
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
