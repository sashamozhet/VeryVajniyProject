using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip randomStartedClip;

    // Start is called before the first frame update
    void Start()
    {        
        //ButtonsManager.onStartRandomingButtonClickedAction += PlayAudioWhenRandomStarted;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudioWhenRandomStarted()
    {
        //audioSource.PlayOneShot(randomStartedClip);
    }
}
