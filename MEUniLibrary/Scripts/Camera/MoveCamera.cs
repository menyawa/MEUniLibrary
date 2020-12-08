using UnityEngine;

namespace MEUniLibrary.Camera {
    /// <summary>
    /// カメラを移動させるクラス
    /// </summary>
    public class MoveCamera : MonoBehaviour {
        [SerializeField] private float sensitivity_;
        [SerializeField] private float maxLimit_;
        [SerializeField] private float minLimit_;

        private void Update() {
            bool draggingMouseWheel = Input.GetMouseButton(2);
            if (draggingMouseWheel) {
                move();
            }
        }

        /// <summary>
        /// カメラを移動させる
        /// </summary>
        private void move() {
            var editCameraTransform = UnityEngine.Camera.main.transform;
            Vector3 pos = editCameraTransform.position;

            //マウスのXY移動を取得、制限をかけつつカメラを移動させる
            pos.x = Mathf.Clamp(pos.x - Input.GetAxis("Mouse X") * sensitivity_, minLimit_, maxLimit_);
            pos.y = Mathf.Clamp(pos.y - Input.GetAxis("Mouse Y") * sensitivity_, minLimit_, maxLimit_);
            editCameraTransform.position = pos;
        }
    }
}
