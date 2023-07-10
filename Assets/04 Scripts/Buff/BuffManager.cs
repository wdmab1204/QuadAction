using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ItemNameSpace
{
    public class BuffManager
    {
        Dictionary<string, Item> itemDic;

        public BuffManager()
        {
            itemDic = new Dictionary<string, Item>();
        }


        public void AddBuffListener(Item item)
        {
            //이미 아이템을 가지고있는가?
            if (itemDic.ContainsKey(item.name))
            {
                itemDic[item.name].Upgrade();
            }
            else
            {
                //가지고 있지 않다면
                itemDic.Add(item.name, item);
                itemDic[item.name].Start();
            }

        }

        public void DeleteBuffListener(Item buff)
        {
            buff.Exit();
        }

        public void Update()
        {
            foreach(var item in itemDic.Values)
            {
                item.Update();
            }
        }
    }
}