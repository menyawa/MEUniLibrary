using UnityEngine;
using MEUniLibrary.UI.Menu;
using MEUniLibrary.UI.Animation;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// 双方向連結メニューをスライドするカーソルボタンのクラス
    /// </summary>
    public class CursorButtonByDoublyLinkedMenu : ButtonBase {
        //メニューの親オブジェクトのTransform
        [SerializeField] private Transform menuParentTransform_;
        //このボタンのAnimationForScale
        [SerializeField] private AnimationForScale animationForScaleByButton_;
        //反対側のこのボタン
        [SerializeField] private CursorButtonByDoublyLinkedMenu oppositeCursorButtonByDoublyLinkedMenu_;

        //前後どちらのメニューを呼ぶのか
        private enum CALLING_MENU_TYPE {
            PREV,
            NEXT
        }
        [SerializeField] private CALLING_MENU_TYPE callingMenuType_;

        new private void Start() {
            base.Start();
            button_.onClick.AddListener(slideMenu);
            button_.onClick.AddListener(switchScaleInMenuEdge);
        }

        private void Update() {
            switchScaleInMenuEdge();
        }

        /// <summary>
        /// メニューをスライドする
        /// </summary>
        private void slideMenu() {
            //ボタンに対応する前後のメニューが無かったら弾く
            //本来は最小化で防げる事態だが、メニューが端の際、連打等されて最小化のアニメーション途中で押されてしまうとエラーが起きる可能性があるため、こうして弾く
            var currentMenu = DoublyLinkedMenu.currentMenu_;
            switch (callingMenuType_) {
                case CALLING_MENU_TYPE.NEXT:
                    if (currentMenu.hasNext() == false) return;
                    break;
                case CALLING_MENU_TYPE.PREV:
                    if (currentMenu.hasPrev() == false) return;
                    break;
            }

            StartCoroutine(currentMenu.replaceMenu(callingMenuType_ == CALLING_MENU_TYPE.NEXT, menuParentTransform_));
        }

        /// <summary>
        /// 対応する前後のメニューが存在「しない」際このボタンを最小化・最大化する
        /// </summary>
        private void switchScaleInMenuEdge() {
            var currentMenu = DoublyLinkedMenu.currentMenu_;
            //現在メニューがそもそも無いなら返す
            if (currentMenu == null) return;

            //参照している方のメニューが存在しているならボタンを最大化(出し)、無いなら(端なら)最小化する
            //またこのメソッドはボタンを押した際に呼ばれるため、もう一方のボタンでは呼ばれない
            //なので反対側のボタンの方も呼び出してスケーリングする
            switch (callingMenuType_) {
                case CALLING_MENU_TYPE.NEXT:
                    if (currentMenu.hasNext()) animationForScaleByButton_.expand();
                    else animationForScaleByButton_.minimize();
                    oppositeCursorButtonByDoublyLinkedMenu_.switchScaleInMenuEdge();
                    break;
                case CALLING_MENU_TYPE.PREV:
                    if (currentMenu.hasPrev()) animationForScaleByButton_.expand();
                    else animationForScaleByButton_.minimize();
                    oppositeCursorButtonByDoublyLinkedMenu_.switchScaleInMenuEdge();
                    break;
            }
        }
    }
}