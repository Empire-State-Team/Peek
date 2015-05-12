namespace Peek.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Text { get; set; }

        [Required]
        public DateTime PostedAt { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Index]
        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
