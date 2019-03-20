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

    public bool gunner = false;

    // this shit below is for the gunner mode x and y
    //private float gunX;
    //private float gunY;
    private Vector3 gunX;
    private Vector3 gunY;
    //private Vector3 TP;
    

	// Use this for initialization
	void Start () {
        gunX[0] = 0.5f; //x
        gunX[1] = 0.8f; //y
        gunX[2] = - 1.0f; //z

        gunY[0] = 0.0f;
        gunY[1] = 0.0f;
        gunY[2] = 0.0f;


        _rotX = transform.eulerAngles.x; 
        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position;

	}
	
	// Update is called once per frame
	void LateUpdate () {

        if (Input.GetKeyDown(KeyCode.Mouse1))

        {
            gunner = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            gunner = false;
        }

        if (gunner == true)
        {
            //transform.position = target.position + (gunX + gunY);
            transform.position = target.position + (gunX);







        }



        if (gunner == false)
        {

            _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;

            _rotX += Input.GetAxis("Mouse Y") * rotSpeed * 3;

            _rotX = Mathf.Clamp(_rotX, minimumVert, maximumVert);

            Quaternion rotation = Quaternion.Euler(-_rotX, _rotY, 0);
            transform.position = target.position - (rotation * _offset);
            transform.LookAt(target);

        }







	}


    



}
