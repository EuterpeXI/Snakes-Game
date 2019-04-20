using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamController : MonoBehaviour
{
    public GameObject laserPoint;
    private LineRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<LineRenderer>();
        if (renderer == null)
            Debug.LogError(
                "LaserBeamController could not find a Line Renderer on its attached object.");
        if (laserPoint == null)
            Debug.LogError(
                "LaserBeamController could not find a laser point reference on its script.");
        renderer.useWorldSpace = true;
    }
    private void Update()
    {

        bool buttDown = Input.GetButton("Fire1");
        bool buttUp = Input.GetButtonUp("Fire1");

        if (buttDown && !buttUp)
        {
            var elevatedPlayer = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z - 1);
            var elevatedPoint = new Vector3(laserPoint.transform.position.x, laserPoint.transform.position.y, laserPoint.transform.position.z - 1);
            Vector3[] pts = { elevatedPlayer, elevatedPoint };
            renderer.SetPositions(pts);
            renderer.enabled = true;
        }
        else
            renderer.enabled = false;

    }
}
