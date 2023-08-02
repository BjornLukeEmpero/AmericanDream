using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    // 셀에 들어갈 아이템 정보
    public Item item;

    // 현재 셀 하나에 있는 아이템 개수
    public byte itemCount;

    public UnityEngine.UI.Image itemImage;

    [SerializeField]
    private TextMeshProUGUI itemCountTxt;

    /// <summary>
    /// 아이템 이미지의 투명도 조절
    /// </summary>
    /// <param name="alpha"></param>
    private void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }

    /// <summary>
    /// 새로운 아이템 획득
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public void AddItem(Item item, byte count = 1)
    {
        this.item = item;
        itemCount = count;
        itemImage.sprite = item.itemIamge;

        if(item.itemType != Item.ItemType.Weapon || item.itemType != Item.ItemType.Armor || item.itemType != Item.ItemType.Tool)
        {
            itemCountTxt.text = itemCount.ToString();
        }
        else
        {
            itemCountTxt.text = "0";
        }

        SetColor(1);
    }

    /// <summary>
    /// 기존에 가진 아이템의 수량 변경
    /// </summary>
    /// <param name="count"></param>
    public void SetItemCount(byte count)
    {
        itemCount += count;
        itemCountTxt.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearCell();
    }

    private void ClearCell()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        itemCountTxt.text = "0";
    }
    
    /// <summary>
    /// 마우스 드래그 시작 시 발생하는 이벤트
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragCell.instance.dragCell = this;
            DragCell.instance.DragSetImage(itemImage);
            DragCell.instance.transform.transform.position = eventData.position;
        }
    }

    /// <summary>
    /// 마우스 드래그 중일 때 계속 발생하는 이벤트
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
            DragCell.instance.transform.position = eventData.position;
    }
     
    /// <summary>
    /// 마우스 드래그 종료 시 발생하는 이벤트
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        DragCell.instance.SetColor(0);
        DragCell.instance.dragCell = null;
    }
    
    /// <summary>
    /// 해당 슬롯에 아이템을 마우스로 드롭한 경우 발생하는 이벤트
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        if (DragCell.instance.dragCell != null)
            ChangeCell();
    }

    /// <summary>
    /// 셀을 드래그해 서로의 위치를 변경 (A, B)
    /// </summary>
    public void ChangeCell()
    {
        // B 셀의 아이템 값을 저장
        Item tempItem = item;
        // B 셀의 셀당 아이템 보유 개수를 저장
        byte tempItemCount = itemCount;

        // A 셀은 dragCell에서 참조하고 ChangeCell() 호출 시 드룹 위치에 A 셀 추가
        AddItem(DragCell.instance.dragCell.item, DragCell.instance.dragCell.itemCount);

        // 드롭 위치 대상 B 셀이 빈 셀이 아닐 경우
        if(tempItem != null)
            // A 셀 자리에 B 셀 추가
            DragCell.instance.dragCell.AddItem(tempItem, tempItemCount);
        else // B 슬롯이 비어있는 경우
            // A 슬롯 비우기
            DragCell.instance.dragCell.ClearCell();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
