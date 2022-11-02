using System.Collections;
using UnityEngine;

public class CameraGameOverProduce : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float height = 5f;
    private float _deg;
    private float deg
    {
        set { _deg = value; }
        get { return _deg % 360; }
    }

    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        deg += Time.unscaledDeltaTime * cameraSpeed;

        var rad = Mathf.Deg2Rad * deg;
        var x = radius * Mathf.Sin(rad);
        var z = radius * Mathf.Cos(rad);

        transform.position = target.position + new Vector3(x, height, z);
        transform.LookAt(target);
    }
}