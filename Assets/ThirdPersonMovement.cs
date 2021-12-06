using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam,RhinoTF;
    public  Animator characterAnimator;
    public Rigidbody rhinoRB;
    

    public float maxSpeed = 10f;
    public float accSpeed = 0.04f;
    public float deAccSpeed = 0.02f;
    public float turnSpeed = 20f;
    public float walkingSpeed = 20f;

    float acceleration = 0f;
    

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    Vector3 moveBackVec3 = new Vector3(0, 0, -1);
    Vector3 moveFrontkVec3 = new Vector3(0, 0, 1);
    Vector3 movingVec;


    void Awake()
    {
        characterAnimator = GetComponentInChildren<Animator>();
        rhinoRB = GetComponentInChildren<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float translation = Input.GetAxis("Vertical") * maxSpeed;
        rhinoRB.AddForce(Physics.gravity * rhinoRB.mass * Time.deltaTime);
        translation *= Time.deltaTime;
        Vector3 direction = new Vector3(0f, translation, 0f).normalized;


 

        //if (Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) )



        if (Input.GetKey("w") || Input.GetKey("s"))
        {
            if (Input.GetKey("w")) movingVec = moveFrontkVec3;
            if (Input.GetKey("s")) movingVec = moveBackVec3;


            //Set walking speed and animation
            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            {
                characterAnimator.SetFloat("Speed", walkingSpeed);
                if (acceleration > walkingSpeed) acceleration -= deAccSpeed;
                else acceleration += accSpeed;
            }
            if ( Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                //Running acceleration and animation
                if (acceleration < 10)
                {
                    acceleration += accSpeed;
                    characterAnimator.SetFloat("Speed", acceleration);
                }
            }
        }
        // Rotation
        if (Input.GetKey(KeyCode.A))
            RhinoTF.Rotate(-Vector3.up * turnSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            RhinoTF.Rotate(Vector3.up * turnSpeed * Time.deltaTime);

    


  

        if (acceleration > 0 && !Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) )
        {
            acceleration -= deAccSpeed;
            characterAnimator.SetFloat("Speed", acceleration);
        }
        
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * movingVec;

            controller.Move(moveDir.normalized * (maxSpeed * acceleration * 0.5f ) * Time.deltaTime);
        }
    }

}
