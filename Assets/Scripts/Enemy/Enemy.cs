using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterMovement, IBattle
{
    public enum STATE
    {
        Create, Normal, Battle, Dead, RunAway
    }

    public STATE myState = STATE.Create;
    public EnemyStat myStat;
    public AIPerception mySensor;
    public float curDelay = 0.0f;
    public Transform myHitPos = null;
    public float attackRange = 2.0f;
    public LayerMask myGround = default;

    [SerializeField] GameObject[] OnDamagedEf = new GameObject[2];
    [SerializeField] Transform SpinePos;

    public AudioClip DamagedSound = null;
    public AudioClip HitSound = null;

    public Transform spinePos
    {
        get => SpinePos;
    }

    protected float spearSize = 0.5f;
    

    protected virtual void ChangeState(STATE ms)
    {
        
    }

    protected virtual void StateProcess()
    {
        
    }

    public void Attacking()
    {
        if (!myAnim.GetBool("IsAttacking") && mySensor.myTargetB.IsLive)
        {
            if (curDelay >= myStat.AttackDelay)
            {
                curDelay = 0.0f;
                myAnim.SetTrigger("Attack");

                EffectSoundManager.Inst.PlayEfSound(EffectSoundManager.Inst.CreateEffectSound(myHitPos.position), HitSound);
            }
        }
    }

    public void OnAttack()
    {
        Collider[] list = Physics.OverlapSphere(myHitPos.position, spearSize, mySensor.myEnemy);

        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(30.0f, 1);
        }
    }

    public void Dying()
    {
        StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.5f);

        while (transform.position.y > 0.0f)
        {
            float delta = 0.5f * Time.deltaTime;

            transform.Translate(Vector3.down * delta);

            yield return null;
        }

        Destroy(gameObject);
    }

    public void timetoRun()
    {
        ChangeState(STATE.RunAway);
    }

    protected void Running()
    {
        myAnim.SetTrigger("Jump");
        myRigid.AddForce(Vector3.back * 2.0f, ForceMode.Impulse);
        myRigid.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
        myAnim.SetBool("IsJumping", true);

    }

    protected void Appear()
    {
        myAnim.SetBool("IsAir", true);
        myRigid.AddForce(Vector3.forward * 3.0f, ForceMode.Impulse);
    }

    public virtual void OnDamage(float dmg, int i)
    {
        if (!myStat.IsdmgDelay)
        {
            myStat.CurHP -= dmg;
            playEffect(i);
            if (myStat.IsBoss)
            {
                StageUI.Inst.Boss.value = myStat.CurHP / myStat.MaxHP;
            }

            EffectSoundManager.Inst.PlayEfSound(EffectSoundManager.Inst.CreateEffectSound(SpinePos.position), DamagedSound);

            if (myStat.CurHP <= 0.0f)
            {
                StopAllCoroutines();
                ChangeState(STATE.Dead);
            }
            else
            {
                myAnim.SetTrigger("OnDamaged");
            }
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

    protected virtual void Awake()
    {
        
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        StateProcess();

        if (myStat.IsdmgDelay)
        {
            myStat.DamagedDelay -= Time.deltaTime;

            if (myStat.DamagedDelay <= 0.0f)
            {
                myStat.IsdmgDelay = false;
            }
        }
    }
}
