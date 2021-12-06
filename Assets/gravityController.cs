using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityController : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody rhinoRB;
    public float gravityMulti = 0.5f; 

    void Awake()
    {
        rhinoRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rhinoRB.position.y > 0)
            rhinoRB.AddForce(Physics.gravity * rhinoRB.mass);
    }
}
