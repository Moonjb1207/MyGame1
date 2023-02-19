using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : Enemy, IBattle
{
    protected override void ChangeState(STATE ms)
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

    protected override void StateProcess()
    {
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Normal:
                if (Physics.Raycast(transform.position, Vector3.down, 0.3f, myGround) && myAnim.GetBool("IsAir"))
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

    private void Awake()
    {
        ChangeState(STATE.Normal);
        myStat.IsBoss = false;
    }


    // Start is called before the first frame update
    protected override void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
