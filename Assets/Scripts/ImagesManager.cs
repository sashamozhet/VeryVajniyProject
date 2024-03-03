using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagesManager : MonoBehaviour
{
    [SerializeField] Image img1;
    [SerializeField] Image img2;
    [SerializeField] Image img3;
    [SerializeField] Image img4;
    [SerializeField] Image img5;
    [SerializeField] Image img6;
    [SerializeField] Image img7;
    [SerializeField] Image img8;
    [SerializeField] Image img9;
    [SerializeField] Image img10;
    [SerializeField] Image img11;
    [SerializeField] Image img12;
    [SerializeField] Image img13;
    [SerializeField] Image img14;
    [SerializeField] Image img15;
    [SerializeField] Image img16;
    [SerializeField] Image tempImage; // временное изображение для изначальной инициализации
    [SerializeField] GameObject background;
    [SerializeField] GameObject display;

    private Image currentImg; 
    private Image prevImageState; // сохраняем сюда изначальные параметры изображения

    private List<Image> cards;
    private System.Random rnd;
    private int prevIndex;



    private void Start()
    {
        cards = new()
        {
            img1, img2, img3, img4, img5, img6, img7, img8, img9, img10, img11, img12, img13, img14, img15, img16
        };
        rnd = new();
    }

    public void ImageResizer()
    {   
        int randNumber = rnd.Next(0, cards.Count);
        currentImg = cards[randNumber];
        prevImageState = tempImage;
        prevImageState.color = currentImg.color;
        prevImageState.transform.position = currentImg.transform.position;
        prevIndex = currentImg.transform.GetSiblingIndex();

        currentImg.transform.SetParent(display.transform);
        currentImg.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);

        
    }

    public void ImageStateReturner()
    {
        currentImg.color = prevImageState.color;
        currentImg.transform.position = prevImageState.transform.position;
        currentImg.transform.SetParent(background.transform);
        currentImg.transform.SetSiblingIndex(prevIndex);
        
    }

}
