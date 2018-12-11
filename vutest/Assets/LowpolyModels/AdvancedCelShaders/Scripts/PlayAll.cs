using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAll : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Q))
        {
            GameObject[] gos = FindObjectsOfType<GameObject>();  //returns GameObject[]
            foreach (GameObject go in gos)
            {

                if (go.GetComponent<Animator>() != null)
                {
                    go.GetComponent<Animator>().SetBool("on", true);
                    go.GetComponent<Animator>().SetBool("off", false);
                }
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            GameObject[] gos = FindObjectsOfType<GameObject>();  //returns GameObject[]
            foreach (GameObject go in gos)
            {

                if (go.GetComponent<Animator>() != null)
                {
                    go.GetComponent<Animator>().SetBool("on", false);
                    go.GetComponent<Animator>().SetBool("off", true);
                }
            }
        }
    }
}
