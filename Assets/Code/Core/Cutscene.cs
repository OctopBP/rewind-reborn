using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

namespace Rewind.Core {
    public class Cutscene : MonoBehaviour {
        [Required, SerializeField] PlayableDirector director;
        [Required, SerializeField] UIDocument uiDocument;

        [MonoReadonly] int test = 0;
        [MonoReadonly] VisualElement container;
        Button skipButton;

        void Start() {
            setupSkipButton();
            director.Play();

            test = 1;
            
            void setupSkipButton() {
                container = uiDocument.rootVisualElement.Q<VisualElement>("container");
                skipButton = container.Q<Button>("skip-btn");
                skipButton.clicked += skip;
            }
        }

        void showSkipButton() {
            container = null;
        }

        void skip() {

        }

        void onEnd() {
            
        }
    }
}
