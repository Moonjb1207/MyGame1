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
    float spearSize = 0.5f;

    void ChangeState(STATE ms)
    {
        if (myState == ms) return;
        myState = ms;
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Normal:
                break;
            case STATE.Battle:
                curDelay = myStat.AttackDelay;
                if (mySensor.myTarget != null)
                    FollowTarget(mySensor.myTarget.transform, myStat.MoveSpeed, myStat.RotSpeed, attackRange, Attacking);
                break;
            case STATE.Dead:
                StopAllCoroutines();
                myAnim.SetTrigger("Dead");
                mySensor.enabled = false;
                //GetComponent<Collider>().enabled = false;
                break;
            case STATE.RunAway:
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Normal:
                if (mySensor.myTarget != null)
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
            ib?.OnDamage(30.0f);
        }
    }

    public void timetoRun()
    {
        ChangeState(STATE.RunAway);
    }

    void Running()
    {

    }

    void Appear()
    {

    }

    public void OnDamage(float dmg)
    {
        if (!myStat.IsdmgDelay)
        {
            myStat.CurHP -= dmg;

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
