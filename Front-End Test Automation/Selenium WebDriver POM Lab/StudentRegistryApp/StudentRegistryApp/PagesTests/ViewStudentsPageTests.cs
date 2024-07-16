using OpenQA.Selenium;
using StudentRegistryPOM.Pages;

namespace StudentRegistryPOM.PagesTests
{
    public class ViewStudentsPageTests : BasePageTests
    {
        [Test]
        public void Test_ViewStudentsPage_Content()
        {
            ViewStudentsPage viewPage = new ViewStudentsPage(driver);
            viewPage.OpenPage();

            Assert.Multiple(() =>
            {
                Assert.That(viewPage.GetPageTitle(), Is.EqualTo("Students"));
                Assert.That(viewPage.GetPageHeading(), Is.EqualTo("Registered Students"));
            });

            var students = viewPage.GetRegisteredStudents();

            foreach (var student in students)
            {
                Assert.That(student.Contains("("), Is.True);
                Assert.That(student.LastIndexOf(")") == student.Length - 1, Is.True);
            }
        }

        [Test]
        public void Test_ViewStudentsPage_Links()
        {
            var viewStudentsPage = new ViewStudentsPage(driver);

            viewStudentsPage.OpenPage();
            viewStudentsPage.HomeLink.Click();
            Assert.That(new HomePage(driver).IsPageOpen(), Is.True);

            viewStudentsPage.OpenPage();
            viewStudentsPage.AddStudentLink.Click();
            Assert.That(new AddStudentPage(driver).IsPageOpen(), Is.True);


            viewStudentsPage.OpenPage();
            viewStudentsPage.ViewStudentsLink.Click();
            Assert.That(new ViewStudentsPage(driver).IsPageOpen(), Is.True);
        }
    }
}