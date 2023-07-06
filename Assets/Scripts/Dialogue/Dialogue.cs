using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Dialogue
{
    // 대사를 말하는 캐릭터명
    public string[] npcName;
    // 대사 내용
    public string[] scripts;

    public Image[] dlgWindows;
}

[System.Serializable]
public class DialogueEvent
{
    
    
    public Vector2 line;
    public Dialogue[] dialogues;
}
