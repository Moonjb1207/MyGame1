using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    Collider myCol = null;
    public LayerMask myPlayer;
    public int type;
    float potionSpeed = 20.0f;

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

        MoveUpDown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveUpDown()
    {
        StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {
        float dist = 0.5f;
        float time = 10.0f;

        Vector3 dir = transform.up;

        while (time > 0)
        {
            time -= Time.deltaTime;

            if (dist <= 0)
            {
                dir = -dir;
                dist = 0.5f;
            }

            float delta = Time.deltaTime;

            if (delta > dist)
            {
                delta = dist;
            }
            dist -= delta;

            transform.Translate(dir * delta, Space.World);

            yield return null;
        }
    }


    public void GetPotion(GameObject player)
    {
        StartCoroutine(GettingPotion(player));
    }

    IEnumerator GettingPotion(GameObject player)
    {
        yield return new WaitForSeconds(1.0f);

        Vector3 dir = player.transform.position - transform.position;
        float dist = dir.magnitude;

        while (dist > 0)
        {
            dir = player.transform.position - transform.position;
            dist = dir.magnitude;

            float delta = potionSpeed * Time.deltaTime;

            if (delta > dist)
            {
                delta = dist;
            }
            dist -= delta;

            transform.Translate(dir.normalized * delta, Space.World);

            yield return null;
        }

        player.GetComponentInParent<Inventory>().get(type);
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if ((myPlayer & 1 << other.gameObject.layer) != 0)
        {
            if (PlayerController.Inst.GetItem)
            {
                GetPotion(other.gameObject);
            }
        }
    }
}
