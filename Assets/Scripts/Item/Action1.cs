using ItemNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemNameSpace
{
    public class Action1 : Item
    {
        private Transform[] objs;

        private float objSpeed;
        private float circleR;
        private float _deg;
        private int objCount;
        private GameObject ball;

        private float deg
        {
            set { _deg = value; }
            get { return _deg % 360; }
        }

        public Action1(SkillScriptableObject item) : base(item) 
        {
            this.objSpeed = item.objSpeed;
            this.circleR = item.circleRadius;
            this.objCount = item.objCount;
            this.ball = item.objProfab;
        }

        public override void Start()
        {
            objs = new Transform[objCount];

            for (int i = 0; i < objCount; i++)
                objs[i] = GameObject.Instantiate(ball).transform;
        }

        public override void Update()
        {
            for (int i = 0; i < objCount; i++)
            {
                deg += Time.deltaTime * objSpeed;

                var rad = Mathf.Deg2Rad * (deg + (i * (360 / objCount)));
                var x = circleR * Mathf.Sin(rad);
                var z = circleR * Mathf.Cos(rad);

                objs[i].position = user.transform.position + new Vector3(x, 1.5f, z);
                objs[i].rotation = Quaternion.Euler(0, (deg + (i * (360 / objCount))) * -1, 0);
            }
        }

        public override void Exit()
        {

        }

        public void StopAction()
        {
            Exit();
        }

    }
}
