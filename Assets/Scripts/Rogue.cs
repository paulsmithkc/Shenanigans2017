using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : Hero
{
	protected override double getPointsForProperty (string property)
	{
		double points;
		switch (property) {
		case "Hemp":
		case "Poppy Seed":
			points = MAXIMUM_PURCHASE_POINTS;
			break;
		case "Chocolate":
		case "Gerbil":
		case "Waffle":
			points = MAXIMUM_PURCHASE_POINTS * .5;
			break;
		case "Chicken":
		case "Blueberry":
			points = -1 * MAXIMUM_PURCHASE_POINTS * .5;
			break;
		case "Invisibility":
			points = MAXIMUM_PURCHASE_POINTS * .666;
			break;
		case "Synthetic":
		case "Chewable":
			points = MAXIMUM_PURCHASE_POINTS * .7;
			break;
		case "Vegan":
		case "Organic":
			points = -1 * MAXIMUM_PURCHASE_POINTS * .7;
			break;
		case "Transparent":
			points = MAXIMUM_PURCHASE_POINTS * .8;
			break;
		case "Rusty":
		case "Shiny":
			points = -1 * MAXIMUM_PURCHASE_POINTS * .8;
			break;
		default :
			points = base.getPointsForProperty (property);
			break;
		}
		return points;
	}
}
