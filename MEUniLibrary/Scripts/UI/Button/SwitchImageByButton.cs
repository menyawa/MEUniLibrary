﻿using UnityEngine;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// ボタンの画像を切り替えるボタンの親クラス
    /// </summary>
    public  class SwitchImageByButton : ButtonBase {
        [SerializeField] private UnityEngine.UI.Image image_;
        [SerializeField] private Sprite nomalSprite_;
        [SerializeField] private Sprite pressedSprite_;

        new protected void Start() {
            base.Start();
            button_.onClick.AddListener(switchImage);
        }

        /// <summary>
        /// 画像を切り替える
        /// </summary>
        public void switchImage() {
            image_.sprite = image_.sprite == nomalSprite_ ? pressedSprite_ : nomalSprite_;
        }

        /// <summary>
        /// 画像をリセットする
        /// </summary>
        public void resetImage() {
            image_.sprite = nomalSprite_;
        }
    }
}
