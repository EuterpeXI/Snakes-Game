using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

/// <summary>
/// Controls the AI for the mice, both MIB and regular mice
/// Movement patterns dependant on what state the AI is in, and if there are any players around
/// </summary>
public class MouseAIBehavior : MonoBehaviour
{
    public enum AIState
    {
        Neutral,
        Suspicious,
        Alarmed
    }

    [SerializeField, Tooltip("Do not need to change")]
    GameObject[] Alarms;
    [SerializeField]
    Transform[] Waypoint = null;
    [SerializeField]
    Vector2 forwardVector = new Vector2(1, 0); //the default forward direction of the AI
    [SerializeField]
    private bool isMoving;
    int rng;
    [SerializeField]
    bool isImmortal = false;
    [SerializeField]
    bool canMoveOnPatrol = true;

    [SerializeField]
    float moveRadius = 2.0f; //The radius that the AI is allowed to move around the waypoint
    [SerializeField]
    float SightRadius = 1.0f; //The radius that the AI can see to detect snakes or the player
    [SerializeField]
    float SightDistance = 1.0f; //The distance forward that the AI can see from current position
    [SerializeField]
    float movementSpeed = 2.0f; //The Speed the AI moves at

    [SerializeField]
    Vector2 MinMaxStateTimer = new Vector2(0.0f, 2.0f); //using a vector2 to store the min and max for the amount of time between moving in neutral and idle states

    [SerializeField, Tooltip("Do not need to change")]
    AIState currentState = AIState.Neutral; //The current state of the AI; whether its staying still, or moving or heading towards nearest alarm

    [SerializeField, Tooltip("Do not need to change")]
    Transform Target = null;  //The saved target of the AI (if immortal will chase after, otherwise will run away)

    // Start is called before the first frame update
    void Start()
    {
        //Find all the alarm locations in the level
        Alarms = GameObject.FindGameObjectsWithTag("Alarm");
        isMoving = false;
    }

    IEnumerator TimedMovePatrol()
    {
        yield return new WaitForSeconds(0.5f);
        isMoving = true;
    }
    // Update is called once per frame
    void Update()
    {
        //DetectSurroundings();

        switch (currentState)
        {
            case AIState.Neutral:
                //Partol around a waypoint(s) or a certain path
                if (Waypoint.Length > 1)
                {
                    if(!isMoving)
                    {
                        rng = Random.Range(0, Waypoint.Length);
                    }

                    if (Waypoint[rng].position != transform.position && !isMoving)
                    {
                        StartCoroutine(TimedMovePatrol());
                    }

                    // PLEASE DELETE THIS IF STATEMENT IF YOU WANT COCAINEE
                    //if(isMoving)
                    //{ 
                        transform.position = Vector3.MoveTowards(transform.position, Waypoint[rng].position, movementSpeed * Time.deltaTime);
                    //}
                    if (Vector3.Distance(Waypoint[rng].position, transform.position) < 0.0001f)
                    {
                        print("changing targets");
                        isMoving = false;
                    }
                }
                else
                {

                }

                //interchange between this state and idle
                break;
            case AIState.Suspicious:
                break;
            case AIState.Alarmed:
                if (isImmortal)
                {
                    //chase after target
                    transform.LookAt(Target);
                    transform.position = Vector2.MoveTowards(transform.position, Target.position, movementSpeed * Time.deltaTime);
                }
                else
                {
                    //check distance and direction to alarm
                    Transform alarm = FindNearestAlarm();
                    RaycastHit2D[] results = new RaycastHit2D[0];
                    //linecast towards the alarm to check if there is a player or snake in the way
                    if (alarm != null)
                        results = Physics2D.LinecastAll(transform.position, alarm.position);
                    else
                        results = Physics2D.LinecastAll(transform.position, transform.position + (Vector3)(forwardVector * SightDistance));

                    foreach (RaycastHit2D hit in results)
                    {
                        if (hit.transform.CompareTag("Player") || hit.transform.CompareTag("snake_bullet"))
                        {
                            //run away => Rotate 180 degrees and run
                            //transform.Rotate(Vector3.forward * 180f);
                            Target = hit.transform;
                        }
                    }

                    //if there are no players or snakes in the way, move towards the alarm
                    if (Target == null && alarm != null)
                        transform.position = Vector2.MoveTowards(transform.position, alarm.position, movementSpeed * Time.deltaTime);
                    else
                        transform.Translate(-forwardVector * movementSpeed * Time.deltaTime);
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Used to Detect any objects around the AI object
    /// </summary>
    private void DetectSurroundings()
    {
        float nearestDist = 10000f;
        Target = null;

        if (SightRadius > 0)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + (Vector3)(forwardVector * SightDistance), SightRadius);

            Debug.DrawLine(transform.position, transform.position + (Vector3)(forwardVector * SightDistance), Color.green);

            //Get Nearest collider and set it as target from sightRadius
            foreach (Collider2D col in colliders)
            {
                if (col.CompareTag("Player") || col.CompareTag("snake_bullet"))
                {
                    float dist = Vector2.Distance(transform.position, col.transform.position);
                    currentState = AIState.Alarmed;

                    //save the closest object as the target object
                    if (dist <= nearestDist)
                    {
                        nearestDist = dist;
                        Target = col.transform;
                    }
                }
            }
        }

        //Determine if any players or snakes are inbetween self and Sight distance
        RaycastHit2D[] results = Physics2D.RaycastAll(transform.position, forwardVector, SightDistance);

        foreach (RaycastHit2D hit in results)
        {
            if (hit.transform.CompareTag("Player") || hit.transform.CompareTag("snake_bullet"))
            {
                float dist = Vector2.Distance(transform.position, hit.transform.position);

                if (dist <= nearestDist)
                {
                    nearestDist = dist;
                    Target = hit.transform;
                }
            }
        }

        if (Target == null && isImmortal)
            currentState = AIState.Suspicious;
    }

    /// <summary>
    /// Finds the closest alarm object to the AI
    /// </summary>
    /// <returns></returns>
    private Transform FindNearestAlarm()
    {
        float nearestDist = 10000f;
        GameObject nearestAlarm = null;

        //loop through all the alarms found at the start of the game and find the nearest one
        foreach(GameObject alarm in Alarms)
        {
            float dist = Vector2.Distance(alarm.transform.position, transform.position);
            if (dist <= nearestDist)
            {
                nearestDist = dist;
                nearestAlarm = alarm;
            }

        }

        if (nearestAlarm != null)
            return nearestAlarm.transform;
        else
            return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        //kill the mouse if it comes in contact with a snake or mouse
        if (other.CompareTag("Player") || other.CompareTag("snake_bullet"))
        {
            //determine whether the mouse is immortal or not
            if (!isImmortal)
                gameObject.SetActive(false); //kill self if not immortal
            else
                other.gameObject.SetActive(false); //kill collided object if immortal
        }

        if (other.CompareTag("Alarm"))
            other.GetComponent<AlarmScript>().isOn = true;
    }
}
