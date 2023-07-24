using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    [HideInInspector] public new string tag;
    public ObjectPool pooler;
    public abstract void OnObjectSpawn();
}

public class Unit : PoolObject
{
    public Transform target;
    public float speed = 1f;
    Vector3[] path;
    bool success = false;
    new Rigidbody rigidbody;
    float updateInterval = .3f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        target = GameObject.Find("Player").transform;

        StartCoroutine("UpdatePath");
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

    IEnumerator UpdatePath()
    {
        if (Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }
        PathRequestManager.RequestPathFind(new PathRequest(transform.position, target.position, OnCompltePastFind));

        while (true)
        {
            yield return new WaitForSeconds(updateInterval);

            if (Vector3.Distance(transform.position, target.position) > .25f)
            {
                PathRequestManager.RequestPathFind(new PathRequest(transform.position, target.position, OnCompltePastFind));
            }
        }
    }

    IEnumerator FollowTarget()
    {
        for(int i=0; i<path.Length; i++)
        {
            var waypoint = path[i];
            transform.LookAt(waypoint);
            while(transform.position != waypoint)
            {
                waypoint.y = transform.position.y;
                rigidbody.velocity = (waypoint - transform.position).normalized * speed;
                //transform.position = Vector3.MoveTowards(transform.position, waypoint, speed * Time.deltaTime);
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

    void OnDestroy()
    {
        pooler.ReturnObject(tag, this);
    }

    public override void OnObjectSpawn()
    {
        //TODO play a particle
    }
}
