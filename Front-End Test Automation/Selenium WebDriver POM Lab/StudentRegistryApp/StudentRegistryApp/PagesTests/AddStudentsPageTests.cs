using StudentRegistryPOM.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistryPOM.PagesTests
{
    public class AddStudentsPageTests : BasePageTests
    {
        [Test]
        public void Test_AddStudentPage_Content()
        {
            AddStudentPage page = new AddStudentPage(driver);
            page.OpenPage();

            Assert.Multiple(() =>
            {
                Assert.That(page.GetPageTitle(), Is.EqualTo("Add Student"));
                Assert.That(page.GetPageHeading(), Is.EqualTo("Register New Student"));
            });

            Assert.That(page.FieldName.Text, Is.EqualTo(""));
            Assert.That(page.FieldEmail.Text, Is.EqualTo(""));

            Assert.That(page.AddButton.Text, Is.EqualTo("Add"));
        }

        [Test]
        public void Test_AddStudentPage_Links()
        {
            AddStudentPage page = new AddStudentPage(driver);

            page.OpenPage();
            page.HomeLink.Click();
            Assert.That(new HomePage(driver).IsPageOpen(), Is.True);

            page.OpenPage();
            page.AddStudentLink.Click();
            Assert.That(new AddStudentPage(driver).IsPageOpen(), Is.True);


            page.OpenPage();
            page.ViewStudentsLink.Click();
            Assert.That(new ViewStudentsPage(driver).IsPageOpen(), Is.True);
        }

        [Test]
        public void Test_AddStudentPage_AddValidStudent()
        {
            AddStudentPage page = new AddStudentPage(driver);
            page.OpenPage();

            string name = GenerateRandomName();
            string email = GenerateRandomEmail(name);

            page.AddStudent(name, email);

            ViewStudentsPage viewStudents = new ViewStudentsPage(driver);

            Assert.That(viewStudents.IsPageOpen(), Is.True);

            var students = viewStudents.GetRegisteredStudents();

            string newStudent = name + " (" + email + ")";

            Assert.True(students.Contains(newStudent));
        }

        [Test]
        public void Test_AddStudentPage_AddInvalidStudent()
        {
            AddStudentPage page = new AddStudentPage(driver);
            page.OpenPage();

            string name = "";
            string email = "test@gmail.com";

            page.AddStudent(name, email);

            string error = page.ErrorMsg.Text;

            Assert.Multiple(() =>
            {
                Assert.That(page.IsPageOpen(), Is.True);
                Assert.That(error.Contains("Cannot add student."));
            });
        }


        private string GenerateRandomName()
        {
            var random = new Random();
            string[] names = { "Ivan", "Toni", "Dani", "Alex" };

            return names[random.Next(names.Length)] + random.Next(999, 9999).ToString();

        }

        private string GenerateRandomEmail(string name)
        {
            var random = new Random();

            return name.ToLower() + random.Next(999, 9999).ToString() + "@gmail.com";
        }
    }
}
