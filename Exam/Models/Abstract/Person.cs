using System;

namespace Exam.Models.Abstract
{
    public abstract class Person : Entity
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
