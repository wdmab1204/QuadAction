using UnityEditor;
using UnityEngine;

namespace BuffNameSpace
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object Asset/Skill")]
    public class SkillScriptableObject : BuffScriptableObject
    {
        public int objCount;
        public float objSpeed;
        public float circleRadius;
        public float objDistance;
        public GameObject objProfab;
    }
}