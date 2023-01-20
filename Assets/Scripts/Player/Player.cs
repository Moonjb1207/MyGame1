using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CharacterMovement, IBattle
{
    protected enum STATE
    {
        Create, Normal, Dead, InBattle, Waiting
    }

    protected STATE myState = STATE.Create;
    public CharacterStat myStat;
    public List<List<int>> combolAttackList = new List<List<int>>();
    public List<List<int>> combohAttackList = new List<List<int>>();
    public Sprite myIMG = null;

    /*
    public int[] playCombo = new int[2];
    public int pressedButton = -1;
    public bool IsRunning = false;
    public string comboName = "";
    public bool IsRightCombo = false;
    */

    public bool IsComboable = false;
    public float JumpPower = 7.0f;
    public float DownPower = 7.0f;
    protected float AttackSize;
    [SerializeField] Transform SpinePos;
    [SerializeField] GameObject[] OnDamagedEf = new GameObject[2];
    [SerializeField] GameObject SpecialEf;
    [SerializeField] GameObject JumpAtEf;


    public LayerMask myGround = default;
    public LayerMask myEnemy = default;
    public Transform myHitPos = null;

    /*
    { { 0, 0, 1 }, { 0, 1, 0 } }
    0 - 0 0 1
    1 - 1 0 1

    */

    public void OnDamage(float dmg, int i)
    {
        myStat.CurHP -= dmg;
        StageUI.Inst.Player.value = myStat.CurHP / myStat.MaxHP;
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

    virtual protected void ChangeState(STATE s)
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
                StopAllCoroutines();
                myAnim.SetTrigger("Dead");
                myAnim.enabled = false;
                StageSystem.Inst.PlayerDead();
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

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(STATE.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    #region Attack

    public void lAttack()
    {
        myAnim.SetInteger("ComboIndex", 0);
        myAnim.SetTrigger("lAttack");
    }

    public void hAttack()
    {
        myAnim.SetInteger("ComboIndex", 0);
        myAnim.SetTrigger("hAttack");
    }
    
    /*
    public void OnlAttack()
    {
        StartCoroutine(lAttacking());
    }

    IEnumerator lAttacking()
    {
        while (myAnim.GetBool("IsAttacking"))
        {
            Collider[] list = Physics.OverlapSphere(myHitPos.position, AttackSize, myEnemy);
            if (list != null)
            {
                foreach (Collider col in list)
                {
                    Damaging(col, 30.0f, 0);
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
            Collider[] list = Physics.OverlapSphere(myHitPos.position, AttackSize, myEnemy);
            if (list != null)
            {
                foreach (Collider col in list)
                {
                    Damaging(col, 40.0f, 0);
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
            Damaging(col, 30.0f, 0);
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
            Collider[] list = Physics.OverlapSphere(myHitPos.position, AttackSize, myEnemy);
            if (list != null)
            {
                foreach (Collider col in list)
                {
                    Damaging(col, 30.0f, 0);
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
            Collider[] list = Physics.OverlapSphere(myHitPos.position, AttackSize, myEnemy);
            if (list != null)
            {
                foreach (Collider col in list)
                {
                    Damaging(col, 40.0f, 0);
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
            Damaging(col, 30.0f, 0);
        }
    }

    void Damaging(Collider col, float dmg, int i)
    {
        IBattle ib = col.GetComponent<IBattle>();
        ib?.OnDamage(dmg, i);
        Enemy enemy = col.GetComponent<Enemy>();
        enemy.myStat.DamagedDelay = 0.5f;
        enemy.myStat.IsdmgDelay = true;
    }
    */

    public void OnComboCheck(bool v)
    {
        IsComboable = v;
    }
    #endregion

    public void Dying()
    {
        StartCoroutine(Dead());
    }
    
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(3.0f);
    }

    public void InCharc()
    {
        ChangeState(STATE.InBattle);
    }

    public void OutCharc()
    {
        ChangeState(STATE.Waiting);
    }

    protected void Damaging(Collider col, float dmg, int i)
    {
        IBattle ib = col.GetComponent<IBattle>();
        ib?.OnDamage(dmg, i);
        Enemy enemy = col.GetComponent<Enemy>();
        enemy.myStat.DamagedDelay = 0.5f;
        enemy.myStat.IsdmgDelay = true;
    }
}
