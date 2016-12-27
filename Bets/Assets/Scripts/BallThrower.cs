using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrower : MonoBehaviour
{
    public Transform Target;
    public GameObject ballModel;
    public float shootAngle;

    private Vector3 GetBallVelocity(Transform target, float angle)
    {
        Vector3 direction = target.position - this.transform.position;
        float h = direction.y;
        direction.y = 0;

        float distance  = direction.magnitude;
        float radianAngle = angle * Mathf.Deg2Rad;

        direction.y = distance * Mathf.Tan(radianAngle);
        distance += h / Mathf.Tan(radianAngle);

        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * radianAngle));
        return velocity * direction.normalized;
    }


    public void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            GameObject ball = Instantiate(ballModel, this.transform.position, Quaternion.identity);
            Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
            rigidbody.velocity = GetBallVelocity(this.Target, this.shootAngle);
            Destroy(ball, 10);
        }

    }
}
