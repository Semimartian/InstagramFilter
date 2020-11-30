using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Unity.Collections;


public class Slitter : MonoBehaviour
{

    [SerializeField] private RenderTexture renderTexture;
    private Texture2D frozenTexture2D;
    private Texture2D livingTexture2D;
    [SerializeField] private Image livingImage;
    [SerializeField] private Image frozenImage;
    // [SerializeField] private SpriteRenderer spriteRenderer;

    private int screenWidth;
    private int screenHeight;

    /*private NativeArray<Color32> livingPixels;
    private NativeArray<Color32> frozenPixels;*/

   /* private int currentX = 0;
    private float lastXMovement = 0;
    [SerializeField] private float movementInterval = 0.07f;*/

    [SerializeField] private SlitBrush slitBrush;
    // Start is called before the first frame update
    private void Start()
    {
        screenWidth = renderTexture.width;
        screenHeight = renderTexture.height;

        CreateTextures();
    }

    private void CreateTextures()
    {
        Texture2D texture = null;
        // NativeArray<Color32> pixels;
        Image image = null;
        for (int i = 0; i < 2; i++)
        {
            switch (i)
            {
                case 0:
                    livingTexture2D = GetNewTexture();
                    texture = livingTexture2D;
                    // pixels = livingPixels;
                    image = livingImage;
                    break;

                case 1:
                    frozenTexture2D = GetNewTexture();
                    texture = frozenTexture2D;
                    // pixels = frozenPixels;
                    image = frozenImage;
                    break;
            }

            texture.filterMode = FilterMode.Point;
            texture.Apply();

            //pixels = texture.GetPixelData<Color32>(0);

            Sprite sprite = Sprite.Create
                  (texture, new Rect(0, 0, screenWidth, screenHeight), new Vector2(0.5f, 0.5f), 100);
            image.sprite = sprite;
            image.SetNativeSize();
        }

        for (int x = 0; x < screenWidth; x++)
        {
            for (int y = 0; y < screenHeight; y++)
            {
                frozenTexture2D.SetPixel(x, y, Color.clear);
            }
        }

    }

    private Texture2D GetNewTexture()
    {
        return new Texture2D(screenWidth, screenHeight, TextureFormat.RGBA32, false);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        float time = Time.time;

        UpdateLivingTexture();

        UpdateSlitBrush(ref deltaTime,ref time);


        /* if (Input.GetKeyDown(KeyCode.P))
         {
             Debug.Log("pixels:" + (width * height).ToString());

             Debug.Log( "byte array length:" + renderTexture2D.GetPixelData<byte>(0).Length);
         }*/
    }

    private void UpdateSlitBrush(ref float deltaTime, ref float time)
    {

        if (time >=slitBrush.lastFreezeTime + slitBrush.freezeInterval )
        {
            IntBounds bounds = slitBrush.GetIntBounds();
            FreezeRect(bounds.minX, bounds.minY, bounds.maxX, bounds.maxY);
            slitBrush.lastFreezeTime = time;
        }

        Vector3 movement = deltaTime * slitBrush.movementPerSecond;
        slitBrush.rectTransform.position += movement;
    }


    private void FreezeRect(int minX, int minY, int maxX, int maxY)
    {
        Debug.Log("FREEZE!");
        if (minX > screenWidth || maxX < 0 ||
            minY > screenHeight || maxY < 0)
        {
            return;
        }

        if (minX < 0)
        {
            minX = 0;
        }
        if (minY < 0)
        {
            minY = 0;
        }
        if (maxX > screenWidth)
        {
            maxX = screenWidth;
        }
        if (maxY > screenHeight)
        {
            maxY = screenHeight;
        }

        int width = maxX - minX;
        int height = maxY - minY;

        Color[] newPixels = livingTexture2D.GetPixels(minX, minY, width, height);
        frozenTexture2D.SetPixels(minX, minY, width, height, newPixels);
        frozenTexture2D.Apply();
    }

    private void UpdateLivingTexture()
    {
        RenderTexture.active = renderTexture;
        livingTexture2D.ReadPixels(new Rect(0, 0, screenWidth, screenHeight), 0, 0);
        livingTexture2D.Apply();
    }
}

/*public struct RGB24
{
    public byte r, g, b;
}*/