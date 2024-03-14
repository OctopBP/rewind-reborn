using System;
using Code.Helpers.Tracker;
using FMOD.Studio;
using FMODUnity;
using UniRx;
using UnityEngine;

namespace Rewind.Core
{
    public class LevelAudio : MonoBehaviour
    {
        [ParamRef, SerializeField] private string progressParam1;
        [SerializeField] private StudioEventEmitter ambientEmitter, musicEmitter;
        [SerializeField] private StudioParameterTrigger progressParam;

        [SerializeField] private EventReference eventReference;

        public class Model
        {
            public Model(ITracker tracker, LevelAudio levelAudio, IObservable<int> progressRx)
            {
                var ambientEmitter = levelAudio.ambientEmitter;
                var musicEmitter = levelAudio.musicEmitter;
                
                ambientEmitter.Play();
                musicEmitter.Play();
                
                var eventInstance = RuntimeManager.CreateInstance(levelAudio.eventReference);
                eventInstance.start();
                eventInstance.setParameterByName(levelAudio.progressParam1, 0);

                progressRx.Subscribe(progress => eventInstance.setParameterByName(levelAudio.progressParam1, progress));
                
                tracker.Track(() => Stop(eventInstance));
            }

            private static void Stop(EventInstance eventInstance)
            { 
                eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                eventInstance.release();
            }
        }
    }
}
