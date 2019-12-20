using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyABP.Game.Laba.Dto
{
    public class CreateLabaOrderInput
    {
        [Required]
        public string Routes { set; get; }
    }
}
