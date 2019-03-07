using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownMovementController : MonoBehaviour
{
    public Vector2 verticalSpeed = new Vector2(0,1);
    public Vector2 horizontalSpeed = new Vector2(1,0);

    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void Move(float horizontalMod, float verticalMod)
    {
        _rigidbody2D.velocity = (horizontalSpeed * horizontalMod)+( verticalSpeed * verticalMod);
    }

}