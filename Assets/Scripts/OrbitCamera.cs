using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Control Script/Mouse Look")]
public class OrbitCamera : MonoBehaviour {
    [SerializeField] private Transform target;

    public float rotSpeed = 1.5f;
    private float _rotY;
    private float _rotX;
    private Vector3 _offset;
    public Vector3 _testoffset;

    public float minimumVert = -45.0f; //these two floats will be used to stop the camera from going too high/low
    public float maximumVert = 45.0f;

    public bool gunner = false;

    // this shit below is for the gunner mode x and y
    //private float gunX;
    //private float gunY;
    private Vector3 gunX;
    private Vector3 gunY;
    //private Vector3 TP;
    
        // Everything contained within here is for the gunner movement
    public enum RotationAxes
    {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXandY;

    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    private float _rotationX = 0;

    public float _testroty =0;

    



        //imagine this is kinda like a bottom bracket lol

	// Use this for initialization
	void Start () {
        gunX[0] = 0.5f; //x
        gunX[1] = -0.5f; //y
        gunX[2] = - 1.0f; //z

       // gunY[0] = 0.0f;
       // gunY[1] = 0.0f;
       // gunY[2] = 0.0f;


        _rotX = transform.eulerAngles.x; 
        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position;

         //public Vector3 _testoffset;
        _testoffset.x = 0;
        _testoffset.y = 0;
        _testoffset.z = 10;



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
            //transform.position = target.position + (gunX);

            //_rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;

            //_rotX += Input.GetAxis("Mouse Y") * rotSpeed * 3;

            //_rotX = Mathf.Clamp(_rotX, minimumVert, maximumVert);

            // Quaternion rotation = Quaternion.Euler(-_rotX, _rotY, 0);

            if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
            }


            _testroty= target.localEulerAngles.y+180;

            Quaternion rotation = Quaternion.Euler(0, _testroty, 0);
            Vector3 newtarget;
            newtarget = target.position - (rotation * _testoffset);

           

            // newtarget.position = newtarget.position;// - (rotation * _testoffset);

            //transform.position = target.position;// - (rotation * _offset);

            transform.position = target.position;
            transform.position = transform.position - (rotation * gunX);

            transform.LookAt(newtarget);


            /*      if (axes == RotationAxes.MouseX)
                  {
                      transform.Rotate(0, Input.GetAxis("Mouse Y") * sensitivityHor, 0);
                  }
                  else if (axes == RotationAxes.MouseY)
                  {
                      _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                      _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
                  }
                  else
                  {
                      float rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityHor;

                      _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                      _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

                      transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
                  }

          */



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
