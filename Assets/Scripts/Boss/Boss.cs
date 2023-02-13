using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : Enemy
{
    protected bool IsPatternEnd = true;

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
                {
                    StarePlayer(mySensor.myTarget.transform, myStat.RotSpeed);
                    IsPatternEnd = false;
                }
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
        }
    }

    protected override void StateProcess()
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
                }

                if (!myAnim.GetBool("IsAttacking"))
                {
                    curDelay += Time.deltaTime;
                }

                if (curDelay >= myStat.AttackDelay)
                {
                    int rndpat = Random.Range(0, 3);

                    StopAllCoroutines();
                    PatternAttack(rndpat);
                }

                if (IsPatternEnd)
                {
                    StarePlayer(mySensor.myTarget.transform, myStat.RotSpeed);
                    IsPatternEnd = false;
                }
                break;
            case STATE.Dead:
                break;
        }
    }

    private void Awake()
    {
        ChangeState(STATE.Normal);
        myStat.IsBoss = true;
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

    public virtual void PatternAttack(int i)
    {

    }
}
