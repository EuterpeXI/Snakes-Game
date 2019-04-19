using UnityEngine;

public class LaserPointController : MonoBehaviour
{
    public static Vector3 position;

    public void MoveLaser(Vector3 location)
    {
        location.z = 0f;
        transform.position = location;
        position = transform.position;
    }
}