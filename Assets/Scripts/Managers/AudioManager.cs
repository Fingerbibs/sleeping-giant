using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource voiceSource;

    public AudioLibrary library;
    
    private void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else{
            Debug.Log("Destroying duplicate AudioManager/SceneLoader");
            Destroy(gameObject);
        }
    }

    private void Start(){
        PlayMainTheme();
    }

    public void PlayMusic(AudioClip clip, bool loop = true){
        musicSource.clip = clip;
        musicSource.loop = loop;

        musicSource.Play();
    }

    public void StopMusic(){
        musicSource.Stop();
    }

    public void ChangeMusic(AudioClip newClip){
        musicSource.clip = newClip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip){
        sfxSource.PlayOneShot(clip);
    }

    public void PlayVoice(AudioClip clip){
        voiceSource.clip = clip;
        voiceSource.loop = true;
        voiceSource.Play();
    }
    
    public void StopVoice(){
        voiceSource.Stop();
    }

    //HELPER FUNCTIONS
    public void PlaySpotlightOn() => PlaySFX(library.spotlightOn);
    public void PlaySpotlightOff() => PlaySFX(library.spotlightOff);
    public void PlayMainTheme() => PlayMusic(library.mainTheme);

    public void PlayGuideVoice() => PlayVoice(library.guideVoice);
}