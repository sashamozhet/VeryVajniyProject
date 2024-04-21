using UnityEngine;

public class TimeManager: MonoBehaviour
{
    [SerializeField] internal float intervalPreEveryMove = 0.01f;
    [SerializeField] internal float intervalChangeWithEveryIteration = 0.01f;
    [SerializeField] internal float durationSortedBgScaleTime = 0.9f;
    [SerializeField] internal float intervalAfterSortedBgScaled = 0.5f;
    [SerializeField] internal float durationBgPreGameTickAnimation = 0.4f;
    [SerializeField] internal float intervalAfterCardsShuffle = 0.5f;
    [SerializeField] internal float intervalPreChosenCardShown = 0.5f;
    [SerializeField] internal float durationBgScaleToZeroWhenCardChosen = 1f;
    [SerializeField] internal float durationChosenCardScaleAnimation = 2f;
}
