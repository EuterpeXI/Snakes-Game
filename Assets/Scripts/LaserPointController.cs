using UnityEngine;

public class LaserPointController : MonoBehaviour
{
    public static Vector3 position;
    public float thresholdToStop;
    public float speed = 20;
    public LayerMask mask;
    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        if (rb == null || cc == null)
            Debug.LogError("Error assigning rigidbody or circle collider to LaserPointController.");
    }

    public void MoveLaser(Vector3 location)
    {
        location.z = Mathf.Abs(Camera.main.transform.position.z);
        if (Vector3.Distance(Camera.main.ScreenToWorldPoint(location), transform.position) > thresholdToStop)
        {
            sr.enabled = true;
            LookAt2D(location);
            rb.velocity = transform.up * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        
        position = transform.position;
    }

    private void LookAt2D(Vector3 pt)
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(pt) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}