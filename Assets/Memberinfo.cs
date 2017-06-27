   /* 
    Licensed under the Apache License, Version 2.0
    
    http://www.apache.org/licenses/LICENSE-2.0
    */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Collections;

public enum FileExtension
{
    mp4,
    jpg,
    jpeg,
    png
}
[XmlRoot(ElementName = "member")]
    public class Member
    {
        [XmlAttribute(AttributeName = "ID")]
        public string ID { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "MarkerPath")]
        public string MarkerPath { get; set; }
        [XmlAttribute(AttributeName = "Tel")]
        public string Tel { get; set; }
        [XmlAttribute(AttributeName = "URL")]
        public string URL { get; set; }
        [XmlAttribute(AttributeName = "ContentPath")]
        public string ContentPath { get; set; }

    //Added Info
    public FileExtension ContentType { get; set; }
}

[XmlRoot(ElementName = "memberinfo")]
public class Memberinfo
{
    const string URL = "http://www.huntermusicthailand.com";
    private static Member instance;

    public static Member Instance
    {
        get
        {
            string data = PlayerPrefs.GetString("member");
            if (instance == null && !string.IsNullOrEmpty(data))
            {
                Debug.Log(data);
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Member));
                    StringReader textReader = new StringReader(data);
                    instance = (Member)xmlSerializer.Deserialize((textReader));
                    textReader.Close();
                }
                catch (Exception e) {
                    Debug.Log(e.ToString());
                }
            }
            return instance;
        }
    }
    public static void SetInstance(Member member)
    {
        try
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Member));
            StringWriter textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, member);
            PlayerPrefs.SetString("member", textWriter.ToString());
            instance = member;
            textWriter.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public static void ClearInstance()
    {
        string MarkerPath = Instance.MarkerPath;
        string contentPath = Instance.ID + "/" + Instance.ContentPath;
        instance = null;
        PlayerPrefs.DeleteAll();
        string markerDir = Application.persistentDataPath + "/Marker/";
        File.Delete(markerDir + MarkerPath + ".fset");
        File.Delete(markerDir + MarkerPath + ".fset3");
        File.Delete(markerDir + MarkerPath + ".iset");
        string contentDir = Application.persistentDataPath + "/content/";
        File.Delete(markerDir + contentPath);
    }
    
    [XmlElement(ElementName = "member")]
    public Member Member { get; set; }




   public static IEnumerator DownloadARData(string id, Action<string> error, Action complete)
    {
        WWW www = new WWW(URL + "/" + id + "/ardata.xml");
        yield return www;
        Debug.Log(www.text + www.error);
        if (string.IsNullOrEmpty(www.error))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Memberinfo));
            //memberinfos
            StringReader reader = new StringReader(www.text);
            Memberinfo memberinfos = serializer.Deserialize(reader) as Memberinfo;
            //StartCoroutine(DownloadMarker(memberinfos.Member));

            
            string markerName = memberinfos.Member.MarkerPath;
            WWW fset = new WWW(URL + "/" + id + "/Marker/" + markerName + ".fset");
            yield return fset;
            if (string.IsNullOrEmpty(fset.error))
            {
                WriteMarker(markerName + ".fset", fset.bytes);
            }
            else
            {
                error("ไม่มี ID นี้ กรุณาใส่ ID ใหม่");
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
                error("ไม่มี ID นี้ กรุณาใส่ ID ใหม่");
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
                error("ไม่มี ID นี้ กรุณาใส่ ID ใหม่");
                yield break;
            }


            string contentName = memberinfos.Member.ContentPath;
            WWW contentWWW = new WWW(URL + "/" + id + "/content/" + contentName);
            yield return contentWWW;
            Debug.Log("www download complete");
            if (string.IsNullOrEmpty(contentWWW.error))
            {
                WriteContent(id, contentName, contentWWW.bytes);
            }
            else
            {
                error("ไม่มี ID นี้ กรุณาใส่ ID ใหม่");
                yield break;
            }
            Debug.Log("DownloadContent : " + id + "/" + contentName);
            string extension = Path.GetExtension(contentName).Substring(1);
            memberinfos.Member.ContentType = (FileExtension)Enum.Parse(typeof(FileExtension), extension);
            Memberinfo.SetInstance(memberinfos.Member);
        }
        else
        {
            error("ไม่มี ID นี้ กรุณาใส่ ID ใหม่");
            yield break;
        }

        complete();
    }
    
    static void WriteContent(string id, string subPath, byte[] data)
    {
        string dir = Application.persistentDataPath + "/" + id;
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        File.WriteAllBytes(dir + "/" + subPath, data);
    }
    static void WriteMarker(string fileName, byte[] data)
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