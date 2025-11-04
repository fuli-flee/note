using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int monsterID = 1;
    public string monsterName = "fuli";
    
    public void Dead()
    {
        Debug.Log("怪物死亡了");
        
        EventCenter.Instance.EventTrigger<Monster>("MonsterDead",this);
    }
}