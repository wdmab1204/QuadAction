using BuffNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TreasureBox : MonoBehaviour
{
    [SerializeField] private BuffScriptableObject hpup;
    [SerializeField] private BuffScriptableObject speedup;
    [SerializeField] private BuffScriptableObject action1;
    [SerializeField] private BuffScriptableObject action2;
    private List<Buff> buffList;

    private void Awake()
    {
        buffList = new List<Buff>();
    }

    private void Start()
    {
        buffList.Add(new HPup(hpup));
        buffList.Add(new SpeedUp(speedup));
        buffList.Add(new Action1((SkillScriptableObject)action1));
        buffList.Add(new Action2((SkillScriptableObject)action2));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var user = collision.gameObject.GetComponent<Player>();
            int randIndex = Random.Range(0, buffList.Count);

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