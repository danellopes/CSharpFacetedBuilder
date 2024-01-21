namespace FacetedBuilder;

public class Person
{
    // address
    public string StreetAddress, Postcode, City;
    // employment
    public string CompanyName, Position;
    public int AnnualIncome;

    public override string ToString()
    {
        return $"{nameof(StreetAddress)}: {StreetAddress}\n{nameof(Postcode)}: {Postcode}\n{nameof(City)}: {City}\n{nameof(CompanyName)}: {CompanyName}\n{nameof(Position)}: {Position}\n{nameof(AnnualIncome)}: {AnnualIncome}";
    }
}

// facade
public class PersonBuilder
{
    protected Person person = new Person();

    public PersonJobBuilder Works => new PersonJobBuilder(person);
    public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

    public static implicit operator Person(PersonBuilder pb)
    {
        return pb.person;
    }
}

public class PersonAddressBuilder : PersonBuilder
{
    public PersonAddressBuilder(Person person)
    {
        this.person = person;
    }

    public PersonAddressBuilder At(string streetAddress)
    {
        person.StreetAddress = streetAddress;
        return this;
    }

    public PersonAddressBuilder WithPostcode(string postcode)
    {
        person.Postcode = postcode;
        return this;
    }

    public PersonAddressBuilder In(string city)
    {
        person.City = city;
        return this;
    }
}

public class PersonJobBuilder : PersonBuilder
{
    public PersonJobBuilder(Person person)
    {
        this.person = person;
    }

    public PersonJobBuilder At(string companyName)
    {
        person.CompanyName = companyName;
        return this;
    }

    public PersonJobBuilder AsA(string position)
    {
        person.Position = position;
        return this;
    }

    public PersonJobBuilder Earning(int amount)
    {
        person.AnnualIncome = amount;
        return this;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var pb = new PersonBuilder();
        Person person = pb
        .Lives.At("123 London Road")
              .In("London")
              .WithPostcode("Sw12Ac")
        .Works.At("Fabrikan")
              .AsA("Engineer")
              .Earning(123000);

        System.Console.WriteLine(person);
    }
}
