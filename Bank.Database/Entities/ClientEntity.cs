using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Database.Entities
{
    [Table("Clients")]
    public class ClientEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public BankAccountEntity? Account { get; set; }
    }
}