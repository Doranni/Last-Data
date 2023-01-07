using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] backMusic;

    private AudioSource audioSource;
    private Coroutine coroutine_backMusic;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        coroutine_backMusic = StartCoroutine(PlayBackMusic());
    }

    private IEnumerator PlayBackMusic()
    {
        while (true)
        {
            int index = Random.Range(0, backMusic.Length - 1);
            audioSource.PlayOneShot(backMusic[index]);
            yield return new WaitForSeconds(backMusic[index].length);
        }
    }
}
