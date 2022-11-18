using ItemNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TreasureBox : MonoBehaviour
{
    [SerializeField] private ItemScriptableObject hpup;
    [SerializeField] private ItemScriptableObject speedup;
    [SerializeField] private SkillScriptableObject hoverAround;
    [SerializeField] private SkillScriptableObject action2;
    private List<Item> buffList;
    public bool onlyAction1;

    private void Awake()
    {
        buffList = new List<Item>();
    }

    private void Start()
    {
        buffList.Add(new HPup(new ItemInfo(hpup.name, hpup.icon)));
        buffList.Add(new SpeedUp(new ItemInfo(speedup.name,speedup.icon)));
        buffList.Add(new HoverAround(new SkillInfo(hoverAround.name,hoverAround.icon,hoverAround.objCount,hoverAround.objSpeed,hoverAround.circleRadius,hoverAround.objDistance,hoverAround.objPrefab)));
        buffList.Add(new HoverAround(new SkillInfo(action2.name, action2.icon, action2.objCount, action2.objSpeed, action2.circleRadius, action2.objDistance, action2.objPrefab)));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var user = collision.gameObject.GetComponent<Player>();
            int randIndex = Random.Range(0, buffList.Count);

            if (onlyAction1) randIndex = 2;

            var item = buffList[randIndex];
            item.SetUser(user);

            var buffManager = user.BuffManager;
            buffManager.AddBuffListener(item);

            GameManager.Instance.SendSystemMessage(item.name + " was used");

            //트레져박스 맵에서 삭제
            Destroy(this.gameObject);
        }
    }
}