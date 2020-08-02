using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责UI框架中个组件或窗体消息传递
/// </summary>
public class MessageCenter 
{
    // 委托 消息传递
    public delegate void DelMessageDelivery(KeyValuesUpdate kv);

    // 消息中心缓存集合
    // <string 数据大的分类，DelMessageDelivery 数据委托 >
    public static Dictionary<string, DelMessageDelivery> _DicMessages = new Dictionary<string, DelMessageDelivery>();

    /// <summary>
    /// 添加消息的监听
    /// </summary>
    /// <param name="messageType">消息分类</param>
    /// <param name="handler">消息委托</param>
    public static void AddMsgListener(string messageType, DelMessageDelivery handler) {
        if (!_DicMessages.ContainsKey(messageType))
        {
            _DicMessages.Add(messageType,null);
        }


        _DicMessages[messageType] += handler;
    }

    /// <summary>
    /// 移除消息的监听
    /// </summary>
    /// <param name="messageType">消息分类</param>
    /// <param name="handler">消息委托</param>
    public static void RemoveMsgListener(string messageType, DelMessageDelivery handler)
    {
        if (_DicMessages.ContainsKey(messageType))
        {
            _DicMessages[messageType] -= handler;
        }

    }

    /// <summary>
    /// 移除所有消息的监听
    /// </summary>    
    public static void ClearAllMsgListener()
    {
        if (_DicMessages != null)
        {
            _DicMessages.Clear();
        }

    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="messageType">消息类型</param>
    /// <param name="kv">键值对（对象）</param>
    public static void SendMessage(string messageType, KeyValuesUpdate kv) {
        DelMessageDelivery del;

        if (_DicMessages.TryGetValue(messageType, out del))
        {
            if (del != null)
            {
                del(kv);
            }
        }
    }

}

/// <summary>
/// 键值对
/// 配合委托，实现委托数据传输
/// </summary>
public class KeyValuesUpdate {

    private string _Key;
    private object _Value;

    public KeyValuesUpdate(string Key, object Value)
    {
        _Key = Key;
        _Value = Value;
    }

    public string Key { get => _Key;  }
    public object Value { get => _Value;  }
}
