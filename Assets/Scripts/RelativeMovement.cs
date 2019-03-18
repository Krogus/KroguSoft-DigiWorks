using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This will require that the character controller module be attatched to the object this code is tied to
[RequireComponent(typeof (CharacterController))]
public class RelativeMovement : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private Transform shoulderCam;
    [SerializeField] private Transform mainCam;

    public float moveSpeed = 6.0f;
    public float rotSpeed = 15.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -20.0f;
    public float minFall = -1.5f;

    private float _vertSpeed;
    private ControllerColliderHit _contact;
    
    private CharacterController _charController;
    private Animator _animator;

    public float pushForce = 3.0f;

    //all the variables above are working so if you fuck up it's below this 
    public bool Sprint = false;

    private void Start()
    {
        _vertSpeed = minFall;

        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            target = shoulderCam;
            // set main cam to inactive
            mainCam.gameObject.SetActive(false);
            shoulderCam.gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            target = mainCam;
            // set shoulder cam to inactive
            shoulderCam.gameObject.SetActive(false);
            mainCam.gameObject.SetActive(true);
        }
        Vector3 movement = Vector3.zero;
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //    movement.x =  moveSpeed + 5.0f;
            //    movement.y =  moveSpeed + 5.0f;
            Sprint = true;
           
        } 
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Sprint = false;
        //    movement.x = horInput * moveSpeed;
        //    movement.y = vertInput * moveSpeed;

        }
        
        if ( Sprint == true)
        {
            pushForce = 5.0f;
            moveSpeed = 12.0f;
            _animator.SetBool("Sprint", true);
        }

        if (Sprint == false)
        {
            pushForce = 3.0f;
            moveSpeed = 6.0f;
            _animator.SetBool("Sprint", false);
        }

        


        //if (_charController.isGrounded) this has been changed to not use the character controller and instead use raycasting
   
       // movement.y = _vertSpeed;
        //movement *= Time.deltaTime;
       // _charController.Move(movement);

        


        //this's movin round
        
        if (horInput !=0 || vertInput !=0)
        {
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);

            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);

        }
        _animator.SetFloat("Speed", movement.sqrMagnitude); //if I did this right, it'll change the SPEED in which the animator will change from idle to run!

        bool hitGround = false;
        RaycastHit hit;
        if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (_charController.height + _charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }


        if (hitGround){
            if (Input.GetButtonDown("Jump"))
            {
                _vertSpeed = jumpSpeed;
            }
            else
            {
                _vertSpeed = minFall; 
                _animator.SetBool("Jumping", false); //this is what's changing the bool for the animator!
            }
        }
        else
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;

            }

            if (_contact != null)
            {
                _animator.SetBool("Jumping", true);
            }



            if (_charController.isGrounded)
            {
                if (Vector3.Dot(movement, _contact.normal) < 0)
                {
                    movement = _contact.normal * moveSpeed;
                }
                else
                {
                    movement += _contact.normal * moveSpeed;
                }
                
            }
            
        }



        movement.y = _vertSpeed;

        movement *= Time.deltaTime;
        _charController.Move(movement);


        
        

        
        
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
        }

    }
}
