using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
public class iOSPluginTest : MonoBehaviour
{
#if UNITY_IPHONE 
  [DllImport("__Internal")]
 extern static private void _log(string message);
#endif
    #region Methods
    void OnGUI()
    {
        if (GUI.Button(new Rect(100, 300, 200, 100), "Log Message"))
        {
#if UNITY_IPHONE
     _log (string.Format ("{0}", "Sample Message"));
#endif
        }

    }
    #endregion
}