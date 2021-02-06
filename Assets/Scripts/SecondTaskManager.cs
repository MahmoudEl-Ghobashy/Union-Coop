using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondTaskManager : MonoBehaviour
{

    public Transform Point1;
    public Transform Point2;
    public Transform Point3;
    public Transform Point4;

    public int NoOfPoints;
    public LineRenderer LineRenderer;

    private Vector3[] positions;

    private void Start()
    {
        positions = new Vector3[NoOfPoints];
    }

    public void Update()
    {
        DrawCubicBezierCurve(); //Calculate curve live during play so tester can change any point position and see the impact instantly. 
    }

    private void DrawCubicBezierCurve()
    {
        for (int i = 1; i < NoOfPoints + 1; i++) //Calculate number of points on the curve during certain time (0 < t < 1), in this case the number of points will be 50
        {
            float t = i / (float)NoOfPoints;
            positions[i - 1] = CalculateCubicBezierPoint(t, Point1.position, Point2.position, Point3.position, Point4.position); //Apply the equation related to 4 points and range varient t.
        }
        LineRenderer.positionCount = NoOfPoints;
        LineRenderer.SetPositions(positions); //Draw the calculated points using the line renderer.
    }

    private Vector3 CalculateCubicBezierPoint(float t, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        //Bezier equation for a 4 points curve. 
        // (1-t)^3*p1 + 3*(1-t)^2 * t * p2 + 3*(1-t) * t^2 * p3 + t^3 * p4 

        Vector3 point = (Mathf.Pow((1 - t), 3) * p1)
            + (3 * Mathf.Pow((1 - t), 2) * t * p2)
            + (3 * (1 - t) * Mathf.Pow(t, 2) * p3)
            + (Mathf.Pow(t, 3) * p4);

        return point;
    }
}
