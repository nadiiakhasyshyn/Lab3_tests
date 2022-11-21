using RestSharp;
using System.Net;
using NUnit.Framework;
using RestSharp.Serialization.Json;
using RestSharp.Authenticators;

namespace RestTestLab3
{
    public class RestfulBookerTests
    {
        RestClient client;
        RestRequest request;
        IRestResponse response;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://restful-booker.herokuapp.com/");
        }

        [Test]
        public void CheckSeccessfullResponse_WhenGetBookingIds()
        {
            // Arrange 
            request = new RestRequest("booking", Method.GET);

            //Act
            response = client.Execute(request);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CheckSeccessfullResponse_WhenCreateBooking()
        {
            // Arrange 
            var testObject = new
            {
                firstname = "Van",
                lastname = "Helsing",
                totalprice = 1000,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2004-01-01",
                    checkout = "2005-01-01"
                },
                additionalneeds = "A more interesting life"
            };

            request = new RestRequest("booking", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(testObject);

            //Act
            response = client.Execute(request);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CheckSeccessfullResponse_WhenUpdateBooking()
        {
            // Arrange 
            var testObject = new
            {
                firstname = "Rhett",
                lastname = "Butler",
                totalprice = 2000,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2021-01-01",
                    checkout = "2022-01-01"
                },
                additionalneeds = "Money"
            };

            request = new RestRequest("booking", Method.GET);
            response = client.Execute(request);
            var bookings = new JsonDeserializer().Deserialize<List<Booking>>(response);

            client.Authenticator = new HttpBasicAuthenticator("admin", "password123");

            request = new RestRequest("booking/" + bookings[1].bookingId, Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            request.AddJsonBody(testObject);

            //Act
            response = client.Execute(request);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var result = new JsonDeserializer().Deserialize<Dictionary<string, string>>(response);
            Assert.That(result["firstname"], Is.EqualTo(testObject.firstname));
            Assert.That(result["lastname"], Is.EqualTo(testObject.lastname));
        }

        [Test]
        public void CheckSeccessfullResponse_WhenDeleteBooking()
        {
            // Arrange 
            request = new RestRequest("booking", Method.GET);
            response = client.Execute(request);
            var bookings = new JsonDeserializer().Deserialize<List<Booking>>(response);

            client.Authenticator = new HttpBasicAuthenticator("admin", "password123");

            request = new RestRequest("booking/" + bookings[0].bookingId, Method.DELETE);
            request.AddHeader("Content-Type", "application/json");

            //Act
            response = client.Execute(request);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
    }

    class Booking
    {
        public string bookingId { get; set; }
    }
}