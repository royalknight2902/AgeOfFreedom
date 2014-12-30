using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using MiniJSON;
using System.Linq;

/// <summary>
/// Collection data. support handle data by ID.
/// </summary>
public class XCollection<T>
{
	private Dictionary<object, T> _data = null;
	protected string idAttribue = "id";

	public int Count
	{
		get
		{
			if (_data == null)
				return 0;
			return _data.Count;
		}
	}
	public XCollection()
	{
		this._data = new Dictionary<object, T>();
	}

	public T[] GetAll()
	{
		T[] items = new T[this._data.Count];
		this._data.Values.CopyTo(items, 0);
		return items;
	}

	public int newUID()
	{
		int max = 0;
		foreach (KeyValuePair<object, T> e in this._data)
		{
			if ((int)e.Key > max)
				max = (int)e.Key;
		}
		return max + 1;
	}

	public void Reset(T[] data)
	{
		PropertyInfo propertyTid = typeof(T).GetProperty("UID");
		//Debug.Log(typeof(T).Name);
		if (propertyTid != null)
		{
			//Debug.Log(propertyTid.Name);
			for (var i = 0; i < data.Length; i++)
			{
				object id = (int)propertyTid.GetValue(data[i], null);
				this._data[(int)id] = data[i];
			}
		}
		else
		{
			Debug.LogError("Parse Error : " + typeof(T).Name);
		}
	}

	public T Get(int tid)
	{
		if (this._data.ContainsKey(tid))
		{
			return this._data[tid];
		}
		return default(T);
	}

	//public T Get(T obj)
	//{
	//    try
	//    {
	//        PropertyInfo propertyTid = typeof(T).GetProperty("UID");
	//        int id = (int)propertyTid.GetValue(item, null);
	//        this._data.Add(id, item);
	//    }
	//    catch (System.ArgumentException)
	//    {
	//        Debug.LogError("TRÙNG KEY TRONG COLLECTION!");
	//    }
	//}

	public T Convert(Hashtable obj)
	{
		//instance object record instance of T.
		T mRecord = Activator.CreateInstance<T>();

		//duyet qua record va set gia tri.
		foreach (DictionaryEntry e in obj)
		{
			//set
			//FieldInfo[] lst = mRecord.GetType().GetFields();
			FieldInfo fi = mRecord.GetType().GetField(e.Key as String);
			//PropertyInfo fi2 = mRecord.GetType().GetField(e.Key as String, BindingFlags.Default);

			if (fi != null)
			{
				if (fi.FieldType == typeof(String))
				{
					fi.SetValue(mRecord, e.Value);
				}
				if (fi.FieldType == typeof(Int32))
				{
					int value = Int32.Parse(e.Value.ToString());
					fi.SetValue(mRecord, value);
				}
				if (fi.FieldType == typeof(Int64))
				{
					long value = Int64.Parse(e.Value.ToString());
					fi.SetValue(mRecord, value);
				}
				if (fi.FieldType == typeof(Double))
				{
					double value = Double.Parse(e.Value.ToString());
					fi.SetValue(mRecord, value);
				}
				if (fi.FieldType == typeof(float))
				{
					float value = float.Parse(e.Value.ToString());
					fi.SetValue(mRecord, value);
				}

			}
		}
		return mRecord;
	}

	public T[] FilterByField(string field, object val)
	{ // (sect_id, 10)
		List<T> lst = new List<T>();

		//lay field key cua item 
		FieldInfo fi = typeof(T).GetField(field);
		if (fi != null)
		{
			foreach (KeyValuePair<object, T> e in this._data)
			{
				T item = e.Value;

				//lay gia tri cua item.
				object fieldVal = fi.GetValue(item);  //item.sec_id.

				//kiem tra so khop gia tri.
				if (fieldVal.Equals(val))
				{            //item.sect_id == 10.
					lst.Add(item);
				}
			}
		}
		else
		{
			Debug.Log("Invalid Option");

		}
		return lst.ToArray();
	}

