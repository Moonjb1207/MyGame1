using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HammerPlayer : Player
{
    List<List<int>> HammerlCombo = new List<List<int>>(){ new List<int> { 0, 0, 1}, new List<int> { 0, 1, 0 } };

    private void Awake()
    {

        myState = STATE.Create;
    }

    // Start is called before the first frame update
    void Start()
    {
        myStat.MaxHP = myStat.CurHP = 100.0f;
        myStat.MoveSpeed = 5.0f;
        myStat.RotSpeed = 700.0f;
        myStat.AttackDelay = 5.0f;
        AttackSize = 3.0f;

        ChangeState(STATE.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override protected void ChangeState(STATE s)
    {
        base.ChangeState(s);
        switch(myState)
        {
            case STATE.InBattle:
                combolAttackList = HammerlCombo;
                break;
            case STATE.Waiting:
                break;
        }
    }




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

    public void OnSpecialAttack()
    {
        Collider[] list = Physics.OverlapSphere(transform.position, 3.0f, myEnemy);
        foreach (Collider col in list)
        {
            Damaging(col, 30.0f, 0);
        }
    }
}
