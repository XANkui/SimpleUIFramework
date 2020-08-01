using SUIFW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 是整个UI框架的核心，用户通过本脚本，来实现框架绝大多数的功能实现
/// </summary>
public class UIManager : MonoBehaviour
{
    /*字段*/
    private static UIManager _Instance = null;
    // UI 窗体预设路径（参数1，窗体预设的名称， 2，表示窗体预设的路径）
    private Dictionary<string, string> _DicFormsPaths;

    // 缓存所有窗体
    private Dictionary<string, BaseUIForm> _DicAllUIForms;
    // 当前显示的UI窗体
    private Dictionary<string, BaseUIForm> _DicCurrentShowUIForms;
    // 定义“栈”集合（具备“反向切换”属性窗体的管理）
    private Stack<BaseUIForm> _StaCurrentUIForms;


    // UI 根节点
    private Transform _TraCanvasTransform = null;
    // 全屏幕显示的节点
    private Transform _TraNormal = null;
    // 固定显示的节点
    private Transform _TraFixed = null;
    // 弹出节点
    private Transform _TraPopUP = null;
    // UI管理脚本的节点
    private Transform _TraUIScripts = null;


    /// <summary>
    /// 得到实例
    /// </summary>
    /// <returns></returns>
    public static UIManager GetInstance (){
        if (_Instance ==null)
        {
            _Instance = new GameObject("_UIManager").AddComponent<UIManager>();
        }

        return _Instance;
    }

    // 初始化核心是数据，加载UI窗体路径到集合中
    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 初始化数据
    /// 功能：
    /// 字段初始化
    /// 初始化加载（根UI窗体）Canvas 预设
    /// 得到 UI 根节点、全屏节点、固定节点、弹出框框节点
    /// 把本脚本作为 “跟UI窗体”的子节点 _TraUIScripts
    /// 根UI窗体 在场景转换的时候，不允许销毁
    /// 初始化 UI 窗体预设 的路径数据
    /// </summary>
    public void Init() {
        // 字段初始化
        _DicAllUIForms = new Dictionary<string, BaseUIForm>();
        _DicCurrentShowUIForms = new Dictionary<string, BaseUIForm>();
        _DicFormsPaths = new Dictionary<string, string>();
        _StaCurrentUIForms = new Stack<BaseUIForm>();

        // 初始化加载（根UI窗体）Canvas 预设
        InitRootCanvasLoading();

        // 得到 UI 根节点、全屏节点、固定节点、弹出框框节点
        GetTra();

        // 把本脚本作为 “跟UI窗体”的子节点 _TraUIScripts
        this.transform.SetParent(_TraUIScripts, false);

        // 根UI窗体 在场景转换的时候，不允许销毁
        DontDestroyOnLoad(_TraCanvasTransform);

        // 初始化 UI 窗体预设 的路径数据
        if (_DicFormsPaths != null)
        {
            _DicFormsPaths.Add("LoginUIForm", @"UIPrefabs/LoginUIForm");
            _DicFormsPaths.Add("SelectHeroUIForm", @"UIPrefabs/SelectHeroUIForm");

        }
    }



