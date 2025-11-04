using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other : MonoBehaviour
{
    private void Awake()
    {
        EventCenter.Instance.AddEventListener<Monster>("MonsterDead",OtherWaitMonsterDeadDo);
    }
    
    public void OtherWaitMonsterDeadDo(Monster info)
    {
        Debug.Log($"其他相关处理{info.monsterName}");
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener<Monster>("MonsterDead",OtherWaitMonsterDeadDo);
    }
}