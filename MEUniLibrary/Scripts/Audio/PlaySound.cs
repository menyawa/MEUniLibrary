using UnityEngine;
using DG.Tweening;

namespace MEUniLibrary.Audio {
    /// <summary>
    /// 効果音、あるいはBGMを鳴らすためのクラス
    /// シングルトンにしているので同じシーン内なら参照をどこのクラスからも持ってこれる
    /// </summary>
    public class PlaySound : SingletonMonoBehaviour<PlaySound> {
        [SerializeField] private AudioSource audioSource_;

        /// <summary>
        /// 一回音を鳴らす(主に効果音用)
        /// </summary>
        /// <param name="sound"></param>
        public void playSoundOnce(AudioClip sound) {
            audioSource_.PlayOneShot(sound);
        }

        /// <summary>
        /// 現在のBGMをフェードアウトし、新規にBGMをかける(必要に応じてフェードアウト開始前にディレイも入れられる)
        /// DOFadeを使う関係上、DOTweenが必要なので注意
        /// </summary>
        /// <param name="bgm"></param>
        /// <param name="delay"></param>
        public void playBGM(AudioClip bgm, float delay = 0.0f) {
            const float DURATION = 0.3f;

            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(delay);
            sequence.Append(audioSource_.DOFade(0f, DURATION));
            sequence.AppendCallback(() => audioSource_.clip = bgm);
            sequence.Append(audioSource_.DOFade(1.0f, DURATION));
            sequence.Play();
        }
    }
}
