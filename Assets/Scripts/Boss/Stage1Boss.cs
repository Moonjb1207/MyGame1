using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Boss : Boss
{
    float linerAttackMax = 20.0f;
    float linerAttackSpeed = 15.0f;
    float circleAttackSpeed = 2.5f;
    int randomAttackCount = 10;
    bool endPat = true;
    bool IsTrigger = false;
    bool endWarn = false;
    bool firstRndAtk = true;
    GameObject[] spears = new GameObject[10];

    public GameObject[] PatEf = new GameObject[3];
    public AudioClip[] Attacksd = new AudioClip[3];

    GameObject efSound;

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
                int rnd = Random.Range(0, 3);
                PatternAttack(rnd);

                IsAttacking = !IsAttacking;
            }

            if (Physics.Raycast(transform.position, Vector3.down, 0.3f, myGround) &&
                myAnim.GetBool("IsAir") && !myAnim.GetBool("JmpAtk"))
            {
                myAnim.SetBool("IsAir", false);
                myAnim.SetTrigger("Landing");
            }
        }

        base.StateProcess();
    }

    public override void OnDamage(float dmg, int i)
    {
        if (IsAttacking && spears[0].activeInHierarchy)
        {
            for (int k = 0; k < randomAttackCount; k++)
            {
                spears[k].SetActive(false);
            }
        }

        base.OnDamage(dmg, i);
    }

    bool EndPat()
    {
        return endPat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((mySensor.myEnemy & 1 << other.gameObject.layer) != 0)
        {
            if (mySensor.myTargetB.IsLive && IsTrigger)
            {
                mySensor.myTargetB.OnDamage(10.0f, 1);
            }
        }
    }

    public void PatWarnEnd()
    {
        endWarn = !endWarn;
    }

    bool patWarnEnd()
    {
        return endWarn;
    }

    //===================================================================
    //돌진 패턴
    void Pattern_1()
    {
        StopAllCoroutines();
        curDelay = 0.0f;

        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(Attacking_1(i));
        }
    }

    IEnumerator Attacking_1(int i)
    {
        while (!endPat)
        {
            yield return new WaitUntil(EndPat);
        }
        endPat = !endPat;

        Transform Target = mySensor.myTarget.transform;

        StarePlayerOnce(Target, myStat.RotSpeed * 2);

        float dist = 0.0f;

        myAnim.SetTrigger("FRWarn");

        while (!endWarn)
        {
            yield return new WaitUntil(patWarnEnd);
        }
        endWarn = !endWarn;

        IsTrigger = !IsTrigger;
        myCollider.isTrigger = true;
        myRigid.useGravity = false;

        myAnim.SetBool("IsFRunning", true);
        GameObject temp = playEffect(PatEf[0], new Vector3(0.0f, 1.0f, 0.0f), transform);
        EffectSoundManager.Inst.PlayEfSound(EffectSoundManager.Inst.CreateEffectSound(spinePos.position), Attacksd[0]);

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
        myAnim.SetBool("IsFRunning", false);
        Destroy(temp);

        myCollider.isTrigger = false;
        myRigid.useGravity = true;
        IsTrigger = !IsTrigger;
        endPat = !endPat;

        if (i == 2)
        {
            IsAttacking = !IsAttacking;
        }

        yield return new WaitForSeconds(0.1f);
    }

    //===================================================================
    //원형 공격 패턴
    void Pattern_2()
    {
        StopAllCoroutines();
        curDelay = 0.0f;
        StartCoroutine(Attacking_2());
    }

    IEnumerator Attacking_2()
    {
        Transform Target = mySensor.myTarget.transform;
        Vector3 dir = Target.position - transform.position;
        float dist = 2 * dir.magnitude / 3;

        StarePlayerOnce(Target, myStat.RotSpeed * 2);

        myAnim.SetBool("JmpAtk", true);
        myRigid.AddForce(transform.up * dist * 1.7f, ForceMode.Impulse);
        myAnim.SetTrigger("Jump");

        while(dist > 0)
        {
            float delta = circleAttackSpeed * Time.deltaTime;

            if (dist < delta)
            {
                delta = dist;
            }
            dist -= delta;

            transform.Translate(transform.forward * delta, Space.World);

            if (Physics.Raycast(transform.position, Vector3.down, 2.0f, myGround) && myAnim.GetBool("IsAir"))
            {
                myAnim.SetBool("IsAir", false);
                myAnim.SetTrigger("AtkLanding");
            }

            yield return null;
        }

        playEffect(PatEf[1], Vector3.zero, transform);
        EffectSoundManager.Inst.PlayEfSound(EffectSoundManager.Inst.CreateEffectSound(spinePos.position), Attacksd[1]);

        Collider[] list = Physics.OverlapSphere(myHitPos.position, 7.0f, mySensor.myEnemy);

        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(10.0f, 1);
        }

        IsAttacking = !IsAttacking;
        myAnim.SetBool("JmpAtk", false);
    }

    //===================================================================
    //여러 무작위 위치 폭격 패턴
    void Pattern_3()
    {
        StopAllCoroutines();
        curDelay = 0.0f;

        Vector3 pos = transform.position + new Vector3(0.0f, 5.0f, 0.0f);

        EffectSoundManager.Inst.PlayEfSound(EffectSoundManager.Inst.CreateEffectSound(pos), Attacksd[2]);
        playEffect(PatEf[2], transform.up * 5.0f, transform);

        myAnim.SetTrigger("RndAtk");

        if (firstRndAtk)
        {
            for (int i = 0; i < randomAttackCount; i++)
            {
                if (firstRndAtk)
                {
                    spears[i] = CreateSpear(pos);

                    if (i == randomAttackCount - 1)
                        firstRndAtk = false;
                }
            }
        }
        else
        {
            CreateSpear(pos);
        }

        efSound = EffectSoundManager.Inst.CreateEffectSound(pos);
        EffectSoundManager.Inst.PlayEfSound(efSound, spears[0].GetComponent<SpearAttack>().ready);

        StartCoroutine(PatWait());
    }

    public GameObject CreateSpear(Vector3 pos)
    {
        GameObject obj = null;
        if (firstRndAtk)
        {
            obj = Instantiate(Resources.Load("Prefabs/Weapons/Spear") as GameObject
                , pos, Quaternion.identity);
        }
        else
        {
            for (int i = 0; i < randomAttackCount; i++)
            {
                spears[i].transform.position = pos;
                spears[i].SetActive(true);
            }
        }

        return obj;
    }

    IEnumerator PatWait()
    {
        while (!endWarn)
        {
            yield return new WaitUntil(patWarnEnd);
        }
        endWarn = !endWarn;

        for (int i = 0; i < randomAttackCount; i++)
        {
            float Randx, y, Randz;
            Randx = Random.Range(-20.0f, 20.0f);
            y = 0.2f;
            Randz = Random.Range(-20.0f, 20.0f);
            Vector3 target = transform.position + new Vector3(Randx, y, Randz);

            spears[i].GetComponent<SpearAttack>().Attack(target);
        }

        yield return new WaitForSeconds(1.0f);
        efSound.GetComponent<AudioSource>().Stop();

        IsAttacking = !IsAttacking;
    }

    //===================================================================
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
