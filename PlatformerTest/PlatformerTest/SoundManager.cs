using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace PlatformerTest
{
    class SoundManager
    {
        ContentManager _content;
        List<String> _soundsToLoad = new List<string>();
        float _volume = 0.5f;
        float _pan = 0f;
        bool _looped;
        float _pitch = 0f;
        SoundEffectInstance currentSound;
        Dictionary<String, SoundEffectInstance> _loadedSounds = new Dictionary<string, SoundEffectInstance>();

        public SoundManager(ContentManager content, bool Looped) {
            _content = content;
            _looped = Looped;
        }

        public void addSound(String Name){
            if (!checkIfAdded(Name)) 
            {
                _soundsToLoad.Add(Name);
            }
        }

        public void loadAllSounds() 
        {
            foreach (String sound in _soundsToLoad) {
                _loadedSounds.Add(sound,_content.Load<SoundEffect>(sound).CreateInstance());
                
            }
        }

        public void playSound(String name){
            SoundEffectInstance soundToPlay = getSoundByName(name);
            if (soundToPlay != null)
            {

                currentSound = soundToPlay;
                currentSound.IsLooped = _looped;
                currentSound.Play();
                currentSound.Volume = _volume;
                currentSound.Pan = _pan;
                currentSound.Pitch = _pitch;
            }
        }

        public bool checkIfAdded(String Name)
        {
            if (_soundsToLoad.Contains(Name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public SoundEffectInstance getSoundByName(String name) {
            if (_loadedSounds.ContainsKey(name))
            {
                return _loadedSounds[name];
            }
            else 
            {
                return null;
            }
        }

        public void SoundVolume(float vol){
            _volume = vol;
            currentSound.Volume = _volume;
        }

        public void SoundPitch(float pitch) {
            _pitch = pitch;
            currentSound.Pitch = _pitch;
        }

        public void SoundPan(float pan) {
            _pan = pan;
            currentSound.Pan = _pan;
        }

        public void stopMusic() {
            currentSound.Stop();
        }

        public void pauseMusic() {
            currentSound.Pause();
        }

        public void Loop(bool looped) {
            _looped = looped;
        }
    }
}
