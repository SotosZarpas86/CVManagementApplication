using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using CVManagementApplication.API.Controllers;
using CVManagementApplication.Business.Services;
using CVManagementApplication.Core.Domain;
using CVManagementApplication.Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CVManagementApplication.Tests.Controllers
{
    public class CandidateControllerTests
    {
        private readonly Mock<ICandidateService> _candidateServiceMock;

        public CandidateControllerTests()
        {
            _candidateServiceMock = new Mock<ICandidateService>();
        }

        [Fact]
        public async Task Given_IRequestForCandidates_When_GetAll_Then_ReturnsList()
        {
            //Arrrange
            var candidatesList = new List<CandidateModel>
            {
                new CandidateModel
                {
                     Id = 1,
                     FirstName = "John",
                     LastName ="Doe",
                     Email = "test@gmail.com",
                     Mobile = "69823123331",
                     CreationTime = DateTime.Now,
                     DegreeID = 1
                },
                new CandidateModel
                {
                     Id = 2,
                     FirstName = "Jane",
                     LastName ="Doe",
                     Email = "test2@gmail.com",
                     Mobile = "69823123334",
                     CreationTime = DateTime.Now,
                     DegreeID = 2
                },
            };
            _candidateServiceMock.Setup(a => a.GetAll()).ReturnsAsync(candidatesList);
            var candidateController = new CandidateController(_candidateServiceMock.Object);

            //Act
            var response = await candidateController.GetAll();

            //Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<CandidateModel>>>(response);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task Given_IRequestToCreateANewCandidate_When_Create_Then_ReturnsNewCandidate()
        {
            var candidate = new CandidateModel
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "test2@gmail.com",
                Mobile = "69823123334",
                CreationTime = DateTime.Now,
                DegreeID = 2
            };
            var candidateInput = new CandidateCreateModel
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "test2@gmail.com",
                Mobile = "69823123334",
                CreationTime = DateTime.Now,
                DegreeID = 2
            };
            _candidateServiceMock.Setup(a => a.Create(It.IsAny<CandidateCreateModel>())).ReturnsAsync(candidate);
            var candidateController = new CandidateController(_candidateServiceMock.Object);

            //Act
            var response = await candidateController.Create(candidateInput);

            //Assert        
            var actionResult = Assert.IsType<ActionResult<CandidateModel>>(response);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task Given_IRequestToEditAnExistingCandidate_When_Update_Then_ReturnsEditedCandidate()
        {
            var candidate = new CandidateModel
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "test2@gmail.com",
                Mobile = "69823123334",
                CreationTime = DateTime.Now,
                DegreeID = 2
            };
            var candidateInput = new CandidateModel
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "test2@gmail.com",
                Mobile = "69823123334",
                CreationTime = DateTime.Now,
                DegreeID = 2
            };
            _candidateServiceMock.Setup(a => a.Edit(It.IsAny<int>(), It.IsAny<CandidateModel>())).ReturnsAsync(candidate);
            var candidateController = new CandidateController(_candidateServiceMock.Object);

            //Act
            var response = await candidateController.Update(1, candidateInput);
            var result = response.Value;

            //Assert        
            var actionResult = Assert.IsType<ActionResult<CandidateModel>>(response);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task Given_IRequestToDeleteACandidate_When_Delete_Then_ReturnsSuccessfulDeletion()
        {
            _candidateServiceMock.Setup(a => a.Delete(It.IsAny<int>())).ReturnsAsync(true);
            var candidateController = new CandidateController(_candidateServiceMock.Object);

            //Act
            var response = await candidateController.Delete(1);

            //Assert        
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task Given_IRequestToDeleteACandidateThatDoesNotExist_When_Delete_Then_ReturnsFailedDeletion()
        {
            _candidateServiceMock.Setup(a => a.Delete(It.IsAny<int>())).ReturnsAsync(false);
            var candidateController = new CandidateController(_candidateServiceMock.Object);

            //Act
            var response = await candidateController.Delete(1);

            //Assert        
            Assert.IsType<NotFoundObjectResult>(response);
        }
    }
}
