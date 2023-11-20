using DecorativeComponents;
using ExampleClasses;

var a = new ObjectA();
var b = new ObjectB();
var c = new ObjectC();
var d = new ObjectD();


Console.WriteLine("\nCombining A and B:");
var ab = a.CombineWith(b);

UpdateEach(ab);

Console.WriteLine("\nCombining A and C:");
var ac = a.CombineWith(c);

UpdateEach(ac);

Console.WriteLine("\nCombining A and B using shell:");

var shell = new ComponentShell();
var shellA = shell.CombineWith(a);
var shellAB = shellA.CombineWith(b);
Console.WriteLine($"All of combinations are same object: {shell.Equals(shellA) && shellA.Equals(shellAB)}");

UpdateEach(shellAB);


Console.WriteLine("\nCombining A and B and C:");

var abc = a.CombineWith(b).CombineWith(c);

UpdateEach(abc);


Console.WriteLine("\nCombining C and D:");

var cd = c.CombineWith(d);

UpdateEach(cd);

void UpdateEach(ComponentBase componentBase)
{

    foreach (var component in componentBase.GetComponents<IUpdateComponent>())
    {
        component.Update();
    }
}