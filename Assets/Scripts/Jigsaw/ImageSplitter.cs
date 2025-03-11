using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSplitter : MonoBehaviour
{
    public static Sprite[] SplitImage(Texture2D image, int rows, int cols)
    {
        int pieceWidth = image.width / cols;
        int pieceHeight = image.height / rows;
        Sprite[] sprites = new Sprite[rows * cols];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Rect rect = new Rect(x * pieceWidth, y * pieceHeight, pieceWidth, pieceHeight);
                sprites[y * cols + x] = Sprite.Create(image, rect, new Vector2(0.5f, 0.5f));
            }
        }
        return sprites;
    }
}
