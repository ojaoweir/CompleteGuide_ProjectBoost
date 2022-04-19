using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketCollision : MonoBehaviour
{
    // Editor Variables
    #pragma warning disable 0649
 
     
    #pragma warning restore 0649    
    // Component references 
     
    // Public variables 
     
    // Private variables 
     
    // Getters 
     
    // Setters 
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // FixedUpdate is called in given interval
    void FixedUpdate()
    {
        
    }
    // Public Methods 
    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case UnityTagConstants.FRIENDLY:
                Debug.Log("IsFriendly");
                break;
            case UnityTagConstants.FINISH:
                int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
                if (nextScene == SceneManager.sceneCountInBuildSettings) nextScene = 0;
                SceneManager.LoadScene(nextScene);

                break;
            default:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }

    // Private Methods    

}