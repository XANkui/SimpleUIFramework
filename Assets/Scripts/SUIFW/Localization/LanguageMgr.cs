using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 语言国际化
/// </summary>
public class LanguageMgr 
{
    private static LanguageMgr _Instance;

    // 语言集合
    private Dictionary<string, string> _DicLanguageCache;

    public LanguageMgr() {
        _DicLanguageCache = new Dictionary<string, string>();

        // 初始化预压缓存集合
        InitLanguageCache();
    }

    /// <summary>
    /// 得到实例
    /// </summary>
    /// <returns></returns>
    public static LanguageMgr GetInstance() {
        if (_Instance ==null)
        {
            _Instance = new LanguageMgr();
        }

        return _Instance;
    }

    /// <summary>
    /// 得到文本信息
    /// </summary>
    /// <param name="languageID"></param>
    /// <returns></returns>
    public string ShowText(string languageID) {
        //

        string strQueryResult = string.Empty;

        // 参数检查
        if (string.IsNullOrEmpty(languageID))
        {
            return null;
        }

        // 查询处理
        if (_DicLanguageCache != null && _DicLanguageCache.Count>=1)
        {
            _DicLanguageCache.TryGetValue(languageID, out strQueryResult);

            if (string.IsNullOrEmpty(strQueryResult) == false)
            {
                return strQueryResult;
            }
        }

        Debug.Log(GetType() + " /ShowText()/ Qurey is Null! Please Check. Parameter languageID = " + languageID);

        return null;
    }


    /// <summary>
    /// 初始化预压缓存集合
    /// </summary>
    private void InitLanguageCache() {
        IConfigManager config = new ConfigManagerByJson("Config/LauguageJSONConfig");
        if (config !=null)
        {
            _DicLanguageCache = config.AppSetting;
        }
    }

}
