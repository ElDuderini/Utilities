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

class Person
{
public string Name;
public virtual void SayHello()
{
Console.WriteLine("Hello");
}
}
class RenFairePerson : Person
{
public override void SayHello()
{
Console.Write("Huzzah!");
}
}

RenFairePerson person = new RenFairePerson();
person.Name = "Igor the Ratcatcher";
person.SayHello();RenFairePerson person = new RenFairePerson();
person.Name = "Igor the Ratcatcher";
person.SayHello();

base.SayHello();
