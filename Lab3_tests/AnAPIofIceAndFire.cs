using RestSharp;
using System.Net;
using NUnit.Framework;

namespace RestTestLab3
{
    public class TestAnAPIofIceAndFireAPITests
    {
        RestClient client;
        string characterID = "583";
        string bookID = "1";
        string houseID = "378";
        RestRequest request;
        IRestResponse response;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://anapioficeandfire.com/api/");
        }

        [Test]
        public void CheckSeccessfullResponse_WhenGetCharacterRequestWithId()
        {
            // arrange
            RestRequest request = new RestRequest("characters/" + characterID, Method.GET);

            // act
            IRestResponse response = client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

    
        [Test]
        public void CheckSeccessfullResponse_WhenGetBookRequestWithId()
        {
            // arrange
            RestRequest request = new RestRequest("books/" + bookID, Method.GET);

            // act
            IRestResponse response = client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CheckSeccessfullResponse_WhenGetHouseRequestWithId()
        {
            // arrange
            RestRequest request = new RestRequest("houses/" + houseID, Method.GET);

            // act
            IRestResponse response = client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

    }
}
