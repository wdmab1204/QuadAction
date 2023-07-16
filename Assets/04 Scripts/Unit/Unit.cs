using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Transform target;
    public float speed = 1f;
    Vector3[] path;
    bool success = false;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PathRequestManager.RequestPathFind(transform.position, target.position, OnCompltePastFind);
    }

    void OnCompltePastFind(bool success, Vector3[] waypoints)
    {
        this.success = success;

        if(this.path != waypoints)
        {
            this.path = waypoints;
            StopCoroutine("FollowTarget");
            StartCoroutine("FollowTarget");
        }
    }

    IEnumerator FollowTarget()
    {
        for(int i=0; i<path.Length; i++)
        {
            var waypoint = path[i];
            while(transform.position != waypoint)
            {
                //transform.position = Vector3.MoveTowards(transform.position, waypoint, speed * Time.deltaTime);
                waypoint.y = transform.position.y;
                rigidbody.velocity = (waypoint - transform.position).normalized * speed;
                yield return null;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (success)
        {
            for(int i=0; i< path.Length; i++)
            {
                if (i == 0)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(transform.position, path[i]);

                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(path[i], Vector3.one * 2f);
                }
                else
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(path[i - 1], path[i]);

                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(path[i], Vector3.one * 2f);
                }
            }
        }
    }
}
