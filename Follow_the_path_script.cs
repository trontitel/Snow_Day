using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_the_path_script : MonoBehaviour
{
    public Path_Script PathToFollow;
    public Path_Script PathToThePlayer;

    public int CurrentWayPointID = 0;
    public float speed;
    private float reachDistance = 1.0f;
    public float rotationSpeed = 5.0f;

    Vector3 last_position;
    Vector3 current_position;

    public bool Cat_is_moving = true;

    void Start()
    {
        last_position = transform.position;
    }

    void Update()
    {
        if(Cat_is_moving)
            FollowThePath();
    }

    private void FollowThePath()
    {
        float distance = Vector3.Distance(PathToFollow.path_objects[CurrentWayPointID].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, PathToFollow.path_objects[CurrentWayPointID].position, Time.deltaTime * speed);

        var rotation = Quaternion.LookRotation(PathToFollow.path_objects[CurrentWayPointID].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        if (distance <= reachDistance)
        {
            CurrentWayPointID++;
        }

        if (CurrentWayPointID >= PathToFollow.path_objects.Count && PathToFollow != PathToThePlayer.GetComponent<Path_Script>())
        {
            CurrentWayPointID = 0;
        }
        else if (CurrentWayPointID >= PathToFollow.path_objects.Count && PathToFollow == PathToThePlayer.GetComponent<Path_Script>())
        {
            Cat_is_moving = false;
        }
    }
}
