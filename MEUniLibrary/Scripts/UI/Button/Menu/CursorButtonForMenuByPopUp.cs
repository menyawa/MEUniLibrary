using UnityEngine;
using MEUniLibrary.UI.Animation;
using MEUniLibrary.UI.Menu;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// ポップアップメニューをスライドするためのカーソルボタンのクラス
    /// </summary>
    public class CursorButtonForMenuByPopUp : ButtonBase {
        [SerializeField] private MenuByPopUp menuByPopUp_;
        //このボタンのAnimationForScale
        [SerializeField] private AnimationForScale animationForButtonScale_;
        //反対側のこのボタン
        [SerializeField] private CursorButtonForMenuByPopUp oppositeCursorButtonForMenuByPopUp_;
        //前後のメニューどちらを呼ぶのかのタイプ
        private enum CALLING_MENU_TYPE {
            PREV,
            NEXT
        }
        [SerializeField] private CALLING_MENU_TYPE callingMenuType_;

        new private void Start() {
            base.Start();
            button_.onClick.AddListener(slide);
            button_.onClick.AddListener(switchScaleAllCursor);
        }

        /// <summary>
        /// メニューをスライドする
        /// </summary>
        private void slide() {
            menuByPopUp_.slide(callingMenuType_ == CALLING_MENU_TYPE.NEXT);
        }

        /// <summary>
        /// 全てのカーソルボタンの大きさを切り替える
        /// </summary>
        private void switchScaleAllCursor() {
            //このメソッドはボタンを押した際に呼ばれるため、もう一方のボタンでは呼ばれない
            //なのでボタンが押された際、反対側のボタンの方も呼び出してスケーリングする
            //変更メソッド内で呼び出してしまうと、無限ループが発生するので注意
            this.switchScaleByButtonInMenuEdge();
            oppositeCursorButtonForMenuByPopUp_.switchScaleByButtonInMenuEdge();
        }

        /// <summary>
        /// メニューの端にいるかいないかに合わせこのボタンの最大化・最小化をする
        /// </summary>
        private void switchScaleByButtonInMenuEdge() {
            //ループ可能なメニューなら端というものが存在しないので無条件で返す
            if (menuByPopUp_.canLoop()) return;

            //参照している方のメニューが存在しているならボタンを最大化(出し)、無いなら(端なら)最小化する
            //最小化する途中で連打されても、メニューの方で端で止まるのでそれを考慮する必要はない
            switch (callingMenuType_) {
                case CALLING_MENU_TYPE.NEXT:
                    if (menuByPopUp_.hasNext()) animationForButtonScale_.expand();
                    else animationForButtonScale_.minimize();
                    break;
                case CALLING_MENU_TYPE.PREV:
                    if (menuByPopUp_.hasPrev()) animationForButtonScale_.expand();
                    else animationForButtonScale_.minimize();
                    break;
            }
        }
    }
}
