using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelControl : MonoBehaviour {

    public bool DeleteOnLost;
    //

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*
        transform.rotation =Quaternion.Euler
            (new Vector3(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            Mathf.Clamp(transform.rotation.eulerAngles.z, 50, 80)));
            */
	}


    public void ModelDetected() 
    {
        foreach (ModelControl childControl in transform.GetComponentsInChildren<ModelControl>())
        {
            childControl.ChildModelDetected();
        }
    }

    public void ModelLost()
    {
        foreach (ModelControl childControl in transform.GetComponentsInChildren<ModelControl>())
        {
            childControl.ChildModelLost();
        }
    }

    public void ChildModelDetected() //감지될 때 실행되는 모든 자식 모델의 코드
    {
        print(gameObject + " child detected");
    }

    public void ChildModelLost()  //사라질 때 실행되는 모든 자식 모델의 코드
    {
        print(gameObject + " child lost");
    }


}
