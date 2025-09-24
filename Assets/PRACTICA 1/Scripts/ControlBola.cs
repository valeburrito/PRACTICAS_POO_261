using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBola : MonoBehaviour
{
    public Rigidbody rb;
    public float fuerzaDeLanzamiento = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            rb.AddForce(Vector3.forward * fuerzaDeLanzamiento);
        }
    }
}
