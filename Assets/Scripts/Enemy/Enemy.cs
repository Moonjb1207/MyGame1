using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterMovement, IBattle
{
    enum State
    {
        Create, Normal, Battle, Dead, RunAway
    }

    State myState = State.Create;
    CharacterStat myStat;
    AIPerception mySensor;
    float curDelay = 0.0f;

    void ChangeState(State ms)
    {
        if (myState == ms) return;
        myState = ms;
        switch (myState)
        {
            case State.Create:
                break;
            case State.Normal:
                break;
            case State.Battle:
                FollowTarget(mySensor.myTarget.transform, myStat.MoveSpeed, myStat.RotSpeed, myStat.AttackDelay, Attacking);
                break;
            case State.Dead:
                break;
            case State.RunAway:
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Create:
                break;
            case State.Normal:
                if (mySensor.myTarget != null)
                    ChangeState(State.Battle);
                break;
            case State.Battle:
                if (mySensor.myTarget != null && !mySensor.myTargetB.IsLive)
                {
                    mySensor.LostTarget();
                    ChangeState(State.Normal);
                }

                if (!myAnim.GetBool("IsAttacking"))
                {
                    curDelay += Time.deltaTime;
                }
                break;
            case State.Dead:
                break;
            case State.RunAway:
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
            ChangeState(State.Dead);
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

        ChangeState(State.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }
}
