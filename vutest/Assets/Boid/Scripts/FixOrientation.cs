using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixOrientation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.SetPositionAndRotation(transform.position, Quaternion.identity);
	}
}
