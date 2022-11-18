using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class test : MonoBehaviour
{
    private void Start()
    {
        Dictionary<int, string> dic = new Dictionary<int, string>();

        dic.Add(1, "one");
        dic.Add(2, "two");

        int key = 1;

        //key를 이미 가지고있다면
        if (dic.ContainsKey(key))
        {
            //dic[key].Length();
        }

        foreach(var a in dic)
        {
            Debug.Log(a.Key +": "+a.Value);
        }
        
    }

}
