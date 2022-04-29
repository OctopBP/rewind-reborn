using UnityEngine;
using UnityEngine.UI;

namespace Rewind.Core.Code.Base.Bootstrap {
    public class MainMenu : MonoBehaviour {
        [SerializeField] public Button loadButton;

        public class Init {
            public readonly MainMenu backing;

            public Init(MainMenu backing) {
                this.backing = backing;
            }
        }
    }
}
