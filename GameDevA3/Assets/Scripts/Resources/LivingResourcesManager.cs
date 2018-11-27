using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class LivingResourcesManager {
	public static Text personCount;
	public static Text foodCount;
	public static Text waterCount;
	public static Text powerText;

	private static int current_people = 0;
	private static int max_people = 0;
	private static int current_food = 0;
	private static int current_water = 0;
	private static int current_power = 0;
	private static int UpkeepPerPerson = 1;
	
	public static bool GetWorkers(int amount)
	{
		if (current_people >= amount)
		{
			current_people -= amount;
			personCount.text = CurrentPersonText();
			return true;
		}
		else
		{
			return false;
		}
	}

	public static void AddWorkers(int amount)
	{
		current_people += amount;
		personCount.text = CurrentPersonText();
	}

	public static void AddLivingSpace(int amount)
	{
		max_people += amount;
		personCount.text = CurrentPersonText();
	}

	public static void AddFood(int amount)
	{
		current_food += amount;
		foodCount.text = CurrentFoodText();
	}

	public static void AddWater(int amount)
	{
		current_water += amount;
		waterCount.text = CurrentWaterText();
	}

	public static void AddPower(int amount)
	{
		current_power += amount;
		powerText.text = CurrentPowerText();
	}

	public static bool GetUpKeep(int people)
	{
		int required_amount = people * UpkeepPerPerson;
		if (required_amount <= current_food && required_amount <= current_water)
		{
			current_food -= required_amount;
			current_water -= required_amount;
			return true;
		}
		else
		{
			return false;
		}
	}

	private static void UpdateAllCounters()
	{
		personCount.text = CurrentPersonText();
		foodCount.text = CurrentFoodText();
		waterCount.text = CurrentWaterText();
		powerText.text = CurrentPowerText();
	}

	private static string CurrentPersonText()
	{
		return current_people.ToString() + "/" + max_people.ToString();
	}

	private static string CurrentFoodText()
	{
		return current_food.ToString();
	}

	private static string CurrentWaterText()
	{
		return current_water.ToString();
	}

	private static string CurrentPowerText()
	{
		return current_power.ToString();
	}
}
