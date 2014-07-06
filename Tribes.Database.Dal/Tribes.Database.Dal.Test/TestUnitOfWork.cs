using System.Data.Entity;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribes.Database.Context;
using Tribes.Database.Context.Entities;

namespace Tribes.Database.Dal.Test
{
    [TestFixture]
    public class TestUnitOfWork
    {
        [Test]
        public void UnitOfWorkCanBeInstantiated()
        {
            var unit = new UnitOfWork(null);
            Assert.IsInstanceOf(typeof(UnitOfWork), unit);
        }

        [Test]
        public void UnitOfWorkCanCreateRepositry()
        {
            var context = new Mock<IDbContext>();
            var unit = new UnitOfWork(context.Object);
            var dbSet = new Mock<IDbSet<Tribe>>();
            var guid = Guid.NewGuid();
            var tribe = new Tribe { Name = "Test Tribe" };

            dbSet.Setup(x => x.Find(guid)).Returns(tribe);
            context.Setup(x => x.Set<Tribe>()).Returns(dbSet.Object);

            var retTribe = unit.Repository<Tribe>().GetById(guid);
            Assert.That(retTribe.Name, Is.EqualTo("Test Tribe"));
            context.Verify();
            dbSet.Verify();
        }

        [Test]
        public void UnitOfWorkInvokeSave()
        {
            var context = new Mock<IDbContext>();
            var unit = new UnitOfWork(context.Object);

            context.Setup(x => x.SaveChanges()).Returns(0);

            unit.Save();
            context.Verify();
        }
    }
}
