using Exam.Models;
using System;
using System.Collections.Generic;

namespace Exam.Data
{
    public class TimetableDataAccess : DbDataAccess<Timetable>
    {
        public override ICollection<Timetable> Select()
        {
            var sqlTimetable = $"Select * from Timetables;";

            var timetables = new List<Timetable>();

            var command = factory.CreateCommand();
            command.CommandText = sqlTimetable;
            command.Connection = connection;

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    timetables.Add(new Timetable
                    {
                        Id = Guid.Parse(dataReader["Id"].ToString()),
                        DoctorId = Guid.Parse(dataReader["DoctorId"].ToString()),
                        PatientId = Guid.Parse(dataReader["PatientId"].ToString()),
                        AppointmentTime = DateTime.Parse(dataReader["AppointmentTime"].ToString())
                    });
                }
            }

            command.Dispose();

            return timetables;
        }

        public override void Update(Timetable entity)
        {
            var sqlTimetable = $"Update Timetables set PatientId = '{entity.PatientId}';";

            using (var command = factory.CreateCommand())
            {
                command.CommandText = sqlTimetable;
                command.Connection = connection;

                command.ExecuteNonQuery();
            }
        }

        public override void Insert(Timetable entity)
        {
            var sqlTimetable = $"Insert into Timetables(Id, DoctorId, PatientId, AppointmentTime) values (@Id, @DoctorId, @PatientId, @AppointmentTime);";

            using (var command = factory.CreateCommand()) 
            {
                command.CommandText = sqlTimetable;
                command.Connection = connection;

                var idParameter = factory.CreateParameter();
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = entity.Id;
                idParameter.ParameterName = "Id";

                command.Parameters.Add(idParameter);
                
                var doctorIdParameter = factory.CreateParameter();
                doctorIdParameter.DbType = System.Data.DbType.Guid;
                doctorIdParameter.Value = entity.DoctorId;
                doctorIdParameter.ParameterName = "DoctorId";

                command.Parameters.Add(doctorIdParameter);
                
                var patientIdParameter = factory.CreateParameter();
                patientIdParameter.DbType = System.Data.DbType.Guid;
                patientIdParameter.Value = entity.PatientId;
                patientIdParameter.ParameterName = "PatientId";

                command.Parameters.Add(patientIdParameter);
                
                var appointmentTimeParameter = factory.CreateParameter();
                appointmentTimeParameter.DbType = System.Data.DbType.DateTime2;
                appointmentTimeParameter.Value = entity.AppointmentTime;
                appointmentTimeParameter.ParameterName = "AppointmentTime";

                command.Parameters.Add(appointmentTimeParameter);

                command.ExecuteNonQuery();
            }
        }

        public override void Delete(Timetable entity)
        {
            var sqlTimetable = $"Delete from Timetables where Id = '{entity.Id}';";

            using (var command = factory.CreateCommand())
            {
                command.CommandText = sqlTimetable;
                command.Connection = connection;

                command.ExecuteNonQuery();
            }
        }
    }
}
