using System;
using System.Collections.Generic;
using System.Linq;
using AudioSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAmbiencePlayer : MonoBehaviour
{
    [SerializeField] List<LevelSoundData> m_soundsForLevels = new();
    [Serializable]
    struct LevelSoundData
    {
        public string LevelName;
        public SoundData SoundDataForLevel;
    }

    void Start()
    {
        foreach (LevelSoundData soundData in m_soundsForLevels.Where(soundData => soundData.LevelName == SceneManager.GetActiveScene().name))
        {
            Debug.Log(soundData.LevelName);
            SoundManager.Instance?.CreateSound()?.WithSoundData(soundData.SoundDataForLevel).WithPosition(transform.position).Play();
        }
    }
}
