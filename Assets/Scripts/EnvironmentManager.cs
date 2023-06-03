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
    // ���� ������ �µ�
    public sbyte temperature;
    // ī�޶� ������Ʈ
    private CameraMovement camera;

    void Start()
    {
        // ī�޶� ���� ī�޶��� ������Ʈ ����
        camera = Camera.main.GetComponent<CameraMovement>();
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
            
        }
    }
}
