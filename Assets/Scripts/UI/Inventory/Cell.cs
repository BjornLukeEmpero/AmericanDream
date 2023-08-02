using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    // ���� �� ������ ����
    public Item item;

    // ���� �� �ϳ��� �ִ� ������ ����
    public byte itemCount;

    public UnityEngine.UI.Image itemImage;

    [SerializeField]
    private TextMeshProUGUI itemCountTxt;

    /// <summary>
    /// ������ �̹����� ���� ����
    /// </summary>
    /// <param name="alpha"></param>
    private void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }

    /// <summary>
    /// ���ο� ������ ȹ��
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
    /// ������ ���� �������� ���� ����
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
    /// ���콺 �巡�� ���� �� �߻��ϴ� �̺�Ʈ
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
    /// ���콺 �巡�� ���� �� ��� �߻��ϴ� �̺�Ʈ
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
            DragCell.instance.transform.position = eventData.position;
    }
     
    /// <summary>
    /// ���콺 �巡�� ���� �� �߻��ϴ� �̺�Ʈ
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        DragCell.instance.SetColor(0);
        DragCell.instance.dragCell = null;
    }
    
    /// <summary>
    /// �ش� ���Կ� �������� ���콺�� ����� ��� �߻��ϴ� �̺�Ʈ
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        if (DragCell.instance.dragCell != null)
            ChangeCell();
    }

    /// <summary>
    /// ���� �巡���� ������ ��ġ�� ���� (A, B)
    /// </summary>
    public void ChangeCell()
    {
        // B ���� ������ ���� ����
        Item tempItem = item;
        // B ���� ���� ������ ���� ������ ����
        byte tempItemCount = itemCount;

        // A ���� dragCell���� �����ϰ� ChangeCell() ȣ�� �� ��� ��ġ�� A �� �߰�
        AddItem(DragCell.instance.dragCell.item, DragCell.instance.dragCell.itemCount);

        // ��� ��ġ ��� B ���� �� ���� �ƴ� ���
        if(tempItem != null)
            // A �� �ڸ��� B �� �߰�
            DragCell.instance.dragCell.AddItem(tempItem, tempItemCount);
        else // B ������ ����ִ� ���
            // A ���� ����
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
