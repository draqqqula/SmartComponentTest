## SmartComponentTest
Recentelty I was experimenting with Composite pattern with goal to make it suitable for Unity-like GameObject system.
I think I created something interesting.
# ComponentBase
Base class for all components. Can be combined with other ComponentBase to make CompositeComponent using CombineWith().
You can specify action that will occur when certain component joins this one using AddGreetingFor.
# CompositeComponent
Contains exactly two of components. Also component himself.
# ComponentShell
Initially empty. Keeps same reference when Combined with other components.
# ExtendedComponent
Acts like ComponentBase but upon creating instance checks class for methods with secial attribute and adds them as Greetings for certain component.
Method must return void and have only one parameter of ComponentBase inherited type ot ComponentBase itself.