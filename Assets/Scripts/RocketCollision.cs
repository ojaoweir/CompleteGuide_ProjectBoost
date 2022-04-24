using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketCollision : MonoBehaviour
{
    // Editor Variables
#pragma warning disable 0649
    [SerializeField]
    private float sceneLoadDelay;
    [SerializeField]
    private AudioClip levelFinished;
    [SerializeField]
    private AudioClip playerCrash;
    [SerializeField]
    private ParticleSystem levelFinishedParticles;
    [SerializeField]
    private ParticleSystem playerCrashParticles;
#pragma warning restore 0649
    // Component references 

    // Public variables 

    // Private variables 
    private bool sceneInTransition;
     
    // Getters 
     
    // Setters 
     
    // Start is called before the first frame update
    void Start()
    {
        sceneInTransition = false;
    }

    // FixedUpdate is called in given interval
    void FixedUpdate()
    {
        
    }
    // Public Methods 
    void OnCollisionEnter(Collision collision)
    {
        if (sceneInTransition) return;
        switch(collision.gameObject.tag)
        {
            case UnityTagConstants.FRIENDLY:
                Debug.Log("IsFriendly");
                break;
            case UnityTagConstants.FINISH:
                PlayerFinish();
                break;
            default:
                PlayerCrash();
                break;
        }
    }

    private void PlayerFinish()
    {
        sceneInTransition = true;
        GetComponent<Movement>().enabled = false;
        var audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.PlayOneShot(levelFinished);
        levelFinishedParticles.Play();
        Invoke("LoadNextLevel", sceneLoadDelay);

    }
    private void PlayerCrash()
    {
        sceneInTransition = true;
        GetComponent<Movement>().enabled = false;
        var audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.PlayOneShot(playerCrash);
        playerCrashParticles.Play();
        Invoke("ReloadLevel", sceneLoadDelay);

    }

    private void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings) nextScene = 0;
        SceneManager.LoadScene(nextScene);
    }
        private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }



    // Private Methods    

}