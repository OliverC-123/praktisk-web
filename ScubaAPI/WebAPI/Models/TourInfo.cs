namespace WebAPI.Models
{
    public class TourInfo
    {
        //const [ID, setID] = usestatue(0)
        public int ID { get; set; }

        public string? Name { get; set; }
        public int NumOfSpotsLeft { get; set; }
        public decimal PricePerPerson { get; set; }
        public string? PayWith { get; set; }
        public string? DrivingType { get; set; }
        public string? Time { get; set; }
        public string? Date { get; set; }
        public string? SpotDescription { get; set; }
    }
}