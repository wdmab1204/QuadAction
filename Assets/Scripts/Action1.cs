using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action1
{
    private Transform[] objs;

    private Transform t;
    private float objSpeed;
    private float circleR;
    private float _deg;
    private int objCount;
    private bool doit;

    private float deg
    {
        set { _deg = value; }
        get { return _deg % 360; }
    }
    public void Init(GameObject ball, Transform t, int objCount, float objSpeed = 1.0f, float circleR = 1.0f)
    {
        this.t = t;
        this.objSpeed = objSpeed;
        this.circleR = circleR;
        this.objCount = objCount;
        this.doit = true;

        objs = new Transform[objCount];

        for (int i = 0; i < objCount; i++)
            objs[i] = GameObject.Instantiate(ball).transform;
        
    }

    public IEnumerator UpdateAction()
    {

        while (doit)
        {

            for (int i = 0; i < objCount; i++)
            {
                deg += Time.deltaTime * objSpeed;

                var rad = Mathf.Deg2Rad * (deg + (i * (360 / objCount)));
                var x = circleR * Mathf.Sin(rad);
                var z = circleR * Mathf.Cos(rad);

                objs[i].position = t.position + new Vector3(x, 1.5f, z);
                objs[i].rotation = Quaternion.Euler(0, (deg + (i * (360 / objCount))) * -1, 0);
            }

            yield return null;
        }
    }

    public void Stop()
    {
        doit = false;
            
    }

    public void StopAction()
    {
        doit = false;
    }
}