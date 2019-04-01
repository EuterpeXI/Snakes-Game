using UnityEngine;

public class ProjectileSpawner : MonoBehaviour, ISpawner
{
    [System.Serializable]
    public class Vector3Bool
    {
        public bool x;
        public bool y;
        public bool z;
    }
    
    public GameObject prefab;
    public bool freezeRotation;

    public Vector3Bool freezeRotations;
    public bool parrent;
    public Vector2 InitVelocity;
    public Vector2 InitForce;
    public ForceMode2D ForceMode;


    public GameObject Spawn()
    {
        //evaluate rotation
        Quaternion rotation = transform.rotation;
        if (freezeRotation)
        {
            rotation.x = (freezeRotations.x)? 0: rotation.x;
            rotation.y = (freezeRotations.y)? 0: rotation.y;
            rotation.z = (freezeRotations.z)? 0: rotation.z;
        }
        
        GameObject obj = Instantiate(prefab, transform.position, rotation);

        //parent the object to the spawner
        if (parrent)
        {
            obj.transform.parent = this.transform;
        }

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.velocity = transform.rotation * InitVelocity;
            rb.AddRelativeForce(InitForce, ForceMode);
        }

        return obj;
    }
}