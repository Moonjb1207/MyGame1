using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerPlayer : Player
{
    private void Awake()
    {
        int[,] HammerlCombo = { { 0, 0, 1 }, { 0, 1, 0 } };
        combolAttackList = HammerlCombo;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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

    void Damaging(Collider col, float dmg, int i)
    {
        IBattle ib = col.GetComponent<IBattle>();
        ib?.OnDamage(dmg, i);
        Enemy enemy = col.GetComponent<Enemy>();
        enemy.myStat.DamagedDelay = 0.5f;
        enemy.myStat.IsdmgDelay = true;
    }
}
