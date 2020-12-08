using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MEUniLibrary.UI.Animation {
    /// <summary>
    /// 少しの間画像を画面に表示するクラス
    /// </summary>
    public class DisplayImageForFewSeconds : MonoBehaviour {
        [SerializeField] private List<Sprite> sprites_;
        [SerializeField] protected float duration_;

        /// <summary>
        /// 指定した画像を一定時間(事前に設定した時間)表示する
        /// 指定しない場合は表示する画像は自動的に0とする(対象画像が1枚の場合指定しない)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IEnumerator display(int index = 0) {
            var image = gameObject.GetComponent<UnityEngine.UI.Image>(); //一回なので動的取得でも実行速度的に事足りる
            image.sprite = sprites_[index];
            //一定時間表示した後見えなくする
            yield return new WaitForSeconds(duration_);
            image.gameObject.SetActive(false);
        }

        /// <summary>
        /// 指定した画像を一定時間(引数で指定された時間)表示する
        /// 指定しない場合は表示する画像は自動的に0とする(対象画像が1枚の場合指定しない)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IEnumerator display(float duration, int index = 0) {
            var image = gameObject.GetComponent<UnityEngine.UI.Image>(); //一回なので動的取得でも実行速度的に事足りる
            image.sprite = sprites_[index];
            //一定時間表示した後見えなくする
            yield return new WaitForSeconds(duration);
            image.gameObject.SetActive(false);
        }
    }
}
