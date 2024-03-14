using System.Collections.Generic;
using Rewind.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Rewind.Core
{ 
    public partial class MainMenu : MonoBehaviour 
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button loadButton;
        [SerializeField] private List<Button> levelsButtons;
        [SerializeField] private Button backButton;

        [SerializeField] private GameObject menuGO;
        [SerializeField] private GameObject levelsGO;

        public partial class Init
        {
            private enum View
            {
                Menu,
                Levels
            }

            private readonly MainMenu backing;
            [PublicAccessor] private readonly ReactiveCommand startPressed = new ReactiveCommand();

            public Init(MainMenu backing)
            {
                this.backing = backing;
                
                backing.startButton.onClick.AddListener(() => startPressed.Execute());

                new[] 
                {
                    backing.backButton.onClick.AsObservable().Select(_ => View.Menu),
                    backing.loadButton.onClick.AsObservable().Select(_ => View.Levels)
                }
                .Merge()
                .Subscribe(view =>
                {
                    backing.menuGO.SetActive(view == View.Menu);
                    backing.levelsGO.SetActive(view == View.Levels);     
                });

                // for (var i = 0; i < backing.levelsButtons.Count; i++) {
                //     var i1 = i;
                //     backing.levelsButtons[i].onClick.AddListener(() => loadLevel?.Invoke(i1));
                // }
            }

            public void Disable() => backing.SetInactive();
        }
    }
}
