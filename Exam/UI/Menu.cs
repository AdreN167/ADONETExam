using System;
using Exam.Data;
using Exam.Models;
using System.Linq;

namespace Exam.UI
{
    public class Menu
    {
        private DoctorDataAccess _doctorDataAccess;
        private PatientDataAccess _patientDataAccess;
        private TimetableDataAccess _timetableDataAccess;
        private DoctorsSpecializationDataAccess _doctorsSpecializationDataAccess;

        private Patient _patient;

        public Menu()
        {
            _doctorDataAccess = new DoctorDataAccess();
            _patientDataAccess = new PatientDataAccess();
            _timetableDataAccess = new TimetableDataAccess();
            _doctorsSpecializationDataAccess = new DoctorsSpecializationDataAccess();
        }

        public void MainMenu()
        {
            var isEnd = false;

            Console.Write("Введите ваше полное имя (И.В.Петров): ");
            var fullName = Console.ReadLine();

            Console.Write("Введите вашу дату рождения (2020-12-31): ");
            var birthDate = DateTime.Parse(Console.ReadLine());

            _patient = new Patient
            {
                FullName = fullName,
                BirthDate = birthDate
            };

            if (_patientDataAccess.Select().Where(patient => patient.FullName == fullName && patient.BirthDate == birthDate).ToList().Count == 0)
            {
                _patientDataAccess.Insert(_patient);
            }

            while (!isEnd)
            {
                Console.Clear();

                ShowMenu("-------------Главное меню------------", "Посмотреть свои записи", "Записаться на прием", "Закрыть");
                Console.Write("Ваш выбор: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    ThrowError("Некорректный ввод!");
                }

                switch (choice)
                {
                    case 1:
                        ShowOwnAppointments();
                        break;

                    case 2:
                        ShowDoctors();
                        break;

                    case 3:
                        isEnd = true;
                        break;

                    default:
                        ThrowError("Такого пункта нет!");
                        break;
                }
            }
        }

        private void ShowDoctors()
        {
            Console.Clear();

            var count = 1;

            foreach (var doctor in _doctorDataAccess.Select())
            {
                var specialization = _doctorsSpecializationDataAccess
                    .Select()
                    .Where(specialization => specialization.Id == doctor.SpecializationId).First().Name;
                Console.WriteLine($"[{count++}] {specialization} {doctor.FullName}");
            }

            Console.Write("Введите номер доктора: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                ThrowError("Некорректный ввод!");
            }

            //TODO
        }

        private void ShowOwnAppointments()
        {
            Console.Clear();
            foreach (var appointment in _timetableDataAccess.Select().Where(timetble => timetble.PatientId == _patient.Id).ToList())
            {
                var specialization = _doctorsSpecializationDataAccess
                    .Select()
                    .Where(specialization => specialization.Id == _doctorDataAccess.Select().Where(doctor => doctor.Id == appointment.DoctorId).First().SpecializationId)
                    .First().Name;
                Console.WriteLine($"[{specialization}] на {appointment.AppointmentTime}");
            }
        }

        private void ShowMenu(string head, params string[] arguments)
        {
            Console.WriteLine(head);
            for (int i = 0; i < arguments.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {arguments[i]}");
            }
        }

        private void ThrowError(string message)
        {
            Console.WriteLine($"\n{message}");
            Console.ReadLine();
        }
    }
}
