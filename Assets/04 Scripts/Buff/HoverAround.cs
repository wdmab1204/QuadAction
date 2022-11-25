using ItemNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemNameSpace
{
    public class HoverAround : Item
    {
        private List<Transform> objs;

        private float objSpeed;
        private float circleR;
        private float deg;
        private int objCount;
        private GameObject ball;

        private float Deg
        {
            set { deg = value; }
            get { return deg % 360; }
        }

        public HoverAround(SkillInfo skillInfo) : base(skillInfo.itemInfo) 
        {
            this.objSpeed = skillInfo.objSpeed;
            this.circleR = skillInfo.circleRadius;
            this.objCount = skillInfo.objCount;
            this.ball = skillInfo.objPrefab;

            objs = new List<Transform>();
        }

        public override void Start()
        {
            for (int i = 0; i < objCount; i++)
                objs.Add(GameObject.Instantiate(ball).transform);
        }

        public override void Update()
        {
            for (int i = 0; i < objCount; i++)
            {
                Deg += Time.deltaTime * objSpeed;

                var rad = Mathf.Deg2Rad * (Deg + (i * (360 / objCount)));
                var x = circleR * Mathf.Sin(rad);
                var z = circleR * Mathf.Cos(rad);

                objs[i].position = user.transform.position + new Vector3(x, 1.5f, z);
                objs[i].rotation = Quaternion.Euler(0, (Deg + (i * (360 / objCount))) * -1, 0);
            }
        }

        public override void Exit()
        {

        }

        public void StopAction()
        {
            Exit();
        }

        public override void Upgrade()
        {
            objs.Add(GameObject.Instantiate(ball).transform);
            objCount += 1;
        }
    }
}
