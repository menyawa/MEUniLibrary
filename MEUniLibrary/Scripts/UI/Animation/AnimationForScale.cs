using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

namespace MEUniLibrary.UI.Animation {
    /// <summary>
    /// 複数のオブジェクト(1つでもよい)のスケールを拡大する・縮小するクラス
    /// </summary>
    public class AnimationForScale : PluralObjectAnimationBase {
        [SerializeField] private List<Transform> transformList_;
        //開始をこの時間分遅延させる
        [SerializeField] private float firstDelay_;
        //拡大方向
        private enum AXIS {
            X, 
            Y, 
            Z, 
            ALL
        }
        [SerializeField] private AXIS axis_;

        private List<Vector3> initialScaleList_; 

        private void Start() {
            //最初のスケールを取っておく(リセット時のため)
            initialScaleList_ = new List<Vector3>(transformList_.Count());
            foreach (var transform in transformList_) {
                initialScaleList_.Add(transform.localScale);
            }
        }

        /// <summary>
        /// セットされたリストのオブジェクトを全て渡されたスケールに拡大する
        /// デフォルト引数を入れることで引数なしのメソッドを兼ねている(引数なしの場合(x, y, z) = (1, 1, 1)になる)
        /// </summary>
        /// <param name="endValue"></param>
        public void expand(float endValue = 1.0f) {
            var endScale = new Vector3(endValue, endValue, endValue);
            expand(endScale, transformList_.Count());
        }

        /// <summary>
        /// セットされたリストのオブジェクトを全て渡されたスケールに拡大する
        /// xyz全て同じスケールにするわけではない場合にはこちらを使う
        /// </summary>
        /// <param name="endScale"></param>
        public void expand(Vector3 endScale) {
            expand(endScale, transformList_.Count());
        }

        /// <summary>
        /// セットされたリストのオブジェクトを指定された個数渡されたスケールに拡大する
        /// デフォルト引数はコンパイル時定数にする必要があるので、メソッドを一つにまとめることはできない
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="length"></param>
        public void expand(float scale, int length) {
            var endValue = new Vector3(scale, scale, scale);
            expand(endValue, length);
        }

        /// <summary>
        /// セットされたリストのオブジェクトを指定された個数渡されたスケールに拡大する
        /// デフォルト引数はコンパイル時定数にする必要があるので、メソッドを一つにまとめることはできない
        /// </summary>
        /// <param name="endScale"></param>
        /// <param name="length"></param>
        public void expand(Vector3 endScale, int length) {
            //渡されたスケールを初期値としてオブジェクトのTransformのリスト数と同じリストを作成、渡す
            expand(Enumerable.Repeat(endScale, transformList_.Count()).ToList(), length);
        }

        /// <summary>
        /// セットされたリストのオブジェクトを指定された個数渡されたスケールのリストに拡大する
        /// </summary>
        /// <param name="endScaleList"></param>
        /// <param name="length"></param>
        public void expand(List<Vector3> endScaleList, int length) {
            initSequence();
            sequence_.AppendInterval(firstDelay_);

            //指定された個数、指定された追加法(終わった後か、前のアニメーションと同時か)でスケールを弄るアニメーションを追加する
            //当初は指定された軸の分岐を加えるとネストが深くなる(あとめんどくさい)ため行なわず、指定軸以外のスケールを1にして対応しようとした
            //だがそれだと縮小で元に戻す際に軸が指定できず、元のスケールに戻せなかった
            //そのため軸設定も追加
            for (int index = 0; index < length; index++) {
                switch (type_) {
                    case TYPE.IN_ORDER:
                        switch (axis_) {
                            case AXIS.X:
                                sequence_.Append(transformList_[index].DOScaleX(endScaleList[index].x, duration_).SetEase(Ease.InOutBack));
                                break;
                            case AXIS.Y:
                                sequence_.Append(transformList_[index].DOScaleY(endScaleList[index].y, duration_).SetEase(Ease.InOutBack));
                                break;
                            case AXIS.Z:
                                sequence_.Append(transformList_[index].DOScaleZ(endScaleList[index].z, duration_).SetEase(Ease.InOutBack));
                                break;
                            case AXIS.ALL:
                                sequence_.Append(transformList_[index].DOScale(endScaleList[index], duration_).SetEase(Ease.InOutBack));
                                break;
                        }
                        break;
                    case TYPE.SAME_TIME:
                        switch (axis_) {
                            case AXIS.X:
                                sequence_.Join(transformList_[index].DOScaleX(endScaleList[index].x, duration_).SetEase(Ease.InOutBack));
                                break;
                            case AXIS.Y:
                                sequence_.Join(transformList_[index].DOScaleY(endScaleList[index].y, duration_).SetEase(Ease.InOutBack));
                                break;
                            case AXIS.Z:
                                sequence_.Join(transformList_[index].DOScaleZ(endScaleList[index].z, duration_).SetEase(Ease.InOutBack));
                                break;
                            case AXIS.ALL:
                                sequence_.Join(transformList_[index].DOScale(endScaleList[index], duration_).SetEase(Ease.InOutBack));
                                break;
                        }
                        break;
                }
            }
            play();
        }

        /// <summary>
        /// オブジェクト群を最小化する
        /// </summary>
        public void minimize() {
            //0に拡大するとすれば、すなわち最小化となる
            //指定した軸で拡大は行われるので、関係ない軸のスケールが最小化されてしまったり、それにより次回以降の拡大動作がおかしくなることはない
            expand(0.0f);
        }

        /// <summary>
        /// オブジェクト群を指定された個数最小化する
        /// </summary>
        public void minimize(int length) {
            //0に拡大するとすれば、すなわち最小化となる
            //指定した軸で拡大は行われるので、関係ない軸のスケールが最小化されてしまったり、それにより次回以降の拡大動作がおかしくなることはない
            expand(0.0f, length);
        }

        /// <summary>
        /// 全てのオブジェクトのスケールを初期値にリセットする
        /// </summary>
        public void reset() {
            expand(initialScaleList_, transformList_.Count());
        }

        /// <summary>
        /// 渡された個数のオブジェクトのスケールを初期値にリセットする
        /// </summary>
        public void reset(int length) {
            expand(initialScaleList_, length);
        }

        /// <summary>
        /// 最初の遅延を設定
        /// インスペクタから設定できるようにするため(シリアライズ化するため)プロパティではなくセッターをメソッドとして作成
        /// まあプロパティ別に作れば良いんだけども…わかりやすいよねこの方が
        /// </summary>
        /// <param name="delay"></param>
        public void setFirstDelay(float delay) {
            firstDelay_ = delay;
        }
    }
}
