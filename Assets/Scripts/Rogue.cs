using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : Hero
{
	protected override float getPointsForProperty (string property)
	{
        float points;
		switch (property) {
		case "Hemp":
		case "Poppy Seed":
			points = MAXIMUM_PURCHASE_POINTS;
			break;
		case "Chocolate":
		case "Gerbil":
		case "Waffle":
			points = MAXIMUM_PURCHASE_POINTS * .5f;
			break;
		case "Chicken":
		case "Blueberry":
			points = MAXIMUM_PURCHASE_POINTS * -.5f;
			break;
		case "Invisibility":
			points = MAXIMUM_PURCHASE_POINTS * .666f;
			break;
		case "Synthetic":
		case "Chewable":
			points = MAXIMUM_PURCHASE_POINTS * .7f;
			break;
		case "Vegan":
		case "Organic":
			points = MAXIMUM_PURCHASE_POINTS * -.7f;
			break;
		case "Transparent":
			points = MAXIMUM_PURCHASE_POINTS * .8f;
			break;
		case "Rusty":
		case "Shiny":
			points = MAXIMUM_PURCHASE_POINTS * -.8f;
			break;
		default :
			points = base.getPointsForProperty (property);
			break;
		}
		return points;
	}
}
