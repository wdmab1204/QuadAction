using UnityEngine;

public class Camera : MonoBehaviour
{
    public Player player;
    public Vector3 distance;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x + distance.x,
            playerTransform.position.y + distance.y,
            playerTransform.position.z + distance.z);
    }
}
