using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using O2F;

public enum ItemCategory
{
	Weapon,
	Potion,
	Shield,
	Book,
	Food
}

public class Item
{
	[TextField]
	public string name;
	[TextField(ETextFieldType.Float)]
	public float price;
	[TextField(ETextFieldType.Integer)]
	public int damage;
	[TextField(ETextFieldType.Integer)]
	public int resistance;
	[DropDown]
	public ItemCategory category;
}