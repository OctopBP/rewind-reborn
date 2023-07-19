using System;
using Code.Helpers.Tracker;
using FMOD.Studio;
using FMODUnity;
using UniRx;
using UnityEngine;

namespace Rewind.Core {
    public class LevelAudio : MonoBehaviour {
        [ParamRef, SerializeField, Readonly] string progressParam1;
        [SerializeField, Readonly] StudioEventEmitter ambientEmitter, musicEmitter;
        [SerializeField, Readonly] StudioParameterTrigger progressParam;

        [SerializeField, Readonly] EventReference eventReference;

        public class Model {
            readonly EventInstance eventInstance;
            
            public Model(ITracker tracker, LevelAudio levelAudio, IObservable<int> progressRx) {
                var ambientEmitter = levelAudio.ambientEmitter;
                var musicEmitter = levelAudio.musicEmitter;
                
                ambientEmitter.Play();
                musicEmitter.Play();
                
                eventInstance = RuntimeManager.CreateInstance(levelAudio.eventReference);
                eventInstance.start();
                eventInstance.setParameterByName(levelAudio.progressParam1, 0);

                progressRx.Subscribe(progress => eventInstance.setParameterByName(levelAudio.progressParam1, progress));
                
                tracker.track(() => stop(eventInstance));
            }

            void stop(EventInstance eventInstance) { 
                eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                eventInstance.release();
            }
        }
    }
}
