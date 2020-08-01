using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJsonPeopleInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 获取json 数据
        string strPeopleInfo = Resources.Load<TextAsset>(@"TestJson/PeopleInfo").text;
        Debug.Log("json strPeopleInfo =="+ strPeopleInfo);
        // 反序列化 json 数据
        PeopleInfo peopleInfo = JsonUtility.FromJson<PeopleInfo>(strPeopleInfo);
        foreach (People item in peopleInfo.People)
        {
            Debug.Log(string.Format("Name == {0}, Age == {1}",item.Name,item.Age));
        }

    }

  
}
