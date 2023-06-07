using UnityEngine;
using System;
using System.Collections.Generic;

namespace SWAssets.Utils
{
	public static class JSONUtils
	{
		[Serializable]
		private class JsonDictionary
		{
			public List<string> keyList = new List<string>();
			public List<string> valueList = new List<string>();
		}

		// Take a Dictionary and return JSON string
		public static string SaveDictionaryJson<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
		{
			JsonDictionary jsonDictionary = new JsonDictionary();
			foreach (TKey key in dictionary.Keys)
			{
				jsonDictionary.keyList.Add(JsonUtility.ToJson(key));
				jsonDictionary.valueList.Add(JsonUtility.ToJson(dictionary[key]));
			}
			string saveJson = JsonUtility.ToJson(jsonDictionary);
			return saveJson;
		}

		// Take a JSON string and return Dictionary<T1, T2>
		public static Dictionary<TKey, TValue> LoadDictionaryJson<TKey, TValue>(string saveJson)
		{
			JsonDictionary jsonDictionary = JsonUtility.FromJson<JsonDictionary>(saveJson);
			Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>();
			for (int i = 0; i < jsonDictionary.keyList.Count; i++)
			{
				TKey key = JsonUtility.FromJson<TKey>(jsonDictionary.keyList[i]);
				TValue value = JsonUtility.FromJson<TValue>(jsonDictionary.valueList[i]);
				ret[key] = value;
			}
			return ret;
		}
	}
}
