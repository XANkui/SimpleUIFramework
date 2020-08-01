using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基于 json 配置文件的管理器
/// </summary>
public class ConfigManagerByJson : IConfigManager
{
    /// <summary>
    /// 保存键值对集合
    /// </summary>
    private Dictionary<string, string> _AppSetting;

    /// <summary>
    /// 只读属性，得到应用设置（键值对集合）
    /// </summary>
    public Dictionary<string, string> AppSetting {
        get {
            return _AppSetting;
        }
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jsonPath">Json配置文件路径</param>
    public ConfigManagerByJson(string jsonPath) {
        _AppSetting = new Dictionary<string, string>();

        // 初始化解析 Json 数据，加载到集合中
        InitAndParseJson(jsonPath);
    }

    public int GetAppSettingMaxNumber()
    {
        if (_AppSetting !=null && _AppSetting.Count>=1)
        {
            return _AppSetting.Count;
        }

        return 0;
    }

    /// <summary>
    /// 初始化解析 Json 数据，加载到集合中
    /// </summary>
    /// <param name="jsonPath"></param>
    private void InitAndParseJson(string jsonPath) {
        TextAsset configInfo = null;
        KeyValueInfo keyValueInfoObj = null;

        // 参数检查
        if (string.IsNullOrEmpty(jsonPath))
        {
            return;
        }

        // 解析 J'son 配置文件
        try
        {
            configInfo = Resources.Load<TextAsset>(jsonPath);
            keyValueInfoObj = JsonUtility.FromJson<KeyValueInfo>(configInfo.text);
        }
        catch 
        {

            throw new JsonParseException(GetType()+ " /InitAndParseJson()/Json Parse Exception！Parameter JsonPath = "+jsonPath);
        }

        // 数据加载到 AppSetting 集合中
        foreach (KeyValueNode nodeInfo in keyValueInfoObj.ConfigInfo)
        {
            _AppSetting.Add(nodeInfo.Key,nodeInfo.Value);
        }

    }
}
