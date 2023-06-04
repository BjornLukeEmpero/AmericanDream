// �ۼ���: ������, ���� �⿩��: �ż��� �ֱ��ۼ�����: 2023-06-04

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �� ī�޶� �̵� ������ ȯ���� ��Ÿ���� Ŭ����
/// </summary>
public class EnvironmentManager : MonoBehaviour
{
    // ī�޶��� �ִ� ��ǥ ����
    public Vector2 maxPos;
    // ī�޶��� �ּ� ��ǥ ����
    public Vector2 minPos;
    // ���� ������ �������
    public AudioClip audioClip;
    // ���� ������ �µ�
    public sbyte temperature;
    // ī�޶� ������Ʈ
    private CameraMovement camera;

    private AudioSource audioSource;

    void Start()
    {
        // ī�޶� ���� ī�޶��� ������Ʈ ����
        camera = Camera.main.GetComponent<CameraMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾ �ٸ� �ʵ�� ������ ���
        if (other.CompareTag("Player"))
        {
            // ī�޶��� �ּ� ��ǥ ������ �̸� ����� ������ ����
            camera.minPosition = minPos;
            // ī�޶��� �ִ� ��ǥ ������ �̸� ����� ������ ����
            camera.maxPosition = maxPos;
            
            // ����� ��������� �����´�
            audioSource.clip = audioClip;
            // ������ ��������� ����Ѵ�
            audioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ��������� �ߴ��Ѵ�.
            audioSource.Stop();
        }
        
        
    }

}
