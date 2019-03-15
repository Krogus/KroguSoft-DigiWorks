using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour {
    [SerializeField] private Vector3 dPos; //Position to offset to when the door opens

    private bool _open; //Boolean to keep track of the open state of the door

    public void Operate()
    {
        if (_open) //Open or close the door depending on the open state
        {
            Vector3 pos = transform.position - dPos;
            transform.position = pos;
        } else
        {
            Vector3 pos = transform.position + dPos;
            transform.position = pos;
        }
        _open = !_open;

    }



    //offset is 0, -2.9, 0 but I dunno where to put that :/



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
