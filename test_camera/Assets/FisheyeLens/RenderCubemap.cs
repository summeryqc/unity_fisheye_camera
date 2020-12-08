using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.IO;

public class RenderCubemap : MonoBehaviour {
    public RenderTexture cubemap;
    public int resolution = 1024;

    private string screenshotsDirectory = "Logs";

    void Initialize() {
        if(cubemap == null) {
            cubemap = new RenderTexture(resolution, resolution, 24);
            cubemap.dimension = TextureDimension.Cube;
        }
    }
    
    void Awake () {
        Initialize();
    }

    void LateUpdate () {
        Camera cam = GetComponent<Camera>();
        Debug.Log(cam.name);

        // One face per frame
        //var faceToRender = Time.frameCount % 6;
        //var faceMask = 1 << faceToRender;
        //cam.RenderToCubemap(cubemap, faceMask, Camera.MonoOrStereoscopicEye.Mono);
        // All six faces per frame, using 63 bitfield
        cam.RenderToCubemap(cubemap, 63, Camera.MonoOrStereoscopicEye.Mono);
    }
    
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination);
        if (Time.frameCount <= 6)
        {
            //SaveBitmap(destination, "test" + Time.frameCount + ".png");
        }   
    }

    // Junhua 
    void SaveBitmap(RenderTexture myRenderTexture, string filename)
    {
        Debug.Log("cubemap width: " + myRenderTexture.width);
        var tex = new Texture2D(myRenderTexture.width, myRenderTexture.height, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, myRenderTexture.width, myRenderTexture.height), 0, 0);
        tex.Apply();

        byte[] bytes = tex.EncodeToPNG();
        Destroy(tex);

        var path = screenshotsDirectory + "/" + "cubemap" + filename;
        File.WriteAllBytes(path, bytes);
    }
}
