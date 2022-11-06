using System.Collections;
using UnityEngine;

namespace ItemNameSpace
{
    public class ItemManager : MonoBehaviour
    {
        PlayerSkill playerSkill;

        public void AddItemListener(Item item)
        {
            item.Start();
            this.playerSkill += item.Update;
        }

        public void DeleteItemListener(Item item)
        {
            this.playerSkill -= item.Update;
            item.Exit();
        }

        private void Update()
        {
            playerSkill?.Invoke();
        }
    }
}