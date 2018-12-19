using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public Vector3 boundingBox;
    public float minTime;
    public float maxTime;

    public Timer timer;

    Vector3 initPos;

    // Use this for initialization
    void Start () {
        timer = new Timer(Random.Range(minTime, maxTime));

        initPos = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        timer.Tick();
        if (timer.Over())
        {
            timer.intervalMax = Random.Range(minTime, maxTime);

            transform.localPosition = new Vector3(
                Random.Range(-boundingBox.x / 2, boundingBox.x / 2),
                Random.Range(-boundingBox.y / 2, boundingBox.y / 2),
                Random.Range(-boundingBox.z / 2, boundingBox.z / 2));
            timer.Reset();
        }
	}
}
