using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        point = transform.position;
    }
    float deg;
    [SerializeField]float objSpeed = 1.0f;
    [SerializeField]float circleR = 1.0f;
    Vector3 point;
    // Update is called once per frame
    void Update()
    {
        if (deg < 360)
        {
            deg += Time.deltaTime * objSpeed;

            var rad = Mathf.Deg2Rad * deg;
            var x = circleR * Mathf.Sin(rad);
            var y = circleR * Mathf.Cos(rad);
            transform.position = point + new Vector3(x, 0, y);
            transform.rotation = Quaternion.Euler(0, deg * -1, 0);
        }
        else
        {
            deg = 0;
        }
    }
}
