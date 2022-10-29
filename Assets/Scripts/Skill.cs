using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName ="Skill", menuName = "Scriptable Object Asset/Skill")]
public class Skill : ScriptableObject
{
    public float attackTime;
    public float attackAnimationCooldown;
    public string particleName;
    public float range;
}