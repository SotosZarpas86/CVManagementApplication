using AutoMapper;
using CVManagementApplication.Business.Mappings;
using CVManagementApplication.Business.Services;
using CVManagementApplication.Core.Domain;
using CVManagementApplication.Core.Entities;
using CVManagementApplication.Core.Interfaces;
using Moq;

namespace CVManagementApplication.Tests.Services
{
    public class CandidateServiceTests
    {
        private readonly Mock<ICandidateRepository> _candidateRepositoryMock;
        private readonly IMapper _mapper;

        public CandidateServiceTests()
        {
            _candidateRepositoryMock = new Mock<ICandidateRepository>();
            _mapper = GetMapper();
        }

        [Fact]
        public async Task Given_IRequestForCandidates_When_GetAll_Then_ReturnsList()
        {
            //Arrrange
            var candidatesList = new List<Candidate>
            {
                new Candidate
                {
                     Id = 1,
                     FirstName = "John",
                     LastName ="Doe",
                     Email = "test@gmail.com",
                     Mobile = "69823123331",
                     CreationTime = DateTime.Now,
                     DegreeID = 1
                },
                new Candidate
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
            _candidateRepositoryMock.Setup(a => a.GetAll()).ReturnsAsync(candidatesList);
            var candidateService = new CandidateService(_candidateRepositoryMock.Object, _mapper);

            //Act
            var result = await candidateService.GetAll();

            //Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result.ElementAtOrDefault(0)?.Id);
            Assert.Equal("John", result.ElementAtOrDefault(0)?.FirstName);
            Assert.Equal("Jane", result.ElementAtOrDefault(1)?.FirstName);
            Assert.Equal("Doe", result.ElementAtOrDefault(1)?.LastName);
            Assert.Equal("test@gmail.com", result.ElementAtOrDefault(0)?.Email);
            Assert.Equal("69823123331", result.ElementAtOrDefault(0)?.Mobile);
            Assert.Equal(2, result.ElementAtOrDefault(1)?.DegreeID);
        }

        [Fact]
        public async Task Given_IRequestToCreateANewCandidate_When_Create_Then_ReturnsNewCandidate()
        {
            var candidateInput = new CandidateCreateModel
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "test2@gmail.com",
                Mobile = "69823123334",
                CreationTime = DateTime.Now,
                DegreeID = 2
            };
            var candidate = new Candidate
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "test2@gmail.com",
                Mobile = "69823123334",
                CreationTime = DateTime.Now,
                DegreeID = 2
            };
            _candidateRepositoryMock.Setup(a => a.Create(It.IsAny<Candidate>())).ReturnsAsync(candidate);
            var candidateService = new CandidateService(_candidateRepositoryMock.Object, _mapper);

            //Act
            var result = await candidateService.Create(candidateInput);

            //Assert        
            Assert.Equal(2, result.Id);
            Assert.Equal("Jane", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("test2@gmail.com", result.Email);
            Assert.Equal("69823123334", result.Mobile);
            Assert.Equal(2, result.DegreeID);
        }

        [Fact]
        public async Task Given_IRequestToEditAnExistingCandidate_When_Edit_Then_ReturnsEditedCandidate()
        {
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
            var candidate = new Candidate
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "test2@gmail.com",
                Mobile = "69823123334",
                CreationTime = DateTime.Now,
                DegreeID = 2
            };
            _candidateRepositoryMock.Setup(a => a.Edit(It.IsAny<Candidate>())).ReturnsAsync(candidate);
            var candidateService = new CandidateService(_candidateRepositoryMock.Object, _mapper);

            //Act
            var result = await candidateService.Edit(1, candidateInput);

            //Assert        
            Assert.Equal(1, result.Id);
            Assert.Equal("Jane", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("test2@gmail.com", result.Email);
            Assert.Equal("69823123334", result.Mobile);
            Assert.Equal(2, result.DegreeID);
        }

        [Fact]
        public async Task Given_IRequestToEditANonExistingCandidate_When_Edit_Then_ActionIsNotSuccessful()
        {
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
            Candidate? candidate = null;
            _candidateRepositoryMock.Setup(a => a.Edit(It.IsAny<Candidate>())).ReturnsAsync(candidate);
            var candidateService = new CandidateService(_candidateRepositoryMock.Object, _mapper);

            //Act
            var result = await candidateService.Edit(1, candidateInput);

            //Assert        
            Assert.Null(result);
        }

        [Fact]
        public async Task Given_IRequestToDeleteACandidate_When_Delete_Then_ReturnsSuccessfulDeletion()
        {           
            _candidateRepositoryMock.Setup(a => a.Delete(It.IsAny<int>())).ReturnsAsync(true);
            var candidateService = new CandidateService(_candidateRepositoryMock.Object, _mapper);

            //Act
            var result = await candidateService.Delete(1);

            //Assert        
            Assert.True(result);
        }

        [Fact]
        public async Task Given_IRequestToDeleteACandidateThatDoesNotExist_When_Delete_Then_ReturnsFailedDeletion()
        {
            _candidateRepositoryMock.Setup(a => a.Delete(It.IsAny<int>())).ReturnsAsync(false);
            var candidateService = new CandidateService(_candidateRepositoryMock.Object, _mapper);

            //Act
            var result = await candidateService.Delete(1);

            //Assert        
            Assert.False(result);
        }

        private static IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CVManagementMapper());
            });

            return mapperConfig.CreateMapper();
        }
    }
}
