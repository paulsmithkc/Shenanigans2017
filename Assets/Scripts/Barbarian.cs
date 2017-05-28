using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : Hero
{
	protected override float getPointsForProperty (string property)
	{
        float points;
		switch (property) {
			case "Beer":
			case "Strength-Increasing" :
				points = MAXIMUM_PURCHASE_POINTS;
				break;
			case "Axe":
			case "Sword":
				points = MAXIMUM_PURCHASE_POINTS * .85f;
				break;
			case "Chocolate":
			case "Vanilla":
			case "Caramel":
				points = MAXIMUM_PURCHASE_POINTS * .5f;
				break;
			case "Stupidity":
				points = MAXIMUM_PURCHASE_POINTS * .333f;
				break;
			case "Hummus":
			case "Hemlock":
				points = MAXIMUM_PURCHASE_POINTS * -.5f;
				break;
			case "Rusty":
			case "Transparent":
			case "Artificial":
				points = MAXIMUM_PURCHASE_POINTS * -.333f;
				break;
			default :
				points = base.getPointsForProperty (property);
				break;
		}
		return points;
	}
}
