using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;
    public  Animator characterAnimator;

    public float speed = 6f;

    float acceleration = 0f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    private void Awake()
    {
        characterAnimator = GetComponentInChildren<Animator>();



    }



    // Update is called once per frame
    void Update()
    {



        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKey(KeyCode.W))

        {
            if (acceleration < 10)
            {
                acceleration += 0.1f;
                characterAnimator.SetFloat("Speed", acceleration);
            }


        }
        else
        {
            if (acceleration > 0)
            {
                acceleration -= 0.5f;
                characterAnimator.SetFloat("Speed", acceleration);
            }
        }

        if (direction.magnitude >= 0.1f)

        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;


            
            controller.Move(moveDir.normalized * (speed * acceleration * 0.5f ) * Time.deltaTime);

        }

    }
}
