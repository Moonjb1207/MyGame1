using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Boss : Boss
{
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //돌진 패턴
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
    }

    //원형 공격 패턴
    void Pattern_2()
    {
        StartCoroutine(Attacking_2());
    }

    IEnumerator Attacking_2()
    {
        yield return null;
    }

    //여러 무작위 위치 폭격 패턴
    void Pattern_3()
    {
        StartCoroutine(Attacking_3());
    }

    IEnumerator Attacking_3()
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
