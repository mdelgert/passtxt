using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassTxt.Shared.Models
{
    [Table("Notes")]
    public class NoteModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
