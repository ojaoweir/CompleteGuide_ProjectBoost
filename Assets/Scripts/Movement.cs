using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Editor Variables
#pragma warning disable 0649
    [SerializeField]
    private float thrustForce;


#pragma warning restore 0649
    // Component references 
    Rigidbody rb;
     
    // Public variables 
     
    // Private variables 
     
    // Getters 
     
    // Setters 
     
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called in given interval
    void Update()
    {
        ProcessInput();
    }    

    private void ProcessInput()
    {
        int rotateDirection = 0;
        if (Input.GetKey(KeyCode.Space)) Thrust();
        if (Input.GetKey(KeyCode.A)) rotateDirection--;
        if (Input.GetKey(KeyCode.D)) rotateDirection++;
        Rotate(rotateDirection);
    }

    private void Rotate(int rotateDirection)
    {
        if (rotateDirection == 0) return;
        Debug.Log("Rotate " + (rotateDirection == -1 ? "Left" : "Right"));
    }

    private void Thrust()
    {
        rb.AddRelativeForce(Vector3.up * thrustForce);
        Debug.Log("Thrust");
    }

    // Public Methods 
     
    // Private Methods    
    
}
