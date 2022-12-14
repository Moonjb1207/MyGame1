using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterMovement, IBattle
{
    enum STATE
    {
        Create, Normal, Dead
    }

    STATE myState = STATE.Create;
    CharacterStat myStat;
    Dictionary<int, int> comboAttackList = new Dictionary<int, int>();

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

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Normal:
                break;
            case STATE.Dead:
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
                break;
            case STATE.Dead:
                break;
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
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            myAnim.SetFloat("x", Input.GetAxis("Horizontal"));
            myAnim.SetFloat("y", Input.GetAxis("Vertical"));
        }
        else
        {
            myAnim.SetFloat("x", Input.GetAxis("Horizontal") * 0.5f);
            myAnim.SetFloat("y", Input.GetAxis("Vertical") * 0.5f);
        }


        if (!myAnim.GetBool("IsAttacking") && Input.GetMouseButtonDown(0))
        {
            lAttack();
        }
        
        if (!myAnim.GetBool("IsAttacking") && Input.GetMouseButtonDown(1))
        {
            hAttack();
        }
    }

    void lAttack()
    {
        myAnim.SetTrigger("lAttack");
    }

    void hAttack()
    {
        myAnim.SetTrigger("hAttack");
    }
}
