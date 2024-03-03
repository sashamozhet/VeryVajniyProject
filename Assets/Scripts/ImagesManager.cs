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
    [SerializeField] GameObject background;
    [SerializeField] GameObject display;
    private Image currentImg;
    private Image prevImageState;


    // Start is called before the first frame update



    public void ImageResizer()
    {
        //if (currentImg != null)
            //currentImg.transform.SetParent(background.transform);

        var img = img3;
        img.color = Color.red;
        currentImg = img;

        // �������� ������� ������
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        RectTransform rt = img.GetComponent<RectTransform>();

        // ��������� ������� ��� ��������� ����������� � ����� ������
        float xPosition = screenWidth / 2;
        float yPosition = screenHeight / 2;

        // ������������� ����� ���������� ������� �����������
        rt.position = new Vector3(xPosition, yPosition, 0);

        img.transform.SetParent(display.transform);
    }

    public void ImageStateReturner()
    {

    }

}
