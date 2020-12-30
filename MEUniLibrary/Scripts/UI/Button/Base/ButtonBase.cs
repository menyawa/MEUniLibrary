using UnityEngine;
using MEUniLibrary.Audio;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// 全てのボタンの基底クラス
    /// </summary>
    public abstract class ButtonBase : MonoBehaviour {
        [SerializeField] protected UnityEngine.UI.Button button_;
        [SerializeField] private AudioClip se_;

        protected void Start() {
            button_.onClick.AddListener(playSound);
        }

        /// <summary>
        /// 登録されたSEを鳴らす
        /// </summary>
        private void playSound() {
            if (se_ != null)
                GameObject.Find(AudioController.objectName_).GetComponent<AudioController>().playSoundOnce(se_);
        }
    }
}
