using System;
using System.Collections.Generic;
using Rewind.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Rewind.Core.Code.Base.Bootstrap {
    public class MainMenu : MonoBehaviour {
        [SerializeField] public Button startButton;
        [SerializeField] public Button loadButton;
        [SerializeField] public List<Button> levelsButtons;
        [SerializeField] public Button backButton;

        [SerializeField] public GameObject menuGO;
        [SerializeField] public GameObject levelsGO;

        public class Init {
            public readonly MainMenu backing;

            public Init(MainMenu backing, Action<int> loadLevel) {
                this.backing = backing;

                backing.loadButton.onClick.AddListener(() => {
                    backing.menuGO.setInactive();
                    backing.levelsGO.setActive();
                });

                backing.backButton.onClick.AddListener(() => {
                    backing.menuGO.setActive();
                    backing.levelsGO.setInactive();
                });
                
                for (var i = 0; i < backing.levelsButtons.Count; i++) {
                    var i1 = i;
                    backing.levelsButtons[i].onClick.AddListener(() => loadLevel?.Invoke(i1));
                }
            }
        }
    }
}
