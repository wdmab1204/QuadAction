using UnityEditor;
using UnityEngine;


namespace BuffNameSpace
{

    public abstract class Buff : ScriptableObject
    {
        new public string name = "New Item";
        public Sprite icon = null;
        protected Character user;

        public Buff(BuffScriptableObject buffInfo) 
        {
            this.name = buffInfo.name;
            this.icon = buffInfo.icon;
        }

        public abstract void Start();
        public abstract void Update();
        public abstract void Exit();
        public virtual void SetUser(Character user) => this.user = user;
    }
}
