using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    private LineRenderer line;
    private EdgeCollider2D col;
    [SerializeField] private List<Vector2> linePoints = new List<Vector2>();

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        if (line.GetPosition(0).x < line.GetPosition(1).x)
        {
            linePoints[0] = new Vector3(line.GetPosition(0).x + 0.5f, line.GetPosition(0).y, line.GetPosition(0).z);
            linePoints[1] = new Vector3(line.GetPosition(1).x - 0.5f, line.GetPosition(1).y, line.GetPosition(1).z);
        }
        else
        {
            linePoints[0] = new Vector3(line.GetPosition(1).x + 0.5f, line.GetPosition(1).y, line.GetPosition(1).z);
            linePoints[1] = new Vector3(line.GetPosition(0).x - 0.5f, line.GetPosition(0).y, line.GetPosition(0).z);
        }
        col.SetPoints(linePoints);
    }
}
