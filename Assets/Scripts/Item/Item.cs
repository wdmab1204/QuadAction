using UnityEditor;
using UnityEngine;


namespace ItemNameSpace
{
    public enum ItemType
    {
        None,
        HPup,
        SpeedUp
    };

    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object Asset/Item")]
    public class Item : ScriptableObject
    {
        new public string name = "New Item";
        public Sprite icon = null;
        public ItemType itemType;

        public virtual void Use() { }
    }
}
