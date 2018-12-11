using UnityEngine;
using UnityEngine.UI;

public class DebugHUD : MonoBehaviour {

    private Text fpsCounter;

    private float frameCount = 0;
    private float dt = 0.0f;
    private float fps = 0.0f;
    private float updateRate = 4.0f;  // 4 updates per sec.

    void Start () {

        fpsCounter = GameObject.Find ("FPSCounter").GetComponent<Text> ();

    }

    private void Update () {

        frameCount++;

        dt += Time.deltaTime;

        if (dt > 1.0 / updateRate) {

            fps = frameCount / dt;
            frameCount = 0;
            dt -= 1.0f / updateRate;

        }

        fpsCounter.text = "FPS: " + (int)fps;

    }

}
