using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    class TestRepository
    {
        [Test]
        public void CanRepositoryBeCreated()
        {
            var context = new Mock<IDbContext>();
            var repo = new Repository<Clan>(context.Object);
            Assert.That(repo, Is.InstanceOf<Repository<Clan>>());
        }

        [Test]
        public void CanRepositoryGetMethodBeInvoked()
        {

            var context = Context();
            var repo = new Repository<Tribe>(context.Object);

            var retQuery = repo.Get();
            Assert.That(retQuery, Is.InstanceOf<IQueryable<Tribe>>());

            var l = retQuery.ToList();

            Assert.AreEqual(3, l.Count());
            Assert.AreEqual("BBB", l[0].Name);
            Assert.AreEqual("ZZZ", l[1].Name);
            Assert.AreEqual("AAA", l[2].Name);
        }

        [Test]
        public void CanRepositoryGetWithFilterMethodBeInvoked()
        {

            var context = Context();
            var repo = new Repository<Tribe>(context.Object);

            var retQuery = repo.Get(x => x.Name == "ZZZ");
            Assert.That(retQuery, Is.InstanceOf<IQueryable<Tribe>>());

            var l = retQuery.ToList();

            Assert.AreEqual(1, l.Count());
            Assert.AreEqual("ZZZ", l[0].Name);
        }

        [Test]
        public void CanRepositoryGetWithOrderByMethodBeInvoked()
        {

            var context = Context();
            var repo = new Repository<Tribe>(context.Object);

            var retQuery = repo.Get(orderBy:x=> x.OrderBy(y=>y.Name));
            Assert.That(retQuery, Is.InstanceOf<IQueryable<Tribe>>());

            var l = retQuery.ToList();

            Assert.AreEqual(3, l.Count());
            Assert.AreEqual("AAA", l[0].Name);
            Assert.AreEqual("BBB", l[1].Name);
            Assert.AreEqual("ZZZ", l[2].Name);
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
