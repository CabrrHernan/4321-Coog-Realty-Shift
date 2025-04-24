using UnityEngine;
using System.Linq;

public class StreakPlate : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 textureSize;
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        Texture2D tex = new Texture2D(1024, 1024);
        texture = tex;
        textureSize = new Vector2(tex.width, tex.height);

        Color[] fillColor = Enumerable.Repeat(Color.white, tex.width * tex.height).ToArray();
        texture.SetPixels(fillColor);
        texture.Apply();

        renderer.material.mainTexture = texture;
    }

}