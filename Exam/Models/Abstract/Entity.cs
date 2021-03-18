using System;

namespace Exam.Models.Abstract
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
