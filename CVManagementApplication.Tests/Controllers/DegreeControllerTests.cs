using CVManagementApplication.API.Controllers;
using CVManagementApplication.Core.Domain;
using CVManagementApplication.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CVManagementApplication.Tests.Controllers
{
    public class DegreeControllerTests
    {
        private readonly Mock<IDegreeService> _degreeServiceMock;

        public DegreeControllerTests()
        {
            _degreeServiceMock = new Mock<IDegreeService>();
        }

        [Fact]
        public async Task Given_IRequestForDegrees_When_GetAll_Then_ReturnsList()
        {
            //Arrrange
            var degreesList = new List<DegreeModel>
            {
                new DegreeModel
                {
                     Id = 1,
                     Name = "Phd",
                     CreationTime = DateTime.Now
                },
                new DegreeModel
                {
                     Id = 2,
                     Name = "BSc",
                     CreationTime = DateTime.Now,
                },
            };
            _degreeServiceMock.Setup(a => a.GetAll()).ReturnsAsync(degreesList);
            var DegreeController = new DegreeController(_degreeServiceMock.Object);

            //Act
            var response = await DegreeController.GetAll();

            //Assert
            var actionResult = Assert.IsType<ActionResult<IList<DegreeModel>>>(response);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task Given_IRequestToCreateANewCandidate_When_Create_Then_ReturnsNewCandidate()
        {
            var Degree = new DegreeModel
            {
                Id = 1,
                Name = "Phd",
                CreationTime = DateTime.Now
            };
            var candidateInput = new DegreeCreateModel
            {
                Name = "Phd",
                CreationTime = DateTime.Now
            };
            _degreeServiceMock.Setup(a => a.Create(It.IsAny<DegreeCreateModel>())).ReturnsAsync(Degree);
            var DegreeController = new DegreeController(_degreeServiceMock.Object);

            //Act
            var response = await DegreeController.Create(candidateInput);

            //Assert        
            var actionResult = Assert.IsType<ActionResult<DegreeModel>>(response);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task Given_IRequestToEditAnExistingCandidate_When_Update_Then_ReturnsEditedCandidate()
        {
            var Degree = new DegreeModel
            {
                Id = 1,
                Name = "Phd",
                CreationTime = DateTime.Now
            };
            var candidateInput = new DegreeModel
            {
                Id = 1,
                Name = "Phd",
                CreationTime = DateTime.Now
            };
            _degreeServiceMock.Setup(a => a.Update(It.IsAny<int>(), It.IsAny<DegreeModel>())).ReturnsAsync(Degree);
            var DegreeController = new DegreeController(_degreeServiceMock.Object);

            //Act
            var response = await DegreeController.Update(1, candidateInput);
            var result = response.Value;

            //Assert        
            var actionResult = Assert.IsType<ActionResult<DegreeModel>>(response);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task Given_IRequestToDeleteACandidate_When_Delete_Then_ReturnsSuccessfulDeletion()
        {
            _degreeServiceMock.Setup(a => a.Delete(It.IsAny<int>())).ReturnsAsync(true);
            var DegreeController = new DegreeController(_degreeServiceMock.Object);

            //Act
            var response = await DegreeController.Delete(1);

            //Assert        
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task Given_IRequestToDeleteACandidateThatDoesNotExist_When_Delete_Then_ReturnsFailedDeletion()
        {
            _degreeServiceMock.Setup(a => a.Delete(It.IsAny<int>())).ReturnsAsync(false);
            var DegreeController = new DegreeController(_degreeServiceMock.Object);

            //Act
            var response = await DegreeController.Delete(1);

            //Assert        
            Assert.IsType<NotFoundObjectResult>(response);
        }
    }
}
