using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTimerCreate : MonoBehaviour {

    Timer timer = new Timer(Random.Range(0.01f, 0.5f));
    public GameObject[] objs;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        timer.Tick();
        if (timer.Over())
        {
            int randomIndex = (int)Random.Range(0, (float)objs.Length);//랜덤으로 한종류 뽑기

            Instantiate(objs[randomIndex],
                transform.position,
                Quaternion.Euler(0f,Random.Range(0f,360f),0f),
                transform);
        }
    }
}
