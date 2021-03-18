using Exam.Models;
using System;
using System.Collections.Generic;

namespace Exam.Data
{
    public class DoctorDataAccess : DbDataAccess<Doctor>
    {
        public override void Insert(Doctor entity) 
        {
            var sqlDoctor = $"Insert into Doctors(Id, FullName, BirthDate, SpecializationId) values (@Id, @FullName, @BirthDate, @SpecializationId);";

            using (var command = factory.CreateCommand())
            {
                command.CommandText = sqlDoctor;
                command.Connection = connection;

                var idParameter = factory.CreateParameter();
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = entity.Id;
                idParameter.ParameterName = "Id";

                command.Parameters.Add(idParameter);

                var fullNameParameter = factory.CreateParameter();
                fullNameParameter.DbType = System.Data.DbType.String;
                fullNameParameter.Value = entity.FullName;
                fullNameParameter.ParameterName = "FullName";

                command.Parameters.Add(fullNameParameter);

                var specializationIdParameter = factory.CreateParameter();
                specializationIdParameter.DbType = System.Data.DbType.Guid;
                specializationIdParameter.Value = entity.SpecializationId;
                specializationIdParameter.ParameterName = "SpecializationId";

                command.Parameters.Add(specializationIdParameter);

                var birthDateParameter = factory.CreateParameter();
                birthDateParameter.DbType = System.Data.DbType.DateTime2;
                birthDateParameter.Value = entity.BirthDate;
                birthDateParameter.ParameterName = "BirthDate";

                command.Parameters.Add(birthDateParameter);

                command.ExecuteNonQuery();
            }
        }

        public override ICollection<Doctor> Select()
        {
            var sqlDoctor = $"Select * from Doctors;";

            var doctors = new List<Doctor>();

            var command = factory.CreateCommand();
            command.CommandText = sqlDoctor;
            command.Connection = connection;

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    doctors.Add(new Doctor
                    {
                        Id = Guid.Parse(dataReader["Id"].ToString()),
                        SpecializationId = Guid.Parse(dataReader["SpecializationId"].ToString()),
                        FullName = dataReader["FullName"].ToString(),
                        BirthDate = DateTime.Parse(dataReader["BirthDate"].ToString())
                    });
                }
            }

            command.Dispose();

            return doctors;
        }

        public override void Update(Doctor entity) { }

        public override void Delete(Doctor entity) { }
    }
}
