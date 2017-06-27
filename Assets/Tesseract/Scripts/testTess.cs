using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class testTess : MonoBehaviour
{
    [SerializeField]
    private ARController arController;
    [SerializeField]
    private ARCamera arCamera;
    private Camera arCameraNumber;
    [SerializeField]
    private ARTrackedObject arTrackedObject;
    [SerializeField]
    private ARMarker arMarker;
    [SerializeField]
    private Transform cropLU;
    [SerializeField]
    private Transform cropLD;
    [SerializeField]
    private Transform cropRU;
    [SerializeField]
    private Transform cropRD;
    //[SerializeField]
    //private Mesh mesh;
    [SerializeField]
    private GameObject contentQuad;
    [SerializeField]
    private MeshRenderer contentMesh;
    private AndroidJavaObject tesseract;
    string tessdataPath;
    string tessdataDir;
    string tessdataFileName = "/eng.traineddata";

    [SerializeField]
    private VideoPlayer videoPlayer;

    private Coroutine showContent;

    [SerializeField]
    private Text messageText;
    [SerializeField]
    private GameObject PopupMessage;
    //[SerializeField]
    //private GameObject PopupMenu;

    // Use this for initialization
    void Start()
    {
        //Debug.Log(Application.temporaryCachePath);
        //Debug.Log("NFTWidth : " + arMarker.NFTWidth);

#if UNITY_ANDROID && !UNITY_EDITOR
        tessdataDir = Application.persistentDataPath + "/tessdata";
        Debug.Log("File : " + tessdataDir + tessdataFileName);
        Debug.Log("Check : " + !Directory.Exists(tessdataDir) + " : " + !File.Exists(tessdataDir + "/" + tessdataFileName));
        if (!File.Exists(tessdataDir + "/" + tessdataFileName))
        {
            Debug.Log("data not Exists : " + tessdataDir + tessdataFileName);
            StartCoroutine(LoadData());
        }
        else
        {
            Debug.Log("data Exists");
            InitApi();
        }
#endif

        arCameraNumber = arCamera.GetComponent<Camera>();

        //Debug.Log(CheckMarkerFromString("001-O01-0001"));
    }

    public void UpdateContent()
    {
        StartCoroutine(Memberinfo.DownloadARData(Memberinfo.Instance.ID,(string e)=> {
            messageText.text = "ไม่สามารถอัพเดทข้อมูลได้";
            PopupMessage.SetActive(true);
        }, () => {
            SceneManager.LoadScene("AR");
        }
        ));
    }

    public void BackToRegister()
    {
        //Memberinfo.SetInstance(null);
        Memberinfo.ClearInstance();
        SceneManager.LoadScene("Register");
    }

    IEnumerator LoadData()
    {
        //WWW loadTessData = new WWW("jar:file://" + tessdataPath + tessdataFileName);
        string path = Application.streamingAssetsPath + tessdataFileName;
        Debug.Log("Path : " + path);
        WWW loadTessData = new WWW(path);
        yield return loadTessData;

        if (!Directory.Exists(tessdataDir))
        {
            Directory.CreateDirectory(tessdataDir);
        }

        if (string.IsNullOrEmpty(loadTessData.error))
        {
            File.WriteAllBytes(tessdataDir + tessdataFileName, loadTessData.bytes);
            InitApi();
        }
        else
        {
            Debug.Log("Fail");
        }
    }

    void LoadContent()
    {
        Member member = Memberinfo.Instance;
        Vector2 size = Vector2.zero;
        string contentPath = Application.persistentDataPath + "/" + member.ID + "/" + member.ContentPath;
        Debug.Log(contentPath);
        Debug.Log(Application.streamingAssetsPath);
        switch (Memberinfo.Instance.ContentType)
        {
            case FileExtension.jpg:
                if (File.Exists(contentPath))
                {
                    //WWW www = new WWW("file:///" + contentPath);
                    //WWW www = new WWW("jar:file:///" + contentPath);

                    Texture2D tex = new Texture2D(2, 2);
                    tex.LoadImage(File.ReadAllBytes(contentPath));
                    tex.Apply();
                    contentQuad.GetComponent<MeshRenderer>().material.mainTexture = tex;
                    size = new Vector2(tex.width, tex.height);
                }
                else
                {
                    Debug.Log("File not exist");
                    BackToRegister();
                }
                break;
            case FileExtension.jpeg:
                goto case FileExtension.jpg;
            case FileExtension.png:
                goto case FileExtension.jpg;
            case FileExtension.mp4:
                videoPlayer.url = "file://" + contentPath;
                videoPlayer.prepareCompleted += SetContectQuad;
                break;
        }
        arMarker.NFTDataName = Memberinfo.Instance.MarkerPath;
        arMarker.enabled = true;
        arTrackedObject.enabled = true;
        arCamera.enabled = true;
        arController.enabled = true;
        Debug.Log("arMarker : " + arMarker.NFTDataName + " : " + arMarker.NFTWidth + " , " + arMarker.NFTHeight + " : " + arMarker.NFTScale);
        contentQuad.transform.localPosition = new Vector3(arMarker.NFTWidth / 2, arMarker.NFTHeight / 2, 0);
        float imgHeight = arMarker.NFTWidth * ((float)size.y / (float)size.x);
        contentQuad.transform.localScale = new Vector3(arMarker.NFTWidth, imgHeight, 1);
    }

    void SetContectQuad(VideoPlayer vp)
    {
        contentQuad.transform.localPosition = new Vector3(arMarker.NFTWidth / 2, arMarker.NFTHeight / 2, 0);
        float imgHeight = arMarker.NFTWidth * ((float)vp.texture.height / (float)vp.texture.width);
        contentQuad.transform.localScale = new Vector3(arMarker.NFTWidth, imgHeight, 1);
    }

    void InitApi()
    {
        tesseract = new AndroidJavaObject("com.googlecode.tesseract.android.TessBaseAPI");
        Debug.Log(tessdataDir + " : " + Directory.Exists(tessdataDir));
        bool testInit = tesseract.Call<bool>("init", Application.persistentDataPath, "eng");
        Debug.Log("init tesseract was successed : " + testInit);
        LoadContent();
    }

    bool SetImage()
    {
        bool isCurrentMarker = false;
        Vector3 pos0 = arCameraNumber.WorldToScreenPoint(cropLD.transform.position);
        Vector3 pos1 = arCameraNumber.WorldToScreenPoint(cropLU.transform.position);
        Vector3 pos2 = arCameraNumber.WorldToScreenPoint(cropRD.transform.position);
        Vector3 pos3 = arCameraNumber.WorldToScreenPoint(cropRU.transform.position);

        Vector3[] posList = new Vector3[] { pos0, pos1, pos2, pos3 };

        Debug.Log("pos0 : " + pos0);
        Debug.Log("pos1 : " + pos1);
        Debug.Log("pos2 : " + pos2);
        Debug.Log("pos3 : " + pos3);
        float xMin = float.MaxValue;
        float yMin = float.MaxValue;
        float xMax = float.MinValue;
        float yMax = float.MinValue;
        foreach (Vector3 v in posList)
        {
            if (v.x < xMin)
            {
                xMin = v.x;
            }
        }
        foreach (Vector3 v in posList)
        {
            if (v.x > xMax)
            {
                xMax = v.x;
            }
        }
        foreach (Vector3 v in posList)
        {
            if (v.y < yMin)
            {
                yMin = v.y;
            }
        }
        foreach (Vector3 v in posList)
        {
            if (v.y > yMax)
            {
                yMax = v.y;
            }
        }

        //Debug.Log("xMin : " + xMin + "yMin : " + yMin + "xMax : " + xMax + "yMax : " + yMax + "pos0 : " + pos0);
        Rect croppedRect = new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
        Debug.Log("cropped Rect : " + croppedRect);
        string fileName = arController.GetScreenShotPath(croppedRect,pos0,pos1,pos2,pos3);
        if (fileName != string.Empty)
        {
            AndroidJavaObject file = new AndroidJavaObject("java.io.File", Application.persistentDataPath, fileName);
            //AndroidJavaObject file = new AndroidJavaObject("java.io.File", Application.persistentDataPath, "sstest.jpg");
            //jo.Call("SetImage", Application.persistentDataPath + "/test.png");
            tesseract.Call("setImage", file);
            string result = tesseract.Call<string>("getUTF8Text");
            Debug.Log("result : " + result);
            if (CheckMarkerFromString(result))
            {
                isCurrentMarker = true;
            }
        }
        //if (!isCurrentMarker)
        //{
        //    messageText.text = "ไม่ตรงกับ ID ของคุณ";
        //    PopupMessage.SetActive(true);
        //}
        return isCurrentMarker;
    }

    bool CheckMarkerFromString(string result)
    {
        bool isCurrentMarker = false;
        if (result.Length == 12)
        {
            Debug.Log("12");
            int temp = 0;
            if (int.TryParse(result.Substring(0, 3), out temp) &&
                result[3] == '-' &&
                int.TryParse(result.Substring(4, 3), out temp) &&
                result[7] == '-' &&
                int.TryParse(result.Substring(8, 3), out temp) &&
                result == Memberinfo.Instance.ID)
            {
                isCurrentMarker = true;
            }
        }
        return isCurrentMarker;
    }

    //public void testPo()
    //{
    //    Debug.Log(PointInTriangle(new Vector2(0f,2f), new Vector2(0,0), new Vector2(2,0), new Vector2(2,2)));

    //    Debug.Log(mesh.vertices[0].ToString() + mesh.vertexCount + " : "+ Input.mousePosition);
    //    //Debug.Log(Camera.main.WorldToScreenPoint( cropTran.TransformPoint(mesh.vertices[0])));
    //}

    void OnGUI()
    {
        //Vector3 pos0 = arCameraNumber.WorldToScreenPoint(cropLU.transform.position);
        //Vector3 pos1 = arCameraNumber.WorldToScreenPoint(cropLD.transform.position);
        //Vector3 pos2 = arCameraNumber.WorldToScreenPoint(cropRU.transform.position);
        //Vector3 pos3 = arCameraNumber.WorldToScreenPoint(cropRD.transform.position);
        //GUI.TextArea(new Rect(pos0.x, Screen.height - pos0.y, 20, 20), "0");
        //GUI.TextArea(new Rect(pos1.x, Screen.height - pos1.y, 20, 20), "1");
        //GUI.TextArea(new Rect(pos2.x, Screen.height - pos2.y, 20, 20), "2");
        //GUI.TextArea(new Rect(pos3.x, Screen.height - pos3.y, 20, 20), "3");
        
        //string pos = GUI.TextArea(new Rect(0, 0, 150, 50), arCamera.smoothPosition.ToString());
        //arCamera.smoothPosition = float.Parse(pos);
        //string rot = GUI.TextArea(new Rect(0, 100, 150, 50), arCamera.smoothRotate.ToString());
        //arCamera.smoothRotate = float.Parse(rot);
    }

    

    public void OnMarkerFound(ARMarker marker)
    {
        Debug.Log("OnMarkerFound : " + marker.name);
        if (CheckMarkerFromString(Memberinfo.Instance.ID))
        {
            showContent = StartCoroutine(ShowContent());
        }
        else
        {
            SetContent();
        }
    }

    public void OnMarkerLost(ARMarker marker)
    {
        Debug.Log("OnMarkerLost : " + marker.name);
        if(showContent!= null)
        {
            StopCoroutine(showContent);
        }
        HideContent();
    }



    private IEnumerator ShowContent()
    {
        yield return new WaitForSeconds(1);
        if (SetImage())
        {
            SetContent();
        }
    }

    private void SetContent()
    {
        contentMesh.enabled = true;
        switch (Memberinfo.Instance.ContentType)
        {
            case FileExtension.mp4:
                videoPlayer.Play();
                break;
        }
    }

    private void HideContent()
    {
        //arCamera.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        arCamera.ClearSmooth();
        contentMesh.enabled = false;
        switch (Memberinfo.Instance.ContentType)
        {
            case FileExtension.mp4:
                videoPlayer.Pause();
                break;
        }
    }
}
