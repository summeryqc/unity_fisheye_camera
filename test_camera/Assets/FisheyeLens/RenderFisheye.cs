using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.IO;

public class RenderFisheye : MonoBehaviour {
    public RenderCubemap cubemap_script;
    public Shader shader;
    public float focalLength_x = 1.0f;
    public float focalLength_y = 1.0f;

    private string screenshotsDirectory = "Logs";

    private Material _material;
    private Material material {
        get {
            if (_material == null) {
                _material = new Material(shader);
                _material.hideFlags = HideFlags.HideAndDontSave;
            }
            return _material;
        }
    }

    void Start () {
    }

    private void OnDisable() {
        if (_material != null)
            DestroyImmediate(_material);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
	    Debug.Log("OnRenderImage: Fisheye");
        if (shader != null) {
            material.SetTexture("_Cube", cubemap_script.cubemap);
       	    material.SetFloat("_FocalLength_x", focalLength_x);
            material.SetFloat("_FocalLength_y", focalLength_y);
            Graphics.Blit(source, destination, material);
        } 
        else {
           Graphics.Blit(source, destination);
        }
        if (Time.frameCount <= 6)
        {
            SaveBitmap(source, "test" + Time.frameCount + ".png");
        }
    }

    void SaveBitmap(RenderTexture myRenderTexture, string filename)
    {
        Debug.Log("final textue width: " + myRenderTexture.width);
        Debug.Log("final textue height: " + myRenderTexture.height);
        var tex = new Texture2D(myRenderTexture.width, myRenderTexture.height, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, myRenderTexture.width, myRenderTexture.height), 0, 0);
        tex.Apply();

        byte[] bytes = tex.EncodeToPNG();
        Destroy(tex);

        var path = screenshotsDirectory + "/" + "fisheye" + filename;
        File.WriteAllBytes(path, bytes);
    }
}
