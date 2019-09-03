//Utilities to be used in future projects
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static string ProcessText(string textIn)
    {
        //return (textIn);

        if (double.TryParse(textIn, out double num))
        {
            return ("Number");
        }
        else
        {
            return ("String");
        }
    }
}
