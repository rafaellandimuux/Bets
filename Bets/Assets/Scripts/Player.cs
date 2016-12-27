using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float TT; 
    public float T;

    public Vector3 PointA;

    public Vector3 ControlA;

    public Vector3 ControlB;

    public Vector3 PointB;

    public Transform Transform;

    

    void Start()
    {
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.PointA, 0.1f);
        Gizmos.DrawWireSphere(this.PointB, 0.1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.ControlA, 0.05f);
        Gizmos.DrawWireSphere(this.ControlB, 0.05f);

        

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.Transform.position, 0.3f);
    }

    public Vector3 Interpolate(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float cX = 3 * (p1.x - p0.x);
        float bX = 3 * (p2.x - p1.x) - cX;
        float aX = p3.x - p0.x - cX - bX;

        float cz = 3 * (p1.z - p0.z);
        float bz = 3 * (p2.z - p1.z) - cz;
        float az = p3.z - p0.z - cz - bz;

        Vector3 position = new Vector3();
        position.x = (float)((aX * Math.Pow(this.T, 3)) + (bX * Math.Pow(this.T, 2)) + (cX * this.T) + p0.x);
        position.y = this.Transform.position.y;
        position.z = (float)((az * Math.Pow(this.T, 3)) + (bz * Math.Pow(this.T, 2)) + (cz * this.T) + p0.z);

        return position;
    }

    void Update()
    {
        List<Touch> touches = InputHelper.GetTouches();
        if (touches.Count == 1)
        {
            Touch touch = touches[0];
            this.TT = touch.position.x / Screen.width;
        }

        this.T = Mathf.Lerp(this.T, this.TT, Time.deltaTime * 4);
        if (this.T > 1)
        {
            this.T = 1;
        }

        if (this.T < 0)
        {
            this.T = 0;
        }

        this.Transform.position = this.Interpolate(this.PointA, this.ControlA, this.ControlB, this.PointB);
        //this.Transform.position = Vector3.Lerp(this.Transform.position, this.TargetPosition, Time.deltaTime * 2); 
    }
}

