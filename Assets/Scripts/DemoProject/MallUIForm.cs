using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MallUIForm : BaseUIForm
{
    private void Awake()
    {
        // 窗体性质
        CurrentUIType.UIForms_TYpe = UIFormType.PopUp;
        CurrentUIType.UIForm_LucenyTYpe = UIFormLucenyType.Translucence;
        CurrentUIType.UIForm_ShowType = UIFormSHowType.ReverseChange;

        // 事件注册
        RegisterButtonObjectEvent("CloseButton",(go)=>{
            CloseUIForm();
        });

        // 注册道具事件：斧钺钩叉
        RegisterButtonObjectEvent("FuButton", (go) => {
            // 打开窗体
            OpenUIForm("PropDetailUIForm");
            // 传递消息
            //KeyValuesUpdate kv = new KeyValuesUpdate("Fu","斧的道具详情：");
            //MessageCenter.SendMessage("Props", kv);

            SendMessage("Props","Fu",new string[] { "斧的道具详情：" ,"斧的详细介绍。。。。。"});
        });
        RegisterButtonObjectEvent("YueButton", (go) => {
            // 打开窗体
            OpenUIForm("PropDetailUIForm");
            // 传递消息
            SendMessage("Props", "Yue", new string[] { "钺的道具详情：", "钺的详细介绍。。。。。" });
        });
        RegisterButtonObjectEvent("GouButton", (go) => {
            // 打开窗体
            OpenUIForm("PropDetailUIForm");
            // 传递消息
            SendMessage("Props", "Gou", new string[] { "钩的道具详情：", "钩的详细介绍。。。。。" });
        });
        RegisterButtonObjectEvent("ChaButton", (go) => {
            // 打开窗体
            OpenUIForm("PropDetailUIForm");
            // 传递消息
            SendMessage("Props", "Cha", new string[] { "叉的道具详情：", "叉的详细介绍。。。。。" });
        });
    }
}
