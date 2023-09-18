using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual long Id { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}