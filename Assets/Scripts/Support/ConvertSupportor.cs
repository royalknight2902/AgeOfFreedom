using UnityEngine;
using System.Collections;

public class ConvertSupportor
{
    public static string convertUpperFirstChar(string str)
    {
        return (char.ToUpper(str[0]) + str.Substring(1, str.Length - 1).ToLower());
    }

    //public static STowerID getID(int ID)
    //{
    //    STowerID sTowerID;
    //    sTowerID.Type = ID / 10;
    //    sTowerID.Level = ID % 10;
    //    return sTowerID;
    //}
}
