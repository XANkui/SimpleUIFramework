using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通用配置管理器接口
/// </summary>
public interface IConfigManager 
{
    /// <summary>
    /// 只读属性，应用设置
    /// 功能：得到键值对结合数据
    /// </summary>
    Dictionary<string, string> AppSetting { get; }

    /// <summary>
    /// 得到配置文件（AppSetting）最大数量
    /// </summary>
    /// <returns></returns>
    int GetAppSettingMaxNumber();
}

public class KeyValueInfo {
    public List<KeyValueNode> ConfigInfo = null;
}

[System.Serializable]
public class KeyValueNode {
    public string Key = null;
    public string Value = null;
}
