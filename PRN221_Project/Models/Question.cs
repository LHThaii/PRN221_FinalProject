using System;
using System.Collections.Generic;

namespace PRN221_Project.Models
{
    public partial class Question
    {
        public int QuestionId { get; set; }
        public string QuestionContent { get; set; } = null!;
        public int CourseId { get; set; }
        public string Answer { get; set; }
        public bool? Status { get; set; }

        public virtual Course Course { get; set; } = null!;
    }
}
