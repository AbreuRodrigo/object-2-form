using System;
using System.Collections;
using System.Collections.Generic;
using O2F;

public enum CharacterClass
{
	Wizard,
	Warrior,
	Rogue,
	Priest
}

[Serializable]
public class Player
{
	[TextField(ETextFieldType.String, true)]
	public string id;

	[TextField(ETextFieldType.String)]
	public string name;

	[TextField(ETextFieldType.Integer)]
	public int life;

	[TextField(ETextFieldType.Integer)]
	public int strength;

	[TextField(ETextFieldType.Integer)]
	public int dexterity;

	[TextField(ETextFieldType.Float)]
	public float precision;

	[DropDown]
	public CharacterClass characterClass;

	[CheckBox]
	public bool controlledByBot;

	[CheckBox]
	public bool isAlive;

	[ListElement(true)]
	public List<Item> items = new List<Item>();
}