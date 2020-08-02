using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 道具详细窗体
/// </summary>
public class PropDetailUIForm : BaseUIForm
{
    public Text NameText; // 道具主题
    public Text DetailInfoText; // 道具详细信息

    private void Awake()
    {
        // 窗体信息
        CurrentUIType.UIForms_TYpe = UIFormType.PopUp;
        CurrentUIType.UIForm_ShowType = UIFormSHowType.ReverseChange;
        CurrentUIType.UIForm_LucenyTYpe = UIFormLucenyType.ImPenetrable;

        /*注册按钮基监听*/
        RegisterButtonObjectEvent("CloseButton",(p)=>{
            CloseUIForm();
        });

        // 注册消息监听
        ReceiveMessage("Props", (message) => {

            if (NameText != null)
            {
                string[] strArray = (string[])message.Value;
                NameText.text = strArray[0];
                DetailInfoText.text = strArray[1];
            }
        });
    }
}
