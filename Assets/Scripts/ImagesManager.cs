using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    [SerializeField] internal GameObject background; // холст, на котором пикчи лежат изначально
    [SerializeField] internal GameObject backgroundAlwaysSorted; // ФЕЙК ХОЛСТ
    [SerializeField] internal GameObject tempBackgroundObject; // объект для сохранения изначального вида бэкграунд-холста
    [SerializeField] GameObject display; // холст, на который выводится финально выбранная пикча

    internal Image currentImg;  //текущее состояние картинки?
    private Image prevImageState; // сохраняем сюда изначальные параметры изображения
    internal List<Image> cards; // объект под список карточек
    public bool CardAnimatedMovedToDisplay {  get; private set; } // для чека, выбрана и анимирована ли уже какая-то карта на момент

    private void Start()
    {
        // сохраняем данные трансформа бэкграунда во временный объект
        tempBackgroundObject.transform.localScale = background.transform.localScale;
        tempBackgroundObject.transform.localPosition = background.transform.localPosition;

        CardAnimatedMovedToDisplay = false; // на старте никакое изображение не выбрано

        // добавляем все карточки в список
        cards = new()
        {
            img1, img2, img3, img4, img5, img6, img7, img8, img9, img10, img11, img12, img13, img14, img15, img16
        };
      
    }

    public Sequence ShuffleCardsSequence()
    {
        Sequence seq = DOTween.Sequence();
        foreach (Image img in cards)
        {
            img.transform.SetSiblingIndex(RandomsVariations.SimpleRandomMinMax(0, cards.Count - 1)); // присваиваем рандомное число каждой карточке как порядковый номер на канвасе       
        }
        return seq;
    }

    public Sequence MakeMovesOnBoard(float defaultInterval, float intervalChange, int randNum)
    {
        Sequence mySequence = DOTween.Sequence(); // создаём сиквенс

        var interval = defaultInterval; // всегда скидываем задержку между анимациями карт в дефолтное состояние
               
        mySequence.PrependInterval(interval); // добавляем эту задержку в очередь выполнений

        var cardsOnBoard = background.GetComponentsInChildren<Image>(); // добавляем в новый список перемешанные карты в новом порядке

        int j = 0; // переменная для прохода по индексу карточек
        for (int i = 0; i <= randNum; i++)
        {
            //mySequence.Append(cardsOnBoard[j].transform.DOPunchScale(new Vector2(0.1f, 0.1f), interval * 1.05f, 0, 0.05f)); // каждую карту увеличиваем немного и возвращаем в исходное, это старый вариант, сейчас работаем не с ним
            mySequence.Append(cardsOnBoard[j].transform.DORotate(new Vector3(0, 0, -10), interval * 1.05f)).AppendInterval(interval).Append(cardsOnBoard[j].transform.DORotate(new Vector3(0, 0, 0), interval * 1.05f)); // каждую карту увеличиваем немного и возвращаем в исходное
            mySequence.AppendInterval(interval); // после каждой анимации задержка перед анимацией следующей карты
            j = j < cardsOnBoard.Length - 1 ? j + 1 : 0; // если индекс карты в списке меньше максимально возможного, добавляем 1. если дошли до конца списка, а ходы еще есть, идём заново
            interval += intervalChange; // увеличиваем задержку между картами с каждым ходом, чтоб замедлить движение
        }
        currentImg = cardsOnBoard[j]; // карта, на которой остановился цикл, и является картой, которую нужно изменить
        return mySequence;
    }

    public Sequence AnimateChosenCard(float scaleDuration)
    {   
        prevImageState = tempImage; // пустая временная переменная для инициализации переменной, хранящей изначальные данные пикчи
        prevImageState.color = currentImg.color; // сохраняем изначальный цвет выбранной пикчи
        prevImageState.transform.localScale = currentImg.transform.localScale; // сохраняем изначальный размер выбранной пикчи
        prevImageState.transform.position = currentImg.transform.position; // сохраняем изначальный размер выбранной пикчи
        Sequence seq = DOTween.Sequence(); // создаём очередь выполнения
        CardAnimatedMovedToDisplay = true; // пикча изменена, теперь 2ую подряд мы изменить не сможем
        seq.AppendCallback(() => { currentImg.transform.SetParent(display.transform); }); // переносим на холст display, чтоб вывести на передний ряд
        seq.AppendCallback(() => { currentImg.transform.position = new Vector2(Screen.width / 2, Screen.height / 2); }); // выносим в центр экрана
        return seq.Append(currentImg.transform.DOScale(1.5f, scaleDuration));        
    }

    public Sequence PreAnimateChosenCard(Image image, float ticksDuration)
    {
        Sequence seq = DOTween.Sequence(); // создаём очередь выполнения
        seq.Append(image.transform.DOScale(1.02f, ticksDuration)).Append(image.transform.DOScale(0.98f, ticksDuration)).SetLoops(6);
        return seq;
    }
    public Sequence BoardStateReturner()
    {
        Sequence seq = DOTween.Sequence(); // создаём очередь
        currentImg.color = prevImageState.color; //откатываем цвет итоговой выбранной пикчи до предыдущего состояния
        currentImg.transform.SetParent(background.transform); //делаем её дочерней канвасу background
        currentImg.transform.position = prevImageState.transform.position; // меняем текущее положение картинки на предыдущее, т.е ставим её обратно   
        currentImg.transform.localScale = prevImageState.transform.localScale; //меняем текущий размер картинки на предыдущий   
        background.transform.localScale = tempBackgroundObject.transform.localScale; // возвращаем размер бэкграунда
        background.transform.localPosition = tempBackgroundObject.transform.localPosition; // возвращаем позицию бэкграунда
        CardAnimatedMovedToDisplay = false;  // изображение снова на месте, булевую откатываем в изначальное состояние
        return seq;
    }
}
