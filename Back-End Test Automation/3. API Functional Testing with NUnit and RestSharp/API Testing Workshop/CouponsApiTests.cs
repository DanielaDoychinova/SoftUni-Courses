using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API_Testing_Workshop
{
    public class CouponsApiTests : IDisposable
    {
        private RestClient client;
        private string token;

        [SetUp]
        public void SetUp()
        {
            client = new RestClient(GlobalConstants.BaseUrl);
            token = GlobalConstants.AuthenticateUser("admin@gmail.com", "admin@gmail.com");

            Assert.That(token, Is.Not.Null.Or.Empty, "Token is null or empty.");
        }
        public void Dispose()
        {
            client?.Dispose();
        }

        [Test, Order(1)]
        public void Test_GetAllCoupons()
        {
            var request = new RestRequest("coupon", Method.Get);

            var response = client.Execute(request);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
                Assert.That(response.Content, Is.Not.Empty, "Response content is empty.");

                var coupons = JArray.Parse(response.Content);


                Assert.That(coupons.Type, Is.EqualTo(JTokenType.Array));
                Assert.That(coupons.Count, Is.GreaterThan(0));


                var colorTitles = coupons.Select(c => c["name"]?.ToString()).ToList();
                Assert.That(colorTitles, Does.Contain("SUMMER21"));
                Assert.That(colorTitles, Does.Contain("WINTER21"));
                Assert.That(colorTitles, Does.Contain("BLACKFRIDAY"));

                foreach (var coupon in coupons)
                {
                    Assert.That(coupon["name"]?.ToString(), Is.Not.Null.Or.Empty);
                    Assert.That(coupon["_id"]?.ToString(), Is.Not.Null.Or.Empty);
                    Assert.That(coupon["expiry"]?.ToString(), Is.Not.Null.Or.Empty);

                    int discountValue = coupon["expiry"].Value<int>();
                    Assert.That(discountValue, Is.GreaterThan(0));

                    Assert.That(DateTime.TryParse(coupon["expiry"]?.ToString(), out var expiryDate), Is.True);
                    Assert.That(expiryDate, Is.GreaterThan(DateTime.Now));
                }

                
            });


        }

        [Test, Order(2)]
        public void Test_AddCoupon()
        {
            var request = new RestRequest("coupon", Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddJsonBody(new
            {
                name = "New Coupon",
                discount = 20,
                expiry = "2026-12-31"

            });

            var response = client.Execute(request);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response.Content, Is.Not.Empty);

                var content = JObject.Parse(response.Content);

                Assert.That(content["_id"].ToString(), Is.Not.Null.And.Not.Empty);
                Assert.That(content["name"].ToString(), Is.EqualTo("New Coupon"));
                Assert.That(content["discount"].Value<int>(), Is.EqualTo(20));
                Assert.That(content["expiry"].ToString(), Is.EqualTo("2026/12/31 12:00:00 AM"));

                Assert.That(content.ContainsKey("createdAt"), Is.True);
                Assert.That(content.ContainsKey("updatedAt"), Is.True);

                Assert.That(DateTime.TryParse(content["expiry"]?.ToString(), out var expiryDate), Is.True);
                Assert.That(expiryDate, Is.GreaterThan(DateTime.Now));
            });
        }

        [Test, Order(3)]
        public void Test_UpdateCoupon()
        {
            var getRequest = new RestRequest("coupon", Method.Get);

            var getResponse = client.Execute(getRequest);


            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
            Assert.That(getResponse.Content, Is.Not.Empty, "Response content is empty.");

            var coupons = JArray.Parse(getResponse.Content);
            var couponToUpdate = coupons.FirstOrDefault(c => c["name"].ToString() == "FLASHSALE");

            Assert.That(couponToUpdate, Is.Not.Empty);

            var couponId = couponToUpdate["_id"].ToString();

            var updateRequest = new RestRequest("coupon/{id}", Method.Put);
            updateRequest.AddUrlSegment("id", couponId);
            updateRequest.AddJsonBody(new
            {
                name = "Updated Coupon",
                discount = 25,
                expiry = "2025-12-31"
            });
            var updateResponse = client.Execute(updateRequest);
            Assert.Multiple(() =>
            {

                Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
                Assert.That(updateResponse.Content, Is.Not.Empty, "Response content is empty.");

                var content = JObject.Parse(updateResponse.Content);

                Assert.That(content["title"].ToString(), Is.EqualTo("Updated Color"));
                Assert.That(content["_id"].ToString(), Is.EqualTo(couponId));

                Assert.That(content["name"].ToString(), Is.EqualTo("Updated Coupon"));
                Assert.That(content["discount"].Value<int>(), Is.EqualTo(25));
                Assert.That(content["expiry"].ToString(), Is.EqualTo("2025/12/31 12:00:00 AM"));

                Assert.That(DateTime.TryParse(content["expiry"]?.ToString(), out var expiryDate), Is.True);
                Assert.That(expiryDate, Is.GreaterThan(DateTime.Now));
            });
        }

        [Test, Order(4)]
        public void Test_DeleteCoupon()
        {
            var getRequest = new RestRequest("coupon", Method.Get);

            var getResponse = client.Execute(getRequest);


            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
            Assert.That(getResponse.Content, Is.Not.Empty, "Response content is empty.");

            var coupons = JArray.Parse(getResponse.Content);
            var couponToDelete = coupons.FirstOrDefault(c => c["name"].ToString() == "SPRING21");

            Assert.That(couponToDelete, Is.Not.Null);

            var couponId = couponToDelete["_id"].ToString();

            var deleteRequest = new RestRequest("coupon/{id}", Method.Delete);
            deleteRequest.AddUrlSegment("id", couponId);
            deleteRequest.AddHeader("Authorization", $"Bearer {token}");

            var deleteResponse = client.Execute(deleteRequest);

            Assert.Multiple(() =>
            {
                Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                var verifyGetRequest = new RestRequest("coupon/{id}", Method.Get);
                verifyGetRequest.AddUrlSegment("id", couponId);

                var verifyGetResponse = client.Execute(verifyGetRequest);
                var verifyListResponse = client.Execute(getRequest);

                var updatedCoupons = JArray.Parse(verifyGetResponse.Content);
                var deletedCoupon = updatedCoupons.FirstOrDefault(c => c["_id"].ToString() == couponId);

                Assert.That(deletedCoupon, Is.Null);
            });

        }
    }
}
