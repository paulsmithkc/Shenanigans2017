using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

	protected const double MAXIMUM_PURCHASE_POINTS = 5.0;
	private const double STARTING_VALUE = 1.0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual double CalculatePurchasePoints(ItemTag tag) {
		var points = STARTING_VALUE;
		var properties = tag.itemName.Split (new char[]{' '});

		foreach (var property in properties) {
			points += getPointsForProperty(property.Replace("-Flavored", string.Empty));
		}

		//calculate based on description also / calculate differently?

		return points;
	}

	protected virtual double getPointsForProperty(string property) {
		return 0.0;
	}
}
