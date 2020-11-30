using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlitBrush : MonoBehaviour
{
    [SerializeField] private Collider2D collider;
    public RectTransform rectTransform;
    public float freezeInterval;
    [HideInInspector] public float lastFreezeTime;
    public Vector2 movementPerSecond;

    public IntBounds GetIntBounds()
    {
        return new IntBounds (collider.bounds);
    }
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

}

public struct IntBounds
{
    public int minX, minY, maxX, maxY;
   // public int width, height;
    public IntBounds(Bounds bounds)
    {
        minX = (int)bounds.min.x;
        minY = (int)bounds.min.y;
        maxX = (int)bounds.max.x;
        maxY = (int)bounds.max.y;

       /* width = (int)bounds.size.x;
        height = (int)bounds.size.y;*/
    }
}