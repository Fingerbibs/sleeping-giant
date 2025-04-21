using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Audio/Audio Library")]
public class AudioLibrary : ScriptableObject
{
    // Sound Effects
    public AudioClip spotlightOn;
    public AudioClip spotlightOff;

    // Voice Clips
    public AudioClip guideVoice;

    // Music
    public AudioClip mainTheme;
}
