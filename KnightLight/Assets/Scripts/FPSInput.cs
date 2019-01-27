using UnityEngine;
using System.Collections;

// basic WASD-style movement control


[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    //This set of public variables is for movement speed
    public float speed = 10.0f;
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
    //public Animation anim;
    public Animator ani;
    void Start()
    {
        ani = GameObject.FindGameObjectWithTag("KnightSprite").GetComponent<Animator>();
        //anim["spin"].layer = 123;
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
            Vector3 inputdirection = Vector3.zero;
            //float deltaX = Input.GetAxis("LeftJoystickHorizontal") * speed;
            //float deltaZ = Input.GetAxis("LeftJoystickVertical") * speed;
            inputdirection.x = Input.GetAxis("LeftJoystickHorizontal")*speed;
            inputdirection.z = Input.GetAxis("LeftJoystickVertical")*speed;
            // ani.SetFloat("Game", 1);
            if (inputdirection.x < 1f && inputdirection.x > -1f)
            {
                inputdirection.x = 0;
            }
            if (inputdirection.z < 1f && inputdirection.z > -1f)
            {
                inputdirection.z = 0;
            }
            //            Vector3 movement = new Vector3(deltaX, 0, deltaZ);
            Vector3 movement = new Vector3(inputdirection.x*-1, 0, inputdirection.z);

            movement = Vector3.ClampMagnitude(movement, speed);

            movement.y = gravity;

            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _charController.Move(movement); //Last line of code related to regular movement
       //     Debug.Log("X : "+inputdirection.x);
         //   Debug.Log("Z: "+inputdirection.z);

            if (inputdirection.x < 0f)
            {
                ani.Play("Knight_Right_walk");
            }
            if(inputdirection.x > 0f)
            {
                ani.Play("Knight_Left_walk");
            }
            //Start of block of code related to running
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isRunning = true;
                speed = runSpeed; //When holding W and Shift, it changed the speed value to the run speed value to make the char move faster
            }
            else
            {
                isRunning = false;
                speed = normalSpeed; //When holding any other combos of keys, the speed value is set to the normal speed value
            }
            

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

