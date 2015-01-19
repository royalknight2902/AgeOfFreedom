using UnityEngine;
using System.Collections;

public class ConvertSupportor
{
    public static string convertUpperFirstChar(string str)
    {
        return (char.ToUpper(str[0]) + str.Substring(1, str.Length - 1).ToLower());
    }
	public static string convertSkillString(string str)
	{
		int posSpace = str.IndexOf (" ");
		return (char.ToUpper(str[0]) + str.Substring(1,posSpace) +  char.ToUpper (str [posSpace + 1]) + (posSpace+ 2 > str.Length - 1?"": str.Substring (posSpace + 2, str.Length - posSpace - 2).ToLower ()));
	}
    //public static STowerID getID(int ID)
    //{
    //    STowerID sTowerID;
    //    sTowerID.Type = ID / 10;
    //    sTowerID.Level = ID % 10;
    //    return sTowerID;
    //}
}
