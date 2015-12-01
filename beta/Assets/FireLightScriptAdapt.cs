using UnityEngine;
using System.Collections;
using System.IO.Ports;


public class FireLightScriptAdapt : MonoBehaviour
	
	
{
	public string PortName = "COM3";
	public int Baudrate = 9600;
	public static SerialPort serial_port;
	private int value_arduino;
	// range coming out from arduino
	public float min = 0;
	public float max = 255;
	ArrayList history = new ArrayList();
	int history_size = 300; // 5 seconds at 60 fps

	
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
			history.Add(valueArduinoFloat);
			if (history.Count >= history_size) {
				history.RemoveAt(0);
			}
			float histMax = min;
			float histMin = max;
			foreach (float item in history)
			{
				if (item > histMax)
					histMax = item;
				if (item < histMin)
				histMin = item;
			}
			float valueScale = (valueArduinoFloat - histMin)/(histMax - histMin);
			print (value_arduino);
			print (valueScale);
			random = Random.Range(0.0f, 1.0f);	
			float noise = Mathf.PerlinNoise(random, Time.time);
			fireLight.GetComponent<Light>().intensity = Mathf.Lerp(0, 8, valueScale);
			particleflare.GetComponent<ParticleSystem>().startSpeed = Mathf.Lerp(0, 5,  valueScale);
		}
		//random = Random.Range(0.0f, 150.0f);
		//float noise = Mathf.PerlinNoise(random, Time.time);
		//fireLight.GetComponent<Light>().intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
	}
}