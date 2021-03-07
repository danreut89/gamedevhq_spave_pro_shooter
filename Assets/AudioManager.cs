using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private AudioClip _explosionSoundClip;
    [SerializeField]
    private AudioClip _powerUpCollectable;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source Compnonet not found on this object");
        }
    }

    public void playExplosionSound()
    {
        _audioSource.clip = _explosionSoundClip;
        _audioSource.Play();
    }

    public void playCollectableSound()
    {
        _audioSource.clip = _powerUpCollectable;
        _audioSource.Play();
    }

}
