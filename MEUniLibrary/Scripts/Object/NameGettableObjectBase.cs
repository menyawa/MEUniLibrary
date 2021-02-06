using UnityEngine;

namespace MEUniLibrary.Object {
    public abstract class NameGettableObjectBase : MonoBehaviour {
        public string objectName_ { 
            get {
                return gameObject.name;
            } 
        }
    }
}
