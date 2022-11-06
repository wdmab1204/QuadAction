using BuffNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action2 : Buff
{
    private Transform[] objs;

    private Vector3 point;

    private float deg; //부채꼴 각도
    private int objCount;
    private float objSpeed;
    private float distance;
    private GameObject ball;

    public Action2(SkillScriptableObject item) : base(item)
    {
        this.objCount = item.objCount;
        this.objSpeed = item.objSpeed;
        this.distance = item.objDistance;
        this.deg = 30.0f;
        this.ball = item.objProfab;
    }

    public override void SetUser(Character user)
    {
        base.SetUser(user);
        this.point = user.transform.position;
    }

    public override void Start()
    {
        if (user == null) Debug.LogError(name+ " Item's user is null");

        objs = new Transform[objCount];

        //오브젝트 생성
        for (int i = 0; i < objCount; i++)
        {
            objs[i] = GameObject.Instantiate(ball).transform;
            objs[i].position = point;
        }
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
            
        while ((point-objs[0].position).sqrMagnitude<=distance) // 일정거리까지 움직일때까지 반복
        {
            var rot = deg / (objCount - 1);
            for (int i = 0; i < objCount; i++)
            {
                var rad = Mathf.Deg2Rad * (rot * i - rot + user.transform.rotation.y);
                var x = Mathf.Sin(rad);
                var z = Mathf.Cos(rad);
                objs[i].transform.Translate(new Vector3(x, 0, z) * Time.deltaTime * objSpeed);
            }
        }
            
        //기존 오브젝트 원위치로 리셋
        foreach(var obj in objs)
        {
            obj.position = point;
        }
    }

    private void DestroyObj()
    {

    }

}