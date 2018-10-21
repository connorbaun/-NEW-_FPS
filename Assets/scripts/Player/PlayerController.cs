using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public int myPlayerNum; //ref to which player I am signed in as (1,2,3,4)

    public int speed = 5; //the speed at which the character moves around (left analog stick)
    public int sprintMultiplier = 2; //this is how we will multiply your forward movespeed when sprinting

    public float horizontalLookSensitivity = 5; //the speed at which the character looks around (right analog stick)
    public float verticalLookSensitivity = 5; //the speed at which the character looks up and down (right analog stick)
    public float _jumpForce = 300;
    public float _doubleJumpForce = 200;
    public float UDrotation; //stores the CURRENT value of our cam's rotation up and down
    public float LRrotation; //stores the CURRENT value of our player object's rotation left and right
    

    private PlayerMotor motor; //we need a reference to the PlayerMotor script attached to the PlayerObject so that we can send data over to it
    private PlayerShoot shoot; //ref to the shooting script which spawns the bullets at a trigger pull
    private Equipment equip;

    // Use this for initialization
    void Start ()
    {
        shoot = GetComponent<PlayerShoot>();
        motor = GetComponent<PlayerMotor>(); //tell unity to seek the playermotor script attached to this object and call it 'motor'    
        equip = GetComponent<Equipment>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //----------BREAKDOWN OF STEPS TO MAKE PLAYER MOVE AROUND

            //1) Create a FLOAT which will store the INPUT of LEFT AND RIGHT on the LEFT ANALOG STICK (collect/store horizontal input)
            //2) Create a VECTOR3 which multiplies the LEFT AND RIGHT INPUT on the LEFT ANALOG STICK by the PLAYER TRANSFORM'S RIGHT DIRECTION. (multiply input value by player's right/left direction/store it in a vector3)

            //3) Create a FLOAT which will store the INPUT of FORWARD AND BACKWARD on the LEFT ANALOG STICK (collect/store forward + backwards input)
            //4) Create a VECTOR3 which multiplies the FORWARD AND BACK INPUT on the LEFT ANALOG STICK by the PLAYER TRANSFORM'S FORWARD DIRECTION. (multiply input value by player's z-axis direction /store it in a vector3)

            //5) Create a VECTOR3 which adds up the SUM of the LEFT/RIGHT and FORWARD/BACK VECTOR3's, and multiplies that sum by our desired SPEED. (add the forward/back and left/right vec3's together. multiply that sum by speed)

            //6) Finally, add the current position of the player's RIGIDBODY to the MOVEMENT VECTOR3 we created. This will move the player. (Actually apply that movement vec3 to the player's current position.)

        // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //----------COLLECT THE PLAYER'S INPUTS (when a player pushes something on his controller, we need to make sure that we store the result of that button/joystick press)

            // LEFT ANALOG STICK:
            //if the player pushes left or right on the left analog stick, store it in a float called _xMov. _xMov will be either -1 0 or 1
            float _xMov = Input.GetAxisRaw(myPlayerNum+ "LSHorizontal");

            //if the player pushes forwards or backwards on the left analog stick, store it in a float called _zMov. _zMov will be either -1 0 or 1
            float _zMov = Input.GetAxisRaw(myPlayerNum+"LSVertical");
        
            //we now need to take that input data for the left stick and do something with it to make it affect the player object's forward/backward and horizontal movement (strafe/walk):       
    
            //make a vector 3 which will take the LEFT AND RIGHT directions of the PLAYER TRANSFORM and multiply it by _xMov
            Vector3 _strafe = (transform.right * _xMov); //we are taking the player's right direction and multiplying it by -1 (left) 0 (nothing) or 1 (right)

            //make a vector 3 which will take the FORWARD AND BACKWARD directions of the PLAYER TRANSFORM and multiply it by _zMov
            Vector3 _walk = (transform.forward * _zMov); //we are taking the player's forward direction and multiplying it by -1 (backward) 0 (nothing) or 1 (forward)

            //APPLY THE INPUT DATA TO THE MOVESPEED WE SET IN THE BEGINNING

            //we need a new vector 3 which serves as the final velocity. it will take our STRAFE and WALK directions, and actually apply SPEED to the sum of those directions. This vector3 applies SPEED to those DIRECTIONS.
        
            Vector3 _velocity = (_strafe + _walk) * speed;


        
            

            //now that we have the actual velocity, which takes into account our speed, we can do 1 of 2 things:

            //OPTION 1:
                //  we can take that velocity calculation and actually apply it to our rigidbody right here and now inside this script
            //OPTION 2:
                //for organization's sake, we can take that _velocity calculation and send it over to another script called PlayerMotor.
                //PlayerMotor will actually PERFORM the movement on the player's RIGIDBODY, using the input data we collected in PlayerController.
                //with OPTION 2, we can have PlayerController be the script for INPUT COLLECTION, and PlayerMotor be the script for actually PERFORMING MOVEMENT.
                //OPTION 2 allows me to more effectively (and cleanly) organize my code. 
        
            //call a reference to the motor's CollectVelocityFromPlayer() function and send the _velocity calculation over.
            motor.CollectVelocityFromPlayerController(_velocity);

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //RIGHT ANALOG STICK:
            //if the player pushes left or right on the right analog stick, store it in a float called _yRot. It will return either -1 0 or 1
            float _yRot = Input.GetAxisRaw(myPlayerNum + "RSHorizontal") * horizontalLookSensitivity; //multipies our l/r input by the sensitivity
            LRrotation += _yRot; //stores the "change" in Left/Right rotation since last frame. it grows/shrinks with each movement of RS
       
        

            //CREATE A VECTOR 3 TO STORE THE INPUT DATA FOR ROTATION    
            //Vector3 _rotation = new Vector3(0f, _yRot, 0f) * horizontalLookSensitivity; //the vector3 _rotation will be multiplied by lookSensitivity, which changes how fast player will turn.

            //we are calling the CollectRotation function inside PlayerMotor and we are inputing the _rotation variable we calculated.
            


            //if the player pushes up or down on the right analog stick, store it in a float called _xRot. It will return either -1 0 or 1
            float _xRot = Input.GetAxisRaw(myPlayerNum + "RSVertical") * verticalLookSensitivity;
            UDrotation += _xRot; //stores the "change" in Up/Down rotation of camera since last frame. it grows/changes with each movement of RS
            UDrotation = Mathf.Clamp(UDrotation, -70, 70); //makes sure that we can't look all the way up or down (flip over our own heads)
            //Debug.Log(UDrotation);

            motor.CollectRotationFromPlayerController(LRrotation,UDrotation); //we are calling the CollectRotation function inside PlayerMotor and we are inputing the _rotation variable we calculated.

        //CREATE A VECTOR 3 TO STORE THE INPUT DATA FOR ROTATION    
        //Vector3 _camRotation = new Vector3(_xRot, 0f, 0f) * verticalLookSensitivity; //the vector3 _camrotation will be multiplied by verticallookSensitivity, which changes how fast player will look up/down.
        //we are calling the CollectCameraRotation function inside PlayerMotor and we are inputing the _rotation variable we calculated.
        //motor.CameraRotation(_camRotation); //send camera rotation over to the PlayerMotor Script. There, the rotation will actually be performed



        //Jump Code
            if (Input.GetButtonDown(myPlayerNum + "Jump"))
            {
                motor.CollectJumpForceFromPlayerController(_jumpForce, _doubleJumpForce);
            }

            //Cycle Weapons Code
            if (Input.GetButtonDown(myPlayerNum + "Switch"))
            {
                GetComponent<Equipment>().CycleEquipment();
            }
            //Fire Code
            if (Input.GetButtonDown(myPlayerNum + "Shoot"))
            {
                shoot.Fire();
            //Debug.Log("shot.");
            }


            //Reload Code
            if (Input.GetButtonDown(myPlayerNum + "Reload"))
            {
                shoot.Reload();
            }

            //Toss Code
             if (Input.GetButtonDown(myPlayerNum + "Throw"))
            {
                equip.ThrowCurrentWeapon();
            }





            /*Sprint Code
            if (Input.GetButton("Fire1") && _zMov == 1) //if the player is clicking left stick and also is pushing forward on the left stick...
            {
            _velocity = (_strafe + _walk) * speed * sprintMultiplier; //THIS version of _velocity will include the sprint multiplier
            } */

    }
}
