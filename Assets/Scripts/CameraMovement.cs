using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // ī�޶� ���� ���
    public Transform target;
    // ī�޶��� �̵��ӵ�
    public float cameraSpeed;
    // ī�޶� �̵� ������ �ִ� ��ǥ��
    public Vector2 maxPosition;
    // ī�޶� �̵� ������ �ּ� ��ǥ��
    public Vector2 minPosition;

    public EnvironmentManager environmentManager;

    
    // Start is called before the first frame update
    void Start()
    {
        


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        maxPosition = environmentManager.maxPos;
        minPosition = environmentManager.minPos;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // ���� ���� ����� ��ġ�� ī�޶� ��ġ�� ���� �ʴٸ�
        if(transform.position != target.position)
        {
            // ī�޶� ��ġ�� ���� ���� ����� ��ġ�� ������Ų��.
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            // �̵� ������ ������ �ּ� x��ǥ�� �ִ� x��ǥ�� �����Ѵ�
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            // �̵� ������ ������ �ּ� y��ǥ�� �ִ� y��ǥ�� �����Ѵ�
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            // ī�޶� ����� �����̸� �׿� �°� õõ�� ���󰣴�.
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed);
        }
    }
}
