using MEUniLibrary.UI.Animation;
using UnityEngine;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// メニューを開く・閉じる際に背景パネルを制御するボタンの親の抽象クラス
    /// スライド、ポップアップ等の各アニメーションクラスは必ずしもメニューを出すと決まっているわけではない
    /// そのようなクラス群にメニューを出す際専用のメンバや処理を記述するのは好ましくなく、かといって継承で対応するのもクラスが増えてしまう
    /// それならボタンを作ってしまえば、とりあえずボタンで開く際はこれ一本で対応できる
    /// ボタンを経由しない時は…そのときにその呼出側のクラスで制御すればいいでしょ
    /// </summary>
    public abstract class FadeBackgroundPanelBySwitchOpeningMenuButton : SwitchOpeningAndClosingOfUIButtonBase {
        [SerializeField] private BackgroundPanelByMenu backgroundPanelByMenu_;

        protected void fadeBackgroundPanel() {
            switch (type_) {
                case TYPE.OPEN:
                    //開いた上でメニュー数をプラスしないと、最初のメニューなのに他のメニューが開いていると判断され背景パネルが開かないことに注意
                    if (backgroundPanelByMenu_.menuExists() == false)
                        backgroundPanelByMenu_.switchPanel(true);
                    backgroundPanelByMenu_.plusMenuCount();
                    break;
                case TYPE.CLOSE:
                    //開く際と手順が逆なことに注意(カウントを減らす前にメニューが開いているか調べると、これが最後のメニューでも閉じられなくなってしまう)
                    //メニュー数を減らした上で数が0になったかどうか確かめ、0だったら背景パネルを閉じる
                    backgroundPanelByMenu_.minusMenuCount();
                    if (backgroundPanelByMenu_.menuExists() == false)
                        backgroundPanelByMenu_.switchPanel(false);
                    break;
            }
        }
    }
}
