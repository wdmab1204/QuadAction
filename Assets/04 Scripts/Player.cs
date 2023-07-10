using UnityEngine;
using ItemNameSpace;
using UnityEngine.UI;

public enum StateType { none, idle, move, attack };

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
            Hit(1, Vector3.zero);
            //GameManager.Instance.AttackToTarget(enemy.Atk.Value, this);
        }
    }

    public override void Hit(int damageAmount, Vector3 force)
    {
        Hp.Value -= damageAmount;
    }

    void OnDamaged(Vector3 targetPos)
    {
        //col.enabled = false; // ����

        int dirc_x = transform.position.x - targetPos.x > 0 ? 1 : -1;
        int dirc_z = transform.position.z - targetPos.z > 0 ? 1 : -1;
        rig.AddForce(new Vector3(dirc_x, 2, dirc_z) * 10.0f, ForceMode.Impulse);

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

    #region Animation
    private Animator anim;

    private void Idle() { anim.SetBool("Move", false); }
    private void Run() { anim.SetBool("Move", true); }
    #endregion

    private BuffManager buffManager;
    public BuffManager BuffManager { get { return buffManager; } }

    public override void Initialize()
    {
        base.Initialize();
        Hp.OnChange = hp => { if (hp <= 0) Die(); };

        col = GetComponent<Collider>();
        rig = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        buffManager = new BuffManager();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        buffManager.Update();
    }
}
