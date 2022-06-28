using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Database.Entities
{
    [Table("Accounts")]
    public class BankAccountEntity
    {
        [Key]
        public int ID { get; set; }
        public long AccountNumber { get; set; }

        public int? ClientFK { get; set; }
        public ClientEntity? Client { get; set; }

        public List<CardEntity>? Cards { get; set; }

        public BankAccountEntity()
        {
            Cards = new List<CardEntity>();
        }
    }
}