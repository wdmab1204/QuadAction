using UnityEngine;
using ItemNameSpace;
using UnityEngine.UI;

public enum StateType { none, idle, move, attack };

public delegate void PlayerSkill();

public class Player : Character
{

    [SerializeField] private Rigidbody rig;
    public StateType stateType;

    #region Movement
    public float rotationSpeed = 180f;

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // -1 ~ 1
        Vector3 velocity = new Vector3(h, 0, v);
        
        velocity.Normalize();

        if (velocity != Vector3.zero)
        {
            rig.velocity = velocity * speed;
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

    #region damaged

    [SerializeField] private Collider col;
    //private void OnTriggerEnter(Collider collision)
    //{
    //    var enemy = collision.GetComponent<Monster>();
    //    if (enemy != null)
    //    {
    //        OnDamaged(collision.transform.position);
    //        GameManager.Instance.AttackToTarget(enemy.GetATK(), this);
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<Monster>();
        if (enemy != null)
        {
            OnDamaged(collision.transform.position);
            GameManager.Instance.AttackToTarget(enemy.GetATK(), this);
        }
    }

    void OnDamaged(Vector3 targetPos)
    {
        //col.enabled = false; // 公利

        int dirc_x = transform.position.x - targetPos.x > 0 ? 1 : -1;
        int dirc_z = transform.position.z - targetPos.z > 0 ? 1 : -1;
        rig.AddForce(new Vector3(dirc_x, 2, dirc_z) * 10.0f, ForceMode.Impulse);

        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        col.enabled = true; //公利 秦力
    }

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

    #endregion


    #region Die
    public override void Die()
    {
        GameManager.Instance.GameOver();
    }
    #endregion

    #region Animation
    [SerializeField] private Animator anim;

    private void Idle() { anim.SetBool("Move", false); }
    private void Run() { anim.SetBool("Move", true); }
    #endregion


    protected override void Awake()
    {
        base.Awake();
        col = GetComponent<Collider>();
        rig = GetComponent<Rigidbody>();
    }

    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }
}
