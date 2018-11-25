using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

    public TargetEvent[] targetEvents;
    public enum TargetNames {forestBase, tree, animal };
    public GameObject testCube;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < targetEvents.Length; i++)
        {
            if(targetEvents[i].isDetected)
            {
                print(i + "detected");
            }
        }

        if (targetEvents[(int)TargetNames.forestBase].isDetected)
        {
            if (targetEvents[(int)TargetNames.tree].isDetected)
            {
                Instantiate(testCube, targetEvents[(int)TargetNames.forestBase].targetTransform);
            }
            if (targetEvents[(int)TargetNames.animal].isDetected)
            {
                Instantiate(testCube, targetEvents[(int)TargetNames.forestBase].targetTransform);
            }
        }

    }
}
