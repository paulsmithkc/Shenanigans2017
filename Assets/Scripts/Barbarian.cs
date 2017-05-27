using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : Character {
	protected override double getPointsForProperty (string property)
	{
		double points;
		switch (property) {
			case "Beer":
			case "Strength-Increasing" :
				points = MAXIMUM_PURCHASE_POINTS;
				break;
			case "Axe":
			case "Sword":
				points = MAXIMUM_PURCHASE_POINTS * .85;
				break;
			case "Chocolate":
			case "Vanilla":
			case "Caramel":
				points = MAXIMUM_PURCHASE_POINTS * .5;
				break;
			case "Stupidity":
				points = MAXIMUM_PURCHASE_POINTS * .333;
				break;
			case "Hummus":
			case "Hemlock":
				points = -1 * MAXIMUM_PURCHASE_POINTS * .5;
				break;
			case "Rusty":
			case "Transparent":
			case "Artificial":
				points = -1 * MAXIMUM_PURCHASE_POINTS * .333;
				break;
			default :
				points = base.getPointsForProperty (property);
				break;
		}
		return points;
	}
}
