using UnityEngine;

public class VolumeChange : MonoBehaviour
{
    public void UpdateVolume(float value)
    {
        AudioListener.volume = value;
    }
}
