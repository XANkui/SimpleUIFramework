using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCityUIForm : BaseUIForm
{
    private void Awake()
    {
        // 窗体性质
        CurrentUIType.UIForm_ShowType = UIFormSHowType.HideOther;

        // 事件注册
        RegisterButtonObjectEvent("MallButton",
            p => {
                OpenUIForm("MallUIForm");
            }
            );
    }
}
