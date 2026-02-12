using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace market.dtos
{
    public class dtopostproduct
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
