using UnityEngine.Events;

/// <summary>
/// 公共Mono管理器
/// </summary>
public class MonoMgr : SingletonAutoMono<MonoMgr>
{
    private event UnityAction updateEvent;
    private event UnityAction fixedUpdateEvent;
    private event UnityAction lateUpdateEvent;

    public void AddUpdateListener(UnityAction updateFun)
    {
        updateEvent += updateFun;
    }
    
    public void RemoveUpdateListener(UnityAction updateFun)
    {
        updateEvent -= updateFun;
    }
    
    public void AddFixedUpdateListener(UnityAction fixedUpdateFun)
    {
        fixedUpdateEvent += fixedUpdateFun;
    }
    
    public void RemoveFixedUpdateListener(UnityAction fixedUpdateFun)
    {
        fixedUpdateEvent -= fixedUpdateFun;
    }
    
    public void AddLateUpdateListener(UnityAction lateUpdateFun)
    {
        lateUpdateEvent += lateUpdateFun;
    }
    
    public void RemoveLateUpdateListener(UnityAction lateUpdateFun)
    {
        lateUpdateEvent -= lateUpdateFun;
    }

    private void Update()
    {
        updateEvent?.Invoke();
    }

    private void FixedUpdate()
    {
        fixedUpdateEvent?.Invoke();
    }

    private void LateUpdate()
    {
        lateUpdateEvent?.Invoke();
    }
}
