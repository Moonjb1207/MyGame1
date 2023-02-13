using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Boss : Boss
{
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
            Vector3 Target = mySensor.myTarget.transform.position;

            while (true)
            {


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
        yield return null;
    }

    //���� ������ ��ġ ���� ����
    void Pattern_3()
    {
        StartCoroutine(Attacking_3());
    }

    IEnumerator Attacking_3()
    {
        yield return null;
    }

    //���� ���� ����
    void Pattern_4()
    {

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
