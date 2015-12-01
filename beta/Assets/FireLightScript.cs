using UnityEngine;
using System.Collections;
using System.IO.Ports;


public class FireLightScript : MonoBehaviour


{
	public string PortName = "COM5";
	public int Baudrate = 9600;
	public static SerialPort serial_port;
	private int value_arduino;
	public float minIntensity = 0.25f;
	public float maxIntensity = 0.5f;

	public Light fireLight;
	public ParticleSystem particleflare;

	float random;
   
	void Start()
	{
		//serial_port = new SerialPort ();
		serial_port = new SerialPort("\\\\.\\" + PortName, Baudrate);
		serial_port.Open ();
		if (serial_port.IsOpen == true)                        
  		{
			Debug.Log ("YES");
		}
	}

	void Update()
	{
		if (serial_port.IsOpen)
		{
			value_arduino = serial_port.ReadByte();
			float valueArduinoFloat = (float)value_arduino * 5;
			print (value_arduino);
			random = Random.Range(0.0f, 1.0f);	
			float noise = Mathf.PerlinNoise(random, Time.time);
			fireLight.GetComponent<Light>().intensity = Mathf.Lerp(0, 8, valueArduinoFloat / 255);
			particleflare.GetComponent<ParticleSystem>().startSpeed = Mathf.Lerp(0, 5,  valueArduinoFloat / 255);
		}
		//random = Random.Range(0.0f, 150.0f);
		//float noise = Mathf.PerlinNoise(random, Time.time);
		//fireLight.GetComponent<Light>().intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
	}
}