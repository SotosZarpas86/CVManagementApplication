using AutoMapper;
using CVManagementApplication.Business.Mappings;
using CVManagementApplication.Business.Services;
using CVManagementApplication.Core.Domain;
using CVManagementApplication.Core.Entities;
using CVManagementApplication.Core.Interfaces;
using Moq;

namespace CVManagementApplication.Tests.Services
{
    public class DegreeServiceTests
    {
        private readonly Mock<IDegreeRepository> _degreeRepositoryMock;
        private readonly IMapper _mapper;

        public DegreeServiceTests()
        {
            _degreeRepositoryMock = new Mock<IDegreeRepository>();
            _mapper = GetMapper();
        }

        [Fact]
        public async Task Given_IRequestForDegrees_When_GetAll_Then_ReturnsList()
        {
            //Arrrange
            var degreesList = new List<Degree>
            {
                new Degree
                {
                     Id = 1,
                     Name = "BSc",
                     CreationTime = DateTime.Now
                },
                new Degree
                {
                     Id = 2,
                     Name = "MSc",
                     CreationTime = DateTime.Now
                },
            };
            _degreeRepositoryMock.Setup(a => a.GetAll()).ReturnsAsync(degreesList);
            var degreeService = new DegreeService(_degreeRepositoryMock.Object, _mapper);

            //Act
            var result = await degreeService.GetAll();

            //Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result.ElementAtOrDefault(0)?.Id);
            Assert.Equal("BSc", result.ElementAtOrDefault(0)?.Name);
            Assert.Equal(2, result.ElementAtOrDefault(1)?.Id);
            Assert.Equal("MSc", result.ElementAtOrDefault(1)?.Name);
        }

        [Fact]
        public async Task Given_IRequestToCreateANewDegree_When_Create_Then_ReturnsNewDegree()
        {
            var degreeInput = new DegreeCreateModel
            {
                Name = "BSc",
                CreationTime = DateTime.Now
            };
            var degree = new Degree
            {
                Id = 1,
                Name = "BSc",
                CreationTime = DateTime.Now
            };
            _degreeRepositoryMock.Setup(a => a.Create(It.IsAny<Degree>())).ReturnsAsync(degree);
            var degreeService = new DegreeService(_degreeRepositoryMock.Object, _mapper);

            //Act
            var result = await degreeService.Create(degreeInput);

            //Assert        
            Assert.Equal(1, result.Id);
            Assert.Equal("BSc", result.Name);
        }

        [Fact]
        public async Task Given_IRequestToEditAnExistingDegree_When_Update_Then_ReturnsEditedDegree()
        {
            var degreeInput = new DegreeModel
            {
                Id = 1,
                Name = "MSc",
                CreationTime = DateTime.Now
            };
            var degree = new Degree
            {
                Id = 1,
                Name = "MSc",
                CreationTime = DateTime.Now,
            };
            _degreeRepositoryMock.Setup(a => a.Update(It.IsAny<Degree>())).ReturnsAsync(degree);
            var degreeService = new DegreeService(_degreeRepositoryMock.Object, _mapper);

            //Act
            var result = await degreeService.Update(1, degreeInput);

            //Assert        
            Assert.Equal(1, result.Id);
            Assert.Equal("MSc", result.Name);
        }

        [Fact]
        public async Task Given_IRequestToEditANonExistingDegree_When_Update_Then_ActionIsNotSuccessful()
        {
            var degreeInput = new DegreeModel
            {
                Id = 1,
                Name = "MSc",
                CreationTime = DateTime.Now
            };
            Degree? degree = null;
            _degreeRepositoryMock.Setup(a => a.Update(It.IsAny<Degree>())).ReturnsAsync(degree);
            var degreeService = new DegreeService(_degreeRepositoryMock.Object, _mapper);

            //Act
            var result = await degreeService.Update(1, degreeInput);

            //Assert        
            Assert.Null(result);
        }

        [Fact]
        public async Task Given_IRequestToDeleteADegree_When_Delete_Then_ReturnsSuccessfulDeletion()
        {
            _degreeRepositoryMock.Setup(a => a.Delete(It.IsAny<int>())).ReturnsAsync(true);
            var degreeService = new DegreeService(_degreeRepositoryMock.Object, _mapper);

            //Act
            var result = await degreeService.Delete(1);

            //Assert        
            Assert.True(result);
        }

        [Fact]
        public async Task Given_IRequestToDeleteADegreeThatDoesNotExist_When_Delete_Then_ReturnsFailedDeletion()
        {
            _degreeRepositoryMock.Setup(a => a.Delete(It.IsAny<int>())).ReturnsAsync(false);
            var degreeService = new DegreeService(_degreeRepositoryMock.Object, _mapper);

            //Act
            var result = await degreeService.Delete(1);

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
