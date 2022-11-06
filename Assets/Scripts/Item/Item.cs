using UnityEditor;
using UnityEngine;


namespace ItemNameSpace
{

    public enum ItemType
    {
        None,
        Use,
        Action
    };


    public abstract class Item : ScriptableObject
    {
        new public string name = "New Item";
        public Sprite icon = null;
        public ItemType itemType;
        protected Character user;

        public Item(ItemScriptableObject itemScriptableObject) 
        {
            this.name = itemScriptableObject.name;
            this.itemType = itemScriptableObject.itemType;
            this.icon = itemScriptableObject.icon;
        }

        public abstract void Start();
        public abstract void Update();
        public abstract void Exit();
        public virtual void SetUser(Character user) => this.user = user;
    }
}
