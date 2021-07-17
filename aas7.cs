using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aas7 : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float ShiftSpeed = 3.5f;
    public float jumpSpeed = 8.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public float curSpeedX = 0;
    public float curSpeedY = 0;
    public float gravity = 20.0f;
    public float health = 24;
    public float rotationX = 0;

    public CharacterController characterController;
    public GameObject respawnpoint;
    public GameObject body;
    public Camera playerCamera;
    public Vector3 moveDirection = Vector3.zero;

    [HideInInspector]
    public bool canMove = true;
    public bool IsGravity = true;
    public bool isShifting = true;
    public bool isHiding = false;
    public bool canJomp = false;
    public bool isShiftable = false;
    //public aas8 splash;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        bool isRunning = (Input.GetKey(KeyCode.LeftControl));
        bool isShifting = IntToBool((int)((BoolToInt(Input.GetKey(KeyCode.LeftAlt)))*BoolToInt(!isShiftable))+(BoolToInt(Input.GetKey(KeyCode.LeftShift))*BoolToInt(isShiftable)));

        RaycastHit hit1;
        var ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray1, out hit1))
        {
            if (hit1.transform.gameObject.tag == "plrInteract" && Input.GetKey("e"))
            {
                if (hit1.transform.gameObject.GetComponent<interaction>().type == "E")
                {
                    hit1.transform.gameObject.GetComponent<interaction>().active = true;
                }
            }
        }

        if (!isHiding)
        {            
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            float movementDirectionY = moveDirection.y;

            // curSpeedX = ((walkingSpeed*Input.GetAxis("Horizontal"))*BoolToInt((isShifting && isRunning)||!(isShifting && isRunning)))            +(((runningSpeed*Input.GetAxis("Vertical")*BoolToInt(isRunning))*BoolToInt(isRunning !=isShifting))               +(((ShiftSpeed*Input.GetAxis("Vertical")*BoolToInt(isShifting))*BoolToInt(isRunning !=isShifting))));
            // curSpeedY = ((walkingSpeed*Input.GetAxis("Horizontal"))*BoolToInt((isShifting && isRunning)||!(isShifting && isRunning)))            +(((runningSpeed*Input.GetAxis("Horizontal")*BoolToInt(isRunning))*BoolToInt(isRunning !=isShifting))             +(((ShiftSpeed*Input.GetAxis("Horizontal")*BoolToInt(isShifting))*BoolToInt(isRunning !=isShifting))));

            moveDirection = (forward * curSpeedX) + (right * curSpeedY);//((runningSpeed*Input.GetAxis("Vertical")*(isRunning))+(curSpeedX = ShiftSpeed*Input.GetAxis("Vertical")*(isShifting)))*((isShifting && isRunning)||!(isShifting && isRunning))

            if (((isShifting && isRunning)||!(isShifting && isRunning)))
            {
                curSpeedX = walkingSpeed*Input.GetAxis("Vertical");
                curSpeedY = walkingSpeed*Input.GetAxis("Horizontal");

                // Debug.Log("dfs"+Input.GetAxis("Horizontal"));
            }
            if (isShifting)
            {
                curSpeedX = ShiftSpeed*Input.GetAxis("Vertical");
                curSpeedY = ShiftSpeed*Input.GetAxis("Horizontal");
            }
            if (isRunning)
            {
                curSpeedX = runningSpeed*Input.GetAxis("Vertical");
                curSpeedY = runningSpeed*Input.GetAxis("Horizontal");
            }

            if (IsGravity == false)
            {
                if (Input.GetKey("q"))
                {
                    moveDirection.y = jumpSpeed;
                }
                if (Input.GetKey("."))
                {
                    moveDirection.y = jumpSpeed;
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    moveDirection.y = jumpSpeed;
                }
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    moveDirection.y = -jumpSpeed;
                }
                if (Input.GetKey("e"))
                {
                    moveDirection.y = -jumpSpeed;
                }
                if (Input.GetKey("/"))
                {
                    moveDirection.y = -jumpSpeed;
                } 
            }
        
            if (IsGravity == true) 
            {
                if (Input.GetButton("Jump") && (characterController.isGrounded)&& canJomp)
                {
                    moveDirection.y = jumpSpeed*2;
                    // Debug.Log("jomp");
                }
                else
                {
                    moveDirection.y = movementDirectionY;
                }
                if (!characterController.isGrounded)
                {
                    moveDirection.y -= gravity*2.5f*Time.deltaTime/1.2f;
                }
            }

            characterController.Move(moveDirection * Time.deltaTime);//do not change

            // Player and Camera rotation
            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }

            if (health <= 0)
            {
                DoRespawn();
            }

            // health-=0.5f;
        }
    }

    void DoRespawn()
    {
        transform.position = respawnpoint.transform.position;
        health = 27;
    }

    int BoolToInt(bool Input)
    {
        if (Input)
        {
            return(1);
        }else
        {
            return(0);
        }
    }

    bool IntToBool(int Input)
    {
        if (Input == 1)
        {
            return(true);
        }else
        {
            return(false);
        }
    }
}
