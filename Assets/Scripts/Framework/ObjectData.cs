using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using MiniJSON;

public class ObjectData
{
	protected string idField = "id";

	public object UID
	{
		get
		{
			FieldInfo fieldTid = this.GetType().GetField(idField);
			object id = fieldTid.GetValue(this);
			return id;
		}
	}

	public string convertJSON()
	{
		Debug.Log(Json.Serialize(this.AsDictionary()));
		string saveStr = Json.Serialize(this.AsDictionary());
		return saveStr;
	}

	public void Save()
	{
		string saveStr = Json.Serialize(this.AsDictionary());
		UnityEngine.PlayerPrefs.SetString(this.GetType().Name, saveStr);
		//Debug.Log(saveStr);
	}

	public T Load<T>() where T : class, new()
	{
		string loaded = UnityEngine.PlayerPrefs.GetString(this.GetType().Name);
		//Debug.Log(this.GetType().Name + loaded);
		if (string.IsNullOrEmpty(loaded))
			return new T();
		Dictionary<string, object> dict = Json.Deserialize(loaded) as Dictionary<string, object>;
		//Dictionary<string, T> dict = Json.Deserialize(loaded) as Dictionary<string, T>;
		return dict.ToObject<T>();
	}
}

public static class ObjectExtensions
{
	public static T ToObject<T>(this IDictionary<string, object> source)
		where T : class, new()
	{
		T someObject = new T();
		Type someObjectType = someObject.GetType();

		FieldInfo[] fields = someObjectType.GetFields(BindingFlags.Public | BindingFlags.Instance);
		foreach (FieldInfo fi in fields)
		{
			try
			{
				object value = Convert.ChangeType(source[fi.Name], fi.FieldType);
				fi.SetValue(someObject, value);
			}
			catch
			{
				Debug.Log(fi.Name + " khong duoc tim thay trong data");
				if(fi.FieldType.ToString().Equals("System.String"))
					fi.SetValue(someObject, "");
				else if (fi.FieldType.ToString().Equals("System.Int32"))
				{
					fi.SetValue(someObject, 0);
				}
				else if (fi.FieldType.ToString().Equals("System.Boolean"))
					fi.SetValue(someObject, false);
			}
		}
		return someObject;
	}

	public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance)
	{
		Dictionary<string, object> rs = source.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance).ToDictionary(propInfo => propInfo.Name,
		propInfo => propInfo.GetValue(source));
		return rs;
	}

}