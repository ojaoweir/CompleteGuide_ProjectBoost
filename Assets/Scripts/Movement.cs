using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Editor Variables
#pragma warning disable 0649
    [SerializeField]
    private float thrustForce;
    [SerializeField]
    private float rotateVelocity;
    [SerializeField]
    private Transform centerOfMass;
    [SerializeField]
    private EngineParticlesController thrustParticles;
    [SerializeField]
    private EngineParticlesController rotateParticles;


#pragma warning restore 0649
    // Component references 
    Rigidbody rb;
    AudioSource audioSource;

    // Public variables 

    // Private variables 
     
    // Getters 
     
    // Setters 
     
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        rb.centerOfMass = centerOfMass.localPosition;
    }

    // FixedUpdate is called in given interval
    void FixedUpdate()
    {
        ProcessInput();
    }    

    private void ProcessInput()
    {
        ProcessThrust();
        ProcessRotate();
    }

    private void Rotate(int rotateDirection)
    {
        if (rotateDirection == 0)
        {
            rotateParticles.StopParticles();
            return;
        }
        rb.freezeRotation = true;
        transform.Rotate(rotateDirection * rotateVelocity * Time.deltaTime * Vector3.forward);
        rb.freezeRotation = false;
        rotateParticles.PlayParticles();
    }

    private void Thrust()
    {
        rb.AddRelativeForce(Vector3.up * thrustForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if(rb != null)
        {
            Gizmos.DrawSphere(transform.position + transform.rotation * rb.centerOfMass, 1);
        }
    }

    private void ProcessRotate()
    {
        int rotateDirection = 0;
        if (Input.GetKey(KeyCode.A)) rotateDirection++;
        if (Input.GetKey(KeyCode.D)) rotateDirection--;
        Rotate(rotateDirection);

    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                thrustParticles.PlayParticles();
            }
            Thrust();
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
                thrustParticles.StopParticles();
            }
        }
    }

    // Public Methods 

    // Private Methods    

}
