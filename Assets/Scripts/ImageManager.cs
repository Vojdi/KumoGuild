using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;

    static ImageManager instance;
    static public ImageManager Instance =>instance;

    private void Awake()
    {
        instance = this;
    }

    public Sprite GetSprite(int id)
    {
        return sprites[id]; 
    }


}
