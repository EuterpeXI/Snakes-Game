using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class LookTowardTransform : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;

    void Start()
    {
        // if the target were are looking at isn't active then this object is not active
        gameObject.SetActive(transform.gameObject.active);
    }

    void Update()
    {

        float rot_z = Mathf.Atan2(transform.position.y - Target.position.y,
                                  transform.position.x - Target.position.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rot_z-90);
        
    }
}