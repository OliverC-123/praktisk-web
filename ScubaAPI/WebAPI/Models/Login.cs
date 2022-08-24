namespace WebAPI.Models
{
    public class Login
    {
        //const [ID, setID] = usestatue(0)
        public int ID { get; set; }

        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}