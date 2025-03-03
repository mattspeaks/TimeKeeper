using Xunit;
using Moq;
using TimeKeeper.Interfaces;
using TimeKeeper.Controllers;
using TimeKeeper.Services;
using Microsoft.AspNetCore.Http;
using TimeKeeper.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;


namespace TimeKeeper.Test.UnitTests.ControllerTests

{
    public class ToDoControllerTests
    {
        private readonly Mock<IToDoNoteService> _mockToDoNoteService = new Mock<IToDoNoteService>();
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        private readonly Mock<IUserService> _mockUserService = new Mock<IUserService>();
        private readonly ToDoController _toDoController;

        public ToDoControllerTests()
        {
            _toDoController = new ToDoController(_mockToDoNoteService.Object, _mockHttpContextAccessor.Object, _mockUserService.Object);
        }



        [Fact]
        public async Task ToDoController_Index()
        {
            // Arrange
            var toDos = new List<ToDoNote>
            {
                new ToDoNote {Id = 1, Label = "Test1", Description = "Test1", IsCompleted = false, UserId = "1"},
                new ToDoNote {Id = 2, Label = "Test2", Description = "Test2", IsCompleted = false, UserId = "1"},
                new ToDoNote {Id = 3, Label = "Test3", Description = "Test3", IsCompleted = true, UserId = "1"}
            };

            var userId = "1";

            _mockUserService.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            _mockToDoNoteService.Setup(service => service.GetToDoNoteByUserID(userId)).ReturnsAsync(toDos);


            // Act
            var response = await _toDoController.Index() as ViewResult;

            // Assert
            _mockUserService.Verify(x => x.GetUserId(It.IsAny<ClaimsPrincipal>()), Times.Once());
            _mockUserService.VerifyNoOtherCalls();
            _mockToDoNoteService.Verify(x => x.GetToDoNoteByUserID(userId), Times.Once());
            _mockToDoNoteService.VerifyNoOtherCalls();
            Assert.NotNull(response);
            Assert.Equal(toDos, response.Model);
        }

        [Fact]
        public async Task ToDoController_NoNotes()
        {
            var userId = "2";
            var emptyList = new List<ToDoNote>();
            _mockUserService.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            _mockToDoNoteService.Setup(service => service.GetToDoNoteByUserID(userId)).ReturnsAsync(emptyList);


            // Act
            var response = await _toDoController.Index() as ViewResult;

            // Assert
            _mockUserService.Verify(x => x.GetUserId(It.IsAny<ClaimsPrincipal>()), Times.Once());
            _mockUserService.VerifyNoOtherCalls();
            _mockToDoNoteService.Verify(x => x.GetToDoNoteByUserID(userId), Times.Once());
            _mockToDoNoteService.VerifyNoOtherCalls();
            Assert.NotNull(response);
            Assert.Equal(emptyList, response.Model);

        }

        [Fact]
        public async Task ToDoController_CreateNote()
        {
            var toDoNote = new ToDoNote { Id = 1, Label = "Test1", Description = "Test1", IsCompleted = false, UserId = "1" };
            var userId = "1";
            _mockUserService.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            _mockToDoNoteService.Setup(service => service.SaveToDoNote(toDoNote)).ReturnsAsync(true);


            //Act
            var response = await _toDoController.Create(toDoNote) as RedirectToActionResult;


            //Assert
            _mockUserService.Verify(x => x.GetUserId(It.IsAny<ClaimsPrincipal>()), Times.Once());
            _mockUserService.VerifyNoOtherCalls();
            _mockToDoNoteService.Verify(x => x.SaveToDoNote(toDoNote), Times.Once());
            _mockToDoNoteService.VerifyNoOtherCalls();
            Assert.NotNull(response);
            Assert.Equal("Index", response.ActionName);

        }

        [Fact]

        public async Task ToDoController_Complete()
        {
            var toDoNote = new ToDoNote { Id = 1, Label = "Testing", Description = "Marking Complete", IsCompleted = false, UserId = "1" };
            var userId = "1";

            _mockToDoNoteService.Setup(service => service.GetToDoNoteById(toDoNote.Id)).ReturnsAsync(toDoNote);
            _mockToDoNoteService.Setup(service => service.UpdateToDoNote(toDoNote)).ReturnsAsync(true);
        
            //Act
            var response = await _toDoController.Complete(toDoNote.Id) as RedirectToActionResult;

            //Assert
            _mockToDoNoteService.Verify(x => x.GetToDoNoteById(toDoNote.Id), Times.Once());
            _mockToDoNoteService.Verify(x => x.UpdateToDoNote(toDoNote), Times.Once());
            _mockToDoNoteService.VerifyNoOtherCalls();
            Assert.NotNull(response);
            Assert.Equal("Index", response.ActionName);
        }

