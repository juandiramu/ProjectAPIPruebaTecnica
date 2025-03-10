using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Data
{
    public partial class Customer
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CustomerId { get; set; }
        public string Name { get; set; }
        public Customer() { }

        public Customer(string name)
        {
            Name = name;

        }
    }
}
