using SUIFW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUIForm : BaseUIForm
{
    private void Awake()
    {
        // 定义本窗体的性质
        base.CurrentUIType.UIForms_TYpe = UIFormType.Normal;
        base.CurrentUIType.UIForm_ShowType = UIFormSHowType.Normal;
        base.CurrentUIType.UIForm_LucenyTYpe = UIFormLucenyType.Lucency;

        // 给按钮定义事件
        RegisterButtonObjectEvent("Login_Button", LoginSys);
    }

    public void LoginSys(GameObject go) {

        // 前台或者后台检查用户名称和密码

        //若果成功，则登陆下一个窗体
        //UIManager.GetInstance().ShowUIForms("SelectHeroUIForm");
        OpenUIForm(ProConst.SELECT_HERO_UIFORM);
    }
}
