using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MallUIForm : BaseUIForm
{
    private void Awake()
    {
        // 窗体性质
        CurrentUIType.UIForms_TYpe = UIFormType.PopUp;
        CurrentUIType.UIForm_LucenyTYpe = UIFormLucenyType.Lucency;
        CurrentUIType.UIForm_ShowType = UIFormSHowType.ReverseChange;

        // 事件注册
        RegisterButtonObjectEvent("CloseButton",(go)=>{
            CloseUIForm();
        });
    }
}
