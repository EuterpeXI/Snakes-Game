using UnityEngine;

public class LaserPointController : MonoBehaviour
{
    public static Vector3 position;

    void Update()
    {
        transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position = transform.position;
    }
}