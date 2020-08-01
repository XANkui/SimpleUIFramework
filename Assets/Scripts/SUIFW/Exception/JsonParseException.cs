using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// json 解析异常
/// 功能：
/// 专门解析json 路径错误或者格式错误引发的异常
/// </summary>
public class JsonParseException : Exception
{
    public JsonParseException() : base() { }

    public JsonParseException(string exceptionMessage) : base(exceptionMessage) { }
}
