using UnityEditor;
using UnityEngine;

namespace BuffNameSpace
{
    public class HPup : Buff
    {

        public HPup(BuffScriptableObject itemScriptableObject) : base(itemScriptableObject)
        {

        }

        public override void Exit()
        {

        }

        public override void Start()
        {
            GameManager.Instance.HealToTarget(5, user);
        }

        public override void Update()
        {

        }

    }
}
