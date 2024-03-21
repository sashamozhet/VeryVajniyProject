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

    private Image currentImg;  //текущее состояние картинки?
    private Image prevImageState; // сохраняем сюда изначальные параметры изображения

    private List<Image> cards; // объект под список карточек
    private RandomsVariations randomizer; // объект под рандомайзер
    private int prevIndex; // индекс изначального положения картинки, чтоб вернуть её на место

    public bool ImageChanged {  get; private set; } // для чека, выведено ли уже какое-то изображение в центр


    private void Start()
    {
        ButtonsManager.onStartRandomingButtonClickedAction += ImageResizer;
        ButtonsManager.onSettingsButtonClickedAction += Test;

        ImageChanged = false; // на старте никакое изображение в центр не выведено

        // добавляем все карточки в список
        cards = new()
        {
            img1, img2, img3, img4, img5, img6, img7, img8, img9, img10, img11, img12, img13, img14, img15, img16
        };

        // создаём объект, который будет рандомить
        randomizer = new();
        
    }

    public void ImageResizer()
    {
        if (ImageChanged) //если изображение изменено
        {
            ImageStateReturner(); //откатываем назад
            
        }
        int randNumber = randomizer.SimpleRandomForCardIndex(0, cards.Count); // рандомим число в рамках длины списка карточек
        currentImg = cards[randNumber]; // картинка, которую меняем = порядковый номер нарандомленного в списке
        prevImageState = tempImage; // пустая временная переменная для инициализации переменной, хранящей изначальные данные пикчи
        prevImageState.color = currentImg.color; // сохраняем изначальный цвет выбранной пикчи
        prevImageState.transform.position = currentImg.transform.position; // сохраняем изначальный трансформ выбранной пикчи
        prevIndex = currentImg.transform.GetSiblingIndex(); // сохраняем изначальный индекс на канвасе, чтоб при возврате не сбивался порядок пикчей

        ImageChanged = true; // пикча изменена, теперь 2ую подряд мы изменить не сможем
        currentImg.transform.SetParent(display.transform); // переносим на холст display, чтоб вывести на передний ряд
        currentImg.transform.position = new Vector2(Screen.width / 2, Screen.height / 2); // увеличиваем размер в 2 раза

    }

    public void ImageStateReturner()
    {
        if (ImageChanged)  
        {
            ImageChanged = false;  //значит изменить его мы не можем
            currentImg.color = prevImageState.color; //откатывает цвет до предыдущего состояния
            currentImg.transform.position = prevImageState.transform.position; //меняем текущее положение картинки на предыдущее, т.е ставим её обратно
            currentImg.transform.SetParent(background.transform); //делаем её дочерней канвасу background
            currentImg.transform.SetSiblingIndex(prevIndex); //Возвращает его в тоже самое место по списку, где он и был
        }            
    }

    public void Test()
    {
        foreach(var c in cards)
        {
            c.transform.SetSiblingIndex(randomizer.SimpleRandomForCardIndex(0, cards.Count));
        }
    }
}
