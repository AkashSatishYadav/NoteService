using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteService.Shared.Events
{
    public class ConversionJob
    {
        public string Format { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public Note Note { get; set; }
    }

    public class Note
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
