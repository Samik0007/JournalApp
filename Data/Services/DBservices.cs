using System;
using System.Collections.Generic;
using System.Text;
using Journal_App.Data.Utils;
using Microsoft.EntityFrameworkCore;


namespace Journal_App.Data.Services
{
    
    public class DBservices
    {
        private readonly DBcontext _context;
        public DBservices(DBcontext context)
        {
            _context = context;
        }
        public async Task InitializeAsync()
        {
            await _context.Database.EnsureCreatedAsync();
        }
    }
}
