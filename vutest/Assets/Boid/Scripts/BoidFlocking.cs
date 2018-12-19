using UnityEngine;
using System.Collections;

public class BoidFlocking : MonoBehaviour
{
    private GameObject Controller;
    private bool inited = false;
    private float minVelocity;
    private float maxVelocity;
    private float randomness;
    private float centering;
    private float velocityMatching;
    private float followAcc;

    private bool avoidanceEnabled;
    private float avoidanceRadius;
    private float avoidanceAcc;

    private GameObject chasee;

    void Start()
    {
        StartCoroutine("BoidSteering");
    }

    IEnumerator BoidSteering()
    {
        while (true)
        {
            if (inited)
            {


                GetComponent<Rigidbody>().velocity = 
                    Vector3.Lerp(
                        GetComponent<Rigidbody>().velocity,
                        GetComponent<Rigidbody>().velocity + Calc() * Time.deltaTime,
                        0.5f);
                

                // enforce minimum and maximum speeds for the boids
                float speed = GetComponent<Rigidbody>().velocity.magnitude;
                if (speed > maxVelocity)
                {
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxVelocity;
                }
                else if (speed < minVelocity)
                {
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * minVelocity;
                }
            }

            GetComponent<Rigidbody>().velocity += AvoidanceCalc() * Time.deltaTime;

            float waitTime = Random.Range(0.1f, 0.15f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private Vector3 Calc()
    {
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);

        randomize.Normalize();
        BoidController boidController = Controller.GetComponent<BoidController>();
        Vector3 flockCenter = boidController.flockCenter;
        Vector3 flockVelocity = boidController.flockVelocity;
        Vector3 follow = chasee.transform.localPosition;

        flockCenter = flockCenter - transform.localPosition;
        flockVelocity = flockVelocity - GetComponent<Rigidbody>().velocity;
        follow = follow - transform.localPosition;

        

        return (flockCenter * centering
            + flockVelocity * velocityMatching
            + follow * followAcc
            
            + randomize * randomness);
    }

    public void SetController(GameObject theController)
    {
        Controller = theController;
        BoidController boidController = Controller.GetComponent<BoidController>();
        minVelocity = boidController.minVelocity;
        maxVelocity = boidController.maxVelocity;
        randomness = boidController.randomness;
        centering = boidController.centering;
        followAcc = boidController.followAcc;
        velocityMatching = boidController.velocityMatching;

        avoidanceEnabled = boidController.avoidanceEnabled;
        avoidanceRadius = boidController.avoidanceRadius;
        avoidanceAcc = boidController.avoidanceAcc;

        chasee = boidController.chasee;
        inited = true;
    }

    private Vector3 Avoidance(float radius)
    {
        float sqrDist = Mathf.Infinity; // 제곱한 거리
        Vector3 collPoint = Vector3.zero; // 내 포지션에서 충돌점까지의 거리

        Collider[] colls =
            Physics.OverlapSphere(
            transform.localPosition,
            radius);

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].gameObject == gameObject) // 자신이면 비교하지 않는다.
            {
                continue;
            }
            else
            {
                // 충돌체와의 거리 계산해서 작으면 그것으로 갱신

                Vector3 collPointCurrent = (colls[i].ClosestPoint(transform.localPosition) - transform.localPosition);
                if (collPointCurrent.sqrMagnitude < sqrDist)
                {
                    sqrDist = collPointCurrent.sqrMagnitude;
                    collPoint = collPointCurrent;
                }
            }
        }

        return collPoint; // (0,0,0)이면 감지되지 않은 것임
    }

    private Vector3 AvoidanceCalc()
    {
        Vector3 avoidPoint = Avoidance(avoidanceRadius);
        Vector3 avoidForce = Vector3.zero;
        if (avoidanceEnabled)
        {
            avoidPoint = Avoidance(avoidanceRadius);
            avoidForce = Vector3.zero;

            if (avoidPoint != Vector3.zero)
            {
                avoidForce = -(Mathf.Pow(1 - (avoidPoint.sqrMagnitude / avoidanceRadius), 2) * avoidPoint.normalized);
            }

        }

        return avoidanceAcc * avoidForce;
    }
}
