using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Awake()
    {
        EventCenter.Instance.AddEventListener<Monster>("MonsterDead",PlayerWaitMonsterDeadDo);
        EventCenter.Instance.AddEventListener("Test",Test);
    }

    public void PlayerWaitMonsterDeadDo(Monster info)
    {
        Debug.Log($"玩家得奖励{info.monsterID}");
    }

    public void Test()
    {
        print("无参数事件监听者");
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener<Monster>("MonsterDead",PlayerWaitMonsterDeadDo);
        EventCenter.Instance.RemoveEventListener("Test",Test);
    }
}