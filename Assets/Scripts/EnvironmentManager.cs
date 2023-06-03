using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 내 카메라 이동 범위와 환경을 나타내는 클래스
/// </summary>
public class EnvironmentManager : MonoBehaviour
{
    // 카메라의 최대 좌표 범위
    public Vector2 maxPos;
    // 카메라의 최소 좌표 범위
    public Vector2 minPos;
    // 현재 지역의 온도
    public sbyte temperature;
    // 카메라 컴포넌트
    private CameraMovement camera;

    void Start()
    {
        // 카메라에 메인 카메라의 컴포넌트 연결
        camera = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 다른 필드로 진입한 경우
        if (other.CompareTag("Player"))
        {
            // 카메라의 최소 좌표 범위를 미리 저장된 값으로 설정
            camera.minPosition = minPos;
            // 카메라의 최대 좌표 범위를 미리 저장된 값으로 설정
            camera.maxPosition = maxPos;
            
        }
    }
}
