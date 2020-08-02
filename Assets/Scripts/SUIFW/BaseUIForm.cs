using SUIFW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// Display 显示状态
/// Hiding 隐藏状态
/// Redisplay 再显示状态
/// Freeze 冻结状态
/// </summary>
public class BaseUIForm : MonoBehaviour
{

    // 字段
    private UIType _CurrentUIType = new UIType();
    // 属性
    public UIType CurrentUIType { get => _CurrentUIType; set => _CurrentUIType = value; }

    #region 窗体的四大状态


    /// <summary>
    /// 显示状态
    /// </summary>
    public virtual void Display() {
        this.gameObject.SetActive(true);

        // 必须是弹出窗口，才设置透明度
        if (_CurrentUIType.UIForms_TYpe == UIFormType.PopUp)
        {
            UIFormMaskMgr.GetInstance().SetMaskWindow(this.gameObject,_CurrentUIType.UIForm_LucenyTYpe);
        }
    }

    /// <summary>
    /// 隐藏状态
    /// </summary>
    public virtual void Hiding() {
        this.gameObject.SetActive(false);

        // 必须是弹出窗口，才设置透明度
        if (_CurrentUIType.UIForms_TYpe == UIFormType.PopUp)
        {
            UIFormMaskMgr.GetInstance().CancelMaskWIndow();
        }
    }

    /// <summary>
    /// 在显示状态
    /// </summary>
    public virtual void Redisplay() {
        this.gameObject.SetActive(true);

        // 必须是弹出窗口，才设置透明度
        if (_CurrentUIType.UIForms_TYpe == UIFormType.PopUp)
        {
            UIFormMaskMgr.GetInstance().SetMaskWindow(this.gameObject, _CurrentUIType.UIForm_LucenyTYpe);
        }
    }

    /// <summary>
    /// 冻结状态
    /// </summary>
    public virtual void Freeze() {
        this.gameObject.SetActive(true);
    }

    #endregion 窗体的四大状态

    #region 封装子类常用的方法

    /// <summary>
    /// 添加按钮事件
    /// </summary>
    /// <param name="buttonName">按钮节点名称</param>
    /// <param name="delHandler">委托：需要注册的委托方法</param>
    protected void RegisterButtonObjectEvent(string buttonName,EventTriggerListener.VoidDelegate delHandler) {
        Transform traLoginButton = UnityHelper.FindTheChildNode(this.gameObject, buttonName);
        if (traLoginButton != null)
        {
            EventTriggerListener.Get(traLoginButton.gameObject).onClick = delHandler;
        }
    }

    /// <summary>
    /// 打开UI窗体
    /// </summary>
    /// <param name="uiFormName"></param>
    protected void OpenUIForm(string uiFormName) {
        UIManager.GetInstance().ShowUIForms(uiFormName);
    }

    /// <summary>
    /// 关闭 当前UI窗体
    /// </summary>
    protected void CloseUIForm() {

        string strCurrentUIFormName = null;
        int position = -1;

        strCurrentUIFormName = GetType().ToString();        // 一般是：命名空间.类名 ；没有命名空间的话直接是 类名
        Debug.Log("strCurrentUIFormName "+ strCurrentUIFormName);
        position = strCurrentUIFormName.IndexOf('.');
        if (position != -1)
        {
            // 去掉命名空间，只要.后面的类名
            strCurrentUIFormName = strCurrentUIFormName.Substring(position+1);
        }

        UIManager.GetInstance().CloseUIForm(strCurrentUIFormName);
    }

    /// <summary>
    /// 消息发送方法
    /// </summary>
    /// <param name="msgType">消息类型</param>
    /// <param name="msgName">消息名称</param>
    /// <param name="msgContent">消息内容</param>
    protected void SendMessage(string msgType, string msgName, object  msgContent) {
        KeyValuesUpdate kvs = new KeyValuesUpdate(msgName, msgContent);
        MessageCenter.SendMessage(msgType, kvs);
    }

    /// <summary>
    /// 接收消息
    /// </summary>
    /// <param name="msgType">消息类型</param>
    /// <param name="handler">消息委托</param>
    protected void ReceiveMessage(string msgType, MessageCenter.DelMessageDelivery handler) {
        MessageCenter.AddMsgListener(msgType, handler);
    }

    /// <summary>
    /// 显示对应的界面语言文字
    /// </summary>
    /// <param name="languageID"></param>
    /// <returns></returns>
    protected string ShowText(string languageID) {
        string strQueryReslut = string.Empty;

        strQueryReslut = LanguageMgr.GetInstance().ShowText(languageID);

        return strQueryReslut;
    }

    #endregion
}
