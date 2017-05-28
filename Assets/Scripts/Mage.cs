using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Hero
{
	protected override float getPointsForProperty (string property)
	{
        float points;
		switch (property) {
		case "Potion":
			points = MAXIMUM_PURCHASE_POINTS;
			break;
		case "Elixir":
			points = MAXIMUM_PURCHASE_POINTS * .8f;
			break;
		case "Cherry":
		case "Blueberry":
		case "Chicken":
			points = MAXIMUM_PURCHASE_POINTS * .5f;
			break;
		case "Wisdom":
			points = MAXIMUM_PURCHASE_POINTS * .333f;
			break;
		case "Gerbil":
		case "Arsenic":
		case "Hemlock":
			points = MAXIMUM_PURCHASE_POINTS * -1;
			break;
		case "Pork":
			points = MAXIMUM_PURCHASE_POINTS * -.5f;
			break;
		case "Rusty":
			points = MAXIMUM_PURCHASE_POINTS * -.6f;
			break;
		case "Shiny":
		case "Transparent":
			points = MAXIMUM_PURCHASE_POINTS * .333f;
			break;
		default :
			points = base.getPointsForProperty (property) * .7f;
			break;
		}
		return points;
	}
	
}
