using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCrop : MonoBehaviour {
    public MeshRenderer meshTest;
	// Use this for initialization
	void Start () {
        //Rect croppedNumber = new Rect(0, 70, 419, 68);
        //Vector2 pos0 = new Vector2(22, 70);
        //Vector2 pos1 = new Vector2(22, 200);
        //Vector2 pos2 = new Vector2(300, 70);
        //Vector2 pos3 = new Vector2(300, 200);

        Rect croppedNumber = new Rect(18, 303, 419, 68);
        Vector2 pos0 = new Vector2(22, 303);
        Vector2 pos1 = new Vector2(22, 361);
        Vector2 pos2 = new Vector2(437, 318);
        Vector2 pos3 = new Vector2(436, 372);

        //Rect croppedNumber = new Rect(0, 73, 218, 29);
        //Vector2 pos0 = new Vector2(0, 73);
        //Vector2 pos1 = new Vector2(0, 100);
        //Vector2 pos2 = new Vector2(218, 73);
        //Vector2 pos3 = new Vector2(218, 100);


        Debug.Log("Point : " + PointInTriangle(new Vector2(18, 303), pos0, pos1, pos2));

        int texW = 240;
        int texH = 320;


        Debug.Log(pos0 + " : " + pos1 + " : " + pos2 + " : " + pos3);

        Vector2 cropedPixelPos = ScreenToPixelPos(croppedNumber.x, croppedNumber.y, texW, texH);
        Vector2 pixel0 = ScreenToPixelPos(pos0.x - croppedNumber.x, pos0.y - croppedNumber.y, texW, texH);
        Vector2 pixel1 = ScreenToPixelPos(pos1.x - croppedNumber.x, pos1.y - croppedNumber.y, texW, texH);
        Vector2 pixel2 = ScreenToPixelPos(pos2.x - croppedNumber.x, pos2.y - croppedNumber.y, texW, texH);
        Vector2 pixel3 = ScreenToPixelPos(pos3.x - croppedNumber.x, pos3.y - croppedNumber.y, texW, texH);

        Debug.Log("Pixel 0 : " + pixel0 + " 1 : " + pixel1 + " 2 : " + pixel2 + " 3 : " + pixel3 + " w : " +texW + " h : " + texH);
        Debug.Log("Pixel : " + PointInTriangle(new Vector2(9, 111), pixel0, pixel1, pixel2));
        int cropedTexWidth = (int)((croppedNumber.width * texW) / Screen.width);
        int cropedTexHeight = (int)((cropedTexWidth * croppedNumber.height) / croppedNumber.width);
        Texture2D croppedTex = new Texture2D(cropedTexWidth, cropedTexHeight);
        Color maskCol = Color.red;
        for (int i = 0; i < cropedTexWidth; i++)
        {
            for (int j = 0; j < cropedTexHeight; j++)
            {
                if (!(PointInTriangle(new Vector2(i, j), pixel0, pixel1, pixel2) || PointInTriangle(new Vector2(i, j), pixel1, pixel2, pixel3)))
                //if (!(PointInTriangle(new Vector2(i, j), pixel0, pixel1, pixel2)))
                {
                    croppedTex.SetPixel(i, j, maskCol);
                }
            }
        }
        croppedTex.Apply();
        meshTest.material.mainTexture = croppedTex;
    }

    Vector2 ScreenToPixelPos(float posX, float posY, int texWidth, int texHeight)
    {
        int screenWidth = 480;
        int screenHeight = 800;

        int cropedTexX = (int)((posX * texWidth) / screenWidth);
        //cropedTexX = Mathf.Clamp(cropedTexX, 0, texWidth);
        int cropedTexY = (int)(((posY) * texHeight) / ((screenWidth * texHeight) / texWidth));
        int d = (int)((((float)(texWidth * screenHeight) / (float)screenWidth) - texHeight) / 2f);
        //Debug.Log("screenWidth : " + screenWidth + " screenHeight : " + screenHeight + " texWidth : " + texWidth + " texHeight : " + texHeight + " d : "+ d);
        //cropedTexY -= d;
        //cropedTexY = Mathf.Clamp(cropedTexY, 0, texHeight);
        return new Vector2(cropedTexX, cropedTexY);
    }

    float sign(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
    }

    bool PointInTriangle(Vector2 pt, Vector2 v1, Vector2 v2, Vector2 v3)
    {
        bool b1, b2, b3;

        b1 = sign(pt, v1, v2) < 0.0f;
        b2 = sign(pt, v2, v3) < 0.0f;
        b3 = sign(pt, v3, v1) < 0.0f;

        return ((b1 == b2) && (b2 == b3));
    }
}
