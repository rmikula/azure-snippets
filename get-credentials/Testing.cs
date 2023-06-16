using Azure.Storage.Blobs;

namespace get_credentials;

public record Person(string Name, string Surname, int age);

public class PersonFactory
{
    private readonly Func<Person> _builder;

    public PersonFactory(Func<Person> builder)
    {
        _builder = builder;
    }

    public Person GetPerson()
    {
        return _builder();
    }
}

public class PersonFactoryBuilder
{
    public PersonFactory CreateFactory(string name)
    {
        return new PersonFactory( () => new Person(name, "Mikula", 1));
    }
}

public static class Testing
{
    private static R SomeMethod<T, R>(T someStartValue, Func<T, R> factory)
    {
        var x = factory(someStartValue);
        return x;
    }


    public static void Test1()
    {
        var startPoint = 30;

        var r = SomeMethod<long, string>(1235, s => startPoint.ToString() + s.ToString());

        // var res2 = SomeMethod(startPoint, i =>  i * 20);

        // Console.WriteLine(res2);
        // var res = SomeMethod( factory: () => 40);
    }

    public static void TestCreateFactoryBuilder()
    {
        var xx = new PersonFactoryBuilder();

        var b = xx.CreateFactory("Martin");
        var b2 = xx.CreateFactory("Roman");


        var p1 = b.GetPerson();
        var p2 = b2.GetPerson();
        
        Console.WriteLine(p1);
        Console.WriteLine(p2);


    }
    
    public static void CreateFactory()
    {
        var xx = new PersonFactory(() =>
        {
            return new Person("Roman", "Mikula", 34);
        });

        var person = xx.GetPerson();
        
        Console.WriteLine(person);
    }
}