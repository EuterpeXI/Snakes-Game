using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileJoystickPlayerController : MonoBehaviour
{
    public TopDownMovementController player;
    
    [Tooltip("this is what relative will calculate")]
    public Transform center;

    [Tooltip("Moves with the input position")]
    public Transform innerCircle;


    void moveCharacter(Vector3 direction)
    {
        direction.z = 0; //to not interfere  with normalization.
        direction = direction.normalized;

        Debug.Log(direction, this);
        player.Move(-direction.x * Time.deltaTime,
                    -direction.y * Time.deltaTime);
        //POTENTIAL BUG if it does not go in right direction adjust here
        // -> depends on orientation of screen ->
    }

// Note this won't stop dragging/ pdating once you start dragging and leave the collider. -> good 
    void OnMouseDrag()
    {
        Debug.Log("Mouse Drag");
        Vector3 input = getMousePosition();
        innerCircle.position = input;

        moveCharacter(center.position - input);
        
    }

    Vector2 getMousePosition()
    {
        //yes it does need to be vector2 ->  strips z axis so that setting position does not change
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }
}