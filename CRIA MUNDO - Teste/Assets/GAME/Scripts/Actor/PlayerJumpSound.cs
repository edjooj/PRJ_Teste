using UnityEngine;

public class PlayerJumpSound : MonoBehaviour
{
    public AudioClip jumpSound; 
    private AudioSource audioSource; 
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = jumpSound;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.Play();
        }
    }
}
