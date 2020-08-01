using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJsonHeroLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        // 测试序列化
        Hero heroObj = new Hero() { Name ="张三",HeroLevel=new Level() { level=100} };
        string strHeroInfo = JsonUtility.ToJson(heroObj);
        Debug.Log("序列化结果：strHeroInfo " + strHeroInfo);

        // 反序列化
        Hero heroStructInfo = JsonUtility.FromJson<Hero>(strHeroInfo);
        Debug.Log("反序列化的结果： Name == "+heroStructInfo.Name +" HeroLevel == "+ heroStructInfo.HeroLevel.level);

        // 测试覆盖反序列化
        Hero hero2Obj = new Hero() { Name ="王五",HeroLevel=new Level() { level = 89} };
        string strHero2Info = JsonUtility.ToJson(hero2Obj);
        Debug.Log("未覆盖反序列化的结果： Name == " + heroObj.Name + " HeroLevel == " + heroObj.HeroLevel.level);
        //覆盖 heroObj 反序列化
        JsonUtility.FromJsonOverwrite(strHero2Info, heroObj);
        Debug.Log("测试覆盖反序列化的结果： Name == " + heroObj.Name + " HeroLevel == " + heroObj.HeroLevel.level);
    }
    


}
