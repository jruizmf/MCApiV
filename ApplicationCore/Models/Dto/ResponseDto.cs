using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models.Dto
{
    public class ReponseDto
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsResponse { get; set; }
    }
}
