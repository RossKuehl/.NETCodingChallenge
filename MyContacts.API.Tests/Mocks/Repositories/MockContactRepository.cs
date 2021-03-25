using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using MyContacts.API.Domain.Models;
using MyContacts.API.Domain.Repositories;

namespace MyContacts.API.Tests.Mocks.Repositories
{
    public class MockContactRepository : Mock<IContactRepository>
    {
        public MockContactRepository MockFindByIdAsync(Contact result)
        {
            Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(result);

            return this;
        }

        public MockContactRepository MockListAsync(IEnumerable<Contact> result)
        {
            Setup(x => x.ListAsync(It.IsAny<Expression<Func<Contact, bool>>>()))
                .ReturnsAsync(result);

            return this;
        }

        public MockContactRepository VerifyMockListAsync(Times times)
        {
            Verify(x => x.ListAsync(It.IsAny<Expression<Func<Contact, bool>>>()), times);

            return this;
        }
    }
}
