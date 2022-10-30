using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action2
{
    private GameObject ball;
    private Transform[] objs;

    private Transform t;
    private Vector3 point;
    private float deg; //부채꼴 각도
    private float objCount;
    private float objSpeed;
    private float distance;


    private bool doit;

    public void Init(GameObject ball, Transform t, int objCount = 1, float theta = 30.0f, float objSpeed = 1.0f, float distance = 1.0f)
    {
        this.t = t;
        this.ball = ball;
        this.point = t.position;
        this.objCount = objCount;
        this.deg = theta;
        this.objSpeed = objSpeed;
        this.distance = distance;
        this.doit = true;

        objs = new Transform[objCount];

        //오브젝트 생성
        for (int i = 0; i < objCount; i++)
        {
            objs[i] = GameObject.Instantiate(ball).transform;
            objs[i].position = point;
        }

    }


    

    public IEnumerator UpdateAction()
    {
        while (doit)
        {
            
            while (Vector3.Distance(point,objs[0].position)<=distance) // 일정거리까지 움직일때까지 반복
            {
                var rot = deg / (objCount - 1);
                for (int i = 0; i < objCount; i++)
                {
                    var rad = Mathf.Deg2Rad * (rot * i - rot + t.rotation.y);
                    var x = Mathf.Sin(rad);
                    var z = Mathf.Cos(rad);
                    objs[i].transform.Translate(new Vector3(x, 0, z) * Time.deltaTime * objSpeed);
                }
                yield return null;
            }
            
            //기존 오브젝트 원위치로 리셋
            foreach(var obj in objs)
            {
                obj.position = point;
            }
            

        }
    }

    private void DestroyObj()
    {

    }

    public void StopAction()
    {
        doit = false;
    }
}