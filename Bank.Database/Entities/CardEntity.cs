using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Database.Entities
{
    [Table("Cards")]
    public class CardEntity
    {
        [Key]
        public int ID { get; set; }
        public long CardNumber { get; set; }
        public double Balance { get; set; }
        public short Password { get; set; }
        public bool IsBlock { get; set; }

        public int? AccountFK { get; set; }
        public BankAccountEntity? Account { get; set; }
    }
}