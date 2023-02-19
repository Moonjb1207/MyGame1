using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MyAction();
public delegate void MyAction2(Transform a, float b);

public class CharacterMovement : CharacterProperty
{
    Coroutine coFollow = null;   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Moving(Vector3 target, float MoveSpeed)
    {
        while (target != null)
        {
            yield return null;
        }
    }

    protected void FollowTarget(Transform target, float MoveSpeed, float RotSpeed, float AttackRange, MyAction reached = null)
    {
        if (coFollow != null) StopCoroutine(coFollow);
        coFollow = StartCoroutine(FollowingTarget(target, MoveSpeed, RotSpeed, AttackRange, reached));
    }

    IEnumerator FollowingTarget(Transform target, float MoveSpeed, float RotSpeed, float AttackRange, MyAction reached = null)
    {
        while (target != null)
        {
            float rdelta = RotSpeed * Time.deltaTime;

            Vector3 dir = target.position - transform.position;

            dir.y = 0.0f;

            Vector3 rot = Vector3.RotateTowards(transform.forward, dir, rdelta * Mathf.Deg2Rad, 0.0f);
            transform.rotation = Quaternion.LookRotation(rot);

            if (dir.magnitude > AttackRange && !myAnim.GetBool("IsAttacking"))
            {
                myAnim.SetBool("IsMoving", true);

                float delta = MoveSpeed * Time.deltaTime;

                if (delta > dir.magnitude - AttackRange)
                {
                    delta = dir.magnitude - AttackRange;
                }

                transform.Translate(dir.normalized * delta, Space.World);
            }
            else if (dir.magnitude <= AttackRange)
            {
                myAnim.SetBool("IsMoving", false);
                reached?.Invoke();
            }

            yield return null;
        }
    }

    protected void StarePlayer(Transform target, float rotSpeed)
    {
        if (coFollow != null) StopCoroutine(coFollow);
        coFollow = StartCoroutine(Staring(target, rotSpeed));
    }

    IEnumerator Staring(Transform target, float rotSpeed)
    {
        while (target != null)
        {
            float rdelta = rotSpeed * Time.deltaTime;

            Vector3 dir = target.position - transform.position;

            dir.y = 0.0f;

            Vector3 rot = Vector3.RotateTowards(transform.forward, dir, rdelta * Mathf.Deg2Rad, 0.0f);
            transform.rotation = Quaternion.LookRotation(rot);

            yield return null;
        }
    }

    protected void StarePlayerOnce(Transform target, float rotSpeed)
    {
        if (coFollow != null) StopCoroutine(coFollow);
        coFollow = StartCoroutine(StaringOnce(target, rotSpeed));
    }

    IEnumerator StaringOnce(Transform target, float rotSpeed)
    {
        Vector3 dir = target.position - transform.position;

        dir.y = 0.0f;

        while (target != null)
        {
            float rdelta = rotSpeed * Time.deltaTime;

            dir.y = 0.0f;

            Vector3 rot = Vector3.RotateTowards(transform.forward, dir, rdelta * Mathf.Deg2Rad, 0.0f);
            transform.rotation = Quaternion.LookRotation(rot);

            yield return null;
        }
    }
}
