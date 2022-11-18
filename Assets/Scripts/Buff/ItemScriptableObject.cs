using UnityEditor;
using UnityEngine;

namespace ItemNameSpace
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object Asset/Item")]
    public class ItemScriptableObject : ScriptableObject
    {
        new public string name = "New Item";
        public Sprite icon = null;
    }
}