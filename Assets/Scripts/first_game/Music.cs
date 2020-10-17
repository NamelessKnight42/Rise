using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip[] audios;

    private bool isPlayMusic = true;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<AudioSource>().clip = audios[0];
        this.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {


        if (transform.position.y > 140&&isPlayMusic)
        {
            this.GetComponent<AudioSource>().clip = audios[1];
            this.GetComponent<AudioSource>().Play();
            isPlayMusic = false;
        }
    }
}
