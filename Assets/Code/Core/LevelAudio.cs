using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Rewind.Core {
    public class LevelAudio : MonoBehaviour {
        [ParamRef, SerializeField] string param0;
        [SerializeField] PARAMETER_ID param1;
        [SerializeField] ParamRef param2;
        [SerializeField] ParameterAutomationLink param3;
        
        public class Model {
            readonly StudioEventEmitter emitter;
            readonly LevelAudio levelAudio;
            
            public Model(LevelAudio levelAudio, StudioEventEmitter emitter) {
                this.levelAudio = levelAudio;
                this.emitter = emitter;
                
                var instance = RuntimeManager.CreateInstance(levelAudio.param0);
                instance.start();
            }

            public void progress() {
                emitter.SetParameter(levelAudio.param1, 1);
            }
        }
    }
}
