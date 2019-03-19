using UnityEngine;

public class LaserPointController : MonoBehaviour
{
    public static Vector3 position;

    public void MoveLaser(Vector3 location)
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(location);
        position = transform.position;
    }
}