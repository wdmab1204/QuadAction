using UnityEditor;
using UnityEngine;

namespace ItemNameSpace
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object Asset/Skill")]
    public class SkillScriptableObject : ItemScriptableObject
    {
        public int objCount;
        public float objSpeed;
        public float circleRadius;
        public float objDistance;
        public GameObject objPrefab;
    }
}