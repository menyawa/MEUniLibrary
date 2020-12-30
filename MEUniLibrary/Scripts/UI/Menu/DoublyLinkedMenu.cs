using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace MEUniLibrary.UI.Menu {
    /// <summary>
    /// 双方向連結リストの考え方を用いたメニューのクラス
    /// </summary>
    public class DoublyLinkedMenu : MonoBehaviour {
        //前後のメニューの参照
        [SerializeField] private GameObject prevMenu_;
        [SerializeField] private GameObject nextMenu_;

        //現在開かれているメニュー
        public static DoublyLinkedMenu currentMenu_;

        /// <summary>
        /// 前後の呼ばれた方の新しいメニューを画面外で生成し、そのオブジェクトを返す
        /// </summary>
        /// <param name="nextMenuIsCalled"></param>
        /// <param name="menuParentTransform"></param>
        /// <returns></returns>
        public GameObject spawnNewMenu(bool nextMenuIsCalled, Transform menuParentTransform) {
            //移動途中でのボタン連打での位置ずれを防止するため、現在のローカル座標経由ではなく、直接座標を入力する
            var newMenu = nextMenuIsCalled ? nextMenu_ : prevMenu_;
            const float LENGTH = 2000f;
            var position = nextMenuIsCalled ? new Vector2(LENGTH, 0f) : new Vector2(LENGTH, 0f) * -1;
            newMenu = Instantiate(newMenu, position, Quaternion.identity, menuParentTransform);
            //スポーン→menuParentObjectの子にする、という順番で働くらしく、ローカル座標で意図した位置に置けないので、もう一回ローカル座標を指定する
            newMenu.transform.localPosition = position;

            return newMenu;
        }

        /// <summary>
        /// 前後の呼ばれた方の新しいメニューに現在のメニューをアニメーションをしながら入れ替える
        /// </summary>
        /// <param name="nextMenuIsCalled"></param>
        /// <returns></returns>
        public IEnumerator replaceMenu(bool nextMenuIsCalled, Transform menuParentTransform) {
            var newMenuTransform = currentMenu_.spawnNewMenu(nextMenuIsCalled, menuParentTransform).transform;
            var currentMenuTransform = currentMenu_.transform;

            //押したカーソルの方向の画面外に新しいメニューを生成、中心にスライド
            //また現在のメニューは反対方向の画面外にスライド
            //SlideUIを用いた移動だと、いちいち始点をリセットなどせねばならないため独自にスライド処理を行う
            const float LENGTH = 2000f;
            const float DURATION = 0.2f;
            const Ease EASE = Ease.OutBack;
            if (nextMenuIsCalled) {
                newMenuTransform.DOLocalMoveX(LENGTH * -1, DURATION).SetRelative().SetEase(EASE);
                currentMenuTransform.DOLocalMoveX(LENGTH * -1, DURATION).SetRelative().SetEase(EASE);
            } else {
                newMenuTransform.DOLocalMoveX(LENGTH, DURATION).SetRelative().SetEase(EASE);
                currentMenuTransform.DOLocalMoveX(LENGTH, DURATION).SetRelative().SetEase(EASE);
            }

            //ここでキャッシュしておかないと、この後currentMenuは中身が入れ替わっているため新しいメニューが破壊されてしまう
            var currentMenuObject = currentMenu_.gameObject;
            //新しいメニューで現在のメニューを入れ替え
            //このタイミング(待機前)で入れ替えないと、待機中は連打しても1つ前のメニューの前後のメニューしかでてこないので注意
            var newMenu = newMenuTransform.GetComponent<DoublyLinkedMenu>();
            currentMenu_ = newMenu;
            //現在のメニューが画面外に行くまで待機
            yield return new WaitForSeconds(DURATION);
            //移動後、現在のメニューのオブジェクトを破壊
            Destroy(currentMenuObject);
        }

        /// <summary>
        /// 前のメニューが存在するか(最初のメニュー「でない」か)
        /// </summary>
        /// <returns></returns>
        public bool hasPrev() {
            return prevMenu_ != null;
        }

        /// <summary>
        /// 後のメニューが存在するか(最後のメニュー「でない」か)
        /// </summary>
        /// <returns></returns>
        public bool hasNext() {
            return nextMenu_ != null;
        }
    }
}
