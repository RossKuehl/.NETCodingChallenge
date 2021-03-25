using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using MyContacts.API.Domain.Models;
using MyContacts.API.Domain.Services;
using MyContacts.API.Domain.Services.Communication;

namespace MyContacts.API.Tests.Mocks.Services
{
    public class MockContactService : Mock<IContactService>
    {
        public MockContactService MockFindByIdAsync(ContactResponse result)
        {
            Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(result);

            return this;
        }

        public MockContactService MockListAsync(IEnumerable<Contact> result)
        {
            Setup(x => x.ListAsync(It.IsAny<Expression<Func<Contact, bool>>>()))
                .ReturnsAsync(result);

            return this;
        }

        public MockContactService VerifyMockListAsync(Times times)
        {
            Verify(x => x.ListAsync(It.IsAny<Expression<Func<Contact, bool>>>()), times);

            return this;
        }
    }
}
