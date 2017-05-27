using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Character {
	protected override double getPointsForProperty (string property)
	{
		double points;
		switch (property) {
		case "Potion":
			points = MAXIMUM_PURCHASE_POINTS;
			break;
		case "Elixir":
			points = MAXIMUM_PURCHASE_POINTS * .8;
			break;
		case "Cherry":
		case "Blueberry":
		case "Chicken":
			points = MAXIMUM_PURCHASE_POINTS * .5;
			break;
		case "Wisdom":
			points = MAXIMUM_PURCHASE_POINTS * .333;
			break;
		case "Gerbil":
		case "Arsenic":
		case "Hemlock":
			points = -1 * MAXIMUM_PURCHASE_POINTS;
			break;
		case "Pork":
			points = -1 * MAXIMUM_PURCHASE_POINTS * .5;
			break;
		case "Rusty":
			points = -1 * MAXIMUM_PURCHASE_POINTS * .6;
			break;
		case "Shiny":
		case "Transparent":
			points = MAXIMUM_PURCHASE_POINTS * .333;
			break;
		default :
			points = base.getPointsForProperty (property) * .7;
			break;
		}
		return points;
	}
	
}
