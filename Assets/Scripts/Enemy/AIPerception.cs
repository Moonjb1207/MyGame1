using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPerception : MonoBehaviour
{
    public LayerMask myEnemy = default;
    public GameObject myTarget = null;
    public IBattle myTargetB = null;

    public MyAction FindTarget = null;

    public void LostTarget()
    {
        myTarget = null;
        StopAllCoroutines();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if ((myEnemy & 1 << other.gameObject.layer) != 0)
        {
            if (myTarget == null)
            {
                myTarget = other.gameObject;
                myTargetB = other.transform.GetComponent<IBattle>();

                if (myTargetB.IsLive)
                {
                    FindTarget?.Invoke();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        LostTarget();
        myTarget = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
