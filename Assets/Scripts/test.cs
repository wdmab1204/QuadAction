using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void hello() { transform.DOMoveX(10, 3); }
    public void bye() { transform.DOMoveX(0, 3); }
}
