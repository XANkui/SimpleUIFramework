using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 用来管理遮挡UIForm 的脚本
/// </summary>
public class UIFormMaskMgr : MonoBehaviour
{
    // 私有单例
    private static UIFormMaskMgr _Instance =null;

    // UI 根节点对象
    private GameObject _GoCanvasRoot = null;

    // UI 脚本节点对象
    private Transform _TraUIScriptsNode = null;

    // 顶层面板
    private GameObject _GoTopPanel;

    // 遮罩面板
    private GameObject _GoMaskPanel;

    // UI 摄像机
    private Camera _UICamera;

    // UI 摄像机原始的 层深
    private float _OriginalUICameraDepth;

    /// <summary>
    /// 得到实例
    /// </summary>
    /// <returns></returns>
    public static UIFormMaskMgr GetInstance() {
        if (_Instance ==null)
        {
            _Instance = new GameObject("_UIFormMaskMgr").AddComponent<UIFormMaskMgr>();
        }


        return _Instance;
    }

    private void Awake()
    {
        // 得到 UI 根节点对象，脚本节点对象
        _GoCanvasRoot = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_CANVAS);
        _TraUIScriptsNode = UnityHelper.FindTheChildNode(_GoCanvasRoot,SysDefine.SYS_NODE_SCRIPTSMANAGER);

        // 把脚本实例，作为 脚本节点对象的 子节点
        UnityHelper.AddChildNodeToParentNode(_TraUIScriptsNode,this.gameObject.transform);

        // 得到 顶层面板 遮罩面板
        _GoTopPanel = _GoCanvasRoot;
        _GoMaskPanel = UnityHelper.FindTheChildNode(_GoCanvasRoot, "_UIFormMaskMgr").gameObject;

        // 得到 UI 摄像机原始的层深
        _UICamera = GameObject.FindGameObjectWithTag("_TagUICamera").GetComponent<Camera>();
        if (_UICamera != null)
        {
            // 得到UI摄像机原始层深
            _OriginalUICameraDepth = _UICamera.depth;
        }
        else {
            Debug.Log(GetType()+" /Awake()/UI_Camera is null, Please Check");
        }
    }

    /// <summary>
    /// 设置遮罩状态
    /// </summary>
    /// <param name="goDisplayUIForm">需要显示的UI窗体</param>
    /// <param name="lucenyType">显示透明度属性</param>
    public void SetMaskWindow(GameObject goDisplayUIForm,UIFormLucenyType lucenyType = UIFormLucenyType.Lucency) {

        // 顶层窗体下移
        _GoTopPanel.transform.SetAsLastSibling();

        // 启用遮罩窗体以及设置头透明度
       
        switch (lucenyType)
        {
                // 完全透明，不能穿透
            case UIFormLucenyType.Lucency:
                //Debug.Log("完全透明");
                _GoMaskPanel.SetActive(true);
                Color newColor1 = new Color(SysDefine.SYS_UIMASK_LUCENCY_COLOR_REB,
                    SysDefine.SYS_UIMASK_LUCENCY_COLOR_REB, SysDefine.SYS_UIMASK_LUCENCY_COLOR_REB,
                    SysDefine.SYS_UIMASK_LUCENCY_COLOR_REB_A);
                _GoMaskPanel.GetComponent<Image>().color = newColor1;

                break;
                //半透明，不能穿透
            case UIFormLucenyType.Translucence:
                //Debug.Log("半透明");
                _GoMaskPanel.SetActive(true);
                Color newColor2 = new Color(SysDefine.SYS_UIMASK_TRANSLUCENCE_COLOR_REB,
                    SysDefine.SYS_UIMASK_TRANSLUCENCE_COLOR_REB, SysDefine.SYS_UIMASK_TRANSLUCENCE_COLOR_REB, 
                    SysDefine.SYS_UIMASK_LUCENCY_COLOR_REB_A);
                _GoMaskPanel.GetComponent<Image>().color = newColor2;
                break;
                // 低透明，不能穿透
            case UIFormLucenyType.ImPenetrable:
                //Debug.Log("低透明");
                _GoMaskPanel.SetActive(true);
                Color newColor3 = new Color(SysDefine.SYS_UIMASK_IMPENETRABLE_COLOR_REB,
                    SysDefine.SYS_UIMASK_IMPENETRABLE_COLOR_REB, SysDefine.SYS_UIMASK_IMPENETRABLE_COLOR_REB, 
                    SysDefine.SYS_UIMASK_IMPENETRABLE_COLOR_REB_A);
                _GoMaskPanel.GetComponent<Image>().color = newColor3;
                break;
                // 可以穿透
            case UIFormLucenyType.Pentrate:
                //Debug.Log("可以穿透");
                if (_GoMaskPanel.activeInHierarchy)
                {
                    _GoMaskPanel.SetActive(false);
                }
                break;
            default:
                break;
        }

        // 遮罩窗体下移
        _GoMaskPanel.transform.SetAsLastSibling();
        // 显示窗体下移
        goDisplayUIForm.transform.SetAsLastSibling();

        // 增加当前 UI 摄像机的层深（保证当前涉嫌你挂机为最前显示）
        if (_UICamera != null)
        {
            _UICamera.depth = _UICamera.depth + 100; // 增加层深
        }
    }

    /// <summary>
    /// 恢复遮罩状态
    /// </summary>
    public void CancelMaskWIndow() {
        // 顶层窗体上移
        _GoTopPanel.transform.SetAsFirstSibling();

        // 禁用遮罩窗体
        if (_GoMaskPanel.activeInHierarchy)
        {
            _GoMaskPanel.SetActive(false);
        }


        // 恢复当前 UI 摄像机的层深
        if (_UICamera != null)
        {
            _UICamera.depth = _OriginalUICameraDepth; // 恢复层深
        }
    }
}
