using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Dialogue
{
    // ��縦 ���ϴ� ĳ���͸�
    public string[] npcName;
    // ��� ����
    public string[] scripts;

    public Image[] dlgWindows;
}

[System.Serializable]
public class DialogueEvent
{
    
    
    public Vector2 line;
    public Dialogue[] dialogues;
}
