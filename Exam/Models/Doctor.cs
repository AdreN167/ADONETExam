using Exam.Models.Abstract;
using System;
using System.Collections.Generic;

namespace Exam.Models
{
    public class Doctor : Person
    {
        public Guid SpecializationId { get; set; }
    }
}
