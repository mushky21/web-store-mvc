﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using webStoreFinal.Validations;

namespace webStoreFinal.Models
{
    public class Product
    {
        [Key]
        public int ProductKey { get; set; }

        public DateTime PublishedDate { get; set; }

        [ForeignKey("SellerId")]
        [InverseProperty("Sells")]
        public virtual MyUser Seller { get; set; }
        public string SellerId { get; set; }

        [ForeignKey("BuyerId")]
        [InverseProperty("Purchases")]
        public virtual MyUser Buyer { get; set; }
        public string BuyerId { get; set; }

        [Required]
        [StringLength(10)]
        public string Title { get; set; }
        [Required]
        [StringLength(20)]
        public string ShortDescription { get; set; }
        [Required]
        [StringLength(100)]
        public string LongDescription { get; set; }



        [Required]
        [ClientSideValidation]
        public double Price { get; set; }

        public byte[] Photo1 { get; set; }
        public byte[] Photo2 { get; set; }
        public byte[] Photo3 { get; set; }

        public State ProductState { get; set; }

    }
    public enum State
    {
        Available,
        InCart,
        Purchased
    }
}