        [Fact]

        public async Task ToDoController_Undo()
        {
            var toDoNote = new ToDoNote { Id = 1, Label = "Testing", Description = "Marking Incomplete", IsCompleted = true, UserId = "1" };
            var userId = "1";

            _mockToDoNoteService.Setup(service => service.GetToDoNoteById(toDoNote.Id)).ReturnsAsync(toDoNote);
            _mockToDoNoteService.Setup(service => service.UpdateToDoNote(toDoNote)).ReturnsAsync(true);

            //Act
            var response = await _toDoController.Undo(toDoNote.Id) as RedirectToActionResult;

            //Assert
            _mockToDoNoteService.Verify(x => x.GetToDoNoteById(toDoNote.Id), Times.Once());
            _mockToDoNoteService.Verify(x => x.UpdateToDoNote(toDoNote), Times.Once());
            _mockToDoNoteService.VerifyNoOtherCalls();
            Assert.NotNull(response);
            Assert.Equal("Index", response.ActionName);
        }

        [Fact]
        public async Task ToDoController_GetNoteById()
        {
            var toDoNote = new ToDoNote { Id = 1, Label = "Test1", Description = "Test1", IsCompleted = false, UserId = "1" };
            var userId = "1";
            var id = 1;

            _mockUserService.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            _mockToDoNoteService.Setup(service => service.GetToDoNoteById(id)).ReturnsAsync(toDoNote);


            //Act
            var response = await _toDoController.Edit(id) as ViewResult;


            //Assert
            _mockUserService.Verify(x => x.GetUserId(It.IsAny<ClaimsPrincipal>()), Times.Once());
            _mockUserService.VerifyNoOtherCalls();
            _mockToDoNoteService.Verify(x => x.GetToDoNoteById(id), Times.Once());
            _mockToDoNoteService.VerifyNoOtherCalls();
            Assert.NotNull(response);
            Assert.Equal(toDoNote, response.Model);
        }

        

        [Fact]
        public async Task ToDoController_UpdateToDoNote()
        {
            var toDoNote = new ToDoNote { Id = 1, Label = "Test1", Description = "Test1", IsCompleted = false, UserId = "1" };
            var userId = "1";
            var id = 1;

            _mockUserService.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            _mockToDoNoteService.Setup(service => service.GetToDoNoteById(id)).ReturnsAsync(toDoNote);
            _mockToDoNoteService.Setup(service => service.UpdateToDoNote(toDoNote)).ReturnsAsync(true);
            //Act

            var response = await _toDoController.UpdateToDoNote(id, toDoNote) as RedirectToActionResult;
            //Assert

            _mockUserService.Verify(x => x.GetUserId(It.IsAny<ClaimsPrincipal>()), Times.Once());
            _mockUserService.VerifyNoOtherCalls();
            _mockToDoNoteService.Verify(x => x.GetToDoNoteById(id), Times.Once());
            _mockToDoNoteService.Verify(x => x.UpdateToDoNote(toDoNote), Times.Once());
            _mockToDoNoteService.VerifyNoOtherCalls();
            Assert.NotNull(response);
            Assert.Equal("Index", response.ActionName);
        }


        [Fact]
        public async Task ToDoController_Destroy()
        {
            var toDoNote = new ToDoNote { Id = 1, Label = "Test1", Description = "Test1", IsCompleted = false, UserId = "1" };
            var id = 1;
            var userId = "1";

            _mockUserService.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            _mockToDoNoteService.Setup(service => service.DeleteToDoNote(id)).ReturnsAsync(true);

            //Act
            var response = await _toDoController.Destroy(id) as RedirectToActionResult;


            //Assert
            _mockUserService.Verify(x => x.GetUserId(It.IsAny<ClaimsPrincipal>()), Times.Once());
            _mockUserService.VerifyNoOtherCalls();
            _mockToDoNoteService.Verify(x => x.DeleteToDoNote(id), Times.Once());
            _mockToDoNoteService.VerifyNoOtherCalls();
            Assert.NotNull(response);
            Assert.Equal("Index", response.ActionName);
        }
    }
}

