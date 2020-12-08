using UnityEngine;
using MEUniLibrary.UI.Animation;

namespace MEUniLibrary.UI.Button {
    public class CursorButton : ButtonBase {
        [SerializeField] private MenuByPopUp menuByPopUp_;
        [SerializeField] private bool callingMenuIsNext_; //次に行くボタンか

        new private void Start() {
            base.Start();
            button_.onClick.AddListener(slide);
        }

        private void slide() {
            menuByPopUp_.slide(callingMenuIsNext_);
        }
    }
}
