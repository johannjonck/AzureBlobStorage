using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Document : EntityBase
    {
        [Required]
        public string FileName { get; set; }

        public string FileFullName { get; set; } 

        public long FileSize { get; set; }

    }
}
