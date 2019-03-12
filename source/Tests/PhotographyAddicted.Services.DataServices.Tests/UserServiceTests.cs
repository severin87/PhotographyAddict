using Microsoft.EntityFrameworkCore;
using Moq;
using PhotographyAddicted.Data;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Web.Areas.Identity.Data;
using PhotographyAddicted.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PhotographyAddicted.Services.DataServices.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void GetCorrectCountNumber()
        {
            var userRepository = new Mock<IRepository<PhotographyAddictedUser>>();

            userRepository.Setup(r => r.All()).Returns(new List<PhotographyAddictedUser>()
            {
                new PhotographyAddictedUser(),
                new PhotographyAddictedUser(),
                new PhotographyAddictedUser()
            }.AsQueryable());

            var service = new UserService(userRepository.Object);
            Assert.Equal(3,service.GetCount());
        }

        [Fact]
        public async Task GetCorrectCountNumberDbContext()
        {
            var options = new DbContextOptionsBuilder<PhotographyAddictedContext>()
                .UseInMemoryDatabase(databaseName: "Find_User_Database")
                .Options;

            var dbContext = new PhotographyAddictedContext(options);

            dbContext.Add(new PhotographyAddictedUser() { Id = "blq"});
            dbContext.Add(new PhotographyAddictedUser());
            dbContext.Add(new PhotographyAddictedUser());
          
            await dbContext.SaveChangesAsync();
            dbContext.PhotographyAddictedUsers.Add(new PhotographyAddictedUser());
            await dbContext.SaveChangesAsync();

            var opit = dbContext.PhotographyAddictedUsers.FirstOrDefault(c=>c.Id=="blq");
            //dbContext.Remove(opit);
            //await dbContext.SaveChangesAsync();

            var repository = new DbRepository<PhotographyAddictedUser>(dbContext);
            //PhotographyAddictedUser opitProba = new PhotographyAddictedUser() {Id = "veche" };
            //repository.Update(opit,opitProba);
            //await repository.SaveChangesAsync();

            var service = new UserService(repository);
            var count = service.GetCount();
                       
            Assert.Equal(4, count);
            Assert.Equal(2,service.GetSpecificUser(2).Count());

        }
    }    

    //public class UsersRepository : IRepository<PhotographyAddictedUser>
    //{
    //    public Task AddAsync(PhotographyAddictedUser entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IQueryable<PhotographyAddictedUser> All()
    //    {
    //return new List<PhotographyAddictedUser>()
    //{
    //    new PhotographyAddictedUser(),
    //    new PhotographyAddictedUser(),
    //    new PhotographyAddictedUser()
    //}.AsQueryable();
    //    }

    //    public void Delete(PhotographyAddictedUser entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<int> SaveChangesAsync()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Update(PhotographyAddictedUser entity)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
