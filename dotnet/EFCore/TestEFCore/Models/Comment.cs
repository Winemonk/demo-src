﻿using System;
using System.Collections.Generic;

namespace TestEFCore.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CommentedDate { get; set; }

    public int PostId { get; set; }

    public virtual Post Post { get; set; } = null!;
}
