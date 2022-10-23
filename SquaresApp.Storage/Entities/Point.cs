using System.ComponentModel.DataAnnotations;

namespace SquaresApp.Storage.Entities
{
    public class Point
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid BatchId { get; set; }
        [Required]
        public int Xaxis { get; set; }
        [Required]
        public int Yaxis { get; set; }
    }
}
