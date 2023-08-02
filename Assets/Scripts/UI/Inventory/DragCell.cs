using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragCell : MonoBehaviour
{
    // DragCell의 UI 이미지 오브젝트를 담음
    static public DragCell instance;
    // 드래그 대상인 인벤토리 셀 참조
    public Cell dragCell;

    [SerializeField]
    // 자기 자신의 이미지 컴포넌트
    private Image imageItem;
    
    // Start is called before the first frame update
    void Start()
    {
        // 시작 시 instance에 자기 자신을 할당
        instance = this;
    }

    // 드래그 되는 슬롯의 이미지 삽입
    public void DragSetImage(Image itemImage)
    {
        // 자기 자신의 이미지에 인수로 들어온 sprite 이미지 할당
        imageItem.sprite = itemImage.sprite;
        // 투명도 값을 1로 바꾸는 함수
        SetColor(1);
    }

    // 본인의 이미지 컴포넌트인 imageItem 이미지 투명도 조절
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
