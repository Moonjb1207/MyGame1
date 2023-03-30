using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Boss : Enemy
{
    protected bool IsAttacking = false;

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
                }
                break;
            case STATE.Dead:
                StageSystem.Inst.spawnEnemy--;
                StageSystem.Inst.clearEnemy++;
                StageSystem.Inst.Score += 100;

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
                if (Physics.Raycast(transform.position, Vector3.down, 0.3f, myGround) && myAnim.GetBool("IsAir"))
                {
                    myAnim.SetBool("IsAir", false);
                    myAnim.SetTrigger("Landing");
                }
                
                if (mySensor.myTarget != null && myAnim.GetBool("endAppear"))
                    ChangeState(STATE.Battle);
                break;
            case STATE.Battle:
                if (mySensor.myTarget == null || !mySensor.myTargetB.IsLive)
                {
                    mySensor.LostTarget();
                }

                if (!IsAttacking)
                {
                    StopAllCoroutines();
                    StarePlayer(mySensor.myTarget.transform, myStat.RotSpeed);
                    curDelay += Time.deltaTime;
                }
                break;
            case STATE.Dead:
                break;
        }
    }

    public override void OnDamage(float dmg, int i)
    {
        base.OnDamage(dmg, i);
        if (IsAttacking)
        {
            IsAttacking = !IsAttacking;
        }
    }

    protected override void Awake()
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

    protected GameObject playEffect(GameObject obj, Vector3 pos, Transform par = null)
    {
        GameObject temp = null;
        if (par != null)
            temp = Instantiate(obj, par);
        else
            temp = Instantiate(obj);

        obj.transform.position = pos;

        return temp;
    }
}
