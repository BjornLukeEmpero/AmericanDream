// 작성자: 이재윤, 간접 기여자: 신성범

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
    // 현재 지역의 배경음악
    public AudioClip audioClip;
    // 현재 지역의 온도
    public byte temperature = new byte();
    // 플레이어 컴포넌트
    private Player player;
    // 카메라 컴포넌트
    private CameraMovement camera;

    private AudioSource audioSource;

    void Start()
    {
        // 카메라에 메인 카메라의 컴포넌트 연결
        player = GetComponent<Player>();
        camera = Camera.main.GetComponent<CameraMovement>();
        audioSource = GetComponent<AudioSource>();
        
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
            // 재생할 배경음악을 가져온다
            audioSource.clip = audioClip;
            // 가져온 배경음악을 재생한다
            audioSource.Play();
            
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 배경음악을 중단한다.
            audioSource.Stop();
        }
        
        
    }

}
