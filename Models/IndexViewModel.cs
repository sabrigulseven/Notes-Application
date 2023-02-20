using Notes.Models.Entities;

namespace Notes.Models;

public class IndexViewModel
{
    public Note? Note { get; set; }

    public IEnumerable<Note>? Notes { get; set; }

    public int? ParentId { get; set; }

}