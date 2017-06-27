using UnityEngine;
using System.Collections;
using System;
//This is needed to import iOS functions
using System.Runtime.InteropServices;
public class UnityIOSBridge : MonoBehaviour
{
    /*
    * Provide function decalaration of the functions defined in iOS
    * and need to be called here.
    */
    [DllImport("__Internal")]
    extern static public void messageFromUnity(string message);
    //Sends message to iOS
    static void SendMessageToIOS()
    {
        messageFromUnity("Hello iOS!");
    }
}