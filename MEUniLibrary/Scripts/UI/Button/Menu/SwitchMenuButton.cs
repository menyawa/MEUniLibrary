using MEUniLibrary.UI.Animation;
using UnityEngine;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// 背景にUIが必要なメニューを新たに生成するボタンのクラス
    /// </summary>
    public class SwitchMenuButton : SwitchOpeningAndClosingOfUIButtonBase {
        //メニューの元のプレハブ
        [SerializeField] private GameObject menuPrefab_;
        //メニューの親オブジェクトのTransform
        [SerializeField] private Transform menuParentTransorm_;

        new private void Start() {
            base.Start();
            button_.onClick.AddListener(switchMenu);
        }

        private void switchMenu() {
            const float DURATION = 0.2f;

            switch (type_) {
                case TYPE.OPEN:
                    //メニューを設定された親の子で生成して拡大し、開く
                    var menuObject = Instantiate(menuPrefab_, Vector3.zero, Quaternion.identity, menuParentTransorm_);
                    //Instantiateによる位置ずれバグも考え、再代入して調整する
                    menuObject.transform.localPosition = Vector3.zero;
                    menuObject.GetComponent<AnimationForScale>().expand();
                    break;
                case TYPE.CLOSE:
                    transform.parent.GetComponent<AnimationForScale>().minimize();

                    //メニューを生成するとすると、閉じる際BackgroundPanelByMenuが探せないのでは？

                    break;
            }
        }
    }
}
