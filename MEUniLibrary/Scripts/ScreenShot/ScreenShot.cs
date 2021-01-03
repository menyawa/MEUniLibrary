using DG.Tweening;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MEUniLibrary.ScreenShot {
    /// <summary>
    /// クリア時のスクリーンショットのクラス
    /// </summary>
    public class ScreenShot : MonoBehaviour, IPointerClickHandler {
        //画像のフォトフレーム
        [SerializeField] private Image photoFrameImage_;
        //ズームした際のサイズ
        [SerializeField] private Vector3 zoomScale_;

        //キャッシュ用の最初の座標・角度・スケール
        private Vector3 firstPos_;
        private Vector3 firstRotation_;
        private Vector3 firstScale_;

        private void Start() {
            firstPos_ = transform.localPosition;
            firstRotation_ = transform.localEulerAngles;
            firstScale_ = transform.localScale;
        }

        /// <summary>
        /// スクリーンショットを撮り、保存してパスを返す
        /// </summary>
        /// <returns></returns>
        public string capture(string imageTitle) {
            var timeStamp = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            var directoryPath = Application.dataPath + "/ScreenShots/";
            var title = imageTitle + "_" + timeStamp + ".png";
            var filePath = directoryPath + title;
            //ディレクトリが無かったら作成しておく
            if (Directory.Exists(directoryPath) == false) {
                Directory.CreateDirectory(directoryPath);
            }
            ScreenCapture.CaptureScreenshot(filePath);

            Debug.Log("Capture ScreenShot!");
            return filePath;
        }

        /// <summary>
        /// スクリーンショットを描画する
        /// </summary>
        /// <param name="filePath"></param>
        public void draw(string filePath) {
            //ファイルを読み取ってスプライトを作成し、動的に取得したこのImageに反映する
            //動的取得は1回のみなのでパフォーマンスに心配はない
            var image = File.ReadAllBytes(filePath);
            var texture = new Texture2D(0, 0);
            texture.LoadImage(image);

            var imageRect = new Rect(0, 0, texture.width, texture.height);
            var imagePivot = Vector2.zero;

            var targetImage = GetComponent<Image>();
            targetImage.sprite = Sprite.Create(texture, imageRect, imagePivot);
        }

        /// <summary>
        /// ズームの状態を切り替える
        /// </summary>
        private void switchZoom() {
            //写真のフレームを透明にフェード→中心の位置・無回転で拡大した状態にする
            //または元の状態に戻す
            var sequence = DOTween.Sequence();
            const float DURATION = 0.5f;
            var alphaEndValue = photoFrameImage_.color.a == 1.0f ? 0.0f : 1.0f;
            sequence.Append(photoFrameImage_.DOFade(alphaEndValue, DURATION));
            var posEndValue = transform.localPosition == firstPos_ ? Vector3.zero : firstPos_;
            sequence.Join(transform.DOLocalMove(posEndValue, DURATION));
            var endRotation = transform.localEulerAngles == firstRotation_ ? Vector3.zero : firstRotation_;
            sequence.Join(transform.DOLocalRotate(endRotation, DURATION));
            var endScale = transform.localScale == firstScale_ ? zoomScale_ : firstScale_;
            sequence.Join(transform.DOScale(endScale, DURATION));
        }

        public void OnPointerClick(PointerEventData pointerEventData) {
            //クリックした際にズームを切り替える
            switchZoom();
        }
    }
}
