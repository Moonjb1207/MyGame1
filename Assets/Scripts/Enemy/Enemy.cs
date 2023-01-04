using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterMovement, IBattle
{
    public enum STATE
    {
        Create, Normal, Battle, Dead, RunAway
    }

    public STATE myState = STATE.Create;
    public EnemyStat myStat;
    public AIPerception mySensor = null;
    public float curDelay = 0.0f;
    public Transform myHitPos = null;
    public float attackRange = 2.0f;
    public LayerMask myGround = default;

    [SerializeField] GameObject[] OnDamagedEf = new GameObject[2];
    [SerializeField] Transform SpinePos;
    
    float spearSize = 0.5f;
    

    protected void ChangeState(STATE ms)
    {
        if (myState == ms) return;
        myState = ms;
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Normal:
                Appear();
                break;
            case STATE.Battle:
                curDelay = myStat.AttackDelay;
                if (mySensor.myTarget != null)
                    FollowTarget(mySensor.myTarget.transform, myStat.MoveSpeed, myStat.RotSpeed, attackRange, Attacking);
                break;
            case STATE.Dead:
                StageSystem.Inst.spawnEnemy--;
                StageSystem.Inst.clearEnemy++;

                StopAllCoroutines();
                
                myAnim.SetTrigger("Dead");
                mySensor.enabled = false;
                myRigid.useGravity = false;
                myCollider.enabled = false;
                break;
            case STATE.RunAway:
                Running();
                break;
        }
    }

    protected void StateProcess()
    {
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Normal:
                if (Physics.Raycast(transform.position, Vector3.down, 0.1f, myGround) && myAnim.GetBool("IsAir"))
                {
                    myAnim.SetBool("IsAir", false);
                    myAnim.SetTrigger("Landing");
                }

                if (mySensor.myTarget != null && myAnim.GetBool("endAppear"))
                    ChangeState(STATE.Battle);
                break;
            case STATE.Battle:
                if (mySensor.myTarget != null && !mySensor.myTargetB.IsLive)
                {
                    mySensor.LostTarget();
                    ChangeState(STATE.Normal);
                }

                if (!myAnim.GetBool("IsAttacking"))
                {
                    curDelay += Time.deltaTime;
                }
                break;
            case STATE.Dead:
                break;
            case STATE.RunAway:
                if (!myAnim.GetBool("IsJumping"))
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    public void Attacking()
    {
        if (!myAnim.GetBool("IsAttacking") && mySensor.myTargetB.IsLive)
        {
            if (curDelay >= myStat.AttackDelay)
            {
                curDelay = 0.0f;
                myAnim.SetTrigger("Attack");
            }
        }
    }

    public void OnAttack()
    {
        Collider[] list = Physics.OverlapSphere(myHitPos.position, spearSize, mySensor.myEnemy);

        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(30.0f, 1);
        }
    }

    public void Dying()
    {
        StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.5f);

        while (transform.position.y > 0.0f)
        {
            float delta = 0.5f * Time.deltaTime;

            transform.Translate(Vector3.down * delta);

            yield return null;
        }

        Destroy(gameObject);
    }

    public void timetoRun()
    {
        ChangeState(STATE.RunAway);
    }

    void Running()
    {
        myAnim.SetTrigger("Jump");
        myRigid.AddForce(Vector3.back * 2.0f, ForceMode.Impulse);
        myRigid.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
        myAnim.SetBool("IsJumping", true);

    }

    void Appear()
    {
        myAnim.SetBool("IsAir", true);
        myRigid.AddForce(Vector3.forward * 3.0f, ForceMode.Impulse);
    }

    public void OnDamage(float dmg, int i)
    {
        if (!myStat.IsdmgDelay)
        {
            myStat.CurHP -= dmg;
            playEffect(i);

            if (myStat.CurHP <= 0.0f)
            {
                ChangeState(STATE.Dead);
            }
            else
            {
                myAnim.SetTrigger("OnDamaged");
            }
        }
    }

    void playEffect(int i)
    {
        GameObject obj = Instantiate(OnDamagedEf[i]);
        obj.transform.position = SpinePos.position;
    }

    public bool IsLive
    {
        get
        {
            if (myStat.CurHP <= 0.0f)
            {
                return false;
            }
            return true;
        }
    }

    void Awake()
    {
        ChangeState(STATE.Normal);
        myStat.IsBoss = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();

        if (myStat.IsdmgDelay)
        {
            myStat.DamagedDelay -= Time.deltaTime;

            if (myStat.DamagedDelay <= 0.0f)
            {
                myStat.IsdmgDelay = false;
            }
        }
    }
}
