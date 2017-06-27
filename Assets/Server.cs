using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Video;

public class Server : MonoBehaviour {
    [SerializeField]
    private VideoPlayer videoPlayer;
    Memberinfo memberinfos;
    const string URL = "http://www.huntermusicthailand.com";
    // Use this for initialization
    void Start () {
        //StartCoroutine()
        StartCoroutine(DownloadARData("1001"));
	}
	
    IEnumerator DownloadARData(string id)
    {
        WWW www = new WWW(URL +"/" + id + "/ardata.xml");
        yield return www;
        Debug.Log(www.text);
        XmlSerializer serializer = new XmlSerializer(typeof(Memberinfo));
        //memberinfos
        StringReader reader = new StringReader(www.text);
        memberinfos = serializer.Deserialize(reader) as Memberinfo;
        Debug.Log(memberinfos.Member.Name);
        StartCoroutine(DownloadContent(memberinfos.Member.ID, memberinfos.Member.ContentPath));
    }

    void DownloadMarker(string id)
    {

    }

    IEnumerator DownloadContent(string id,string fileName)
    {
        WWW www = new WWW(URL + "/" + id + "/content/" + fileName);
        yield return www;
        Debug.Log("www download complete");
        if (www.error == null)
        {
            string dir = Application.persistentDataPath + "/" + id;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllBytes(dir + "/" + fileName, www.bytes);
            Debug.Log("DownloadContent : " + dir + "/" + fileName);
            videoPlayer.source = VideoSource.Url;
            videoPlayer.url = dir + "/" + fileName;
            
            videoPlayer.Play();
        }
    }
}
