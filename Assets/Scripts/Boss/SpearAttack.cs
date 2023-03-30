using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearAttack : MonoBehaviour
{
    float spearSpeed = 45.0f;
    Collider myCol = null;

    public AudioClip moving;
    public AudioClip ready;

    public LayerMask myEnemy = default;
    public IBattle myTargetB = null;

    // Start is called before the first frame update
    void Start()
    {
        if (myCol == null)
        {
            myCol = GetComponent<Collider>();
            if (myCol == null)
            {
                myCol = GetComponentInChildren<Collider>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((myEnemy & 1 << other.gameObject.layer) != 0)
        {
            if (myTargetB == null)
            {
                myTargetB = other.transform.GetComponent<IBattle>();

                if (myTargetB.IsLive)
                {
                    myTargetB.OnDamage(10.0f, 1);
                }
            }
        }
    }

    public void Attack(Vector3 target)
    {
        StartCoroutine(Attacking(target));
    }

    IEnumerator Attacking(Vector3 target)
    {
        StartCoroutine(StaringOnce(target));

        yield return new WaitForSeconds(1.0f);

        Vector3 dir = target - transform.position;

        float dist = dir.magnitude;

        while (dist > 0.0f)
        {
            float delta = spearSpeed * Time.deltaTime;

            if (delta > dist)
            {
                delta = dist;
            }
            dist -= delta;

            transform.Translate(transform.forward * delta, Space.World);

            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        gameObject.SetActive(false);
    }

    IEnumerator StaringOnce(Vector3 target)
    {
        Vector3 dir = target - transform.position;

        while (target != null)
        {
            float rdelta = 1000.0f * Time.deltaTime;

            Vector3 rot = Vector3.RotateTowards(transform.forward, dir, rdelta * Mathf.Deg2Rad, 0.0f);
            transform.rotation = Quaternion.LookRotation(rot);

            yield return null;
        }
    }
}
