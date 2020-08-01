using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHeroUIForm : BaseUIForm
{
    private void Awake()
    {
        // 窗体性质
        CurrentUIType.UIForm_ShowType = UIFormSHowType.HideOther;

        // 注册事件
        RegisterButtonObjectEvent("Confirm_Button", EnterMainCityUIFrom);
        RegisterButtonObjectEvent("Back_Button", BackLoginUIForm);
    }

    // Start is called before the first frame update
    void Start()
    {
        // 显示 UI 管理器内部的状态
        //Debug.Log("所有窗体集合中的数量 = " +UIManager.GetInstance().ShowAllUIFormsCount());
        //Debug.Log("当前显示窗体集合中的数量 = " +UIManager.GetInstance().ShowCurrentUIFormsCount());
        //Debug.Log("栈中窗体集合中的数量 = " +UIManager.GetInstance().ShowStackUIFromsCount());
    }


    public void EnterMainCityUIFrom(GameObject go) {
        Debug.Log("进入主城UI");
    }

    public void BackLoginUIForm(GameObject go) {
        //UIManager.GetInstance().CloseUIForm(ProConst.SELECT_HERO_UIFORM);
        CloseUIForm();
    }
}
