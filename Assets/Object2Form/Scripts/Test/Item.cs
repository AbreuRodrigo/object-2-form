using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using O2F;

public enum ItemCategory
{
	Weapon,
	Armor,
	Shield,
	Potion,
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

	public Item() { }

	public Item(string name, float price, int damage, int resistance, ItemCategory category)
	{
		this.name = name;
		this.price = price;
		this.damage = damage;
		this.resistance = resistance;
		this.category = category;
	}
}