	/// <summary>
	/// Filter by Fields 
	/// Ghi chu:
	/// Number Comparer supports : ==, >, <, >=, <=.
	/// String comparer suports : only ==.
	/// </summary>
	/// <param name="filters">mang cac filter [key1, compare1, value1, key2, compare2, value2 ,....]. vi du: , ["level", ">=", 10]</param>
	/// <returns></returns>
	public T[] FilterByFields(object[] filters)
	{
		if (filters.Length % 3 != 0)
		{
			Debug.LogError("Loi cu phap filter.");
			return null;
		}
		List<T> lst = new List<T>();
		bool matching;
		bool matching_all;
		foreach (KeyValuePair<object, T> e in this._data)
		{
			T item = e.Value;
			matching_all = true;
			for (int i = 0; i < filters.Length; i += 3)
			{
				matching = false;
				string key = (String)filters[i];
				string comparer = (String)filters[i + 1];
				object value = filters[i + 2];

				FieldInfo fi = typeof(T).GetField(key);
				if (fi != null)
				{
					object fieldVal = fi.GetValue(item);
					if (comparer == "==" || comparer == "=")
					{
						matching = fieldVal.Equals(value);
					}
					else // >, <, >=, <=
					{
						//convert to double. va compare.
						switch (comparer)
						{
							case ">":
								matching = Double.Parse(fieldVal.ToString()) > Double.Parse(value.ToString());
								break;
							case "<":
								matching = Double.Parse(fieldVal.ToString()) < Double.Parse(value.ToString());
								break;
							case ">=":
								matching = Double.Parse(fieldVal.ToString()) >= Double.Parse(value.ToString());
								break;
							case "<=":
								matching = Double.Parse(fieldVal.ToString()) <= Double.Parse(value.ToString());
								break;
						}
					}
				}
				else
				{
					Debug.LogError("Field not found " + key);
				}

				if (matching == false)
				{
					matching_all = false;
					break;
				}
			}
			if (matching_all)
				lst.Add(item);
		}
		return lst.ToArray();
	}

	public void Add(T item)
	{
		try
		{
			PropertyInfo propertyTid = typeof(T).GetProperty("UID");
			object id = propertyTid.GetValue(item, null);
			this._data.Add(id, item);
		}
		catch (System.ArgumentException)
		{
			Debug.LogError("TRÙNG KEY TRONG COLLECTION!");
		}
	}

	// delete with id (int)
	public bool Delete(int idDelete)
	{
		try
		{
			return this._data.Remove(idDelete);
		}
		catch (System.Exception ex)
		{
			Debug.LogError(ex.Message);
			return false;
		}
	}

	public bool Delete(T item)
	{
		try
		{
			PropertyInfo propertyTid = typeof(T).GetProperty("UID");
			int id = (int)propertyTid.GetValue(item, null);
			return this._data.Remove(id);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
			return false;
		}
		//return this._data.Remove((int)item.GetType().GetField(idAttribue).GetValue(item));
	}

	public void DeleteAll()
	{
		this._data.Clear();
	}

	public void Save()
	{
		var dataJson = this.GetAll();
		IList il = new List<Dictionary<string, object>>();
		for (int i = 0; i < dataJson.Length; i++)
		{
			il.Add(dataJson[i].AsDictionary());
		}
		string json = Json.Serialize(il);
#if UNITY_EDITOR
//		UnityEngine.Debug.Log(json);
#endif
		PlayerPrefs.SetString(typeof(T).ToString() + "s", json);
	}

	public T[] Load<T>() where T : class, new()
	{
		string json = PlayerPrefs.GetString(typeof(T).ToString() + "s");
		//Debug.Log (typeof(T).ToString() + "s" + json);
		IList loaded = Json.Deserialize(json) as IList;

		//Debug.Log("loaded.Count = " + loaded.Count);

		T[] items = new T[loaded.Count];

		for (int i = 0; i < loaded.Count; i++)
		{
			items[i] = new T();
			DictionaryToObject((Dictionary<string, object>)loaded[i], items[i]);
		}

		return items;
	}

	public static void DictionaryToObject(Dictionary<string, object> src, object des)
	{
		FieldInfo[] fields = des.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

		for (int i = 0; i < fields.Length; i++)
		{
			object val = src[fields[i].Name];
			fields[i].SetValue(des, System.Convert.ChangeType(val, fields[i].FieldType));
		}
	}
}
