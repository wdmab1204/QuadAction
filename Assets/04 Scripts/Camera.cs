using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player; // 플레이어 Transform 컴포넌트
    public float distance = 5f; // 카메라와 플레이어 사이의 거리
    public float height = 2f; // 카메라의 높이
    [Range(0, 360)] public float offset;

    private float rotationSpeed = .5f; // 카메라 회전 속도

    private float currentRotation = 0f; // 현재 카메라 회전값


    private void Update()
    {
        // 플레이어의 위치에 카메라를 배치
        Vector3 playerPosition = player.position;
        Vector3 cameraPosition = CalculateCameraPosition();
        transform.position = cameraPosition;

        // 카메라 회전값 갱신
        currentRotation = offset * rotationSpeed;
        Quaternion rotation = Quaternion.Euler(0f, currentRotation, 0f);

        // 플레이어 주변을 돌아다니는 카메라 회전 적용
        Vector3 lookAtPosition = playerPosition;
        lookAtPosition.y += height;
        transform.LookAt(playerPosition);
        transform.RotateAround(playerPosition, Vector3.up, currentRotation);
    }

    // 구면 좌표계를 사용하여 카메라 위치 계산
    private Vector3 CalculateCameraPosition()
    {
        float radianAngle = currentRotation * Mathf.Deg2Rad;
        float x = Mathf.Sin(radianAngle) * distance;
        float z = Mathf.Cos(radianAngle) * distance;
        Vector3 offset = new Vector3(x, height, z);
        Vector3 cameraPosition = player.position + offset;

        return cameraPosition;
    }

    //public void SmoothFollow()
    //{
    //    Vector3 targetPos = target.position + offset;
    //    Vector3 smoothFollow = Vector3.Lerp(transform.position,
    //    targetPos, smoothSpeed);

    //    transform.position = smoothFollow;
    //    transform.LookAt(target);
    //}
}
