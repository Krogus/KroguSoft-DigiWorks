using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour {

    public Camera MainCamera;
    public Transform CameraBase = null;
    public int speed = 4;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("gay");
            Camera.main.transform.position = CameraBase.position;
        }
    }
}
