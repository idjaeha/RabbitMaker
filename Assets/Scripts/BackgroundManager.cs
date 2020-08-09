using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    private readonly string IMAGE_PATH = "Images/Backgrounds/{0}";
    void Start()
    {

    }
    public void ChangeBackground(string imageName)
    {
        string imagePath = string.Format(IMAGE_PATH, imageName);
        Sprite background = Resources.Load(imagePath, typeof(Sprite)) as Sprite;
        if (background == null)
        {
            Debug.Log("해당 이미지가 존재하지 않습니다.");
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = background;
        }
    }
}
