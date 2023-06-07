using UnityEngine;
using System.Collections.Generic;

namespace SWAssets.Utils
{
	public static class RandomUtils
	{
		#region Variables
		static List<string> firstNameMaleList = new List<string>(){"Gabe","Cliff","Tim","Ron","Jon","John","Mike","Seth","Alex","Steve","Chris","Will","Bill","James","Jim",
										"Ahmed","Omar","Peter","Pierre","George","Lewis","Lewie","Adam","William","Ali","Eddie","Ed","Dick","Robert","Bob","Rob",
										"Neil","Tyson","Carl","Chris","Christopher","Jensen","Gordon","Morgan","Richard","Wen","Wei","Luke","Lucas","Noah","Ivan","Yusuf",
										"Ezio","Connor","Milan","Nathan","Victor","Harry","Ben","Charles","Charlie","Jack","Leo","Leonardo","Dylan","Steven","Jeff",
										"Alex","Mark","Leon","Oliver","Danny","Liam","Joe","Tom","Thomas","Bruce","Clark","Tyler","Jared","Brad","Jason"};
		static List<string> cityNameList = new List<string>(){"Alabama","New York","Old York","Bangkok","Lisbon","Vee","Agen","Agon","Ardok","Arbok",
							"Kobra","House","Noun","Hayar","Salma","Chancellor","Dascomb","Payn","Inglo","Lorr","Ringu",
							"Brot","Mount Loom","Kip","Chicago","Madrid","London","Gam",
							"Greenvile","Franklin","Clinton","Springfield","Salem","Fairview","Fairfax","Washington","Madison",
							"Georgetown","Arlington","Marion","Oxford","Harvard","Valley","Ashland","Burlington","Manchester","Clayton",
							"Milton","Auburn","Dayton","Lexington","Milford","Riverside","Cleveland","Dover","Hudson","Kingston","Mount Vernon",
							"Newport","Oakland","Centerville","Winchester","Rotary","Bailey","Saint Mary","Three Waters","Veritas","Chaos","Center",
							"Millbury","Stockland","Deerstead Hills","Plaintown","Fairchester","Milaire View","Bradton","Glenfield","Kirkmore",
							"Fortdell","Sharonford","Inglewood","Englecamp","Harrisvania","Bosstead","Brookopolis","Metropolis","Colewood","Willowbury",
							"Hearthdale","Weelworth","Donnelsfield","Greenline","Greenwich","Clarkswich","Bridgeworth","Normont",
							"Lynchbrook","Ashbridge","Garfort","Wolfpain","Waterstead","Glenburgh","Fortcroft","Kingsbank","Adamstead","Mistead",
							"Old Crossing","Crossing","New Agon","New Agen","Old Agon","New Valley","Old Valley","New Kingsbank","Old Kingsbank",
							"New Dover","Old Dover","New Burlington","Shawshank","Old Shawshank","New Shawshank","New Bradton", "Old Bradton","New Metropolis","Old Clayton","New Clayton"
			};
		static string alphabet = "ABCDEFGHIJKLMNOPQRSTUVXYWZ";
		#endregion

		// Gets a random city name
		public static string GetRandomCityName() => cityNameList[UnityEngine.Random.Range(0, cityNameList.Count)];

		// Get a random male name and optionally single letter surname
		public static string GetRandomMaleName(bool withSurname = false) =>
			firstNameMaleList[UnityEngine.Random.Range(0, firstNameMaleList.Count)] +
				(withSurname ? " " + alphabet[UnityEngine.Random.Range(0, alphabet.Length)] + "." : "");

		public static string GetIdStringLong(int chars = 8)
		{
			string alphabet = "0123456789abcdefghijklmnopqrstuvxywzABCDEFGHIJKLMNOPQRSTUVXYWZ";
			string ret = "";
			for (int i = 0; i < chars; i++)
			{
				ret += alphabet[UnityEngine.Random.Range(0, alphabet.Length)];
			}
			return ret;
		}

		public static string GetMonthName(int month)
		{
			switch (month)
			{
				default:
				case 0: return "January";
				case 1: return "February";
				case 2: return "March";
				case 3: return "April";
				case 4: return "May";
				case 5: return "June";
				case 6: return "July";
				case 7: return "August";
				case 8: return "September";
				case 9: return "October";
				case 10: return "November";
				case 11: return "December";
			}
		}

		public static string GetMonthNameShort(int month) => GetMonthName(month).Substring(0, 3);

		public static Vector3 GetRandomDir() =>
			new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;

		public static Vector3 GetRandomDir3D() =>
			new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized;

		public static Color GetRandomColor() =>
			new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);

		public static bool Chance(int chance, int chanceMax = 100) => UnityEngine.Random.Range(0, chanceMax) < chance;
	}
}
