using ItemNameSpace;
using System.Collections;
using UnityEngine;


public class TreasureBox : MonoBehaviour
{
    public Item[] items;
    public bool isWeapon1;
    public bool isWeapon2;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var user = collision.gameObject.GetComponent<Player>();

            if (isWeapon1 == true)
            {
                user.CreateAction();
                Destroy(this.gameObject);
                return;
            }
            if (isWeapon2 == true)
            {
                user.CreateAction2();
                Destroy(this.gameObject);
                return;
            }
            //아이템중 랜덤으로 하나가 나옴
            Item item = SetItemfromType(items[Random.Range(0, items.Length)]);
            //그 아이템을 사용 or 플레이어에게 적용
            item.Use(user);
            //위 효과를 텍스트로 띄움(게임매니저를 통해서)
            GameManager.Instance.SendItemMessage(item.itemType + " was used");
            Debug.Log(item.name + "을(를) 사용하였습니다");

            //트레져박스 맵에서 삭제
            Destroy(this.gameObject);
        }
    }

    private Item SetItemfromType(Item item)
    {
        Item newItem = item;
        if (item.itemType == ItemType.HPup)
            newItem = new HPup(item);
        else if (item.itemType == ItemType.SpeedUp)
            newItem = new SpeedUp(item);
        else
            newItem.name = "Error";


        return newItem;
    }
}