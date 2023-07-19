using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

namespace Rewind.Core {
    public class Cutscene : MonoBehaviour {
        [Required, SerializeField, Readonly] PlayableDirector director;
        [Required, SerializeField, Readonly] UIDocument uiDocument;

        [MonoReadonly] VisualElement container;
        [MonoReadonly] Button skipButton;

        void Start() {
            setupSkipButton();
            director.Play();

            void setupSkipButton() {
                container = uiDocument.rootVisualElement.Q<VisualElement>("container");
                skipButton = container.Q<Button>("skip-btn");
                skipButton.clicked += skip;
            }
        }

        void showSkipButton() {
            
        }

        void skip() {

        }

        void onEnd() {
            
        }
    }
}
