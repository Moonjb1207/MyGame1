using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterMovement, IBattle
{
    enum STATE
    {
        Create, Normal, Battle, Dead, RunAway
    }

    STATE myState = STATE.Create;
    CharacterStat myStat;
    AIPerception mySensor;
    float curDelay = 0.0f;

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
                FollowTarget(mySensor.myTarget.transform, myStat.MoveSpeed, myStat.RotSpeed, myStat.AttackDelay, Attacking);
                break;
            case STATE.Dead:
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

    void Attacking()
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

    public void OnDamage(float dmg)
    {
        myStat.CurHP -= dmg;

        if (myStat.CurHP <= 0.0f)
        {
            ChangeState(STATE.Dead);
        }
        else
        {

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

    // Start is called before the first frame update
    void Start()
    {
        myStat.MaxHP = myStat.CurHP = 100.0f;
        myStat.MoveSpeed = 5.0f;
        myStat.RotSpeed = 700.0f;
        myStat.AttackDelay = 5.0f;

        ChangeState(STATE.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }
}
