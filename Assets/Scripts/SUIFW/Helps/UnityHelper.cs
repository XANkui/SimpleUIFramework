using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityHelper : MonoBehaviour
{

    /// <summary>
    /// 查找子节点对象
    /// </summary>
    /// <param name="goParent">父节点</param>
    /// <param name="childName">要查找子节点名称</param>
    /// <returns></returns>
    public static Transform FindTheChildNode(GameObject goParent, string childName) {

        Transform searchTrans = null;
        searchTrans = goParent.transform.Find(childName);
        if (searchTrans == null)
        {
            // 遍历递归查找子节点
            foreach (Transform trans in goParent.transform)
            {
                searchTrans = FindTheChildNode(trans.gameObject, childName);

                if (searchTrans != null)
                {
                    return searchTrans;
                }
            }
        }

        return searchTrans;
    }

    /// <summary>
    /// 获取对应子节点的指定脚本
    /// </summary>
    /// <typeparam name="T">脚本名称</typeparam>
    /// <param name="goParent">父节点</param>
    /// <param name="childName">子节点名称</param>
    /// <returns></returns>
    public static T GetTheChildNodeComponentScripts<T>(GameObject goParent, string childName)where T :Component{
        Transform searchTransdormNode = null;
        // 查找子节点
        searchTransdormNode = FindTheChildNode(goParent,childName);

        if (searchTransdormNode != null)
        {
            return searchTransdormNode.gameObject.GetComponent<T>();
        }
        else {
            return null;
        }
    }

    /// <summary>
    /// 给指定直接点添加组件（脚本）
    /// </summary>
    /// <typeparam name="T">组件（脚本）名称</typeparam>
    /// <param name="goParent">父节点</param>
    /// <param name="childName">子节点名称</param>
    /// <returns></returns>
    public static T AddChildNodeComponent<T>(GameObject goParent, string childName) where T : Component
    {

        Transform searchTransform = null;

        // 查找特定子节点
        searchTransform = FindTheChildNode(goParent, childName);

        // 若果查找成功，则考虑是否有相同脚本挂载了，有则删除，没有则直接添加
        if (searchTransform != null)
        {
            //若果已经挂载了相同的脚本，则先删除
            T[] componentArray = searchTransform.GetComponents<T>();
            for (int i = 0; i < componentArray.Length; i++)
            {
                if (componentArray[i] != null)
                {
                    Destroy(componentArray[i]);
                }
            }
            // 添加组件
            return searchTransform.gameObject.AddComponent<T>();
        }
        else {

            // 若果查找不成功，则返回 null
            return null;
        }
 
    }

    /// <summary>
    /// 给子节点添加父对象
    /// </summary>
    /// <param name="parentTr">父对象</param>
    /// <param name="childTr">子节点</param>
    public static void AddChildNodeToParentNode(Transform parentTr, Transform childTr) {

        childTr.SetParent(parentTr,false);
        childTr.localPosition = Vector3.zero;
        childTr.localScale = Vector3.one;
        childTr.localEulerAngles = Vector3.zero;
    }
}
