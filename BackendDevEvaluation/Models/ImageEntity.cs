using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendDevEvaluation.Models
{
    public class ImageEntity
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public byte[] Data { get; set; }
    }
}
