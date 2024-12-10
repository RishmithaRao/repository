using FunctionApp3;
using FunctionApp3.Helpers;
using FunctionApp3.Models;
using FunctionApp3.Repository.Interface;
using Moq;
using Xunit;

namespace FunctionAppTest
{
    public class DeleteEmployeeTests
    {
        [Fact]
        public async Task DeleteEmployee_ShouldReturnTrue_WhenDeleteSucceeds()
        {
            // Arrange
            var mockRepository = new Mock<IEmployeeRepository<Employee>>();
            var id = "123"; // Test employee ID
            mockRepository
                .Setup(r => r.DeleteItemAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask); // Simulate successful deletion

            // Inject the mock repository into the function
            var function = new Function1(mockRepository.Object); // Assuming your function class is named `FunctionClass`

            // Act
            var result = await function.DeleteEmployee(null, null, id);

            // Assert
            Assert.True(result); // Assert that the result is true (deletion succeeded)
            mockRepository.Verify(r => r.DeleteItemAsync(id, Constants.TblEmployee, id), Times.Once); // Verify that DeleteItemAsync was called once
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnFalse_WhenDeleteFails()
        {
            // Arrange
            var mockRepository = new Mock<IEmployeeRepository<Employee>>();
            var id = "123"; // Test employee ID
            mockRepository
                .Setup(r => r.DeleteItemAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Delete failed")); // Simulate failure (exception thrown)

            // Inject the mock repository into the function
            var function = new Function1(mockRepository.Object); // Assuming your function class is named `FunctionClass`

            // Act
            var result = await function.DeleteEmployee(null, null, id);

            // Assert
            Xunit.Assert.False(result); // Assert that the result is false (deletion failed)
            mockRepository.Verify(r => r.DeleteItemAsync(id, Constants.TblEmployee, id), Times.Once); // Verify that DeleteItemAsync was called once
        }
    }

}