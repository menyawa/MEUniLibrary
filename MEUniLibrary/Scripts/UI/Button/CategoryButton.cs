using MEUniLibrary.UI.Animation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// SlidePointToPointUIを用いて対象のカテゴリのUIを出すボタンのクラス
    /// カテゴリを全部まとめて１グループとして扱うことに注意
    /// 選択中のグループが１つしか取れないので、違うグループのカテゴリ群にこのクラスを用いることは出来ない
    /// </summary>
    public class CategoryButton : ButtonBase {
        //このボタンに属するカテゴリのSlidePointToPointUI
        [SerializeField] private AnimationForSlideByPointToPoint slidePointToPointCategory_;
        //現在選択中のカテゴリグループのSlidePointToPointUI
        public static AnimationForSlideByPointToPoint slidePointToPointSelectGroup_ { private set; get; }

        static CategoryButton() {
            //Startの前に一応選択グループをリセットしておく
            SceneManager.sceneLoaded += resetSelectGroup;
        }

        new private void Start() {
            base.Start();
            button_.onClick.AddListener(switchCategory);
        }

        private void switchCategory() {
            //既に出ているボタンが押したボタンのカテゴリ→今出ているボタンをしまう
            //既に任意のカテゴリのボタンが出ていて別のカテゴリのボタンを押す→今出ているボタンをしまい、押したボタンのカテゴリのボタンを出す
            //まだどのカテゴリのボタンも出ていない→押したボタンのカテゴリを出す
            if (slidePointToPointSelectGroup_ != null) {
                if (slidePointToPointSelectGroup_ == slidePointToPointCategory_) {
                    slideSelectGroup(true, true);
                } else {
                    slideSelectGroup(true);
                    slidePointToPointCategory_.slide(false);
                    slidePointToPointSelectGroup_ = slidePointToPointCategory_;
                }
            } else {
                    slidePointToPointCategory_.slide(false);
                slidePointToPointSelectGroup_ = slidePointToPointCategory_;
            }
        }

        /// <summary>
        /// 選択グループを空にする
        /// </summary>
        /// <param name="loadingScene"></param>
        /// <param name="loadSceneMode"></param>
        private static void resetSelectGroup(Scene loadingScene, LoadSceneMode loadSceneMode) {
            slidePointToPointSelectGroup_ = null;
        }

        /// <summary>
        /// 現在選択中のグループカテゴリのUIをスライドさせ、指定によっては選択グループを空にする
        /// </summary>
        /// <param name="isDisplayed"></param>
        /// <param name="clearSelectGroup"></param>
        public static void slideSelectGroup(bool isDisplayed, bool clearSelectGroup = false) {
            if (slidePointToPointSelectGroup_ != null) slidePointToPointSelectGroup_.slide(isDisplayed);
            //引っ込める場合でも、グループを選択したままにする事があることに注意
            if (clearSelectGroup) slidePointToPointSelectGroup_ = null;
        }
    }
}