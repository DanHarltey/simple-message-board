using MessageBoard.Contracts;
using MessageBoard.RestApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace MessageBoard.RestApi.FunctionalTests
{
    public class MessagesControllerTests : IClassFixture<WebAppFactoryInMemory>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public MessagesControllerTests(WebAppFactoryInMemory factory) => _factory = factory;

        [Fact]
        public async Task Get_Messages_Returns_Success()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/Messages");

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Post_Messages_Returns_Success()
        {
            var client = _factory.CreateClient();

            var helloMessage = JsonSerializer.Serialize(new SaveMessageRequest
            {
                Text = "Hello world"
            });

            var response = await client.PostAsync("/Messages", new StringContent(helloMessage, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Post_Too_Large_Messages_Returns_BadRequest()
        {
            var client = _factory.CreateClient();

            var tooLongMessage = string.Concat(Enumerable.Range(0, MessagesSize.MaxLength + 1).Select(x => 'a'));

            var message = JsonSerializer.Serialize(new SaveMessageRequest
            {
                Text = tooLongMessage
            });

            var response = await client.PostAsync("/Messages", new StringContent(message, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_Null_Messages_Returns_BadRequest()
        {
            var client = _factory.CreateClient();

            var message = JsonSerializer.Serialize(new SaveMessageRequest
            {
                Text = null
            });

            var response = await client.PostAsync("/Messages", new StringContent(message, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_Empty_Messages_Returns_BadRequest()
        {
            var client = _factory.CreateClient();

            var message = JsonSerializer.Serialize(new SaveMessageRequest
            {
                Text = string.Empty
            });

            var response = await client.PostAsync("/Messages", new StringContent(message, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_Messages_Then_Can_Read_Message()
        {
            var client = _factory.CreateClient();

            var messageText = "Hello world. My name is " + Guid.NewGuid();

            var helloMessage = JsonSerializer.Serialize(new SaveMessageRequest
            {
                Text = messageText
            });

            var postResponse = await client.PostAsync("/Messages", new StringContent(helloMessage, Encoding.UTF8, "application/json"));
            postResponse.EnsureSuccessStatusCode();

            var getResponse = await client.GetAsync("/Messages");
            getResponse.EnsureSuccessStatusCode();

            var responseText = await getResponse.Content.ReadAsStringAsync();

            var messagesReceived = JsonSerializer.Deserialize<List<string>>(responseText);

            Assert.NotEmpty(messagesReceived);
            Assert.Contains(messageText, messagesReceived);
        }
    }
}
