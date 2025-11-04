using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    private void Awake()
    {
        EventCenter.Instance.AddEventListener<Monster>("MonsterDead",TaskWaitMonsterDeadDo);
    }
    
    public void TaskWaitMonsterDeadDo(Monster info)
    {
        Debug.Log("任务记录");
    }
    
    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener<Monster>("MonsterDead",TaskWaitMonsterDeadDo);
    }
}