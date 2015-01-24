﻿using UnityEngine;
using System.Collections;

public class CrazinessModifier : MonoBehaviour, IUseInteraction, IBreakInteraction
{
	[Range(-3, 3)]
	public int crazinessOnBreak = 0;
	[Range(-3, 3)]
	public int crazinessOnUse = 0;

	#region IBreakInteraction implementation
	public void Break ()
	{
		Player.AddCraziness (crazinessOnBreak);
	}
	#endregion

	#region IUseInteraction implementation
	public void Use ()
	{
		Player.AddCraziness (crazinessOnUse);
	}
	#endregion
}