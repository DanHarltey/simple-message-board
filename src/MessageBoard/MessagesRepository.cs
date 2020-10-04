using MessageBoard.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard
{
    public class MessagesRepository : IMessages
    {
        private readonly MessageBoardContext _context;

        public MessagesRepository(MessageBoardContext context) => _context = context;

        public Task AddMessage(string messageText)
        {
            _context.Messages.Add(new Message
            {
                Text = messageText
            });

            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> Get() => await _context.Messages.Select(x => x.Text).ToListAsync();
    }
}
