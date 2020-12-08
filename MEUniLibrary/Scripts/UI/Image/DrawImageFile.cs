using System.IO;
using UnityEngine;

namespace MEUniLibrary.UI.Image {
    /// <summary>
    /// 画像ファイルを指定された画像に描画するクラス
    /// </summary>
    public class DrawImageFile : MonoBehaviour {
        [SerializeField] private UnityEngine.UI.Image image_;
        [SerializeField] private string filePath_;

        /// <summary>
        /// 事前に指定されたパスの画像を描画する
        /// </summary>
        public void draw() {
            var imageFileBytes = File.ReadAllBytes(filePath_);
            var texture = new Texture2D(0, 0);
            texture.LoadImage(imageFileBytes);

            var imageFileRect = new Rect(0, 0, texture.width, texture.height);
            var imagePivot = new Vector2(0f, 0f);
            image_.sprite = Sprite.Create(texture, imageFileRect, imagePivot);
        }

        /// <summary>
        /// 引数で渡されたパスの画像を描画する
        /// </summary>
        /// <param name="filePath"></param>
        public void draw(string filePath) {
            var imageFileBytes = File.ReadAllBytes(filePath);
            var texture = new Texture2D(0, 0);
            texture.LoadImage(imageFileBytes);

            var imageFileRect = new Rect(0, 0, texture.width, texture.height);
            var imagePivot = new Vector2(0f, 0f);
            image_.sprite = Sprite.Create(texture, imageFileRect, imagePivot);
        }
    }
}
