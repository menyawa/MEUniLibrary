using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace MEUniLibrary.Audio {
    /// <summary>
    /// BGM、あるいはSEを鳴らすためのクラス
    /// 動的取得の関係上AudioSourceを付加したオブジェクトに付けるのが前提なので注意
    /// </summary>
    public class AudioController : MonoBehaviour {
        //動的取得のためのキー
        //定数にせずAwakeで代入することで、修正し忘れを防ぐ
        //BGM・SEどちらもそう連続して鳴らす機会はほぼ無いため、動的取得でも十分間に合う
        public static string objectName_ { private set; get; }

        private void Awake() {
            objectName_ = gameObject.name;
        }

        /// <summary>
        /// 一回音を鳴らす(主に効果音用)
        /// </summary>
        /// <param name="sound"></param>
        public void playSoundOnce(AudioClip sound) {
            GetComponent<AudioSource>().PlayOneShot(sound);
        }

        /// <summary>
        /// 現在のBGMをフェードアウトし、新規にBGMをかける(必要に応じてフェードアウト開始前にディレイも入れられる)
        /// DOFadeを使う関係上、DOTweenが必要なので注意
        /// </summary>
        /// <param name="nextBGM"></param>
        /// <param name="delay"></param>
        public void playBGM(AudioClip nextBGM, float delay = 0.0f) {
            const float DURATION = 0.3f;
            var audioSource = GetComponent<AudioSource>();
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(delay);
            sequence.Append(audioSource.DOFade(0f, DURATION));
            sequence.AppendCallback(() => audioSource.clip = nextBGM);
            sequence.Append(audioSource.DOFade(1.0f, DURATION));
            sequence.Play();
        }

        /// <summary>
        /// 今流れているBGMを止める
        /// 最初に遅延を挟むこともできる
        /// DOTweenを使わない関係上コルーチンなので注意
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        public IEnumerator stopBGM(float delay = 0.0f) {
            yield return new WaitForSeconds(delay);

            GetComponent<AudioSource>().Stop();
        }

        /// <summary>
        /// 今流れているBGMの長さを取得
        /// </summary>
        /// <returns></returns>
        public float getCurrentBGMLength() {
            var audioSource = GetComponent<AudioSource>();
            //セットされていないならメッセージを出して0秒とする
            if(audioSource.clip == null) {
                Debug.Log("BGMはセットされていません");
                return 0;
            }

            return audioSource.clip.length;
        }

        /// <summary>
        /// 現在BGMを何秒再生しているか返す
        /// </summary>
        /// <returns></returns>
        public float getCurrentBGMTime() {
            var audioSource = GetComponent<AudioSource>();
            //セットされていないならメッセージを出して0秒とする
            if (audioSource.clip == null) {
                Debug.Log("BGMはセットされていません");
                return 0;
            }

            return audioSource.time;
        }
    }
}
