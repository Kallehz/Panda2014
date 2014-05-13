using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PandaApp.Models
{
    public class SubtitleLine
    {
        public int ID { get; set; }
        public int Index { get; set; }
        public int SubtitleID { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public string TimeFrom { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public string TimeTo { get; set; }
    }
}