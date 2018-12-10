using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelOrientation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.rotation = 
            Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(GetComponent<Rigidbody>().velocity),
                0.04f);
	}
}
