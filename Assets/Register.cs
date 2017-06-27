using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml.Serialization;
using System;

public class Register : MonoBehaviour {
    
    #region UI object
    [SerializeField]
    private InputField IdField;
    [SerializeField]
    private Button Summit;

    [SerializeField]
    private GameObject Main;
    [SerializeField]
    private GameObject Popup;
    [SerializeField]
    private Text PopupText;
    [SerializeField]
    private Button OK;
#endregion

    Memberinfo memberinfos;
    const string URL = "http://www.huntermusicthailand.com";

    [SerializeField]
    private GameObject loadingPopup;

    void Start () {
#if UNITY_EDITOR
        //Memberinfo.SetInstance(null);
        Memberinfo.ClearInstance();
#endif

        if (Memberinfo.Instance == null)
        {
            Main.SetActive(true);
            IdField.ActivateInputField();
            Summit.onClick.AddListener(() =>
            {
                if (string.IsNullOrEmpty(IdField.text))
                {
                    ShowPopup("กรุณาใส่ ID ของคุณ");
                }
                else
                {
                    Debug.Log("ok");
                    StartCoroutine(DownloadARData(IdField.text));
                }
            });
        }
        else
        {
            SceneManager.LoadScene("AR");
        }
	}
    void ShowPopup(string text)
    {
        loadingPopup.SetActive(false);
        PopupText.text = text;
        Popup.SetActive(true);
    }
    IEnumerator DownloadARData(string id)
    {
        WWW www = new WWW(URL + "/" + id + "/ardata.xml");
        yield return www;
        Debug.Log(www.text + www.error);
        if (string.IsNullOrEmpty(www.error))
        {
            loadingPopup.SetActive(true);
            XmlSerializer serializer = new XmlSerializer(typeof(Memberinfo));
            //memberinfos
            StringReader reader = new StringReader(www.text);
            memberinfos = serializer.Deserialize(reader) as Memberinfo;
            StartCoroutine(DownloadMarker(memberinfos.Member));
        }
        else
        {
            IdField.text = string.Empty;
            ShowPopup("ไม่มี ID นี้ กรุณาใส่ ID ใหม่");
            yield break;
        }
    }

    IEnumerator DownloadMarker(Member member)
    {
        string id = member.ID;
        string markerName = member.MarkerPath;
        WWW fset = new WWW(URL + "/" + id + "/Marker/" + markerName + ".fset");
        yield return fset;
        if (string.IsNullOrEmpty(fset.error))
        {
            WriteMarker(markerName + ".fset", fset.bytes);
        }
        else
        {
            IdField.text = string.Empty;
            ShowPopup("ไม่มี ID นี้ กรุณาใส่ ID ใหม่");
            yield break;
        }
        WWW fset3 = new WWW(URL + "/" + id + "/Marker/" + markerName + ".fset3");
        yield return fset3;
        if (string.IsNullOrEmpty(fset3.error))
        {
            WriteMarker(markerName + ".fset3", fset3.bytes);
        }
        else
        {
            IdField.text = string.Empty;
            ShowPopup("ไม่มี ID นี้ กรุณาใส่ ID ใหม่");
            yield break;
        }
        WWW iset = new WWW(URL + "/" + id + "/Marker/" + markerName + ".iset");
        yield return iset;
        if (string.IsNullOrEmpty(iset.error))
        {
            //WriteCache(markerName + ".iset", iset.bytes);
            WriteMarker(markerName + ".iset", iset.bytes);
        }
        else
        {
            IdField.text = string.Empty;
            ShowPopup("ไม่มี ID นี้ กรุณาใส่ ID ใหม่");
            yield break;
        }
        StartCoroutine(DownloadContent(member));
    }

    IEnumerator DownloadContent(Member member)
    {
        string id = member.ID;
        string contentName = member.ContentPath;
        WWW www = new WWW(URL + "/" + id + "/content/" + contentName);
        yield return www;
        Debug.Log("www download complete");
        if (string.IsNullOrEmpty(www.error))
        {
            WriteContent(id,contentName, www.bytes);
        }
        else
        {
            IdField.text = string.Empty;
            ShowPopup("ไม่มี ID นี้ กรุณาใส่ ID ใหม่");
            yield break;
        }
        Debug.Log("DownloadContent : " + id + "/" + contentName);
        string extension = Path.GetExtension(contentName).Substring(1);
        member.ContentType = (FileExtension)Enum.Parse(typeof(FileExtension), extension.ToLower());
        Memberinfo.SetInstance(member);
        SceneManager.LoadScene("AR");
    }

    void WriteContent(string id,string subPath,byte[] data)
    {
        string dir = Application.persistentDataPath + "/" + id;
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        File.WriteAllBytes(dir + "/" + subPath, data);
    }
    void WriteMarker(string fileName, byte[] data)
    {
        string dir = Application.persistentDataPath + "/Marker";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        File.WriteAllBytes(dir + "/" + fileName, data);
    }
    void WriteCache(string fileName, byte[] data)
    {
        string dir = Application.temporaryCachePath + "/" + fileName;
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        File.WriteAllBytes(dir + "/" + fileName, data);
    }

}
