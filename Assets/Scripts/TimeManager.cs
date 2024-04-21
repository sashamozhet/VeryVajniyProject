using UnityEngine;

public class TimeManager: MonoBehaviour
{
    [SerializeField] internal float intervalPreEveryMove = 0.01f; // дефолтный интервал перед каждым новым выбором карты при переборе по доске
    [SerializeField] internal float intervalChangeWithEveryIteration = 0.01f; // изменения интервала с каждым шагом
    [SerializeField] internal float durationSortedBgScaleTime = 0.9f; // время анимации скейла фейк-холста при "сортировке" доски (при повторных запусках рандома)
    [SerializeField] internal float intervalAfterSortedBgScaled = 0.5f; // задержка после показа анимации скейла фейк-холста после "сортировки" доски
    [SerializeField] internal float durationBgPreGameTickAnimation = 0.4f; // время анимации скейла бэкграунда туда-обратно перед перебором по доске
    [SerializeField] internal float intervalAfterCardsShuffle = 0.5f; // задержка после шафла карт по доске
    [SerializeField] internal float intervalPreChosenCardShown = 0.5f; // задержка перед показом итоговой карты
    [SerializeField] internal float durationBgScaleToZeroWhenCardChosen = 1f; // время скейла в 0 бэкграунд-холста, когда итоговая карта выбрана
    [SerializeField] internal float durationChosenCardScaleAnimation = 2f; // время показа анимации скейла выбранной итоговой карты
}
