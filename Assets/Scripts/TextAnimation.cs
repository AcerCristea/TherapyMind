using System.Collections;
using UnityEngine;
using TMPro;

public class TextAnimation : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public AudioClip[] audioClips; // Array for storing audio clips
    public float textSpeed;

    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        PlayAudioClip(index); // Play the first audio
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            PlayAudioClip(index); // Play the corresponding audio for the new line
            StartCoroutine(TypeLine());
        }
        else
        {
            audioSource.Stop(); // Stop playing audio when the dialogue ends
        }
    }

    void PlayAudioClip(int lineIndex)
    {
        if (audioClips != null && lineIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[lineIndex];
            audioSource.Play();
        }
    }
}
