namespace CompanyApp;

public static class AppConstants
{
    public static class Country
    {
        public static readonly Guid Ukraine = Guid.Parse("7d9a314b-96bd-4424-bc10-000000000001");
    }

    public static class Region
    {
        public static readonly Guid Kyivska = Guid.Parse("9dbdd4a3-eae8-4e3c-87da-000000000001");
        public static readonly Guid Lvivska = Guid.Parse("9dbdd4a3-eae8-4e3c-87da-000000000002");
        public static readonly Guid Odeska = Guid.Parse("9dbdd4a3-eae8-4e3c-87da-000000000003");
    }

    public static class City
    {
        public static readonly Guid Kyiv = Guid.Parse("e4fb86c8-13e0-4f8b-937e-000000000001");
        public static readonly Guid Lviv = Guid.Parse("e4fb86c8-13e0-4f8b-937e-000000000002");
        public static readonly Guid Odesa = Guid.Parse("e4fb86c8-13e0-4f8b-937e-000000000003");
        public static readonly Guid Rivne = Guid.Parse("e4fb86c8-13e0-4f8b-937e-000000000004");
    }

    public static class Address
    {
        public static readonly Guid Main1 = Guid.Parse("03854339-4c0e-4c16-9174-000000000001");
        public static readonly Guid Main2 = Guid.Parse("03854339-4c0e-4c16-9174-000000000002");
        public static readonly Guid Main3 = Guid.Parse("03854339-4c0e-4c16-9174-000000000003");
        public static readonly Guid Main4 = Guid.Parse("03854339-4c0e-4c16-9174-000000000004");
    }

    public static class Position
    {
        public static readonly Guid Engineer = Guid.Parse("40bb3f12-2e37-484b-b639-000000000001");
        public static readonly Guid HrManager = Guid.Parse("40bb3f12-2e37-484b-b639-000000000002");
        public static readonly Guid Accountant = Guid.Parse("40bb3f12-2e37-484b-b639-000000000003");
        public static readonly Guid Designer = Guid.Parse("40bb3f12-2e37-484b-b639-000000000004");
    }

    public static class Employee
    {
        public static readonly Guid Alice = Guid.Parse("b88bad51-2bc0-48f9-90a0-000000000001");
        public static readonly Guid Bob = Guid.Parse("b88bad51-2bc0-48f9-90a0-000000000002");
        public static readonly Guid Carol = Guid.Parse("b88bad51-2bc0-48f9-90a0-000000000003");
        public static readonly Guid Dave = Guid.Parse("b88bad51-2bc0-48f9-90a0-000000000004");
        public static readonly Guid Evelyn = Guid.Parse("b88bad51-2bc0-48f9-90a0-000000000005");
    }

    public static class Department
    {
        public static readonly Guid IT = Guid.Parse("6156d0ec-3807-4ad3-b6c5-000000000001");
        public static readonly Guid HR = Guid.Parse("6156d0ec-3807-4ad3-b6c5-000000000002");
        public static readonly Guid Accounting = Guid.Parse("6156d0ec-3807-4ad3-b6c5-000000000003");
    }

    public static class Company
    {
        public static readonly Guid TestCompany = Guid.Parse("04e019b2-f515-4631-9112-000000000001");
    }
}