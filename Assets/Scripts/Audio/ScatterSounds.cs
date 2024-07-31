using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterSounds : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource source;
    [Range(0.1f,0.5f)]
    public float volumeChangeMultiplier = 0.2f;
    [Range(0.1f, 0.5f)]
    public float pitchChangeMultiplier = 0.2f;

     //Timer
    public float timer = 10f;
    public float minTime;
    public float maxTime;

    //Trigger Confirmation 

    private bool scatterOn = false;    


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (scatterOn)
        {
            
            timer -= Time.deltaTime;
               
            if(timer <= 0)
            {
                source.clip = sounds[Random.Range(0, sounds.Length)];
                source.volume = Random.Range(1 - volumeChangeMultiplier, 1);
                source.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
                source.PlayOneShot(source.clip);
                timer = Random.Range(minTime, maxTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
   {
        if (other.CompareTag("Player"))
        {
            scatterOn = true;
        } 
   }
   private void OnTriggerExit(Collider other) 
   {
     if (other.CompareTag("Player"))
        {
           scatterOn = false;
        } 
   }
}

