using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {
    [SerializeField] private Transform target;

    public float rotSpeed = 1.5f;
    private float _rotY;
    private float _rotX;
    private Vector3 _offset;


    public float minimumVert = -45.0f; //these two floats will be used to stop the camera from going too high/low
    public float maximumVert = 45.0f;




	// Use this for initialization
	void Start () {


        _rotX = transform.eulerAngles.x; 
        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position;

	}
	
	// Update is called once per frame
	void LateUpdate () {
       
        _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;

        _rotX += Input.GetAxis("Mouse Y") * rotSpeed * 3;

        _rotX = Mathf.Clamp(_rotX, minimumVert, maximumVert);





        //Quaternion xrotation = Quaternion.Euler(0, 0, _rotX);
       // transform.position = target.position - (xrotation * _offset);

        Quaternion rotation = Quaternion.Euler(-_rotX, _rotY, 0);
        transform.position = target.position - (rotation * _offset);
        transform.LookAt(target);

	}




    //void LateUpdate()
    //{
    //    float horInput = Input.GetAxis("Horizontal");
    //    if (horInput != 0)
    //    {
    //        _rotY += horInput * rotSpeed;
    //
    //    }
    //    else
    //    {
    //        _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
//
   //     }
   //     Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
   //     transform.position = target.position - (rotation * _offset);
   //     transform.LookAt(target);

    //}










}
