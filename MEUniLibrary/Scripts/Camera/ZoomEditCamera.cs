using UnityEngine;

namespace MEUniLibrary.Camera {
    /// <summary>
    /// マウスホイールを回した際、カメラをズームイン・ズームアウトするクラス
    /// </summary>
    public class ZoomEditCamera : MonoBehaviour {
        //ズームの上限・下限・スピード
        [SerializeField] private float maxLimit_;
        [SerializeField] private float minLimit_;
        [SerializeField] private float speed_;

        private void Update() {
            zoom();
        }

        private void zoom() {
            var editCamera = UnityEngine.Camera.main;
            float input = Input.GetAxis("Mouse ScrollWheel");
            float changeCameraSize = editCamera.orthographicSize - input * speed_;
            editCamera.orthographicSize = Mathf.Clamp(changeCameraSize, minLimit_, maxLimit_);
        }
    }
}
