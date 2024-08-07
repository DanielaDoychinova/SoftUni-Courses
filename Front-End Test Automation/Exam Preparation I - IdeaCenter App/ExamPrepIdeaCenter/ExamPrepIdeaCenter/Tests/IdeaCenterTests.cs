using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrepIdeaCenter.Tests
{
    public class IdeaCenterTests : BaseTest
    {
        public string lastCreatedIdeaTitle;
        public string LastCreatedIdeaDescription;

        [Test, Order(1)]
        public void CreateIdeaWithInvalidDataTest()
        {
            createIdeaPage.OpenPage();
            createIdeaPage.CreateBtn.Click();
            createIdeaPage.AssertErrorMessages();
        }

        [Test, Order(2)]
        public void CreateRandomIdea()
        {
            lastCreatedIdeaTitle = "Idea " + GenerateRandowString(5);
            LastCreatedIdeaDescription = "Description " + GenerateRandowString(10);

            createIdeaPage.OpenPage();

            createIdeaPage.CreateIdea(lastCreatedIdeaTitle, "", LastCreatedIdeaDescription);

            Assert.That(driver.Url, Is.EqualTo(myIdeasPage.Url), "Url is not correct.");
            Assert.That(myIdeasPage.DescriptionLastIdea.Text.Trim(), Is.EqualTo(LastCreatedIdeaDescription), "Description is not as expected.");
        }

        [Test, Order(3)]
        public void ViewLastCreatedIdea()
        {
            myIdeasPage.OpenPage();
            myIdeasPage.ViewBtnLastIdea.Click();

            Assert.That(ideasReadPage.IdeaTitle.Text.Trim(), Is.EqualTo(lastCreatedIdeaTitle), "Title do not match.");
            Assert.That(ideasReadPage.IdeaDescription.Text.Trim(), Is.EqualTo(LastCreatedIdeaDescription), "Description do not match.");
        }

        [Test, Order(4)]
        public void EditIdeaTest()
        {
            myIdeasPage.OpenPage();
            myIdeasPage.EditBtnLastIdea.Click();

            string updatedTitle = "Changed Title: " + lastCreatedIdeaTitle;
            string updatedDescr = "Changed Descr: " + LastCreatedIdeaDescription;

            editIdeaPage.TitleField.Clear();
            editIdeaPage.TitleField.SendKeys(updatedTitle);

            editIdeaPage.DescriptionField.Clear();
            editIdeaPage.DescriptionField.SendKeys(updatedDescr);

            editIdeaPage.EditBtn.Click();

            Assert.That(driver.Url, Is.EqualTo(myIdeasPage.Url), "Not correctly redirected.");

            myIdeasPage.ViewBtnLastIdea.Click();

            Assert.That(ideasReadPage.IdeaTitle.Text.Trim(), Is.EqualTo(updatedTitle), "Title is not as expected.");
            Assert.That(ideasReadPage.IdeaDescription.Text.Trim(), Is.EqualTo(updatedDescr), "Description is not as expected.");
        }

        [Test, Order(5)]
        public void DeleteLastCreatedIdea()
        {
            myIdeasPage.OpenPage();
            myIdeasPage.DeleteBtnLastIdea.Click();

            bool isIdeaDeleted = myIdeasPage.IdeasCards.All(card => card.Text.Contains(LastCreatedIdeaDescription));

            Assert.IsTrue(isIdeaDeleted, "The idea was not deleted.");
        }
    }
}
