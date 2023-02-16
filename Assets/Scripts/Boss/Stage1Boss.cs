using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Boss : Boss
{
    public GameObject[] RanAttWarn = new GameObject[10];
    public GameObject LinerAttWarn;
    public GameObject CircleAttWarn;

    float linerAttackMax = 20.0f;
    float linerAttackSpeed = 10.0f;

    float circleAttackSpeed = 5.0f;

    int randomAttackCount = 10;

    bool endWarning = false;

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

    bool EndWarning()
    {
        return endWarning;
    }

    //돌진 패턴
    void Pattern_1()
    {
        StartCoroutine(Attacking_1());
    }

    IEnumerator Attacking_1()
    {
        StartCoroutine(LinerWarning());

        yield return new WaitUntil(EndWarning);

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
        endWarning = false;
    }
    
    IEnumerator LinerWarning()
    {
        LinerAttWarn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        LinerAttWarn.transform.position = Vector3.zero;

        Vector3 scale = new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 pos = Vector3.zero;

        while (LinerAttWarn.transform.localScale.y > 10.0f)
        {
            scale.y += Time.deltaTime;
            LinerAttWarn.transform.localScale = scale;

            pos.z += Time.deltaTime / 2;
            LinerAttWarn.transform.position = pos;

            yield return null;
        }

        endWarning = true;
    }

    //원형 공격 패턴
    void Pattern_2()
    {
        StartCoroutine(Attacking_2());
    }

    IEnumerator Attacking_2()
    {
        StartCoroutine(CircleWarning());

        yield return new WaitUntil(EndWarning);

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
        endWarning = false;
    }

    IEnumerator CircleWarning()
    {
        Transform Target = mySensor.myTarget.transform;
        Vector3 dir = Target.position - transform.position;
        float dist = dir.magnitude / 2;

        CircleAttWarn.transform.Translate(transform.forward * dist, Space.World);

        Vector3 scale = Vector3.zero;

        while (scale.x > 7.0f)
        {
            scale.x += Time.deltaTime;
            scale.y += Time.deltaTime;

            CircleAttWarn.transform.localScale = scale;

            yield return null;
        }

        endWarning = true;
    }

    //여러 무작위 위치 폭격 패턴
    void Pattern_3()
    {
        for (int i = 0; i < randomAttackCount; i++)
        {

        }
    }

    //원뿔 공격 패턴
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
