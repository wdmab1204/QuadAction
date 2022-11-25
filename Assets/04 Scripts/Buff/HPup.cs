using UnityEditor;
using UnityEngine;

namespace ItemNameSpace
{
    public class HPup : Item
    {

        public HPup(ItemInfo iteminfo) : base(iteminfo)
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

        public override void Upgrade()
        {
            GameManager.Instance.HealToTarget(5, user);
        }
    }
}
