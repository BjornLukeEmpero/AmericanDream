using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // 카메라가 따라갈 대상
    public Transform target;
    // 카메라의 이동속도
    public float cameraSpeed;
    // 카메라가 이동 가능한 최대 좌표값
    public Vector2 maxPosition;
    // 카메라가 이동 가능한 최소 좌표값
    public Vector2 minPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        // 만약 따라갈 대상의 위치와 카메라 위치가 같지 않다면
        if(transform.position != target.position)
        {
            // 카메라 위치를 현재 따라갈 대사의 위치에 고정시킨다.
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            // 이동 가능한 범위의 최소 x좌표와 최대 x좌표를 지정한다
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            // 이동 가능한 범위의 최소 y좌표와 최대 y좌표를 지정한다
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            // 카메라가 대상이 움직이면 그에 맞게 천천히 따라간다.
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed);
        }
    }
}
