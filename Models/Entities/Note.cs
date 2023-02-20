using System;
using System.Collections.Generic;

namespace Notes.Models.Entities;

public partial class Note
{
    public int Id { get; set; }

    public int? Parentid { get; set; }

    public string? Content { get; set; }

    public virtual ICollection<Note> InverseParent { get; } = new List<Note>();

    public virtual Note? Parent { get; set; }
}
