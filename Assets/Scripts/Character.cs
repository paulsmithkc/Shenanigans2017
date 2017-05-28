using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

	protected const double MAXIMUM_PURCHASE_POINTS = 5.0;
	private const double STARTING_VALUE = 1.0;
	private const double BASE_NO_SALE_CHANCE = 1.0;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	/// <summary>
	/// Buys the item.
	/// Current appends a frequence range for each item 
	/// based on its relative value, then randomly selects from the resulting distribution.
	/// </summary>
	/// <returns>The item.</returns>
	public Item buyItem()
	{
		Item selectedItem = null;

		var counter = (SalesCounter)GameObject.FindObjectOfType(typeof(SalesCounter));
		var distribution = new List<Item> ();
		var items = counter.items;

		foreach (var item in items)
		{
			var frequency = (int)CalculatePurchasePoints (item);

			if (frequency >= BASE_NO_SALE_CHANCE) {
				distribution.AddRange (System.Linq.Enumerable.Repeat (item, frequency));
			}
		}

		if (distribution.Count > 0) {
			selectedItem = distribution[Random.Range(0, distribution.Count)];
		}

		return selectedItem;
	}

	public virtual double CalculatePurchasePoints(Item item) {
		var points = STARTING_VALUE;
		var properties = item.itemTag.itemName.Split (new char[]{' '});

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
