using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioSource instance;

    private void Awake()
    {
        // —охран€ем ссылку на AudioSource в статической переменной
        instance = GetComponent<AudioSource>();
    }

}