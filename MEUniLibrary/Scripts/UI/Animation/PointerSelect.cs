using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace MEUniLibrary.UI.Animation {
    /// <summary>
    /// 選択されたUIに隙間を取った上でポインターを合わせる
    /// </summary>
    public class PointerSelect : MonoBehaviour, IPointerEnterHandler {
        [SerializeField] private RectTransform pointerRectTransform_;
        //x, yでどのくらいこのUIの座標との間に隙間を取るか
        [SerializeField] private Vector2 margin_;

        public void OnPointerEnter(PointerEventData pointerEventData) {
            Vector2 endPos = transform.localPosition;
            endPos += margin_;
            pointerRectTransform_.DOLocalMove(endPos, 0.2f);
        }
    }
}
