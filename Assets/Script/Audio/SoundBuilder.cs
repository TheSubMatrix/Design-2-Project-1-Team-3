using AudioSystem;
using UnityEngine;

namespace AudioSystem
{
    public class SoundBuilder
    {
        readonly SoundManager m_soundManager;
        SoundData m_soundData;
        Vector3 m_position = Vector3.zero;
        bool m_randomPitch;
        Transform m_parent;

        public SoundBuilder(SoundManager soundManager)
        {
            m_soundManager = soundManager;
        }

        public SoundBuilder WithSoundData(SoundData soundData)
        {
            m_soundData = soundData;
            return this;
        }

        public SoundBuilder WithPosition(Vector3 position)
        {
            m_position = position;
            return this;
        }

        public SoundBuilder WithRandomPitch()
        {
            m_randomPitch = true;
            return this;
        }

        public SoundBuilder AttachedTo(Transform parent)
        {
            m_parent = parent;
            return this;
        }

        public void Play()
        {
            if (!m_soundManager.CanPlaySound(m_soundData)) return;
            SoundEmitter emitter = m_soundManager.Get();
            emitter.Initialize(m_soundData);
            emitter.transform.position = m_position;
            emitter.transform.parent = m_parent ?? m_soundManager.transform;
            if (m_randomPitch)
            {
                emitter.WithRandomPitch();
            }
            if (m_soundData.PlayedFrequently)
            {
                m_soundManager.FrequentEmitters.Enqueue(emitter);
            }
            emitter.Play();

        }
    }
}