    /// <summary>
    /// 显示（打开）UI 窗体
    /// 功能：
    /// 1、根据 UI 窗体的名称，加载到“所有UI窗体”到缓存集合中
    /// 2、根据不同的UI窗体显示模式，分别做不同的加载处理
    /// </summary>
    /// <param name="uiFormName"></param>
    public void ShowUIForms(string uiFormName) {

        BaseUIForm baseUIForm = null;

        // 参数检查
        if (string.IsNullOrEmpty(uiFormName))
        {
            return;
        }

       
        // 指定的 UI 窗体的名称，加载到 “所有UI窗体”
        baseUIForm = LoadFormsToAllUIFormsCache(uiFormName);
        if (baseUIForm ==null)
        {
            return;
        }

        // 是否清空“栈集合”中的数据
        if (baseUIForm.CurrentUIType.IsClearStack == true)
        {
            ClearStackArray();
        }
        

        // 根据不同的UI窗体的显示模式，分别做不同的加载处理
        switch (baseUIForm.CurrentUIType.UIForm_ShowType)
        {
            case UIFormSHowType.Normal:
                // 把窗体加载到 当前UI显示集合中
                LoadUIToCurrentUIForms(uiFormName);
                break;
            case UIFormSHowType.ReverseChange:
                // 把窗体入栈操作
                PushUIFormToStack(uiFormName);
                break;
            case UIFormSHowType.HideOther:
                EnterUIFormAndHideOther(uiFormName);
                break;
            default:
                break;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uiFormName"></param>
    public void CloseUIForm(string uiFormName) {

        BaseUIForm baseUIForm;

        //参数检查
        if (string.IsNullOrEmpty(uiFormName))
        {
            return;
        }

        // 所有UI窗体 集合中，如果没有记录，则直接返回
        _DicAllUIForms.TryGetValue(uiFormName, out baseUIForm);
        if (baseUIForm == null)
        {
            return;
        }
        // 根据窗体不同显示类型，分别做不同处理

        switch (baseUIForm.CurrentUIType.UIForm_ShowType)
        {
            case UIFormSHowType.Normal:
                // 普通窗体的关闭
                ExitUIForm(uiFormName);
                break;
            case UIFormSHowType.ReverseChange:
                // 反向切换窗体的关闭
                PopUIForm();
                break;
            case UIFormSHowType.HideOther:
                // 隐藏其他窗体关闭
                ExitUIFormAndDisplayOther(uiFormName);

                break;
            default:
                break;
        }
    }


    #region 显示 UI 管理器 内部核心数据，测试使用

    /// <summary>
    /// 显示所有窗体的集合数量
    /// </summary>
    /// <returns></returns>
    public int ShowAllUIFormsCount() {
        if (_DicAllUIForms != null)
        {
            return _DicAllUIForms.Count;
        }
        else {
            return 0;
        }
    }

    /// <summary>
    /// 显示当前窗体集合中的数量
    /// </summary>
    /// <returns></returns>
    public int ShowCurrentUIFormsCount() {
        if (_DicCurrentShowUIForms != null)
        {
            return _DicCurrentShowUIForms.Count;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// 显示栈集合中的窗体的数量
    /// </summary>
    /// <returns></returns>
    public int ShowStackUIFromsCount() {
        if (_StaCurrentUIForms != null)
        {
            return _StaCurrentUIForms.Count;
        }
        else
        {
            return 0;
        }
    }

    #endregion 显示 UI 管理器 内部核心数据，测试使用

    #region 私有方法

    /// <summary>
    /// 初始化加载（根UI窗体）Canvas 预设
    /// </summary>
    private void InitRootCanvasLoading()
    {
        ResourcesMgr.GetInstance().LoadAsset(SysDefine.SYS_PATH_CANVAS, false);
    }

    /// <summary>
    /// 得到 UI 根节点、全屏节点、固定节点、弹出框框节点
    /// </summary>
    private void GetTra()
    {
        _TraCanvasTransform = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_CANVAS).transform;       

        _TraNormal = UnityHelper.FindTheChildNode(_TraCanvasTransform.gameObject,SysDefine.SYS_NODE_NORMAL);
        _TraFixed = UnityHelper.FindTheChildNode(_TraCanvasTransform.gameObject, SysDefine.SYS_NODE_FIXED);
        _TraPopUP = UnityHelper.FindTheChildNode(_TraCanvasTransform.gameObject, SysDefine.SYS_NODE_POPUP);
        _TraUIScripts = UnityHelper.FindTheChildNode(_TraCanvasTransform.gameObject, SysDefine.SYS_NODE_SCRIPTSMANAGER);
    }

    /// <summary>
    /// 根据 UI 窗体的名称，加载到“所有UI窗体”到缓存集合中
    /// 功能：检查是否已经加载，没有加载就加载，否则直接返回加载过的
    /// </summary>
    /// <param name="uiFormName"></param>
    /// <returns></returns>
    private BaseUIForm LoadFormsToAllUIFormsCache(string uiFormName) {


        BaseUIForm baseUIResult = null;

        _DicAllUIForms.TryGetValue(uiFormName, out baseUIResult);

        if (baseUIResult ==null)
        {
            // 加载指定路径的 “UI 窗体”
            baseUIResult = LoadUIForm(uiFormName);
        }

        return baseUIResult;
    }

    /// <summary>
    /// 加载指定路径的 “UI 窗体”
    /// 功能
    /// 1、根据 UI窗体名称，加载预设克隆体
    /// 2、根据不同预设克隆体中带的脚本中不同的位置信息，加载到根窗体下不同的节点
    /// 3、隐藏刚创建的UI克隆体
    /// 4、把克隆体，加入到所有UI窗体缓存集合中
    /// </summary>
    /// <param name="uiFormName"></param>
    private BaseUIForm LoadUIForm(string uiFormName) {
        // UI 窗体路径
        string strUIFormPath = null;
        // 创建的UI克隆体预设
        GameObject goCloneUIPrefab = null;
        // 窗体基类
        BaseUIForm baseUIForm = null;

        // 根据 UI窗体名称，得到对应的加载路径
        _DicFormsPaths.TryGetValue(uiFormName,out strUIFormPath);

        // 根据 UI窗体名称，加载对应 预设克隆体
        if (string.IsNullOrEmpty(strUIFormPath)==false)
        {
            goCloneUIPrefab = ResourcesMgr.GetInstance().LoadAsset(strUIFormPath,false);
        }

        // 设置 UI克隆体的父节点（根据不同预设克隆体中带的脚本中不同的位置信息）
        if (_TraCanvasTransform != null && goCloneUIPrefab != null)
        {
            baseUIForm = goCloneUIPrefab.GetComponent<BaseUIForm>();
            if (baseUIForm == null)
            {
                Debug.Log("baseUIForm ==null，请确认窗体预设上是否挂载 BaseUIForm 的子类脚本，参数 uiFormName = " + uiFormName);

                return null;
            }

            switch (baseUIForm.CurrentUIType.UIForms_TYpe)
            {
                case UIFormType.Normal:         // 普通窗体节点
                    goCloneUIPrefab.transform.SetParent(_TraNormal, false);
                    break;
                case UIFormType.Fixed:          // 固定窗体节点
                    goCloneUIPrefab.transform.SetParent(_TraFixed, false);
                    break;
                case UIFormType.PopUp:          // 弹出窗体节点
                    goCloneUIPrefab.transform.SetParent(_TraPopUP, false);
                    break;
                default:
                    break;
            }

            // 设置克隆体隐藏
            goCloneUIPrefab.SetActive(false);

            // 把克隆体，加入到所有UI窗体的缓存集合中
            _DicAllUIForms.Add(uiFormName, baseUIForm);

            return baseUIForm;
        }
        else {
            Debug.Log("_TraCanvasTransform == null Or goCloneUIPrefab == null , Please Check, uiFormName = "+ uiFormName);
        }


        Debug.Log("不可预知的错误 , Please Check, uiFormName = " + uiFormName);
        
        return null;
    }// Method_end

    /// <summary>
    /// 把窗体加载到 当前UI显示集合中
    /// </summary>
    /// <param name="uiFormName"></param>
    private void LoadUIToCurrentUIForms(string uiFormName) {
        BaseUIForm baseUIForm;                  // 窗体基类
        BaseUIForm baseUIFormFromAllCache;      // 从“所有窗体集合”中得到的窗体

        // 如果正在显示的集合中，存在这个UI窗体，则直接返回
        _DicCurrentShowUIForms.TryGetValue(uiFormName,out baseUIForm);
        if (baseUIForm !=null)
        {
            return;
        }

        // 把当前窗体，加载到正在显示集合中
        _DicAllUIForms.TryGetValue(uiFormName,out baseUIFormFromAllCache);
        if (baseUIFormFromAllCache != null)
        {
            _DicCurrentShowUIForms.Add(uiFormName, baseUIFormFromAllCache);
            baseUIFormFromAllCache.Display();       //显示当前窗体
        }
    }

    /// <summary>
    /// UI 窗体入栈
    /// </summary>
    /// <param name="uiFormName"></param>
    private void PushUIFormToStack(string uiFormName) {

        BaseUIForm baseUIForm;

        // 判断 “栈”集合中，是否有其他窗体，有则冻结处理
        if (_StaCurrentUIForms.Count > 0)
        {
            BaseUIForm topUIForm = _StaCurrentUIForms.Peek();
            // 栈顶元素做冻结处理
            topUIForm.Freeze();
        }

        // 判断 UI 所有窗体 集合中，是否有其他的窗体，有则处理
        _DicAllUIForms.TryGetValue(uiFormName, out baseUIForm);
        if (baseUIForm != null)
        {
            // 当前窗体显示状态
            baseUIForm.Display();
            //把指定的UI窗体，入栈操作
            _StaCurrentUIForms.Push(baseUIForm);
        }
        else {
            Debug.Log("baseUIForm == null, Please Check, uiFormName = "+ uiFormName);
        }


        
    }

    /// <summary>
    /// 退出指定窗体
    /// </summary>
    /// <param name="uiFormName"></param>
    private void ExitUIForm(string uiFormName) {

        BaseUIForm baseUIForm;

        // 正在显示集合中，如果没有记录，直接返回
        _DicCurrentShowUIForms.TryGetValue(uiFormName,out baseUIForm);
        if (baseUIForm ==null)
        {
            return;
        }

        // 指定窗体，笔记为 隐藏状态，且从正在显示集合中移除
        baseUIForm.Hiding();
        _DicCurrentShowUIForms.Remove(uiFormName);
    }

    /// <summary>
    /// 反向切换属性窗体的出栈逻辑
    /// </summary>    
    private void PopUIForm() {

        if (_StaCurrentUIForms.Count>=2)
        {
            // 出栈处理
            BaseUIForm topUIForm = _StaCurrentUIForms.Pop();

            // 做隐藏处理
            topUIForm.Hiding();

            // 下一个窗体左重新显示处理
            BaseUIForm nextUIForm = _StaCurrentUIForms.Peek();
            nextUIForm.Redisplay();
        }
        else if (_StaCurrentUIForms.Count == 1)
        {
            // 出栈处理
            BaseUIForm topUIForm = _StaCurrentUIForms.Pop();

            // 做隐藏处理
            topUIForm.Hiding();
        }
    }

    /// <summary>
    /// (属于隐藏其他)打开窗体，且隐藏其他窗体
    /// </summary>
    /// <param name="uiFormName"></param>
    private void EnterUIFormAndHideOther(string uiFormName) {
        BaseUIForm baseUIForm;              // UI窗体基类
        BaseUIForm baseUIFormFrommAll;      // 从集合中得到的UI窗体基类

        // 参数检查
        if (string.IsNullOrEmpty(uiFormName)==true)
        {
            return;
        }

        _DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUIForm);
        if (baseUIForm !=null)
        {
            return;
        }

        // 把 “正在显示集合”与 栈集合中所有窗体都隐藏
        foreach (BaseUIForm baseUI in _DicCurrentShowUIForms.Values)
        {
            baseUI.Hiding();
        }

        foreach (BaseUIForm staUI in _StaCurrentUIForms)
        {
            staUI.Hiding();
        }


        // 把当前窗体加入到 “正在显示窗体”集合中，且做显示处理
        _DicAllUIForms.TryGetValue(uiFormName,out baseUIFormFrommAll);
        if (baseUIFormFrommAll !=null)
        {
            _DicCurrentShowUIForms.Add(uiFormName, baseUIFormFrommAll);
            // 窗体显示
            baseUIFormFrommAll.Display();
        }
    }

    /// <summary>
    /// (属于隐藏其他)关闭窗体，且显示其他窗体
    /// </summary>
    /// <param name="uiFormName"></param>
    private void ExitUIFormAndDisplayOther(string uiFormName)
    {
        BaseUIForm baseUIForm;              // UI窗体基类

        // 参数检查
        if (string.IsNullOrEmpty(uiFormName) == true)
        {
            return;
        }

        _DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUIForm);
        if (baseUIForm == null)
        {
            return;
        }

        // 当前窗体隐藏状态，且从 正在显示 集合中移除本窗体
        baseUIForm.Hiding();
        _DicCurrentShowUIForms.Remove(uiFormName);

        // 把 “正在显示集合”与 栈集合 中所有窗体都定义重新显示状态
        foreach (BaseUIForm baseUI in _DicCurrentShowUIForms.Values)
        {
            baseUI.Redisplay();
        }

        foreach (BaseUIForm staUI in _StaCurrentUIForms)
        {
            staUI.Redisplay();
        }

    }

    /// <summary>
    /// 是否清空 “栈集合”中的数据
    /// </summary>
    /// <returns></returns>
    private bool ClearStackArray() {
        if (_StaCurrentUIForms !=null && _StaCurrentUIForms.Count>=1)
        {
            // 清空栈集合
            _StaCurrentUIForms.Clear();
            return true;
        }

        return false;
    }

    #endregion
}//Class_end
