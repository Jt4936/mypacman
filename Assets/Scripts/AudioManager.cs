using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static int m_audioSize;
    public AudioSource audioSource;      
    public AudioClip introClip;   
    public AudioClip normalClip;
    public AudioClip deadClip;
    public AudioClip scaredClip;

    private bool isIntroPlayed = false; 


    // Use this for initialization
    void Start()
    {

        if (PlayerPrefs.HasKey("AudioSize"))
        {
            m_audioSize = PlayerPrefs.GetInt("AudioSize");
        }
        else
        {
            m_audioSize = 100;
        }
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = m_audioSize / 100f;
        }

        StartCoroutine(PlayIntroAndNormal());

    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = m_audioSize / 100f;
    }

    private IEnumerator PlayIntroAndNormal()
    {
       
        if (introClip != null)
        {
            audioSource.clip = introClip;
            audioSource.Play();
            yield return new WaitForSeconds(4f); 
        }

       
        if (normalClip != null)
        {
            audioSource.clip = normalClip;
            audioSource.loop = true; 
            audioSource.Play();
        }
    }

    public void PlayClip(string clipName)
    {
        if(clipName == "dead")
        {
            audioSource.clip = deadClip;
            audioSource.Play();
        }
        else if (clipName == "scared")
        {
            audioSource.clip = scaredClip;
            audioSource.Play();
        }
    }
}
