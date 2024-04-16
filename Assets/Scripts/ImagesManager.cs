using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

using System;

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

    [SerializeField] int movesMin;
    [SerializeField] int movesMax;
    [SerializeField] float defaultInterval;
    [SerializeField] float intervalChangeWithEveryIteration;
    [SerializeField] ButtonsManager buttonsManager;  

    private Image currentImg;  //текущее состояние картинки?
    private Image prevImageState; // сохраняем сюда изначальные параметры изображения

    private List<Image> cards; // объект под список карточек
    private RandomsVariations randomizer; // объект под рандомайзер
    private int prevIndex; // индекс изначального положения картинки, чтоб вернуть её на место

    public bool ImageChanged {  get; private set; } // для чека, выведено ли уже какое-то изображение в центр

    public void StartChosingImageProcess()
    {
        MakeRandomMinMaxAmountOfMoves();
    }

    private void Start()
    {
        //SuperManager.GameStarted += ShuffleCardsOnBoard;

        ImageChanged = false; // на старте никакое изображение в центр не выведено

        // добавляем все карточки в список
        cards = new()
        {
            img1, img2, img3, img4, img5, img6, img7, img8, img9, img10, img11, img12, img13, img14, img15, img16
        };

        // создаём объект, который будет рандомить
        randomizer = new();
        
    }

    public void ShuffleCardsOnBoard()
    {
        foreach (Image img in cards)
        {           
            img.transform.DOScale(1.03f, 0.4f).SetLoops(4, LoopType.Yoyo);
            img.transform.SetSiblingIndex(randomizer.SimpleRandomMinMax(0, cards.Count)); // присваиваем рандомное число каждой карточке как порядковый номер на канвасе
        }
    }

    public void MakeRandomMinMaxAmountOfMoves()
    {
        if (ImageChanged) //если изображение изменено
        {
            ImageStateReturner(); //откатываем назад            
        }

        ShuffleCardsOnBoard(); // перемешиваем карты на доске

        Sequence mySequence = DOTween.Sequence(); // создаём сиквенс

        var cardsOnBoard = background.GetComponentsInChildren<Image>(); // добавляем в новый список перемешанные карты в новом порядке

        var interval = defaultInterval; // всегда скидываем задержку между анимациями карт в дефолтное состояние
               
        mySequence.PrependInterval(interval); // добавляем эту задержку в очередь выполнений
        
        
        double randNum = randomizer.SimpleRandomMinMax(movesMin, movesMax); // рандомим количество ходов
        int j = 0; // переменная для прохода по индексу карточек
        for (int i = 0; i < randNum; i++)
        {
            mySequence.Append(cardsOnBoard[j].transform.DOScale(1.03f, 0.1f).SetLoops(2, LoopType.Yoyo)); // каждую карту увеличиваем немного и возвращаем в исходное
            mySequence.AppendInterval(interval); // после каждой анимации задержка перед анимацией следующей карты
            j = j < cardsOnBoard.Length - 1 ? j + 1 : 0; // если индекс карты в списке меньше максимально возможного, добавляем 1. если дошли до конца списка, а ходы еще есть, идём заново
            interval += intervalChangeWithEveryIteration; // увеличиваем задержку между картами с каждым ходом, чтоб замедлить движение
        }
        currentImg = cardsOnBoard[j]; // карта, на которой остановился цикл, и является картой, которую нужно изменить
        mySequence.Append(ImageResizer()); // по завершению действий выше вызываем метод, показывающий итоговую карту
        mySequence.AppendCallback(() => { buttonsManager.EnableOrDisableButtons(true); }); // по завершению анимации показа карты включаем кнопки
    }

    public Sequence ImageResizer()
    {   
        prevImageState = tempImage; // пустая временная переменная для инициализации переменной, хранящей изначальные данные пикчи
        prevImageState.color = currentImg.color; // сохраняем изначальный цвет выбранной пикчи
        prevImageState.transform.localScale = currentImg.transform.localScale; // сохраняем изначальный размер выбранной пикчи
        prevImageState.transform.position = currentImg.transform.position; // сохраняем изначальный размер выбранной пикчи
        prevIndex = currentImg.transform.GetSiblingIndex(); // сохраняем изначальный индекс на канвасе, чтоб при возврате не сбивался порядок пикчей
        Sequence seq = DOTween.Sequence();
        ImageChanged = true; // пикча изменена, теперь 2ую подряд мы изменить не сможем
        seq.AppendCallback(() => { currentImg.transform.SetParent(display.transform); }); // переносим на холст display, чтоб вывести на передний ряд
        seq.AppendCallback(() => { currentImg.transform.position = new Vector2(Screen.width / 2, Screen.height / 2); }); // выносим в центр экрана
        return seq.Append(currentImg.transform.DOScale(1.7f, 3f));        
    }

    public void ImageStateReturner()
    {
        if (ImageChanged)  
        {
            ImageChanged = false;  //значит изменить его мы не можем
            currentImg.color = prevImageState.color; //откатываем цвет до предыдущего состояния
            currentImg.transform.position = prevImageState.transform.position; //меняем текущее положение картинки на предыдущее, т.е ставим её обратно
            currentImg.transform.localScale = prevImageState.transform.localScale; // возвращаем размер
            currentImg.transform.SetParent(background.transform); //делаем её дочерней канвасу background
            currentImg.transform.SetSiblingIndex(prevIndex); //Возвращает его в тоже самое место по списку, где он и был
        }            
    }

}
