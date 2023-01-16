using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : CharacterMovement, IBattle
{
    public enum STATE
    {
        Create, Normal, Battle, Dead,
    }

    STATE myState = STATE.Create;
    public EnemyStat myStat;
    public float curDelay = 0.0f;
    public AIPerception mySensor = null;
    public LayerMask myGround = default;


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
                {
                    StarePlayer(mySensor.myTarget.transform, myStat.RotSpeed);
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

                if (curDelay >= myStat.AttackDelay)
                {
                    int rndpat = Random.Range(0, 3);

                    PatternAttack(rndpat);
                }
                break;
            case STATE.Dead:
                break;
        }
    }

    void Appear()
    {
        myAnim.SetBool("IsAir", true);
        myRigid.AddForce(Vector3.forward * 3.0f, ForceMode.Impulse);
    }

    public void OnDamage(float dmg, int i)
    {

    }

    public bool IsLive
    {
        get
        {
            return true;
        }
    }

    private void Awake()
    {
        ChangeState(STATE.Normal);
        myStat.IsBoss = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    public virtual void PatternAttack(int i)
    {

    }
}
