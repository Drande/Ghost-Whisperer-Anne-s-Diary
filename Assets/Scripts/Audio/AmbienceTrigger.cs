using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceTrigger : MonoBehaviour
{
    //Ambiences
    public AudioSource ambSource;
    public AudioClip ambClip;

    

    private void Start()
    {
        ambSource = GetComponent<AudioSource>();

        ambSource.clip = ambClip;
    }
   private void OnTriggerEnter(Collider other) 
   {
        if (other.CompareTag("Player"))
        {
            if (!ambSource.isPlaying)
            {
                ambSource.Play();
            }
        } 
   }
   private void OnTriggerExit(Collider other) 
   {
     if (other.CompareTag("Player"))
        {
            if (ambSource.isPlaying)
            {
                ambSource.Stop();
            }
        } 
   }
}
