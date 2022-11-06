using UnityEditor;
using UnityEngine;

namespace ItemNameSpace
{
    public class HPup : Item
    {

        public HPup(ItemScriptableObject itemScriptableObject) : base(itemScriptableObject)
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
