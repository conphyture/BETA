﻿using UnityEngine;
using System.Collections;
using LSL;
using System.Threading;
using System.Collections.Generic;


public class LSLControllerBreathe : MonoBehaviour {

	private float[] sample = new float[2];

	float realTime = 0;
	float lastTime = 0;
	
	liblsl.StreamInlet inlet = null;
	
	private Thread dataThread;
	private bool finished = false;
	
	public float Breathe = 0; // Between 0 and 1

	public int maxWindowSize = 10;

	//Timer

	List<float> listMax = new List<float>();
	List<float> listMin = new List<float>();
	
	// Use this for initialization
	void Start () {
		
		dataThread = new Thread(new ThreadStart(GetData));
		dataThread.Start();

		listMax.Add(0.0f);
		listMin.Add(0.0f);

		lastTime = Time.realtimeSinceStartup;
		realTime = Time.realtimeSinceStartup;
	}
	
	void GetData()
	{
		while(!finished)
		{
			if (inlet != null)
			{
				inlet.pull_sample(sample, 0.5f);

				if (listMax.Count < maxWindowSize)
				{
					if (listMax.Count < (realTime - lastTime))
					{
						listMax.Add(float.MinValue);
					}
					if (listMin.Count < (realTime - lastTime))
					{
						listMin.Add(float.MaxValue);
					}
				}
				else
				{
					if (lastTime < (realTime - 1.0f))
					{
						lastTime = realTime;
						listMax.RemoveAt(0);
						listMin.RemoveAt(0);
						listMax.Add(float.MinValue);
						listMin.Add(float.MaxValue);
					}
				}


				int currentItem = listMax.Count;
				Debug.Log(currentItem);
				if (sample[0] > listMax[currentItem-1])
				{
					listMax[currentItem-1] = sample[0];
				}
				if (sample[0] < listMin[currentItem-1])
				{
					listMin[currentItem-1] = sample[0];
				}

				float min = float.MaxValue;
				float max = float.MinValue;
				foreach(float element in listMin)
				{
					if (element < min)
					{
						min = element;
					}
				}
				foreach(float element in listMax)
				{
					if (element > max)
					{
						max = element;
					}
				}

				Breathe = (sample[0]-min) / (max-min);
				//Debug.Log (Breathe);
			}
			else
			{
				// wait until an EEG stream shows up
				liblsl.StreamInfo[] results = liblsl.resolve_stream("type", "breath", 1, 0.5f);
				
				// open an inlet and print some interesting info about the stream (meta-data, etc.)
				inlet = new liblsl.StreamInlet(results[0]);
				Debug.Log(inlet.info().as_xml());

				inlet.pull_sample(sample, 0.5f);

				listMax[0] = sample[0];
				listMin[0] = sample[0];
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		realTime = Time.realtimeSinceStartup;
	}
	
	void OnApplicationQuit() {
		
		finished = true;
	}
}
