using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyContacts.API.Controllers;
using MyContacts.API.Domain.Models;
using MyContacts.API.Domain.Services.Communication;
using MyContacts.API.Mapping;
using MyContacts.API.Resources;
using MyContacts.API.Tests.Mocks.Services;
using Xunit;
using PhoneType = MyContacts.API.Domain.Models.PhoneType;

namespace MyContacts.API.UnitTests.ControllerTests
{
    public class ContactsControllerTests
    {
        [Fact]
        public async Task ContactsController_GetAllAsync_ContactsExist()
        {
            //Arrange
            var mockContacts = new List<Contact>
            {
                new Contact
                {
                    Id = 100,
                    FirstName = "Ross",
                    LastName = "Kuehl",
                    Email = "rosskuehl@outlook.com",
                    Phone = new List<Phone>
                    {
                        new Phone
                        {
                            Id = 100,
                            Number = "757-707-0904",
                            Type = PhoneType.Mobile,
                            ContactId = 100
                        }
                    }
                },
                new Contact
                {
                    Id = 101,
                    FirstName = "Joe",
                    LastName = "Nobody",
                    Email = "jnobody123@gmails.com",
                    Phone = new List<Phone>
                    {
                        new Phone
                        {
                            Id = 101,
                            Number = "555-123-4567",
                            Type = PhoneType.Home,
                            ContactId = 100
                        }
                    }
                }
            };

            var mockContactService = new MockContactService().MockListAsync(mockContacts);

            var modelToResourceProfile = new ModelToResourceProfile();
            var resourceToModelProfile = new ResourceToModelProfile();
            var mapperConfig = new MapperConfiguration(c => c.AddProfiles(new List<Profile> { modelToResourceProfile, resourceToModelProfile }));

            var controller = new ContactsController(mockContactService.Object, new Mapper(mapperConfig));

            //Act
            var result = await controller.GetAllAsync();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<ContactResource>>(result);
            mockContactService.VerifyMockListAsync(Times.Once());
        }

        [Fact]
        public async Task ContactsController_GetAllAsync_NoContacts()
        {
            //Arrange
            var mockContactService = new MockContactService().MockListAsync(new List<Contact>());

            var modelToResourceProfile = new ModelToResourceProfile();
            var resourceToModelProfile = new ResourceToModelProfile();
            var mapperConfig = new MapperConfiguration(c => c.AddProfiles(new List<Profile> { modelToResourceProfile, resourceToModelProfile }));

            var controller = new ContactsController(mockContactService.Object, new Mapper(mapperConfig));

            //Act
            var result = await controller.GetAllAsync();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<ContactResource>>(result);
            mockContactService.VerifyMockListAsync(Times.Once());
        }

        [Fact]
        public async Task ContactsController_FindByIdAsync_ContactExists()
        {
            //Arrange
            var myContact = new Contact
            {
                Id = 100,
                FirstName = "Ross",
                LastName = "Kuehl",
                Email = "rosskuehl@outlook.com",
                Phone = new List<Phone>
                {
                    new Phone
                    {
                        Id = 100,
                        Number = "757-707-0904",
                        Type = PhoneType.Mobile,
                        ContactId = 100
                    }
                }
            };
            var response = new ContactResponse(myContact);

            var mockContactService = new MockContactService().MockFindByIdAsync(response);

            var modelToResourceProfile = new ModelToResourceProfile();
            var resourceToModelProfile = new ResourceToModelProfile();
            var mapperConfig = new MapperConfiguration(c => c.AddProfiles(new List<Profile> { modelToResourceProfile, resourceToModelProfile }));

            var controller = new ContactsController(mockContactService.Object, new Mapper(mapperConfig));

            //Act
            var result = await controller.FindByIdAsync(myContact.Id) as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ContactResource>(result.Value);
        }

        [Fact]
        public async Task ContactsController_FindByIdAsync_NoContact()
        {
            //Arrange
            var response = new ContactResponse("Contact not found.");

            var mockContactService = new MockContactService().MockFindByIdAsync(response);

            var modelToResourceProfile = new ModelToResourceProfile();
            var resourceToModelProfile = new ResourceToModelProfile();
            var mapperConfig = new MapperConfiguration(c => c.AddProfiles(new List<Profile> { modelToResourceProfile, resourceToModelProfile }));

            var controller = new ContactsController(mockContactService.Object, new Mapper(mapperConfig));

            //Act
            var result = await controller.FindByIdAsync(100) as BadRequestObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ErrorResource>(result.Value);
        }
    }
}
