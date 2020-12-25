using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLib
{
    public class Customer
    {
        public Customer()
        {

        }
        public Customer(string userName, string password, string firstName, string lastName, string address, int defaultStore)
        {
            this.UserName = userName;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.DefaultStore = defaultStore;
        }
        private int customerId;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int CustomerId { get => customerId; set => customerId = value; }
        private string userName;
        [Required]
        public string UserName {get => userName; set => userName = value; }
        private string password;
        [Required]
        public string Password { get => password; set => password = value; }
        private string firstName;
        [Required]
        public string FirstName { get => firstName; set => firstName = value; }
        private string lastName;
        [Required]
        public string LastName { get => lastName; set => lastName = value; }
        private string address;
        [Required]
        public string Address { get => address; set => address = value; }
        private int defaultStore;
        [ForeignKey("LocationId")]
        public int DefaultStore { get => defaultStore; set => defaultStore = value; }
        private int cartId;
        [ForeignKey("CartId")]
        public int CartId { get => cartId; set => cartId = value; }        
    }
}