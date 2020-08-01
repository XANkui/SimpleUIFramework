using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 系统常量
/// 全局性方法
/// 系统枚举类型
/// 委托定义
/// </summary>
public class SysDefine 
{

    /*路径常量*/
    public const string SYS_PATH_CANVAS = "Canvas";

    /*标签常量*/
    public const string SYS_TAG_CANVAS = "_TagCanvas";

    /*节点常量*/
    public const string SYS_NODE_NORMAL = "Normal";
    public const string SYS_NODE_FIXED = "Fixed";
    public const string SYS_NODE_POPUP = "PopUp";
    public const string SYS_NODE_SCRIPTSMANAGER = "_ScriptsMgr";

    /**/

    /*全局性的方法*/

    /*委托*/
}


#region 系统枚举类型
/// <summary>
/// UI窗体的类型
/// </summary>
public enum UIFormType {

    // 普通窗体
    Normal,
    // 固定窗体
    Fixed,
    // 弹出窗体
    PopUp
}

/// <summary>
/// UI 窗体的显示类型
/// </summary>
public enum UIFormSHowType
{
    // 普通
    Normal,
    // 反向切换
    ReverseChange,
    // 隐藏其他
    HideOther
}

/// <summary>
/// UI 窗体透明度
/// </summary>
public enum UIFormLucenyType {
    // 完全透明，不可穿透
    Lucency,

    //半透明，不可穿透
    Translucence,

    // 低透明度，不能穿透
    ImPenetrable,

    // 可以穿透
    Pentrate
}


#endregion
