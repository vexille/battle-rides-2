using Frictionless;
using UnityEngine;

namespace LuftSchloss {
	public class InputHandler : MonoBehaviour {

	    private Vector3 GetMouseWorldPosition() {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return mousePosition;
	    }        
    }
}
