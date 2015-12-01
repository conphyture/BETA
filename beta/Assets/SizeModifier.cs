using UnityEngine;
using System.Collections;

public class SizeModifier : MonoBehaviour {

	public GameObject objectToTransform = null;
    public GameObject lightToTransform;
	public LSLControllerBreathe controller = null;
    private ParticleSystem particleSys;
    private Light lightObject;
    private float baseEmission;
    private float baseIntensity;
    public float emissionScale;
    public float intensityScale;

	// Use this for initialization
	void Start () {
        if (objectToTransform)
        {
            particleSys = objectToTransform.GetComponent<ParticleSystem>();
            if (particleSys)
                baseEmission = particleSys.emissionRate;
        }
        if (lightToTransform)
        {
            lightObject = lightToTransform.GetComponent<Light>();
            if (lightObject)
                baseIntensity = lightObject.intensity;
        }
	}
	
	// Update is called once per frame
	void Update () {
		float scale = controller.Breathe;
		objectToTransform.transform.localScale = new Vector3(scale, scale, scale);
        particleSys.emissionRate = Mathf.Lerp(baseEmission, emissionScale * baseEmission, scale);
        lightObject.intensity = Mathf.Lerp(baseIntensity, intensityScale * baseIntensity, scale);
	}
}
