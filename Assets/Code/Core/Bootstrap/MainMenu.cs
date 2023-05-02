using System.Collections.Generic;
using Rewind.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Rewind.Core {
  public partial class MainMenu : MonoBehaviour {
    [SerializeField] Button startButton;
    [SerializeField] Button loadButton;
    [SerializeField] List<Button> levelsButtons;
    [SerializeField] Button backButton;

    [SerializeField] GameObject menuGO;
    [SerializeField] GameObject levelsGO;

    public partial class Init {
      enum View {
        Menu,
        Levels
      }
      
      readonly MainMenu backing;
      [PublicAccessor] readonly ReactiveCommand startPressed = new ReactiveCommand();

      public Init(MainMenu backing) {
        this.backing = backing;
        
        backing.startButton.onClick.AddListener(() => startPressed.Execute());

        new[] {
          backing.backButton.onClick.AsObservable().Select(_ => View.Menu),
          backing.loadButton.onClick.AsObservable().Select(_ => View.Levels)
        }.Merge()
          .Subscribe(view => {
            backing.menuGO.SetActive(view == View.Menu);
            backing.levelsGO.SetActive(view == View.Levels);   
          });

        // for (var i = 0; i < backing.levelsButtons.Count; i++) {
        //   var i1 = i;
        //   backing.levelsButtons[i].onClick.AddListener(() => loadLevel?.Invoke(i1));
        // }
      }

      public void disable() => backing.setInactive();
    }
  }
}
