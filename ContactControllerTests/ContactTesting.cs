using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ContactsApi.Dtos;
using ContactsApi.Services;
using FluentAssertions;

namespace ContactControllerTests
{
    public class ContactTesting
    {
        private readonly Mock<IContactService> contactServiceMock;
        private readonly Mock<ILogger<ContactController>> loggerMock;
        private readonly ContactController contactController;

        public ContactTesting()
        {
            contactServiceMock = new Mock<IContactService>();
            loggerMock = new Mock<ILogger<ContactController>>();
            contactController = new ContactController(contactServiceMock.Object, loggerMock.Object);
        }

        [Fact]
        public async Task GetAllContacts_ShouldReturnOkWithContactDtos()
        {
            var fakeContactDtos = new List<ContactDto>
            {
                new ContactDto { Id = 1, FirstName = "John Doe" },
                new ContactDto { Id = 2, FirstName = "Jane Doe" }
            };

            contactServiceMock.Setup(service => service.GetAllContacts())
                              .ReturnsAsync(fakeContactDtos);

            var result = await contactController.GetAllContacts();

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(fakeContactDtos);
        }

        [Fact]
        public async Task GetAllContacts_ShouldReturnNotFoundWhenEmptyList()
        {
            contactServiceMock.Setup(service => service.GetAllContacts())
                              .ReturnsAsync(new List<ContactDto>());

            var result = await contactController.GetAllContacts();

            result.Should().BeOfType<NotFoundObjectResult>();
        }


        [Fact]
        public async Task GetContactById_ShouldReturnOkWithContactDto()
        {
           
            var contactId = 1;
            var existingContact = new ContactDto { Id = contactId, FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com" };

            contactServiceMock.Setup(service => service.GetContactById(contactId))
                              .ReturnsAsync(existingContact);

         
            var result = await contactController.GetContactById(contactId);


            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(existingContact);
        }




        [Fact]
        public async Task GetContact_ShouldReturnOkForValidId()
        {
            var fakeContactDto = new ContactDto { Id = 1, FirstName = "John Doe" };

            
            contactServiceMock.Setup(service => service.GetContactById(1))
                              .ReturnsAsync(fakeContactDto);

         
            var result = await contactController.GetContactById(1);

           
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(fakeContactDto);
        }

    }
}
