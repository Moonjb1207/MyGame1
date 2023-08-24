using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundManager : MonoBehaviour
{
    public static EffectSoundManager Inst = null;

    float effectVolume;

    public Queue<GameObject> efSoundQueue = new Queue<GameObject>();

    public float EffectVolume
    {
        get => effectVolume;
    }

    private void Awake()
    {
        Inst = this;

        if (SettingManager.Inst != null)
        {
            effectVolume = SettingManager.Inst.EFSound;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVolume(float f)
    {
        effectVolume = f;
    }

    public GameObject CreateEffectSound(Vector3 pos)
    {
        GameObject obj;

        if (efSoundQueue.Count == 0)
        {
            obj = Instantiate(Resources.Load("Prefabs/EfSound") as GameObject, pos, Quaternion.identity, this.transform);

        }
        else
        {
            obj = efSoundQueue.Dequeue();
            obj.transform.position = pos;

        }
        obj.SetActive(true);

        return obj;
    }

    public void PlayEfSound(GameObject obj, AudioClip efs)
    {
        StartCoroutine(PlayingSound(obj, efs));
    }

    IEnumerator PlayingSound(GameObject obj, AudioClip efs)
    {
        AudioSource myClip = obj.GetComponent<AudioSource>();
        myClip.clip = efs;
        myClip.volume = effectVolume;

        myClip.Play();

        while (myClip.isPlaying)
        {
            yield return null;
        }

        efSoundQueue.Enqueue(obj);
        obj.SetActive(false);
    }
}
