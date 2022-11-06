using System.Collections;
using UnityEngine;

namespace BuffNameSpace
{
    public class BuffManager
    {
        PlayerSkill playerSkill;

        public void AddBuffListener(Buff buff)
        {
            buff.Start();
            this.playerSkill += buff.Update;
        }

        public void DeleteBuffListener(Buff buff)
        {
            this.playerSkill -= buff.Update;
            buff.Exit();
        }

        public void Update()
        {
            playerSkill?.Invoke();
        }
    }
}