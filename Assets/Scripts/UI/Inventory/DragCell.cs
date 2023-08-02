using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragCell : MonoBehaviour
{
    // DragCell�� UI �̹��� ������Ʈ�� ����
    static public DragCell instance;
    // �巡�� ����� �κ��丮 �� ����
    public Cell dragCell;

    [SerializeField]
    // �ڱ� �ڽ��� �̹��� ������Ʈ
    private Image imageItem;
    
    // Start is called before the first frame update
    void Start()
    {
        // ���� �� instance�� �ڱ� �ڽ��� �Ҵ�
        instance = this;
    }

    // �巡�� �Ǵ� ������ �̹��� ����
    public void DragSetImage(Image itemImage)
    {
        // �ڱ� �ڽ��� �̹����� �μ��� ���� sprite �̹��� �Ҵ�
        imageItem.sprite = itemImage.sprite;
        // ���� ���� 1�� �ٲٴ� �Լ�
        SetColor(1);
    }

    // ������ �̹��� ������Ʈ�� imageItem �̹��� ���� ����
    public void SetColor(float alpha)
    {
        Color color = imageItem.color;
        color.a = alpha;
        imageItem.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
