using DG.Tweening;

namespace MEUniLibrary.UI.Animation {
    /// <summary>
    /// メニューの背景パネルのクラス
    /// 主な役割は最初のメニューが開いたら背景パネルをフェードインし、最後のメニューが閉じたらフェードアウトすること
    /// メニューの出し方はスライドイン、ポップアップ、フェードとたくさんある
    /// また、メニューは場合によっては二重で出すこともある
    /// さらに、各アニメーションクラスは必ずしもメニューを出すとは限らない、別のボタン類等を出すのかもしれない
    /// かといって背景パネルをそれぞれ別オブジェクトとして用意すると、メニューを何重にもするたびに背景パネルが濃くなっていくことになる
    /// そういったことを勘案し、独立でメニュー数に応じて背景パネルを制御するクラスが必要だと判断
    /// </summary>
    public class BackgroundPanelByMenu : DOTweenAnimationSequenceBase {
        //メニューが何個開いているかのカウント
        private int menuCount_;

        /// <summary>
        /// 引数で渡された状態に応じて透明度とアクティブの状態を変化する
        /// アクティブにする時、非アクティブにする時どちらもメニュー数は0なので、menuExistsではどちらなのか判断できないことに注意
        /// </summary>
        /// <param name="active"></param>
        public void switchPanel(bool active) {
            var image = GetComponent<UnityEngine.UI.Image>();
            var endValue = active ? 150f / 255f : 0.0f;
            if (active) {
                //開始前にアクティブにしないとアニメーションが見えない
                gameObject.SetActive(active);
                image.DOFade(endValue, duration_);
            } else {
                //透明にする場合、アニメーションしきってから非アクティブにしないとアニメーションが見えないので注意
                image.DOFade(endValue, duration_)
                    .OnComplete(() =>
                    gameObject.SetActive(active)
                );
            }
        }

        /// <summary>
        /// メニュー数のカウントを1プラスする
        /// </summary>
        public void plusMenuCount() {
            menuCount_++;
        }

        /// <summary>
        /// メニュー数のカウントを1マイナスする
        /// </summary>
        public void minusMenuCount() {
            menuCount_--;
        }
        
        /// <summary>
        /// メニューが存在しているか返す
        /// </summary>
        /// <returns></returns>
        public bool menuExists() {
            return menuCount_ > 0;
        }
    }
}
