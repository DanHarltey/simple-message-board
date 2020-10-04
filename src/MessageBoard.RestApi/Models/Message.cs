using MessageBoard.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.RestApi.Models
{
    public class Message
    {
        [Required, MaxLength(MessagesSize.MaxLength)]
        public string Text { get; set; }
    }
}
