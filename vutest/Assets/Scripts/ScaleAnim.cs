using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnim : MonoBehaviour {

    public float appearTime = 0.5f;
    public float disappearTime = 0.5f;
    Vector3 scaleMax;
    public float scaleMul = 1;
    float scaleMulStep = 0;

    // Use this for initialization
    void Start () {
        scaleMax = transform.localScale;

    }
	
	// Update is called once per frame
	void Update () {
        scaleMul = Mathf.Clamp(scaleMul + scaleMulStep * Time.deltaTime, 0, 1);
        transform.localScale = scaleMax * ((Mathf.Sin((scaleMul * 180f - 90f) * Mathf.Deg2Rad) + 1f)/2f);

    }

    public void Appear()
    {
        scaleMulStep = 1 / appearTime;
    }
    public void Disappear()
    {
        scaleMulStep = -(1 / disappearTime);
    }
}
