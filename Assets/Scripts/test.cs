using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOMoveX(10, 3));
        s.Append(transform.DOMoveX(0, 3));
    }

    public void hello() { transform.DOMoveX(10, 3); }
    public void bye() { transform.DOMoveX(0, 3); }
}
