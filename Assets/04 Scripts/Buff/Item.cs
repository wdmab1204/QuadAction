using UnityEditor;
using UnityEngine;


namespace ItemNameSpace
{

    public struct ItemInfo
    {
        public string name;
        public Sprite icon;

        public ItemInfo(string name, Sprite icon)
        {
            this.name = name;
            this.icon = icon;
        }
    }

    public struct SkillInfo
    {
        public ItemInfo itemInfo;
        public int objCount;
        public float objSpeed;
        public float circleRadius;
        public float objDistance;
        public GameObject objPrefab;

        public SkillInfo(string name, Sprite icon, int objCount, float objSpeed, float circleRadius, float objDistance, GameObject objPrefab) : this()
        {
            this.itemInfo.name = name;
            this.itemInfo.icon = icon;
            this.objCount = objCount;
            this.objSpeed = objSpeed;
            this.circleRadius = circleRadius;
            this.objDistance = objDistance;
            this.objPrefab = objPrefab;
        }

    }

    public abstract class Item 
    {
        public string name = "New Item";
        public Sprite icon = null;
        protected Character user;

        public Item(ItemInfo itemInfo) 
        {
            this.name = itemInfo.name;
            this.icon = itemInfo.icon;
        }

        public abstract void Start();
        public abstract void Update();
        public abstract void Exit();
        public virtual void SetUser(Character user) => this.user = user;
        public abstract void Upgrade();
    }
}
