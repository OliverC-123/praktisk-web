namespace ScubaAPI.Models
{
    public class IMG 
    {
        public int? Id { get; set; }
        public int StedID { get; set; }
        public Sted? Sted { get; set; }
        public string? Image { get; set; }
        public string? Default { get; set; }
        public string? Cover { get; set; }
    }
}
