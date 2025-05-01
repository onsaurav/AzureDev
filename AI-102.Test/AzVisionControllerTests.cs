using AI_102.Controllers;
using AI_102.Helper;
using AI_102.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace AI_102.Tests.Controllers
{
    public class AzVisionControllerTests
    {
        private readonly Mock<AzVisionServiceHelper> _mockAzVisionServiceHelper;
        private readonly AzVisionController _controller;

        public AzVisionControllerTests()
        {
            _mockAzVisionServiceHelper = new Mock<AzVisionServiceHelper>(null);
            _controller = new AzVisionController(_mockAzVisionServiceHelper.Object);
        }

        [Fact]
        public async Task AnalyzeImage_ReturnsResult_WhenImageUrlIsValid()
        {
            // Arrange
            var imageUrl = "https://example.com/image.jpg";
            var expectedResult = new Result
            {
                IsSuccessful = true,
                Message = "Image analyzed successfully",
                Data = new { Description = "Sample description" }
            };

            _mockAzVisionServiceHelper
                .Setup(helper => helper.AnalyzeImage(imageUrl))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.AnalyzeImage(imageUrl);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccessful);
            Assert.Equal("Image analyzed successfully", result.Message);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task AnalyzeImage_ReturnsErrorResult_WhenImageUrlIsInvalid()
        {
            // Arrange
            var imageUrl = "invalid-url";
            var expectedResult = new Result
            {
                IsSuccessful = false,
                Message = "Invalid image URL",
                Data = null
            };

            _mockAzVisionServiceHelper
                .Setup(helper => helper.AnalyzeImage(imageUrl))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.AnalyzeImage(imageUrl);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.Equal("Invalid image URL", result.Message);
            Assert.Null(result.Data);
        }
    }
}

