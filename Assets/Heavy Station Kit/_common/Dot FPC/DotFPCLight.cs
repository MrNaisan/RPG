using UnityEngine;
using System.Collections;

public class DotFPCLight : MonoBehaviour {

	private Light _light=null;
    private DotControlCenter ccInstance = null;
    private KeyCode flashlightShortcut = KeyCode.L;

    void Start () {
		_light = GetComponent<Light>();
        // Update cofiguration 
        if (DotControlCenter.instance != null) {
            if (DotControlCenter.instance.trackChangesSettings) { ccInstance = DotControlCenter.instance; };
            UpdateConfig(DotControlCenter.instance);
        }
    }

    void Update () {
		if ( _light != null) { 
           if (ccInstance != null) { UpdateConfig(ccInstance); }
           if (Input.GetKeyDown (flashlightShortcut)) {
               _light.enabled = !_light.enabled;
            }
		}	
	}

    void UpdateConfig(DotControlCenter c) {
        flashlightShortcut = c.flashlightShortcut;
    }
}
