using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class BezierVisual : MonoBehaviour
{
    public List<Vector2> controlPoints;
    public GameObject pointPrefab;
    List<GameObject> mPointGameObjects = new List<GameObject>();
    LineRenderer[] mLineRenderers = null;
    public float lineWidth;
    public Color lineColor;
    public Color bezierCurveColor;
    public float bezierLineWidth;


    private LineRenderer CreateLine()
    {
        GameObject obj = new GameObject();
        LineRenderer lr = obj.AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = lineColor;
        lr.endColor = lineColor;
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        return lr;
    }

    void Start()
    {
        // Here we will create the actual lines.
        mLineRenderers = new LineRenderer[2];
        mLineRenderers[0] = CreateLine();
        mLineRenderers[1] = CreateLine();

        // Set the name of these lines to distinguish.
        mLineRenderers[0].gameObject.name = "LineRenderer_obj_0";
        mLineRenderers[1].gameObject.name = "LineRenderer_obj_1";

        // Now create the instances of the control points.
        for (int i = 0; i < controlPoints.Count; i++)
        {
            GameObject obj = Instantiate(pointPrefab, controlPoints[i], Quaternion.identity);
            obj.name = "ControlPoint_" + i.ToString();
            mPointGameObjects.Add(obj);
        }
    }
    void Update()
    {
        // We will now draw the lines every frame.
        LineRenderer lineRenderer = mLineRenderers[0];
        LineRenderer curveRenderer = mLineRenderers[1];

        List<Vector2> pts = new List<Vector2>();
        for (int i = 0; i < mPointGameObjects.Count; i++)
        {
            pts.Add(mPointGameObjects[i].transform.position);
        }

        // set the lineRenderer for showing the straight lines between
        // the control points.
        lineRenderer.positionCount = pts.Count;
        for (int i = 0; i < pts.Count; i++)
        {
            lineRenderer.SetPosition(i, pts[i]);
        }

        // We can now see the straight lines connecting the control points.
        // We will now proceed to draw the curve based on the bezier points.
        List<Vector2> curve = BezierCurve.PointList2(pts, 0.01f);
        curveRenderer.startColor = bezierCurveColor;
        curveRenderer.endColor = bezierCurveColor;
        curveRenderer.positionCount = curve.Count;
        curveRenderer.startWidth = bezierLineWidth;
        curveRenderer.endWidth = bezierLineWidth;

        for (int i = 0; i < curve.Count; i++)
        {
            curveRenderer.SetPosition(i, curve[i]);
        }
    }
    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isMouse)
        {
            if (e.clickCount == 2 && e.button == 0)
            {
                Vector2 rayPos = new Vector2(
                  Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                  Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

                InsertNewControlPoint(rayPos);
            }
        }
    }
    void InsertNewControlPoint(Vector2 p)
    {
        if (mPointGameObjects.Count >= 18)
        {
            Debug.Log("Cannot create any more control points. Max is 18");
            return;
        }

        GameObject obj = Instantiate(pointPrefab, p, Quaternion.identity);
        obj.name = "ControlPoint_" + mPointGameObjects.Count.ToString();
        mPointGameObjects.Add(obj);
    }
}
