using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip meow;
    public AudioClip purr;


    [SerializeField] GameObject CatAnimObj;
    private Animator anim_cat;

    void Start()
    {
        anim_cat = CatAnimObj.GetComponent<Animator>();
    }

    bool MeowPlaying = false;
    bool PurrPlaying = false;

    void Update()
    {
        if (anim_cat.GetBool("PlayerNerby") && !MeowPlaying)
        {
            InvokeRepeating("PlayMeow", 1f, Random.Range(3, 15));
            MeowPlaying = true;
        }
        else if (!anim_cat.GetBool("PlayerNerby"))
        {
            CancelInvoke("PlayMeow");
            MeowPlaying = false;
        }

        if (anim_cat.GetBool("Pet") && !PurrPlaying)
        {
            CancelInvoke("PlayMeow");
            PlayPurr();
            PurrPlaying = true;
        }
        else if (!anim_cat.GetBool("Pet") && PurrPlaying)
        {
            MeowPlaying = false;
            audioSource.Stop();
            PurrPlaying = false;
        }

    }

    private void PlayMeow()
    {
        audioSource.PlayOneShot(meow);
    }

    private void PlayPurr()
    {
        audioSource.PlayOneShot(purr);
    }
}
