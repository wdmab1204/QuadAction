﻿using UnityEditor;
using UnityEngine;

namespace ItemNameSpace
{
    public class HPup : Item
    {
        //나중에 ItemScriptableObject로 바꾸기
        public HPup(Item item)
        {
            this.name = item.name;
            this.icon = item.icon;
            this.itemType = item.itemType;
        }
        public override void Use(Character user)
        {
            GameManager.Instance.HealToTarget(5, user);

            //이펙트나 효과음 추가
        }
    }
}
