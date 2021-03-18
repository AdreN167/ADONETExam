using Exam.Services;
using Exam.UI;

namespace Exam
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigurationService.Init();
            var menu = new Menu();
            menu.MainMenu();
        }
    }
}
