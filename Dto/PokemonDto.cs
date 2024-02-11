namespace WebApplicationTutorial.Dto
{
    public class PokemonDto
    {
        // Creating a class with less fields, so as not to send sensitive data on Response
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
