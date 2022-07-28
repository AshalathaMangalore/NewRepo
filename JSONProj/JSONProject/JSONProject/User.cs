namespace JSONProject
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int age { get; set; }
        public Address Address { get; set; }
        public List<Phonenumber> Phonenumber { get; set; }
    }
    public class Address
    {
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }
    public class Phonenumber
    {
        public string Type { get; set; }
        public string Number { get; set; }
    }
}