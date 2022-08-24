namespace WebAPI.Models
{
    public class ContactInfo
    {
        //const [ID, setID] = usestatue(0)
        public int ID { get; set; }

        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? OpeningHoursWeekdays { get; set; }
        public string? OpeningHoursWeekends { get; set; }
    }
}