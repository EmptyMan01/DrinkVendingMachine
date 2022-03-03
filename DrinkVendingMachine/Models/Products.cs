using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrinkVendingMachine.Models
{
    public partial class Products
    {
        public Products()
        {
            Me = new HashSet<Memory>();
        }
        public int IdProduct { get; set; }
        public string Sprite { get; set; }
        [DisplayName("Name Sprite")]
        public string NameSprite { get; set; }
        public int Quantity{ get; set; }
        string GG { get; set; }
        public int Price { get; set; }
        [NotMapped] 
        [DisplayName("Upload File")]
        public IFormFile SpriteFile { get; set; }


        public virtual ICollection<Memory> Me { get; set; }
    }

}
