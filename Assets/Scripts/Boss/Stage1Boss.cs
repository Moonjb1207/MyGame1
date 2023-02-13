using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Boss : Boss
{
    float linerAttackMax = 20.0f;
    float linerAttackSpeed = 10.0f;

    float circleAttackSpeed = 5.0f;

    int randomAttackCount = 10;

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
        base.StateProcess();
    }

    //���� ����
    void Pattern_1()
    {
        StartCoroutine(Attacking_1());
    }

    IEnumerator Attacking_1()
    {
        for (int i = 0; i < 3; i++)
        {
            Transform Target = mySensor.myTarget.transform;

            StarePlayer(Target, myStat.RotSpeed * 2);

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
        }

        IsPatternEnd = true;
    }

    //���� ���� ����
    void Pattern_2()
    {
        StartCoroutine(Attacking_2());
    }

    IEnumerator Attacking_2()
    {
        Transform Target = mySensor.myTarget.transform;
        Vector3 dir = Target.position - transform.position;
        float dist = dir.magnitude / 2;

        StarePlayer(Target, myStat.RotSpeed * 2);
        myRigid.AddForce(transform.up * 7.0f, ForceMode.Acceleration);

        while(dist <= 0)
        {
            float delta = circleAttackSpeed * Time.deltaTime;

            if (dist < delta)
            {
                delta = dist;
            }
            dist -= delta;

            transform.Translate(transform.forward * delta, Space.World);

            yield return null;
        }

        IsPatternEnd = true;
    }

    //���� ������ ��ġ ���� ����
    void Pattern_3()
    {
        for (int i = 0; i < randomAttackCount; i++)
        {

        }
    }

    //���� ���� ����
    void Pattern_4()
    {
        StartCoroutine(Attacking_4());
    }

    IEnumerator Attacking_4()
    {
        yield return null;
    }

    public override void PatternAttack(int i)
    {
        switch(i)
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
