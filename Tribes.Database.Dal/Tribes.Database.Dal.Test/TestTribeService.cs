using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Tribes.Database.Context;
using Tribes.Database.Context.Entities;

namespace Tribes.Database.Dal.Test
{
    [TestFixture]
    public class TestTribeService
    {
        [Test]
        public void CanTribeServiceBeCreated()
        {
            var service = new TribesService();
            Assert.That(service, Is.InstanceOf<TribesService>());
        }

        [Test]
        public void CanTribeServiviceReturnAllTribes()
        {
            var context = Context();
            
            var service = new TribesService(context.Object);

            var tribes = service.GetAllTribes();

            Assert.That(tribes.Count(), Is.EqualTo(3));
            context.Verify();
        }



        private static Mock<TribesContext> Context()
        {
            var tribes = new List<Tribe>
            {
                new Tribe {Name = "BBB"},
                new Tribe {Name = "ZZZ"},
                new Tribe {Name = "AAA"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Tribe>>();
            mockSet.As<IQueryable<Tribe>>().Setup(m => m.Provider).Returns(tribes.Provider);
            mockSet.As<IQueryable<Tribe>>().Setup(m => m.Expression).Returns(tribes.Expression);
            mockSet.As<IQueryable<Tribe>>().Setup(m => m.ElementType).Returns(tribes.ElementType);
            mockSet.As<IQueryable<Tribe>>().Setup(m => m.GetEnumerator()).Returns(tribes.GetEnumerator());

            var context = new Mock<TribesContext>();
            context.Setup(x => x.Tribes).Returns(mockSet.Object);
            context.Setup(x => x.Set<Tribe>()).Returns(mockSet.Object);
            return context;
        }
    }
}
