using Exam.Models.Abstract;
using System;

namespace Exam.Models
{
    public class Timetable : Entity
    {
        public Guid DoctorId { get; set; }
        public Guid? PatientId { get; set; }
        public DateTime AppointmentTime { get; set; }
    }
}
