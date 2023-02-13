using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateSpear(Vector3 pos, Vector3 target)
    {
        
    }

    public void Attack(Vector3 target)
    {

    }

    IEnumerator Attacking(Vector3 target)
    {
        Vector3 dir = target - transform.position;

        float dist = dir.magnitude;

        while (dist <= 0.0f)
        {
            yield return null;
        }
    }
}
