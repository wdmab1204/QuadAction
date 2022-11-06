using UnityEditor;
using UnityEngine;

namespace BuffNameSpace
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object Asset/Item")]
    public class BuffScriptableObject : ScriptableObject
    {
        new public string name = "New Item";
        public Sprite icon = null;
    }
}