using SUIFW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUIForm : BaseUIForm
{
    public Text Title_Text;
    public Text Login_Button_Text;

    private void Awake()
    {
        Log.Write(GetType() + " Awake()/");
        Log.SyncLogCatchToFile();

        // 定义本窗体的性质
        base.CurrentUIType.UIForms_TYpe = UIFormType.Normal;
        base.CurrentUIType.UIForm_ShowType = UIFormSHowType.Normal;
        base.CurrentUIType.UIForm_LucenyTYpe = UIFormLucenyType.Lucency;

        // 给按钮定义事件
        RegisterButtonObjectEvent("Login_Button", LoginSys);
    }

    private void LoginSys(GameObject go) {

        // 前台或者后台检查用户名称和密码

        //若果成功，则登陆下一个窗体
        //UIManager.GetInstance().ShowUIForms("SelectHeroUIForm");
        OpenUIForm(ProConst.SELECT_HERO_UIFORM);
    }

    private void Start()
    {
        InitLanguageText();
    }

    /// <summary>
    /// 初始话界面语言
    /// </summary>
    void InitLanguageText() {
        if (Title_Text != null)
        {
            string strLoginSysText = ShowText("LogonSystem");
            Title_Text.text = strLoginSysText;
        }

        if (Login_Button_Text != null)
        {
            string strLoginText = ShowText("Logon");
            Login_Button_Text.text = strLoginText;
        }
    }
}
