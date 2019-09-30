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

//List info
var index = names.IndexOf("Felipe");
if (index != -1)
  Console.WriteLine($"The name {names[index]} is at index {index}");

var notFound = names.IndexOf("Not Found");
  Console.WriteLine($"When an item is not found, IndexOf returns {notFound}");

  names.Sort();
  foreach (var name in names)
  {
    Console.WriteLine($"Hello {name.ToUpper()}!");
  }


/* Useful stuff:
 *
 *  cube = transform.Find("Cube").gameObject; //find a reference by transform
 *  bndCheck = GetComponent<BoundsCheck>(); // then get a script attached to that object
 *  Vector3 vel = Random.onUnitSphere; //Random xyz velocity
 *  vel.Normalize(); //make length of vector 1m
 *   vel *= Random.Range(driftMinMax.x, driftMinMax.y, drifMinMax.z); //set the drift
 *           transform.rotation = Quaternion.identity;
        rotPerSecond = new Vector3(Random.Range(rotMinMax.x, rotMinMax.y),
            Random.Range(rotMinMax.x, rotMinMax.y),
            Random.Range(rotMinMax.x, rotMinMax.y));
 *
 */
