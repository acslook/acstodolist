

using Acs.TodoList.Domain.Entities;
using Acs.TodoList.Domain.Interfaces.Repositories;
using Acs.TodoList.Services;

namespace Acs.TodoList.Tests
{
    public class TodoItemServiceTests
    {
        private Mock<ITodoItemRepository> _repositoryItem;
        private readonly Fixture _fixture;

        public TodoItemServiceTests()
        {
            _repositoryItem = new Mock<ITodoItemRepository>();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task TodoItemService_Add_ShouldSuccessExecute()
        {
            // Arrange
            var service = new TodoItemService(_repositoryItem.Object);
            var newItem = _fixture.Create<Item>();

            // Action
            await service.Add(newItem);

            // Assert
            _repositoryItem.Verify(d => d.Add(It.IsAny<Item>()), Times.Once);
        }

        [Fact]
        public async Task TodoItemService_Update_ShouldSuccessExecute()
        {
            // Arrange
            var service = new TodoItemService(_repositoryItem.Object);
            var newItem = _fixture.Create<Item>();

            // Action
            await service.Update(newItem);

            // Assert
            _repositoryItem.Verify(d => d.Update(It.IsAny<Item>()), Times.Once);
        }

        [Fact]
        public async Task TodoItemService_GetById_ShouldSuccessExecute()
        {
            // Arrange
            var service = new TodoItemService(_repositoryItem.Object);
            var newItem = _fixture.Create<Item>();

            // Action
            await service.GetbyId(Guid.NewGuid());

            // Assert
            _repositoryItem.Verify(d => d.GetbyId(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task TodoItemService_GetAllWithPagination_ShouldSuccessExecute()
        {
            // Arrange
            var listItems = _fixture.CreateMany<Item>(100).ToList();

            _repositoryItem.Setup(r => r.GetAllWithPagination(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new Domain.Dtos.ResponseDto
                {
                    Content = listItems
                });

            var service = new TodoItemService(_repositoryItem.Object);

            // Action
            var result = await service.GetAllWithPagination(10, 10);

            // Assert
            _repositoryItem.Verify(d => d.GetAllWithPagination(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            Assert.True(listItems.Any());
        }
    }
}