using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SnachPat.Models
{
    public class PhotoModel
    {
        [Key]
        public int Photo_Id { get; set; }
        public string photoPath { get; set; }
        public string photoName { get; set; }
        public bool IsChecked { get; set; }
        public DateTime DateOfAdd { get; set; }
    }
}
