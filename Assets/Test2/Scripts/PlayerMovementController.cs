using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerMovementController : MonoBehaviour
{

    public Joystick joystick;
    public FixedTouchField fixedTouchField;
    private RigidbodyFirstPersonController rigidbodyfirstpersioncontroller;
    

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyfirstpersioncontroller = GetComponent<RigidbodyFirstPersonController>();
        
        animator = GetComponent<Animator>();
    }

    
	
    void FixedUpdate()
    {
        rigidbodyfirstpersioncontroller.joystickInputAxis.x = joystick.Horizontal;
        rigidbodyfirstpersioncontroller.joystickInputAxis.y = joystick.Vertical;
        rigidbodyfirstpersioncontroller.mouseLook.lookInputAxis = fixedTouchField.TouchDist;

        animator.SetFloat("Horizontal", joystick.Horizontal);
        animator.SetFloat("Vertical", joystick.Vertical);

        if (Mathf.Abs(joystick.Horizontal) > 0.9f || Mathf.Abs(joystick.Vertical) > 0.9f)
        {
            rigidbodyfirstpersioncontroller.movementSettings.ForwardSpeed = 8f;
            animator.SetBool("IsRunning", true);
        }
        else
        {
            rigidbodyfirstpersioncontroller.movementSettings.ForwardSpeed = 4f;
            animator.SetBool("IsRunning", false);
        }
    }
}
