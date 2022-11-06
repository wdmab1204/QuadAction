using ItemNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TreasureBox : MonoBehaviour
{
    [SerializeField] private ItemScriptableObject hpup;
    [SerializeField] private ItemScriptableObject speedup;
    [SerializeField] private ItemScriptableObject action1;
    [SerializeField] private ItemScriptableObject action2;
    private List<Item> itemList;

    private void Awake()
    {
        itemList = new List<Item>();
    }

    private void Start()
    {
        itemList.Add(new HPup(hpup));
        itemList.Add(new SpeedUp(speedup));
        itemList.Add(new Action1((SkillScriptableObject)action1));
        itemList.Add(new Action2((SkillScriptableObject)action2));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var user = collision.gameObject.GetComponent<Player>();
            int randIndex = Random.Range(0, itemList.Count);

            var item = itemList[1];
            item.SetUser(user);

            var itemmanage = collision.gameObject.GetComponent<ItemManager>();
            itemmanage.AddItemListener(item);

            GameManager.Instance.SendItemMessage(item.name + " was used");

            //트레져박스 맵에서 삭제
            Destroy(this.gameObject);
        }
    }
}