using RestSharpServices;
using System.Net;
using System.Reflection.Emit;
using System.Text.Json;
using RestSharp;
using RestSharp.Authenticators;
using NUnit.Framework.Internal;
using RestSharpServices.Models;
using System;

namespace TestGitHubApi
{
    public class TestGitHubApi
    {
        private GitHubApiClient client;
        string repoName = "test-nakov-repo";
        int lastCreatedIssueNumber;
        long lastCreatedCommentId;


        [SetUp]
        public void Setup()
        {            
            client = new GitHubApiClient("https://api.github.com/repos/testnakov/", "DanielaDoychinova", "token");
        }


        [Test, Order(1)]
        public void Test_GetAllIssuesFromARepo()
        {
            var issues = client.GetAllIssues(repoName);

            Assert.That(issues, Has.Count.GreaterThan(0), "There should be more than one issue");

            foreach (var issue in issues)
            {
                Assert.That(issue.Id, Is.GreaterThan(0), "Issue ID should be bigger than 0.");
                Assert.That(issue.Number, Is.GreaterThan(0), "Issue Number should be bigger than 0.");
                Assert.That(issue.Title, Is.Not.Empty, "Issue Title should not be empty.");
            }
        }

        [Test, Order(2)]
        public void Test_GetIssueByValidNumber()
        {

            int issueNumber = 1;


            var issue = client.GetIssueByNumber(repoName,
                issueNumber);

            Assert.IsNotNull(issue, "The data from the response should not be null.");
            Assert.That(issue.Id, Is.GreaterThan(0), "Issue ID should be greater than 0.");
            Assert.That(issue.Number, Is.GreaterThan(0), "Issue Number should be greater than 0.");
        }

        [Test, Order(3)]
        public void Test_GetAllLabelsForIssue()
        {
            int issueNumber = 6;

            var labels = client.GetAllLabelsForIssue(repoName, issueNumber);

            Assert.That(labels, Has.Count.GreaterThan(0), "Labels Count should be greater than 0.");

            foreach (var label in labels)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(label.Id, Is.GreaterThan(0), "Label ID should be bigger than 0.");
                    Assert.That(label.Name, Is.Not.Empty, "Label Name should not be empty.");
                });


                Console.WriteLine("Label: " + label.Id + " - Name: " + label.Name);
            }
        }

        [Test, Order(4)]
        public void Test_GetAllCommentsForIssue()
        {

            int issueNumber = 6;

            var comments = client.GetAllCommentsForIssue(repoName, issueNumber);

            Assert.That(comments, Has.Count.GreaterThan(0), "Comments Count should be bigger than 0.");

            foreach (var comment in comments)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(comment.Id, Is.GreaterThan(0), "Comment ID should be greater than 0.");
                    Assert.That(comment.Body, Is.Not.Empty, "Comment Body should not be empty.");
                });

                Console.WriteLine("Comment: " + comment.Id + " - Body: " + comment.Body);
            }
        }

        [Test, Order(5)]
        public void Test_CreateGitHubIssue()
        {
            string title = "Some title";
            string body = "Some body";

            var issue = client.CreateIssue(repoName, title, body);

            Assert.Multiple(() =>
            {
                Assert.That(issue.Id, Is.GreaterThan(0), "Issue ID should be greater than 0.");
                Assert.That(issue.Number, Is.GreaterThan(0), "Issue Number should be greater than 0.");
                Assert.That(issue.Title, Is.EqualTo(title), "Issue title should be 'Some title'.");
                Assert.That(issue.Body, Is.EqualTo(body), "Issue Body should be 'Some body'.");
            });

            Console.WriteLine(issue.Number);
            lastCreatedIssueNumber = issue.Number;
        }

        [Test, Order(6)]
        public void Test_CreateCommentOnGitHubIssue()
        {
            var commentBody = "Some body";

            var comment = client.CreateCommentOnGitHubIssue(repoName, lastCreatedIssueNumber, commentBody);

            Assert.That(comment.Body, Is.EqualTo(commentBody), "Comment body should be 'Some body'.");

            Console.WriteLine(comment.Id);
            lastCreatedCommentId = comment.Id;
        }

        [Test, Order(7)]
        public void Test_GetCommentById()
        {
            var comment = client.GetCommentById(repoName, lastCreatedCommentId);

            Assert.IsNotNull(comment);
            Assert.That(comment.Id, Is.EqualTo(lastCreatedCommentId), "Comment ID should be the same as lastCreatedCommentId.");
        }


        [Test, Order(8)]
        public void Test_EditCommentOnGitHubIssue()
        {
            string newBody = "New body";

            var comment = client.EditCommentOnGitHubIssue(repoName, lastCreatedCommentId, newBody);

            Assert.IsNotNull(comment);
            Assert.That(comment.Id, Is.EqualTo(lastCreatedCommentId), "Comment ID should be the same as lastCreatedCommentId.");
            Assert.That(comment.Body, Is.EqualTo(newBody), "Comment body should be 'New body'.");
        }

        [Test, Order(9)]
        public void Test_DeleteCommentOnGitHubIssue()
        {
            var result = client.DeleteCommentOnGitHubIssue(repoName, lastCreatedCommentId);

            Assert.IsTrue(result, "The comment should be deleted.");

        }


    }
}

