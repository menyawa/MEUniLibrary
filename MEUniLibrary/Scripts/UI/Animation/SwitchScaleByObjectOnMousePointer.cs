using UnityEngine;
using UnityEngine.EventSystems;

namespace MEUniLibrary.UI.Animation {
    /// <summary>
    /// マウスポインタを置いたオブジェクトを一定値大きくし、外したら元に戻すクラス
    /// </summary>
    public class SwitchScaleByObjectOnMousePointer : AnimationForScale, IPointerEnterHandler, IPointerExitHandler {
        //ポインターが乗っている際のスケール
        //マイナスの値を入れれば、小さくすることも可能
        [SerializeField] private Vector3 scaleWithPointer_;
        //通常時のスケール
        //これはStartで入れた値とする
        private Vector3 nomalScale_;

        private void Start() {
            nomalScale_ = transform.localScale;
        }

        public virtual void OnPointerEnter(PointerEventData eventData) {
            expand(scaleWithPointer_);
        }

        public virtual void OnPointerExit(PointerEventData eventData) {
            expand(nomalScale_);
        }
    }
}
