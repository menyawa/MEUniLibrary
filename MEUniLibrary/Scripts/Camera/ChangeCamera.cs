using UnityEngine;

namespace MEUniLibrary.Camera {
    /// <summary>
    /// 2組のカメラを切り替えるクラス
    /// ショートカットキーで切り替える場合(他クラスからの操作でない場合)はカメラ本体に貼り付けると、非アクティブのため操作が効かなくなることに注意
    /// </summary>
    public class ChangeCamera : MonoBehaviour {
        [SerializeField] private GameObject mainCamera_;
        [SerializeField] private GameObject subCamera_;
        //ショートカットキーを使用するかどうか(Cキーをショートカットキーとして扱わない可能性を考慮)
        [SerializeField] private bool canUseShortCutKey_;

        // Update is called once per frame
        private void Update() {
            if (canUseShortCutKey_ && Input.GetKeyDown(KeyCode.C))
                change();
        }

        public void change() {
            if (mainCamera_.activeSelf) {
                subCamera_.SetActive(true);
                mainCamera_.SetActive(false);
            } else {
                mainCamera_.SetActive(true);
                subCamera_.SetActive(false);
            }
        }
    }
}