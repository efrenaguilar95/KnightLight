using UnityEngine;
using System.Collections;

// basic WASD-style movement control


[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    //This set of public variables is for movement speed
    public float speed = 6.0f;
    public float gravity = -9.8f;
    public float runSpeed = 15.0f;
    public float normalSpeed = 6.0f;
    public bool isRunning = false;
    public float crchSpeed = 4.0f;
    public float jumpRate = 0.5f;
    private float nextJump = 0.5f;

    private bool _canMove = true;

    //These private variables relate to crouching
    private Transform tr;
    private float height;

    private CharacterController _charController;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        tr = transform;
        height = _charController.height;
    }

    void Update()
    {
        if (_canMove == true)
        {
            //Start of block of code related to regular movement
            float h = height;

            float deltaX = Input.GetAxis("Horizontal") * speed;
            float deltaZ = Input.GetAxis("Vertical") * speed;
            Vector3 movement = new Vector3(deltaX, 0, deltaZ);
            movement = Vector3.ClampMagnitude(movement, speed);

            movement.y = gravity;

            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _charController.Move(movement); //Last line of code related to regular movement

            //Start of block of code related to running
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            {
                isRunning = true;
                speed = runSpeed; //When holding W and Shift, it changed the speed value to the run speed value to make the char move faster
            }
            else
            {
                isRunning = false;
                speed = normalSpeed; //When holding any other combos of keys, the speed value is set to the normal speed value
            }
            //End of block of code related to running

            if (Time.time >= nextJump && _charController.isGrounded && Input.GetKey("space")) //GHETTO JUMP
            {
                nextJump = Time.time + jumpRate;
                transform.Translate(Vector3.up * 145.0f * Time.deltaTime, Space.World);
            }

            //Start of block of code related to crouching
            if (Input.GetKey(KeyCode.C)) //Hold down C to make the character crouch
            {
                h = 0.5f * height; //The character crouches by editing the height of the character controller
                speed = crchSpeed; // slow down when crouching
            }

            Vector3 tmpPosition = tr.position; //This is used to modify the character position when crouching/standing
            var lastHeight = _charController.height; // crouch/stand up smoothly 
            _charController.height = Mathf.Lerp(_charController.height, h, 5 * Time.deltaTime); //This is the direct change to the character controller height
            tmpPosition.y += (_charController.height - lastHeight) / 2; // fix vertical position
            tr.position = tmpPosition; //Change the character position
                                       //End of block of code related to crouching
        }
    }

    public void SetMove()
    {
        if(_canMove == true)
        {
            _canMove = false;
        }
        else
        {
            _canMove = true;
        }
    }


}

