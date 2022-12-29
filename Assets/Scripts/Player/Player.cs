using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CharacterMovement, IBattle
{
    enum STATE
    {
        Create, Normal, Dead
    }

    STATE myState = STATE.Create;
    public CharacterStat myStat;
    int[,] combolAttackList = { { 0, 0, 1 }, { 0, 1, 0 } };
    int[,] combohAttackList = new int[2, 3];
    int[] playCombo = new int[2];
    int pressedButton = -1;
    bool IsRunning = false;
    bool IsComboable = false;
    string comboName = "";
    bool IsRightCombo = false;
    float JumpPower = 7.0f;
    float DownPower = 7.0f;
    float HammerSize = 1.0f;

    public LayerMask myGround = default;
    public LayerMask myEnemy = default;
    public Transform myHitPos = null;

    /*

    0 - 0 0 1
    1 - 1 0 1

    */

    public void OnDamage(float dmg)
    {
        myStat.CurHP -= dmg;
        StageUI.Inst.Player.value = myStat.CurHP / myStat.MaxHP;

        if (myStat.CurHP <= 0.0f)
        {
            ChangeState(STATE.Dead);
        }
        else
        {
            myAnim.SetTrigger("OnDamaged");
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
                StageUI.Inst.Player.value = myStat.CurHP / myStat.MaxHP;
                break;
            case STATE.Dead:
                myAnim.SetTrigger("Dead");
                myAnim.enabled = false;
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

        if (!myAnim.GetBool("IsAttacking"))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                IsRunning = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                IsRunning = false;
            }

            if (!IsRunning)
            {
                myAnim.SetFloat("x", Input.GetAxis("Horizontal") * 0.5f);
                myAnim.SetFloat("y", Input.GetAxis("Vertical") * 0.5f);
            }
            else
            {
                myAnim.SetFloat("x", Input.GetAxis("Horizontal") * 0.5f);
                myAnim.SetFloat("y", Input.GetAxis("Vertical"));
            }
        }

        if (!myAnim.GetBool("IsAir"))
        {
            if (!myAnim.GetBool("IsAttacking"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    lAttack();

                    for (int i = 0; i < 2; i++)
                    {
                        playCombo[i] = i;
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        pressedButton = 0;
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        pressedButton = 1;
                    }

                    if (IsComboable)
                    {
                        IsRightCombo = false;

                        int n = myAnim.GetInteger("ComboIndex");

                        if (++n > 3)
                        {
                            n = 0;

                            for (int i = 0; i < 2; i++)
                            {
                                playCombo[i] = i;
                            }
                        }

                        for (int i = 0; i < 2; i++)
                        {
                            if (playCombo[i] != -1)
                            {
                                if (combolAttackList[i, n] != pressedButton)
                                {
                                    playCombo[i] = -1;
                                }
                                else if (combolAttackList[i, n] == pressedButton)
                                {
                                    comboName = "lAttack" + i;
                                    IsRightCombo = true;
                                }
                            }
                        }

                        if (!IsRightCombo)
                        {
                            comboName = "";
                            myAnim.SetInteger("ComboIndex", 0);
                        }

                        myAnim.SetInteger("ComboIndex", n);
                        myAnim.SetTrigger(comboName);

                    }
                }
            }

            if (!myAnim.GetBool("IsAttacking"))
            {
                if (Input.GetMouseButtonDown(1))
                {
                    hAttack();
                }
            }
            else
            {

            }
        }

        if (!myAnim.GetBool("IsAir") && !myAnim.GetBool("IsAttacking"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myAnim.SetTrigger("Jump");
                myRigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            }
        }

        if (Physics.Raycast(transform.position, Vector3.down, 0.1f, myGround) && myAnim.GetBool("IsAir"))
        {
            myAnim.SetBool("IsAir", false);
            myAnim.SetTrigger("Landing");
        }

        if (myAnim.GetBool("IsJumping") && IsComboable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                myAnim.SetTrigger("jAttack");
                myRigid.AddForce(Vector3.down * DownPower, ForceMode.Impulse);
            }
        }
    }

    void lAttack()
    {
        myAnim.SetInteger("ComboIndex", 0);
        myAnim.SetTrigger("lAttack");
    }

    void hAttack()
    {
        myAnim.SetInteger("ComboIndex", 0);
        myAnim.SetTrigger("hAttack");
    }

    public void OnlAttack()
    {
        StartCoroutine(lAttacking());
    }

    IEnumerator lAttacking()
    {
        while (myAnim.GetBool("IsAttacking"))
        {
            Collider[] list = Physics.OverlapSphere(myHitPos.position, HammerSize, myEnemy);
            if (list != null)
            {
                foreach (Collider col in list)
                {
                    IBattle ib = col.GetComponent<IBattle>();
                    ib?.OnDamage(10.0f);
                    Enemy enemy = col.GetComponent<Enemy>();
                    enemy.myStat.DamagedDelay = 0.5f;
                    enemy.myStat.IsdmgDelay = true;
                }
            }

            yield return null;
        }
    }

    public void OnhAttack()
    {
        StartCoroutine(hAttacking());
    }

    IEnumerator hAttacking()
    {
        while (myAnim.GetBool("IsAttacking"))
        {
            Collider[] list = Physics.OverlapSphere(myHitPos.position, HammerSize, myEnemy);
            if (list != null)
            {
                foreach (Collider col in list)
                {
                    IBattle ib = col.GetComponent<IBattle>();
                    ib?.OnDamage(20.0f);
                    Enemy enemy = col.GetComponent<Enemy>();
                    enemy.myStat.DamagedDelay = 0.5f;
                    enemy.myStat.IsdmgDelay = true;
                }
            }

            yield return null;
        }
    }

    public void OnjAttack()
    {
        Collider[] list = Physics.OverlapSphere(myHitPos.position, 3.0f, myEnemy);
        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(30.0f);
            Enemy enemy = col.GetComponent<Enemy>();
            enemy.myStat.DamagedDelay = 0.5f;
            enemy.myStat.IsdmgDelay = true;
        }
    }

    public void OnAttack3()
    {
        StartCoroutine(attack3ing());
    }

    IEnumerator attack3ing()
    {
        while (myAnim.GetBool("IsAttacking"))
        {
            Collider[] list = Physics.OverlapSphere(myHitPos.position, HammerSize, myEnemy);
            if (list != null)
            {
                foreach (Collider col in list)
                {
                    IBattle ib = col.GetComponent<IBattle>();
                    ib?.OnDamage(15.0f);
                    Enemy enemy = col.GetComponent<Enemy>();
                    enemy.myStat.DamagedDelay = 0.5f;
                    enemy.myStat.IsdmgDelay = true;
                }
            }

            yield return null;
        }
    }

    public void OnMovingAttack()
    {
        StartCoroutine(movingAttacking());
    }

    IEnumerator movingAttacking()
    {
        while(myAnim.GetBool("IsAttacking"))
        {
            Collider[] list = Physics.OverlapSphere(myHitPos.position, HammerSize, myEnemy);
            if (list != null)
            {
                foreach (Collider col in list)
                {
                    IBattle ib = col.GetComponent<IBattle>();
                    ib?.OnDamage(10.0f);
                    Enemy enemy = col.GetComponent<Enemy>();
                    enemy.myStat.DamagedDelay = 0.5f;
                    enemy.myStat.IsdmgDelay = true;
                }
            }

            yield return null;
        }
    }

    public void OnSpecialAttack()
    {
        Collider[] list = Physics.OverlapSphere(transform.position, 3.0f, myEnemy);
        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(30.0f);
            Enemy enemy = col.GetComponent<Enemy>();
            enemy.myStat.DamagedDelay = 0.5f;
            enemy.myStat.IsdmgDelay = true;
        }
    }

    public void OnComboCheck(bool v)
    {
        IsComboable = v;
    }
}
