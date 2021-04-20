using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestioProject.BLL;
using TestioProject.Controllers;
using TestioProject.DAL.Models;
using TestioProject.PL.Models;
using TestioProject.PL.Services;
using Xunit;

namespace TestProject.xUnit
{
    public class AdminControllerTests
    {
        [Fact]
        public void IndexTest()
        {
            ////Arrange
            //var mockUserManager = new Mock<UserManager<ApplicationUser>>();
            //var mockDataManager = new Mock<DataManager>();
            //var controller = new AdminController(mockDataManager.Object, mockUserManager.Object);

            ////Act
            //ViewResult result = controller.Index() as ViewResult;

            ////Assert
            //Assert.Equal("Index", result?.ViewName);

            //var result = controller.Index();

            //var viewResult = Assert.IsType<ViewResult>(result);

            //store.Setup(x => x.FindByIdAsync("123")).ReturnsAsync(new IdentityUser() { UserName = "test@gmail.com", Id = "123" });
        }
        //[Fact]
        //public void TestService()
        //{
        //    var mockDataManager = new Mock<IDataManager>();
        //    mockDataManager.Setup(x => x.Tests.GetTestById(1, true)).Returns(new Test() { Id = 1, Name = "test1", Description = "desc1", UserId = "1" });

        //    Assert.Equal(typeof(TestViewModel), typeof(TestViewModel));

        //}
    }
}
