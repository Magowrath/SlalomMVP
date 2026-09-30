using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Stores the players rigid body for referance
    private Rigidbody myRigidBody;
    //Stores component values of the movement vector
    private float movementX;
    private float movementY;
    //Controls the speed of the player, is editor accessable thanks to public decleration
    public float speed = 0;


    //Stores a referance to the jumpActions action Value as defined by the active actionMap

    private InputAction jumpAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gets the players rigid body component
        myRigidBody = GetComponent<Rigidbody>();

        //Finds the correct action in the actionMap to referance as jumpAction
        jumpAction = InputSystem.actions.FindAction("jump");
    }

    // Is called by the player movement controler detects input
    void OnMove(InputValue movementValue)
    {
        //makes a vector 2d from the inputValue
        Vector2 movementVector = movementValue.Get<Vector2>();
        //Gets the component values of the movement vector
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    //FizedUpdate is called once per frame where frame rate is fixed
    //Is this set somewhere?
    void FixedUpdate()
    {
        //Creates a 3D vector out of the 2D input
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        //Applies the movement vector to the ball as force, magnitude is effected by the speed multiplyer
        myRigidBody.AddForce(movement * speed);



        if (jumpAction.IsPressed())
        {
            myRigidBody.AddForce(Vector3.up * speed, ForceMode.VelocityChange);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
        other.gameObject.SetActive(false);
        }
        
    }

}