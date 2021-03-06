using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Audio Events/Simple")]
public class SimpleAudioEvent : AudioEvent
{
    public AudioClip[] audioClips;
    
    [MinMaxRange(0,1)]
    public RangedFloat volume;

    [MinMaxRangeAttribute(0,2)]
    public RangedFloat pitch;

    public override void Play(AudioSource _audioSource) {
        if (audioClips.Length == 0) return;
        audioSource = _audioSource;
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.volume = Random.Range(volume.minValue, volume.maxValue);
        audioSource.pitch = Random.Range(pitch.minValue, pitch.maxValue);
        audioSource.Play();
    }
}
