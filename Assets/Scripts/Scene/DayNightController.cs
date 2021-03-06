﻿using UnityEngine;
using System.Collections;
using System;

public class DayNightController : MonoBehaviour {
	
	const float DAY_DURATION = 30;
	public static event Action DayBegin;
	public static event Action DayEnd;

	public Camera mainCamera;
	public Color NightBeginColor;
	public Color NightMiddleColor;
	public Color NightEndColor;
	public Color DayBeginColor;
	public Color DayMiddleColor;
	public Color DayEndColor;

	private float dayRatio;
	private float time;
	private bool day = true;
	int dayPeriod = 0;

	private static DayNightController instance;

	public static float getDayRatio()
	{
		return instance.dayRatio;
	}
	public static bool isDay()
	{
		return instance.day;
	}

	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
		}
	}
	

	// Update is called once per frame
	void Update () 
	{
		if (SceneManager.isGameOver()) return;

		time += Time.deltaTime;
		if(time >= DAY_DURATION) {
			day = !day;
			time = 0;
			dayPeriod++;

//			Debug.Log ("DAY CHANGE");
			if (dayPeriod % 2 == 0)
			{
				dayPeriod = 0;
				Action handler = DayBegin;
				if (handler != null) handler();
			}
			else
			{
				Action handler = DayEnd;
				if (handler != null) handler();
			}

		}
		dayRatio = time/DAY_DURATION;
		setCameraBackgroundColor ();
	}



	private void setCameraBackgroundColor ()
	{
		Color begin;
		Color middle;
		Color end;
		if (day) {
			begin = DayBeginColor;
			middle = DayMiddleColor;
			end = DayEndColor;
		}
		else {
			begin = NightBeginColor;
			middle = NightMiddleColor;
			end = NightEndColor;
		}
		Color c;
		if (dayRatio < 0.5f) {
			c = new Color (MathHelper.Map (dayRatio, 0, 0.5f, begin.r, middle.r), 
			               MathHelper.Map (dayRatio, 0, 0.5f, begin.g, middle.g), 
			               MathHelper.Map (dayRatio, 0, 0.5f, begin.b, middle.b), 1);
		}
		else {
			c = new Color (MathHelper.Map (dayRatio, 0.5f, 1, middle.r, end.r), 
			               MathHelper.Map (dayRatio, 0.5f, 1, middle.g, end.g), 
			               MathHelper.Map (dayRatio, 0.5f, 1, middle.b, end.b), 1);
		}
		mainCamera.backgroundColor = c;
	}
}
