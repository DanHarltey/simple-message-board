namespace MessageBoard.Data
{
    using MessageBoard.Contracts;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Message
    {
        public int Id { get; set; }

        [Required, MaxLength(MessagesSize.MaxLength)]
        public string Text { get; set; }
    }
}
