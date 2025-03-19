namespace YourRentalCarAPI.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;

        public int PhoneNumber {  get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
