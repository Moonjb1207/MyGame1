using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Boss : Boss
{
    float linerAttackMax = 20.0f;
    float linerAttackSpeed = 80.0f;

    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void ChangeState(STATE ms)
    {
        base.ChangeState(ms);
    }

    protected override void StateProcess()
    {
        if (myState == STATE.Battle)
        {
            if (curDelay >= myStat.AttackDelay)
            {
                PatternAttack(0);

                IsAttacking = !IsAttacking;
            }
        }

        base.StateProcess();
    }

    //=====================================================================
    //직선 움직임 후 지나온 위치 공격
    void Pattern_1()
    {
        StopAllCoroutines();
        curDelay = 0.0f;

        StartCoroutine(Attacking_1());
    }

    IEnumerator Attacking_1()
    {
        Transform target = mySensor.myTarget.transform;

        StarePlayerOnce(target, myStat.RotSpeed * 2);
        Vector3 prepos = transform.position;

        float dist = 0.0f;
        while (dist < linerAttackMax)
        {
            float delta = linerAttackSpeed * Time.deltaTime;
            
            if (delta > linerAttackMax - dist)
            {
                delta = linerAttackMax - dist;
            }
            dist += delta;

            transform.Translate(transform.forward * delta, Space.World);

            yield return null;
        }
        Vector3 curpos = transform.position;

        Vector3 center = curpos + prepos;
        center.y = curpos.y;
        Vector3 scale = new Vector3(4.0f, 3.0f, 20.0f);

        Collider[] list = Physics.OverlapBox(center, scale, Quaternion.identity, mySensor.myEnemy);

        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(10.0f, 1);
        }

        IsAttacking = !IsAttacking;
    }

    //=====================================================================
    //제자리 원형 공격
    void Pattern_2()
    {

    }

    //=====================================================================
    //부채꼴 공격
    void Pattern_3()
    {

    }

    public override void PatternAttack(int i)
    {
        switch (i)
        {
            case 0:
                Pattern_1();
                break;
            case 1:
                Pattern_2();
                break;
            case 2:
                Pattern_3();
                break;
        }
    }
}
